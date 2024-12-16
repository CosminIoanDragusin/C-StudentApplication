
namespace StudentManagement
{
    partial class Information
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.back_to_login = new System.Windows.Forms.Button();
            this.confirm_forgot = new System.Windows.Forms.Button();
            this.forgot_answer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ExitIcon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Question = new System.Windows.Forms.ComboBox();
            this.information_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // back_to_login
            // 
            this.back_to_login.BackColor = System.Drawing.SystemColors.Highlight;
            this.back_to_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back_to_login.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_to_login.ForeColor = System.Drawing.Color.White;
            this.back_to_login.Location = new System.Drawing.Point(8, 357);
            this.back_to_login.Name = "back_to_login";
            this.back_to_login.Size = new System.Drawing.Size(279, 30);
            this.back_to_login.TabIndex = 20;
            this.back_to_login.Text = "< Back";
            this.back_to_login.UseVisualStyleBackColor = false;
            this.back_to_login.Click += new System.EventHandler(this.back_to_login_Click);
            // 
            // confirm_forgot
            // 
            this.confirm_forgot.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirm_forgot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm_forgot.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirm_forgot.ForeColor = System.Drawing.Color.White;
            this.confirm_forgot.Location = new System.Drawing.Point(8, 253);
            this.confirm_forgot.Name = "confirm_forgot";
            this.confirm_forgot.Size = new System.Drawing.Size(279, 30);
            this.confirm_forgot.TabIndex = 17;
            this.confirm_forgot.Text = "Confirm";
            this.confirm_forgot.UseVisualStyleBackColor = false;
            this.confirm_forgot.Click += new System.EventHandler(this.confirm_forgot_Click);
            // 
            // forgot_answer
            // 
            this.forgot_answer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgot_answer.Location = new System.Drawing.Point(8, 215);
            this.forgot_answer.Name = "forgot_answer";
            this.forgot_answer.Size = new System.Drawing.Size(279, 24);
            this.forgot_answer.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Answer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Question";
            // 
            // ExitIcon
            // 
            this.ExitIcon.AutoSize = true;
            this.ExitIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitIcon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitIcon.ForeColor = System.Drawing.Color.Red;
            this.ExitIcon.Location = new System.Drawing.Point(291, -1);
            this.ExitIcon.Name = "ExitIcon";
            this.ExitIcon.Size = new System.Drawing.Size(21, 22);
            this.ExitIcon.TabIndex = 12;
            this.ExitIcon.Text = "X";
            this.ExitIcon.Click += new System.EventHandler(this.ExitIcon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "Forgot Password";
            // 
            // comboBox_Question
            // 
            this.comboBox_Question.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Question.FormattingEnabled = true;
            this.comboBox_Question.Items.AddRange(new object[] {
            "What is your favourite color ?",
            "Your mother name ?",
            "Your city name ?",
            "Your favourite food ?"});
            this.comboBox_Question.Location = new System.Drawing.Point(8, 164);
            this.comboBox_Question.Name = "comboBox_Question";
            this.comboBox_Question.Size = new System.Drawing.Size(275, 24);
            this.comboBox_Question.TabIndex = 25;
            // 
            // information_username
            // 
            this.information_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.information_username.Location = new System.Drawing.Point(6, 104);
            this.information_username.Name = "information_username";
            this.information_username.Size = new System.Drawing.Size(279, 24);
            this.information_username.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Username";
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 411);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.information_username);
            this.Controls.Add(this.comboBox_Question);
            this.Controls.Add(this.back_to_login);
            this.Controls.Add(this.confirm_forgot);
            this.Controls.Add(this.forgot_answer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExitIcon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Information";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back_to_login;
        private System.Windows.Forms.Button confirm_forgot;
        private System.Windows.Forms.TextBox forgot_answer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ExitIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Question;
        private System.Windows.Forms.TextBox information_username;
        private System.Windows.Forms.Label label2;
    }
}