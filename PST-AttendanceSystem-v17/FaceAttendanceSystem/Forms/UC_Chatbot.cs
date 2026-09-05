using FaceAttendanceSystem.Controls;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_Chatbot : UserControl
    {
        private readonly System.Windows.Forms.Timer _typingTimer = new() { Interval = 400 };
        private int _typingDots;
        private ChatBubbleControl? _typingBubble;

        public UC_Chatbot()
        {
            InitializeComponent();
            _typingTimer.Tick += TypingTimer_Tick;
            sendButton.Click += SendButton_Click;
            inputTextBox.KeyDown += InputTextBox_KeyDown;

            AddBotMessage(
                "Hello! 👋 I'm the PST Attendance Assistant.\n\n" +
                "Type a student's index number (e.g. 22APP1234) or their name, " +
                "and I'll tell you who they are and whether they've attended their lectures.");
        }

        private void InputTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendButton_Click(sender, EventArgs.Empty);
            }
        }

        private void SendButton_Click(object? sender, EventArgs e)
        {
            string text = inputTextBox.Text.Trim();
            if (text.Length == 0)
            {
                return;
            }

            AddUserMessage(text);
            inputTextBox.Clear();
            inputTextBox.Focus();

            ShowTypingIndicator();

            // A short, deliberate "thinking" delay makes the assistant feel more
            // natural/advanced rather than answering instantly like a lookup table.
            var responseTimer = new System.Windows.Forms.Timer { Interval = 700 };
            responseTimer.Tick += (s2, e2) =>
            {
                responseTimer.Stop();
                responseTimer.Dispose();
                HideTypingIndicator();
                string reply = ChatbotService.GetResponse(text);
                AddBotMessage(reply);
            };
            responseTimer.Start();
        }

        private void ShowTypingIndicator()
        {
            _typingDots = 0;
            _typingBubble = AddBubble(".", isFromUser: false);
            _typingTimer.Start();
        }

        private void TypingTimer_Tick(object? sender, EventArgs e)
        {
            if (_typingBubble == null)
            {
                _typingTimer.Stop();
                return;
            }

            _typingDots = (_typingDots % 3) + 1;
            _typingBubble.UpdateText(new string('.', _typingDots));
        }

        private void HideTypingIndicator()
        {
            _typingTimer.Stop();
            if (_typingBubble != null)
            {
                _nextBubbleTop -= _typingBubble.Height + 12;
                chatPanel.Controls.Remove(_typingBubble);
                _typingBubble.Dispose();
                _typingBubble = null;
            }
        }

        private void AddUserMessage(string text) => AddBubble(text, isFromUser: true);

        private void AddBotMessage(string text) => AddBubble(text, isFromUser: false);

        private int _nextBubbleTop = 10;

        private ChatBubbleControl AddBubble(string text, bool isFromUser)
        {
            int containerWidth = chatPanel.ClientSize.Width;
            int maxWidth = (int)(containerWidth * 0.72);
            var bubble = new ChatBubbleControl(text, isFromUser, Math.Max(maxWidth, 200));

            bubble.Left = isFromUser
                ? Math.Max(containerWidth - bubble.Width - 30, 10)
                : 15;
            bubble.Top = _nextBubbleTop;

            chatPanel.Controls.Add(bubble);
            _nextBubbleTop += bubble.Height + 12;

            chatPanel.ScrollControlIntoView(bubble);
            return bubble;
        }
    }
}
