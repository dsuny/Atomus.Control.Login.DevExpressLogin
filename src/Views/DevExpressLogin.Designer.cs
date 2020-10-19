namespace Atomus.Control.Login
{
    partial class DevExpressLogin
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label3 = new DevExpress.XtraEditors.LabelControl();
            this.Label1 = new DevExpress.XtraEditors.LabelControl();
            this.Label2 = new DevExpress.XtraEditors.LabelControl();
            this.Bnt_Join = new DevExpress.XtraEditors.SimpleButton();
            this.Bnt_Login = new DevExpress.XtraEditors.SimpleButton();
            this.Bnt_Exit = new DevExpress.XtraEditors.SimpleButton();
            this.IS_EMAIL_SAVE = new DevExpress.XtraEditors.CheckEdit();
            this.IS_AUTO_LOGIN = new DevExpress.XtraEditors.CheckEdit();
            this.Language = new DevExpress.XtraEditors.ComboBoxEdit();
            this.EMAIL = new DevExpress.XtraEditors.TextEdit();
            this.ACCESS_NUMBER = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.IS_EMAIL_SAVE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS_AUTO_LOGIN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Language.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMAIL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ACCESS_NUMBER.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Appearance.ForeColor = System.Drawing.Color.White;
            this.Label3.Appearance.Options.UseBackColor = true;
            this.Label3.Appearance.Options.UseForeColor = true;
            this.Label3.Appearance.Options.UseTextOptions = true;
            this.Label3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Label3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Label3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Label3.Location = new System.Drawing.Point(0, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(94, 25);
            this.Label3.TabIndex = 11;
            this.Label3.Text = "언어";
            // 
            // Label1
            // 
            this.Label1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Appearance.ForeColor = System.Drawing.Color.White;
            this.Label1.Appearance.Options.UseBackColor = true;
            this.Label1.Appearance.Options.UseForeColor = true;
            this.Label1.Appearance.Options.UseTextOptions = true;
            this.Label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Label1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Label1.Location = new System.Drawing.Point(0, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 25);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "사용자";
            // 
            // Label2
            // 
            this.Label2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Appearance.ForeColor = System.Drawing.Color.White;
            this.Label2.Appearance.Options.UseBackColor = true;
            this.Label2.Appearance.Options.UseForeColor = true;
            this.Label2.Appearance.Options.UseTextOptions = true;
            this.Label2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Label2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Label2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Label2.Location = new System.Drawing.Point(0, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(94, 30);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "비밀번호";
            // 
            // Bnt_Join
            // 
            this.Bnt_Join.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Bnt_Join.Appearance.ForeColor = System.Drawing.Color.White;
            this.Bnt_Join.Appearance.Options.UseBackColor = true;
            this.Bnt_Join.Appearance.Options.UseForeColor = true;
            this.Bnt_Join.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.Bnt_Join.Location = new System.Drawing.Point(35, 78);
            this.Bnt_Join.Name = "Bnt_Join";
            this.Bnt_Join.Size = new System.Drawing.Size(131, 25);
            this.Bnt_Join.TabIndex = 14;
            this.Bnt_Join.Text = "가입/비밀번호변경";
            this.Bnt_Join.Click += new System.EventHandler(this.Bnt_Join_Click);
            // 
            // Bnt_Login
            // 
            this.Bnt_Login.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Bnt_Login.Appearance.ForeColor = System.Drawing.Color.White;
            this.Bnt_Login.Appearance.Options.UseBackColor = true;
            this.Bnt_Login.Appearance.Options.UseForeColor = true;
            this.Bnt_Login.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.Bnt_Login.Location = new System.Drawing.Point(175, 78);
            this.Bnt_Login.Name = "Bnt_Login";
            this.Bnt_Login.Size = new System.Drawing.Size(73, 25);
            this.Bnt_Login.TabIndex = 15;
            this.Bnt_Login.Text = "접속";
            this.Bnt_Login.Click += new System.EventHandler(this.Bnt_Login_Click);
            // 
            // Bnt_Exit
            // 
            this.Bnt_Exit.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Bnt_Exit.Appearance.ForeColor = System.Drawing.Color.White;
            this.Bnt_Exit.Appearance.Options.UseBackColor = true;
            this.Bnt_Exit.Appearance.Options.UseForeColor = true;
            this.Bnt_Exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.Bnt_Exit.Location = new System.Drawing.Point(256, 78);
            this.Bnt_Exit.Name = "Bnt_Exit";
            this.Bnt_Exit.Size = new System.Drawing.Size(73, 25);
            this.Bnt_Exit.TabIndex = 16;
            this.Bnt_Exit.Text = "종료";
            this.Bnt_Exit.Click += new System.EventHandler(this.Bnt_Exit_Click);
            // 
            // IS_EMAIL_SAVE
            // 
            this.IS_EMAIL_SAVE.Location = new System.Drawing.Point(253, 29);
            this.IS_EMAIL_SAVE.Margin = new System.Windows.Forms.Padding(2);
            this.IS_EMAIL_SAVE.Name = "IS_EMAIL_SAVE";
            this.IS_EMAIL_SAVE.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.IS_EMAIL_SAVE.Properties.Appearance.Options.UseForeColor = true;
            this.IS_EMAIL_SAVE.Properties.Caption = "저장";
            this.IS_EMAIL_SAVE.Size = new System.Drawing.Size(52, 19);
            this.IS_EMAIL_SAVE.TabIndex = 17;
            this.IS_EMAIL_SAVE.CheckedChanged += new System.EventHandler(this.IS_EMAIL_SAVE_CheckedChanged);
            // 
            // IS_AUTO_LOGIN
            // 
            this.IS_AUTO_LOGIN.Location = new System.Drawing.Point(252, 54);
            this.IS_AUTO_LOGIN.Margin = new System.Windows.Forms.Padding(2);
            this.IS_AUTO_LOGIN.Name = "IS_AUTO_LOGIN";
            this.IS_AUTO_LOGIN.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.IS_AUTO_LOGIN.Properties.Appearance.Options.UseForeColor = true;
            this.IS_AUTO_LOGIN.Properties.Caption = "자동로그인";
            this.IS_AUTO_LOGIN.Size = new System.Drawing.Size(96, 19);
            this.IS_AUTO_LOGIN.TabIndex = 18;
            this.IS_AUTO_LOGIN.CheckedChanged += new System.EventHandler(this.IS_AUTO_LOGIN_CheckedChanged);
            // 
            // Language
            // 
            this.Language.Location = new System.Drawing.Point(100, 3);
            this.Language.Name = "Language";
            this.Language.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.Language.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Language.Properties.Appearance.Options.UseBackColor = true;
            this.Language.Properties.Appearance.Options.UseForeColor = true;
            this.Language.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Language.Properties.EditValueChangedDelay = 100;
            this.Language.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Language.Size = new System.Drawing.Size(96, 20);
            this.Language.TabIndex = 20;
            this.Language.SelectedIndexChanged += new System.EventHandler(this.Language_SelectedIndexChanged);
            this.Language.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Language_KeyPress);
            // 
            // EMAIL
            // 
            this.EMAIL.Location = new System.Drawing.Point(100, 28);
            this.EMAIL.Name = "EMAIL";
            this.EMAIL.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.EMAIL.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.EMAIL.Properties.Appearance.Options.UseBackColor = true;
            this.EMAIL.Properties.Appearance.Options.UseForeColor = true;
            this.EMAIL.Size = new System.Drawing.Size(149, 20);
            this.EMAIL.TabIndex = 21;
            this.EMAIL.TextChanged += new System.EventHandler(this.EMAIL_TextChanged);
            this.EMAIL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EMAIL_KeyPress);
            // 
            // ACCESS_NUMBER
            // 
            this.ACCESS_NUMBER.Location = new System.Drawing.Point(100, 54);
            this.ACCESS_NUMBER.Name = "ACCESS_NUMBER";
            this.ACCESS_NUMBER.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.ACCESS_NUMBER.Properties.Appearance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ACCESS_NUMBER.Properties.Appearance.Options.UseBackColor = true;
            this.ACCESS_NUMBER.Properties.Appearance.Options.UseForeColor = true;
            this.ACCESS_NUMBER.Properties.PasswordChar = '*';
            this.ACCESS_NUMBER.Size = new System.Drawing.Size(149, 20);
            this.ACCESS_NUMBER.TabIndex = 22;
            this.ACCESS_NUMBER.TextChanged += new System.EventHandler(this.ACCESS_NUMBER_TextChanged);
            this.ACCESS_NUMBER.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ACCESS_NUMBER_KeyPress);
            // 
            // DevExpressLogin
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.ACCESS_NUMBER);
            this.Controls.Add(this.EMAIL);
            this.Controls.Add(this.Language);
            this.Controls.Add(this.IS_AUTO_LOGIN);
            this.Controls.Add(this.IS_EMAIL_SAVE);
            this.Controls.Add(this.Bnt_Exit);
            this.Controls.Add(this.Bnt_Login);
            this.Controls.Add(this.Bnt_Join);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DevExpressLogin";
            this.Size = new System.Drawing.Size(594, 455);
            this.Load += new System.EventHandler(this.DefaultLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DefaultLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DefaultLogin_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.IS_EMAIL_SAVE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IS_AUTO_LOGIN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Language.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EMAIL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ACCESS_NUMBER.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl Label3;
        private DevExpress.XtraEditors.LabelControl Label1;
        private DevExpress.XtraEditors.LabelControl Label2;
        private DevExpress.XtraEditors.SimpleButton Bnt_Join;
        private DevExpress.XtraEditors.SimpleButton Bnt_Login;
        private DevExpress.XtraEditors.SimpleButton Bnt_Exit;
        private DevExpress.XtraEditors.CheckEdit IS_EMAIL_SAVE;
        private DevExpress.XtraEditors.CheckEdit IS_AUTO_LOGIN;
        private DevExpress.XtraEditors.ComboBoxEdit Language;
        private DevExpress.XtraEditors.TextEdit EMAIL;
        private DevExpress.XtraEditors.TextEdit ACCESS_NUMBER;
    }
}
