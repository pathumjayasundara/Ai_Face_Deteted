namespace FaceAttendanceSystem
{
    partial class ChangePasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            titleLabel = new Label();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            oldPasswordLabel = new Label();
            oldPasswordTextBox = new TextBox();
            newPasswordLabel = new Label();
            newPasswordTextBox = new TextBox();
            confirmPasswordLabel = new Label();
            confirmPasswordTextBox = new TextBox();
            changeButton = new Button();
            cancelButton = new Button();
            messageLabel = new Label();
            SuspendLayout();
            //
            // titleLabel
            //
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(141, 21, 58);
            titleLabel.Location = new Point(30, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Change Password";
            //
            // usernameLabel
            //
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            usernameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            usernameLabel.Location = new Point(30, 70);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Text = "Username";
            //
            // usernameTextBox
            //
            usernameTextBox.Font = new Font("Segoe UI", 10F);
            usernameTextBox.Location = new Point(30, 93);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(380, 27);
            //
            // oldPasswordLabel
            //
            oldPasswordLabel.AutoSize = true;
            oldPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            oldPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            oldPasswordLabel.Location = new Point(30, 133);
            oldPasswordLabel.Name = "oldPasswordLabel";
            oldPasswordLabel.Text = "Old Password";
            //
            // oldPasswordTextBox
            //
            oldPasswordTextBox.Font = new Font("Segoe UI", 10F);
            oldPasswordTextBox.Location = new Point(30, 156);
            oldPasswordTextBox.Name = "oldPasswordTextBox";
            oldPasswordTextBox.PasswordChar = '●';
            oldPasswordTextBox.Size = new Size(380, 27);
            //
            // newPasswordLabel
            //
            newPasswordLabel.AutoSize = true;
            newPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            newPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            newPasswordLabel.Location = new Point(30, 196);
            newPasswordLabel.Name = "newPasswordLabel";
            newPasswordLabel.Text = "New Password";
            //
            // newPasswordTextBox
            //
            newPasswordTextBox.Font = new Font("Segoe UI", 10F);
            newPasswordTextBox.Location = new Point(30, 219);
            newPasswordTextBox.Name = "newPasswordTextBox";
            newPasswordTextBox.PasswordChar = '●';
            newPasswordTextBox.Size = new Size(380, 27);
            //
            // confirmPasswordLabel
            //
            confirmPasswordLabel.AutoSize = true;
            confirmPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            confirmPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            confirmPasswordLabel.Location = new Point(30, 259);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Text = "Confirm New Password";
            //
            // confirmPasswordTextBox
            //
            confirmPasswordTextBox.Font = new Font("Segoe UI", 10F);
            confirmPasswordTextBox.Location = new Point(30, 282);
            confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            confirmPasswordTextBox.PasswordChar = '●';
            confirmPasswordTextBox.Size = new Size(380, 27);
            //
            // changeButton
            //
            changeButton.BackColor = Color.FromArgb(141, 21, 58);
            changeButton.Cursor = Cursors.Hand;
            changeButton.FlatStyle = FlatStyle.Flat;
            changeButton.FlatAppearance.BorderSize = 0;
            changeButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            changeButton.ForeColor = Color.White;
            changeButton.Location = new Point(30, 330);
            changeButton.Name = "changeButton";
            changeButton.Size = new Size(180, 42);
            changeButton.Text = "🔑 Change Password";
            changeButton.UseVisualStyleBackColor = false;
            //
            // cancelButton
            //
            cancelButton.BackColor = Color.Gray;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Location = new Point(230, 330);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(180, 42);
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = false;
            //
            // messageLabel
            //
            messageLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            messageLabel.Location = new Point(30, 385);
            messageLabel.MaximumSize = new Size(380, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(380, 50);
            messageLabel.Text = "";
            //
            // ChangePasswordForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 450);
            Controls.Add(messageLabel);
            Controls.Add(cancelButton);
            Controls.Add(changeButton);
            Controls.Add(confirmPasswordTextBox);
            Controls.Add(confirmPasswordLabel);
            Controls.Add(newPasswordTextBox);
            Controls.Add(newPasswordLabel);
            Controls.Add(oldPasswordTextBox);
            Controls.Add(oldPasswordLabel);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(titleLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label titleLabel;
        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label oldPasswordLabel;
        private TextBox oldPasswordTextBox;
        private Label newPasswordLabel;
        private TextBox newPasswordTextBox;
        private Label confirmPasswordLabel;
        private TextBox confirmPasswordTextBox;
        private Button changeButton;
        private Button cancelButton;
        private Label messageLabel;
    }
}
