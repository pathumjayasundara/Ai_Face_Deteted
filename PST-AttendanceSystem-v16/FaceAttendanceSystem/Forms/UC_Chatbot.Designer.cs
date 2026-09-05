namespace FaceAttendanceSystem.Forms
{
    partial class UC_Chatbot
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
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            botAvatarLabel = new Label();
            headerTitleLabel = new Label();
            headerSubtitleLabel = new Label();
            chatPanel = new Panel();
            inputPanel = new Panel();
            inputTextBox = new TextBox();
            sendButton = new Button();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            inputPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(238, 232, 236);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(chatPanel, 0, 1);
            mainLayout.Controls.Add(inputPanel, 0, 2);
            mainLayout.Name = "mainLayout";
            //
            // headerPanel
            //
            headerPanel.BackColor = Color.FromArgb(141, 21, 58);
            headerPanel.Controls.Add(headerSubtitleLabel);
            headerPanel.Controls.Add(headerTitleLabel);
            headerPanel.Controls.Add(botAvatarLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Name = "headerPanel";
            //
            // botAvatarLabel
            //
            botAvatarLabel.AutoSize = true;
            botAvatarLabel.Font = new Font("Segoe UI Emoji", 26F);
            botAvatarLabel.Location = new Point(20, 14);
            botAvatarLabel.Name = "botAvatarLabel";
            botAvatarLabel.Text = "🤖";
            //
            // headerTitleLabel
            //
            headerTitleLabel.AutoSize = true;
            headerTitleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            headerTitleLabel.ForeColor = Color.White;
            headerTitleLabel.Location = new Point(80, 14);
            headerTitleLabel.Name = "headerTitleLabel";
            headerTitleLabel.Text = "PST Attendance Assistant";
            //
            // headerSubtitleLabel
            //
            headerSubtitleLabel.AutoSize = true;
            headerSubtitleLabel.Font = new Font("Segoe UI", 9.5F);
            headerSubtitleLabel.ForeColor = Color.FromArgb(212, 160, 23);
            headerSubtitleLabel.Location = new Point(80, 44);
            headerSubtitleLabel.Name = "headerSubtitleLabel";
            headerSubtitleLabel.Text = "🟢 Online - ask about any registered student";
            //
            // chatPanel
            //
            chatPanel.AutoScroll = true;
            chatPanel.BackColor = Color.FromArgb(238, 232, 236);
            chatPanel.Dock = DockStyle.Fill;
            chatPanel.Name = "chatPanel";
            //
            // inputPanel
            //
            inputPanel.BackColor = Color.White;
            inputPanel.Controls.Add(sendButton);
            inputPanel.Controls.Add(inputTextBox);
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.Padding = new Padding(15, 12, 15, 12);
            inputPanel.Name = "inputPanel";
            //
            // inputTextBox
            //
            inputTextBox.Dock = DockStyle.Fill;
            inputTextBox.Font = new Font("Segoe UI", 11F);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.PlaceholderText = "Type a student's index number or name...";
            //
            // sendButton
            //
            sendButton.BackColor = Color.FromArgb(141, 21, 58);
            sendButton.Cursor = Cursors.Hand;
            sendButton.Dock = DockStyle.Right;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sendButton.ForeColor = Color.White;
            sendButton.Name = "sendButton";
            sendButton.Text = "Send ➤";
            sendButton.UseVisualStyleBackColor = false;
            sendButton.Width = 110;
            //
            // UC_Chatbot
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Name = "UC_Chatbot";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private Label botAvatarLabel;
        private Label headerTitleLabel;
        private Label headerSubtitleLabel;
        private Panel chatPanel;
        private Panel inputPanel;
        private TextBox inputTextBox;
        private Button sendButton;
    }
}
