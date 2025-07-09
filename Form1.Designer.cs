namespace BlockPlayer
{
    partial class Form1
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
            Video = new LibVLCSharp.WinForms.VideoView();
            Painel = new TransparentPanel();
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            SuspendLayout();
            // 
            // Video
            // 
            Video.AllowDrop = true;
            Video.BackColor = Color.Black;
            Video.Dock = DockStyle.Fill;
            Video.Location = new Point(0, 0);
            Video.MediaPlayer = null;
            Video.Name = "Video";
            Video.Size = new Size(800, 450);
            Video.TabIndex = 0;
            Video.Text = "videoView1";
            Video.DragDrop += Video_DragDrop;
            Video.DragEnter += Video_DragEnter;
            // 
            // Painel
            // 
            Painel.BackColor = Color.Transparent;
            Painel.Dock = DockStyle.Fill;
            Painel.ForeColor = Color.Transparent;
            Painel.Location = new Point(0, 0);
            Painel.Name = "Painel";
            Painel.Size = new Size(800, 450);
            Painel.TabIndex = 1;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(Painel);
            Controls.Add(Video);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Block Player";
            FormClosed += Form1_FormClosed;
            DragDrop += Video_DragDrop;
            DragEnter += Video_DragEnter;
            ((System.ComponentModel.ISupportInitialize)Video).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView Video;
        private TransparentPanel Painel;
    }
}
