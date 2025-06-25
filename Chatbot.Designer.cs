namespace Chatbot
{
    partial class Chatbot
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            OutputText = new RichTextBox();
            InputText = new TextBox();
            SendButton = new Button();
            ModelSelector = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // OutputText
            // 
            OutputText.Anchor = AnchorStyles.Left;
            OutputText.BackColor = Color.FromArgb(32, 32, 32);
            OutputText.ForeColor = SystemColors.Window;
            OutputText.HideSelection = false;
            OutputText.Location = new Point(12, 37);
            OutputText.Name = "OutputText";
            OutputText.ReadOnly = true;
            OutputText.Size = new Size(234, 612);
            OutputText.TabIndex = 0;
            OutputText.Text = "";
            // 
            // InputText
            // 
            InputText.BackColor = Color.FromArgb(32, 32, 32);
            InputText.ForeColor = SystemColors.Window;
            InputText.Location = new Point(12, 655);
            InputText.Name = "InputText";
            InputText.Size = new Size(1113, 23);
            InputText.TabIndex = 1;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(1131, 655);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(121, 23);
            SendButton.TabIndex = 2;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // ModelSelector
            // 
            ModelSelector.AutoCompleteSource = AutoCompleteSource.ListItems;
            ModelSelector.BackColor = Color.FromArgb(32, 32, 32);
            ModelSelector.Dock = DockStyle.Right;
            ModelSelector.ForeColor = SystemColors.Window;
            ModelSelector.FormattingEnabled = true;
            ModelSelector.Location = new Point(1079, 0);
            ModelSelector.Name = "ModelSelector";
            ModelSelector.Size = new Size(185, 23);
            ModelSelector.TabIndex = 3;
            ModelSelector.Text = "Model";
            ModelSelector.SelectedIndexChanged += ModelSelector_SelectedIndexChanged;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(252, 37);
            webView.Name = "webView";
            webView.Size = new Size(1000, 612);
            webView.TabIndex = 5;
            webView.ZoomFactor = 1D;
            // 
            // Chatbot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(1264, 690);
            Controls.Add(webView);
            Controls.Add(ModelSelector);
            Controls.Add(SendButton);
            Controls.Add(InputText);
            Controls.Add(OutputText);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Chatbot";
            Text = "Chatbot2025";
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox OutputText;
        private TextBox InputText;
        private Button SendButton;
        private ComboBox ModelSelector;
        private ContextMenuStrip contextMenuStrip1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
    }
}
