using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using Atomus.Diagnostics;
using Atomus.Control.Login.Controllers;
using Atomus.Control.Login.Models;
using Atomus.Security;
using DevExpress.XtraEditors;
using System.Web;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Atomus.Control.Login
{
    public partial class DevExpressLogin : XtraUserControl, IAction
    {
        private AtomusControlEventHandler beforeActionEventHandler;
        private AtomusControlEventHandler afterActionEventHandler;

        private Point point;

        private bool isPadeIn;
        private Timer timerFade;

        #region Init
        public DevExpressLogin()
        {
            InitializeComponent();

            this.timerFade = new Timer
            {
                Interval = 10
            };
            this.timerFade.Tick += this.TimerFade_Tick;

            this.point = new Point(0, 0);


            if (this.GetAttribute("EnableJoin") != "Y")
                this.Bnt_Join.Visible = false;
        }
        #endregion

        #region Dictionary
        #endregion

        #region Spread
        #endregion

        #region IO
        object IAction.ControlAction(ICore sender, AtomusControlArgs e)
        {
            try
            {
                this.beforeActionEventHandler?.Invoke(this, e);

                switch (e.Action)
                {
                    default:
                        throw new AtomusException("'{0}'은 처리할 수 없는 Action 입니다.".Translate(e.Action));
                }
            }
            finally
            {
                this.afterActionEventHandler?.Invoke(this, e);
            }
        }

        private bool Login(string EMAIL, string ACCESS_NUMBER, string AUTO_LOGIN_ACCESS_NUMBER, bool AUTO_LOGIN)
        {
            Service.IResponse result;
            ISecureHashAlgorithm secureHashAlgorithm;
            string LoginType;
            string LDAPUrl;
            string Domain;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                secureHashAlgorithm = ((ISecureHashAlgorithm)this.CreateInstance("SecureHashAlgorithm"));

                if (Config.Client.GetAttribute("Sessionkey") != null)
                {
                    AUTO_LOGIN = true;
                    AUTO_LOGIN_ACCESS_NUMBER = secureHashAlgorithm.ComputeHashToBase64String(this.GetAttribute("Guid"));
                }
                else
                {
                    LoginType = this.GetAttribute("LoginType");
                    LDAPUrl = this.GetAttribute("LDAPUrl");
                    Domain = this.GetAttribute("Domain");

                    if (!LoginType.IsNullOrWhiteSpace() && LoginType == "LDAP" && !LDAPUrl.IsNullOrWhiteSpace())
                    {
                        try
                        {
                            this.LDAPQuery(LDAPUrl, Domain, EMAIL, ACCESS_NUMBER);

                            AUTO_LOGIN = true;
                            AUTO_LOGIN_ACCESS_NUMBER = secureHashAlgorithm.ComputeHashToBase64String(this.GetAttribute("GuidLDAP"));
                        }
                        catch (Exception ex)
                        {
                            this.MessageBoxShow(this, ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }

                //자동 로그인 이고 저장된 패스워드가 없으면 현재 입력한 패스워드를 암호화
                //저장된 패스워드가 있으면 패스워드는 암호화되어 있기 때문에 그냥 패스
                if (AUTO_LOGIN && (AUTO_LOGIN_ACCESS_NUMBER == null || AUTO_LOGIN_ACCESS_NUMBER == ""))
                    AUTO_LOGIN_ACCESS_NUMBER = secureHashAlgorithm.ComputeHashToBase64String(ACCESS_NUMBER);

                result = this.Search(new DevExpressLoginSearchModel()
                {
                    EMAIL = EMAIL,
                    ACCESS_NUMBER = (AUTO_LOGIN) ? AUTO_LOGIN_ACCESS_NUMBER : secureHashAlgorithm.ComputeHashToBase64String(ACCESS_NUMBER)//자동 로그인이 아니면 패스워드 암호화해서 전송
                });

                if (result.Status == Service.Status.OK)
                {
                    if (result.DataSet != null && result.DataSet.Tables.Count >= 1)
                        foreach (DataTable _DataTable in result.DataSet.Tables)
                            for (int i = 1; i < _DataTable.Columns.Count; i++)
                                foreach (DataRow _DataRow in _DataTable.Rows)
                                    Config.Client.SetAttribute(string.Format("{0}.{1}", _DataRow[0].ToString(), _DataTable.Columns[i].ColumnName), _DataRow[i]);

                    this.timerFade.Stop();
                    this.FindForm().Opacity = 1.0F;

                    if (AUTO_LOGIN)
                    {
                        Properties.Settings.Default.Password = AUTO_LOGIN_ACCESS_NUMBER;
                        Properties.Settings.Default.Save();
                    }

                    return true;
                }
                else
                {
                    this.MessageBoxShow(this, result.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return false;
        }

        private async void GetBackgroundImage()
        {
            try
            {
                this.BackgroundImage = await this.GetAttributeWebImage("BackgroundImage");

                if (this.BackgroundImage != null)
                {
                    this.afterActionEventHandler?.Invoke(this, "Form.Size", this.BackgroundImage.Size);
                }
            }
            catch (Exception exception)
            {
                DiagnosticsTool.MyTrace(exception);
            }
        }


        private void LDAPQuery(string LDAPUrl, string domain, string userID, string password)
        {
            System.Net.Mail.MailAddress mailAddres;
            System.DirectoryServices.DirectoryEntry directoryEntry;
            System.DirectoryServices.DirectorySearcher directorySearcher;
            System.DirectoryServices.SearchResult searchResult;

            if (this.IsMailAddress(userID, out mailAddres) && mailAddres != null)
            {
                userID = mailAddres.User;
                domain = mailAddres.Host;
            }

            if (userID.Contains("\\"))
            {
                domain = userID.Substring(0, userID.IndexOf('\\'));
                userID = userID.Substring(userID.IndexOf('\\') + 1);
            }

            directoryEntry = new System.DirectoryServices.DirectoryEntry(LDAPUrl, string.Format("{0}\\{1}", domain, userID), password);
            directorySearcher = new System.DirectoryServices.DirectorySearcher(directoryEntry);
            directorySearcher.ClientTimeout = new TimeSpan(3000);

            directorySearcher.Filter = string.Format("(SAMAccountName={0})", userID);
            searchResult = directorySearcher.FindOne();

            if (searchResult == null)
                throw new Exception("Not found Valid User");
            else
                Config.Client.SetAttribute("System.DirectoryServices.SearchResult", searchResult);
        }
        #endregion

        #region Event
        event AtomusControlEventHandler IAction.BeforeActionEventHandler
        {
            add
            {
                this.beforeActionEventHandler += value;
            }
            remove
            {
                this.beforeActionEventHandler -= value;
            }
        }
        event AtomusControlEventHandler IAction.AfterActionEventHandler
        {
            add
            {
                this.afterActionEventHandler += value;
            }
            remove
            {
                this.afterActionEventHandler -= value;
            }
        }

        private void DefaultLogin_Load(object sender, EventArgs e)
        {
            string SSOUser;
            string TimeKey;

            try
            {
                this.FindForm().Opacity = 0;

                this.GetBackgroundImage();

                this.SetLoginControlLocation();

                this.SetLoginControlColor();

                this.SetLanguageList();

                if (!Properties.Settings.Default.Language.Equals(""))
                    this.Language.Text = Properties.Settings.Default.Language;

                if (this.Language.Text.Length > 0)
                {
                    this.EMAIL.Focus();
                }

                this.EMAIL.Text = Properties.Settings.Default.Email;
                this.IS_EMAIL_SAVE.Checked = Properties.Settings.Default.IsEmilSave;

                if (this.EMAIL.Text.Length > 0)
                {
                    this.ACCESS_NUMBER.Focus();
                }

                this.IS_AUTO_LOGIN.Checked = Properties.Settings.Default.AutoLogin;

                if (this.IS_AUTO_LOGIN.Checked)
                {
                    this.ACCESS_NUMBER.Text = Properties.Settings.Default.Password;
                    this.ACCESS_NUMBER.Tag = Properties.Settings.Default.Password;
                }

                SSOUser = Config.Client.GetAttribute("UriParameter.SSOUser")?.ToString();
                TimeKey = Config.Client.GetAttribute("UriParameter.TimeKey")?.ToString();

                if (SSOUser != null && SSOUser.Length > 0 && TimeKey != null && TimeKey.Length > 0)
                    try
                    {
                        DiagnosticsTool.MyTrace(new Exception(SSOUser));
                        Config.Client.SetAttribute("Sessionkey", Decrypt(HttpUtility.UrlDecode(SSOUser), TimeKey));
                    }
                    catch (Exception ex)
                    {
                        DiagnosticsTool.MyTrace(ex);
                    }

                if (Config.Client.GetAttribute("Sessionkey") != null)
                    this.Bnt_Login_Click(this.Bnt_Login, null);

                this.FadeInStrart();


                //this.SetAttribute("DecryptKey", "kolon@12!{0}");
                //this.SetAttribute("DecryptSalt", "SXZhbiBNZWR2ZWRldg==");

                //string tmp;
                //tmp = Encrypt("Test", "Test1");
                //tmp = HttpUtility.UrlEncode(tmp);
                //tmp = HttpUtility.UrlDecode("6AmDNFFn%2boee5sjuS%2bWAoiybvPdQc137atyrfUawIeYJ8eeD%2bOmK2mlbySiHxjKO");
                //tmp = Decrypt(tmp, "201910070418");
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private async void UrlPopup(string userID, bool isEmail)
        {
            string popupUrl;
            string popupPeriod;
            string[] tmps;
            DateTime popupPeriodStart;
            DateTime popupPeriodEnd;

            try
            {
                popupUrl = this.GetAttribute("PopupUrl");
                popupPeriod = this.GetAttribute("PopupPeriod");

                if (!popupUrl.IsNullOrEmpty() && !popupPeriod.IsNullOrEmpty() && popupPeriod.Contains("~"))
                {
                    if (isEmail)
                        popupUrl = popupUrl.Replace("gubun=i", "gubun=s");

                    tmps = popupPeriod.Split('~');

                    if (DateTime.TryParse(tmps[0], out popupPeriodStart) && DateTime.TryParse(tmps[1], out popupPeriodEnd))
                        if (popupPeriodStart <= DateTime.Now && popupPeriodEnd >= DateTime.Now)
                        {
                            if (!await this.IsUrlPopup(string.Format(popupUrl, userID)))
                                Process.Start(string.Format(popupUrl, userID));
                        }
                }
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }
        }


        private async Task<bool> IsUrlPopup(string popupUrl)
        {
            string tmp1;

            try
            { 
                tmp1 = System.Text.Encoding.UTF8.GetString(await this.GetAttributeWebBytes(new Uri(popupUrl.Replace("popup.asp", "check.asp"))));

                if (tmp1.Contains("form"))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
                return false;
            }
        }


        private void Bnt_Join_Click(object sender, EventArgs e)
        {
            this.afterActionEventHandler?.Invoke(this, "Login.JoinNew");
        }

        IErrorAlert errorAlertLogin_Click;
        private void Bnt_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (Config.Client.GetAttribute("Sessionkey") != null)
                    this.EMAIL.Text = Config.Client.GetAttribute("Sessionkey").ToString();

                this.errorAlertLogin_Click = this.errorAlertLogin_Click ?? this.ErrorAlert(false);
                this.errorAlertLogin_Click.Clear();

                if (Config.Client.GetAttribute("Sessionkey") == null)
                    this.errorAlertLogin_Click.TextLengthGreaterThan(0, this.Language, this.EMAIL, this.ACCESS_NUMBER);
                else
                    this.errorAlertLogin_Click.TextLengthGreaterThan(0, this.Language, this.EMAIL);

                if (!this.errorAlertLogin_Click.Result)
                    return;

                this.Language.Enabled = false;
                this.EMAIL.Enabled = false;
                this.ACCESS_NUMBER.Enabled = false;

                this.Bnt_Join.Enabled = false;
                this.Bnt_Login.Enabled = false;
                this.Bnt_Exit.Enabled = false;

                this.beforeActionEventHandler?.Invoke(this, "Login.Start");

                if (this.Login(this.EMAIL.Text, this.ACCESS_NUMBER.Text, (string)this.ACCESS_NUMBER.Tag, this.IS_AUTO_LOGIN.Checked))
                {
                    //this.EMAIL.TextChanged -= this.EMAIL_TextChanged;
                    this.afterActionEventHandler?.Invoke(this, "Login.Ok");
                    //this.Language_SelectedIndexChanged(this.Language, null);

                    if (!this.Translator().SourceCultureName.Equals(this.Translator().TargetCultureName))
                    {
                        this.Translator().Restoration(this.Controls);
                        this.Translator().Translate(this.Controls);
                    }

                    if ((this.EMAIL.Text.Contains("@") && this.EMAIL.Text.Contains(".")) || Config.Client.GetAttribute("Sessionkey") != null)
                        this.UrlPopup(this.EMAIL.Text, true);
                    else
                        this.UrlPopup(this.EMAIL.Text, false);
                }
                else
                    this.afterActionEventHandler?.Invoke(this, "Login.Fail");
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
            finally
            {
                this.Bnt_Exit.Enabled = true;
                this.Bnt_Login.Enabled = true;
                this.Bnt_Join.Enabled = true;

                this.ACCESS_NUMBER.Enabled = true;
                this.EMAIL.Enabled = true;
                this.Language.Enabled = true;
            }
        }

        private void Bnt_Exit_Click(object sender, EventArgs e)
        {
            this.afterActionEventHandler?.Invoke(this, "Login.Cancel");
        }

        private void DefaultLogin_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Control userControl;

            try
            {
                userControl = (System.Windows.Forms.Control)sender;

                if (e.Button != MouseButtons.Left)
                    return;

                userControl.FindForm().Left = userControl.FindForm().Left + (e.X - this.point.X);
                userControl.FindForm().Top = userControl.FindForm().Top + (e.Y - this.point.Y);
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }
        private void DefaultLogin_MouseDown(object sender, MouseEventArgs e)
        {
            this.point.X = e.X;
            this.point.Y = e.Y;
        }
        /// <summary>
        /// DropDownClosed 할때 마우스 위치를 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Language_DropDownClosed(object sender, EventArgs e)
        {
            this.point = this.PointToClient(new Point(MousePosition.X, MousePosition.Y));
        }

        private void TimerFade_Tick(object sender, EventArgs e)
        {
            Timer timer;

            timer = (Timer)sender;
            try
            {
                if (this.isPadeIn)
                {
                    if (this.ParentForm.Opacity >= 1.0F)
                    {
                        this.timerFade.Enabled = false;
                        this.IS_AUTO_LOGIN.Enabled = true;

                        if (Config.Client.GetAttribute("Sessionkey") == null)
                            if (this.IS_AUTO_LOGIN.Checked && this.MessageBoxShow(this, "자동로그인 하시겠습니까?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            this.Bnt_Login_Click(this.Bnt_Login, null);
                    }
                    else
                    {
                        this.IS_AUTO_LOGIN.Enabled = false;
                        this.FindForm().Opacity += 0.01F;
                    }
                }
                else
                {
                    if (this.ParentForm.Opacity <= 0.0F)
                    {
                        this.timerFade.Enabled = false;
                        this.FindForm().Close();
                    }
                    else
                        this.FindForm().Opacity -= 0.01F;
                }
            }
            catch (Exception exception)
            {
                timer?.Stop();

                DiagnosticsTool.MyTrace(exception);

                try
                {
                    this.FindForm().Opacity = 1.0F;
                }
                catch (Exception ex)
                {
                    DiagnosticsTool.MyTrace(ex);

                }
            }
        }

        private void Language_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.Control control;

            try
            {
                control = (System.Windows.Forms.Control)sender;

                if (e.KeyChar == Convert.ToChar(Keys.Enter) && control.Text.Length > 0)
                {
                    this.EMAIL.Focus();
                    this.Language_SelectedIndexChanged(sender, null);
                }
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void EMAIL_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.Control control;

            try
            {
                control = (System.Windows.Forms.Control)sender;

                if (e.KeyChar == Convert.ToChar(Keys.Enter) && control.Text.Length > 0)
                    this.ACCESS_NUMBER.Focus();
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void ACCESS_NUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.Control control;

            try
            {
                control = (System.Windows.Forms.Control)sender;

                if (e.KeyChar == Convert.ToChar(Keys.Enter) && control.Text.Length > 0)
                    this.Bnt_Login_Click(this.Bnt_Login, null);
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private bool Language_SelectedIndexChangedIn = false;
        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit comboBox;
            string tmp;

            try
            {
                if (this.Language_SelectedIndexChangedIn)
                    return;

                this.Language_SelectedIndexChangedIn = true;

                comboBox = (ComboBoxEdit)sender;

                tmp = comboBox.Text;
                this.Translator().TargetCultureName = tmp;

                //if (!this.Translator().SourceCultureName.Equals(this.Translator().TargetCultureName))
                //{
                //    this.Translator().Restoration(this.Controls);
                //    this.Translator().Translate(this.Controls);
                //}
                this.Translator().Restoration(this.Controls);
                this.Translator().Translate(this.Controls);

                comboBox.Text = tmp;

                if (!Properties.Settings.Default.Language.Equals(comboBox.Text))
                {
                    Properties.Settings.Default.Language = comboBox.Text;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
            finally
            {
                this.Language_SelectedIndexChangedIn = false;
            }
        }

        private void IS_EMAIL_SAVE_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit checkBox;

            try
            {
                checkBox = (CheckEdit)sender;

                if (checkBox.Checked)
                    Properties.Settings.Default.Email = this.EMAIL.Text;
                else
                {
                    Properties.Settings.Default.Email = "";
                    this.IS_AUTO_LOGIN.Checked = false;
                }

                Properties.Settings.Default.IsEmilSave = checkBox.Checked;
                Properties.Settings.Default.Save();
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }
        private void IS_AUTO_LOGIN_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit checkBox;

            try
            {
                checkBox = (CheckEdit)sender;

                if (checkBox.Checked)
                {
                    this.IS_EMAIL_SAVE.Checked = true;
                    this.IS_EMAIL_SAVE.Enabled = false;
                }
                else
                {
                    this.IS_EMAIL_SAVE.Enabled = true;
                    this.ACCESS_NUMBER.Text = "";
                    this.ACCESS_NUMBER.Tag = "";

                    Properties.Settings.Default.Password = "";
                }

                Properties.Settings.Default.AutoLogin = checkBox.Checked;
                Properties.Settings.Default.Save();
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void EMAIL_TextChanged(object sender, EventArgs e)
        {
            TextEdit textBox;

            try
            {
                textBox = (TextEdit)sender;

                if (this.IS_EMAIL_SAVE.Checked)
                    Properties.Settings.Default.Email = textBox.Text;
                else
                    Properties.Settings.Default.Email = "";

                Properties.Settings.Default.Save();
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void ACCESS_NUMBER_TextChanged(object sender, EventArgs e)
        {
            TextEdit textBox;

            try
            {
                textBox = (TextEdit)sender;

                if (textBox.Tag != null)
                    textBox.Tag = null;
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }
        #endregion

        #region ETC
        private void SetLoginControlLocation()
        {
            Point point;

            try
            {
                point = this.GetAttributePoint("LoginControlLocation");

                this.Label3.Location = new Point(this.Label3.Location.X + point.X, this.Label3.Location.Y + point.Y);
                this.Language.Location = new Point(this.Language.Location.X + point.X, this.Language.Location.Y + point.Y);
                this.Label1.Location = new Point(this.Label1.Location.X + point.X, this.Label1.Location.Y + point.Y);
                this.EMAIL.Location = new Point(this.EMAIL.Location.X + point.X, this.EMAIL.Location.Y + point.Y);
                this.IS_EMAIL_SAVE.Location = new Point(this.IS_EMAIL_SAVE.Location.X + point.X, this.IS_EMAIL_SAVE.Location.Y + point.Y);
                this.Label2.Location = new Point(this.Label2.Location.X + point.X, this.Label2.Location.Y + point.Y);
                this.ACCESS_NUMBER.Location = new Point(this.ACCESS_NUMBER.Location.X + point.X, this.ACCESS_NUMBER.Location.Y + point.Y);
                this.IS_AUTO_LOGIN.Location = new Point(this.IS_AUTO_LOGIN.Location.X + point.X, this.IS_AUTO_LOGIN.Location.Y + point.Y);
                this.Bnt_Join.Location = new Point(this.Bnt_Join.Location.X + point.X, this.Bnt_Join.Location.Y + point.Y);
                this.Bnt_Login.Location = new Point(this.Bnt_Login.Location.X + point.X, this.Bnt_Login.Location.Y + point.Y);
                this.Bnt_Exit.Location = new Point(this.Bnt_Exit.Location.X + point.X, this.Bnt_Exit.Location.Y + point.Y);
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void SetLoginControlColor()
        {
            Color backColor;
            Color foreColor;

            try
            {
                backColor = this.GetAttributeColor("LoginControlBackColor");
                foreColor = this.GetAttributeColor("LoginControlForeColor");

                this.Label3.ForeColor = foreColor;
                this.Language.ForeColor = foreColor;
                this.Language.BackColor = backColor;

                this.Label1.ForeColor = foreColor;
                this.EMAIL.ForeColor = foreColor;
                this.EMAIL.BackColor = backColor;
                this.IS_EMAIL_SAVE.ForeColor = foreColor;

                this.Label2.ForeColor = foreColor;
                this.ACCESS_NUMBER.ForeColor = foreColor;
                this.ACCESS_NUMBER.BackColor = backColor;
                this.IS_AUTO_LOGIN.ForeColor = foreColor;

                this.Bnt_Join.ForeColor = foreColor;
                this.Bnt_Login.ForeColor = foreColor;
                this.Bnt_Exit.ForeColor = foreColor;
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }

        private void SetLanguageList()
        {
            string[] tmps;

            try
            {
                var cultureNames = from Cultures in CultureInfo.GetCultures(CultureTypes.AllCultures)
                                   where Cultures.Name.Contains("-")
                                   orderby Cultures.Name
                                   select Cultures.Name;

                if (this.GetAttribute("LanguageList") != null && this.GetAttribute("LanguageList") != "")
                {
                    tmps = this.GetAttribute("LanguageList").Split(',');

                    foreach (string name in tmps)
                        this.Language.Properties.Items.Add(name);
                }
                else
                    foreach (string name in cultureNames)
                        this.Language.Properties.Items.Add(name);

                //if (Properties.Settings.Default.Language.Equals(""))
                //    this.Language.Text = CultureInfo.CurrentCulture.Name;
            }
            catch (Exception exception)
            {
                this.MessageBoxShow(this, exception);
            }
        }
        
        private void FadeInStrart()
        {
            this.isPadeIn = true;
            this.timerFade.Enabled = true;
        }

        //private void FadeOutStrart()
        //{
        //    this._IsPadeIn = true;
        //    this._TimerFade.Enabled = true;
        //}

        private bool IsMailAddress(string mailAddressString, out System.Net.Mail.MailAddress mailAddress)
        {
            try
            {
                mailAddress = new System.Net.Mail.MailAddress(mailAddressString);
                return true;
            }
            catch
            {
                mailAddress = null;
                return false;
            }
        }

        private string Decrypt(string cipher, string type)
        {
            string EncryptionKey;
            byte[] cipherBytes;

            //DiagnosticsTool.MyTrace(new Exception(string.Format("cipher : {0}", cipher)));
            //DiagnosticsTool.MyTrace(new Exception(string.Format("type : {0}", type)));
            //DiagnosticsTool.MyTrace(new Exception(string.Format("DecryptKey : {0}", this.GetAttribute("DecryptKey"))));

            EncryptionKey = string.Format(this.GetAttribute("DecryptKey"), type);

            cipherBytes = Convert.FromBase64String(cipher);

            using (System.Security.Cryptography.Rijndael encryptor = System.Security.Cryptography.Rijndael.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, Convert.FromBase64String(this.GetAttribute("DecryptSalt")));

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    cipher = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipher;
        }
        #endregion
    }
}