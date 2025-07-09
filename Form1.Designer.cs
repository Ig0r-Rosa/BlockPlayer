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
            components = new System.ComponentModel.Container();
            Video = new LibVLCSharp.WinForms.VideoView();
            Painel = new TransparentPanel();
            TempoAtualFinal = new Label();
            BarraVideo = new TrackBar();
            TimerVideo = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            Painel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).BeginInit();
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
            Video.Size = new Size(978, 544);
            Video.TabIndex = 0;
            Video.Text = "videoView1";
            Video.DragDrop += Video_DragDrop;
            Video.DragEnter += Video_DragEnter;
            // 
            // Painel
            // 
            Painel.BackColor = Color.Transparent;
            Painel.Controls.Add(TempoAtualFinal);
            Painel.Controls.Add(BarraVideo);
            Painel.Dock = DockStyle.Fill;
            Painel.ForeColor = Color.Transparent;
            Painel.Location = new Point(0, 0);
            Painel.Name = "Painel";
            Painel.Size = new Size(978, 544);
            Painel.TabIndex = 1;
            Painel.Click += Painel_Click;
            // 
            // TempoAtualFinal
            // 
            TempoAtualFinal.AutoSize = true;
            TempoAtualFinal.Location = new Point(12, 18);
            TempoAtualFinal.Name = "TempoAtualFinal";
            TempoAtualFinal.Size = new Size(117, 25);
            TempoAtualFinal.TabIndex = 2;
            TempoAtualFinal.Text = "00:00 / 00:00";
            TempoAtualFinal.Visible = false;
            // 
            // BarraVideo
            // 
            BarraVideo.BackColor = Color.FromArgb(64, 64, 64);
            BarraVideo.Dock = DockStyle.Top;
            BarraVideo.Location = new Point(0, 0);
            BarraVideo.Maximum = 1000;
            BarraVideo.MaximumSize = new Size(0, 15);
            BarraVideo.Name = "BarraVideo";
            BarraVideo.Size = new Size(978, 15);
            BarraVideo.TabIndex = 1;
            BarraVideo.TickStyle = TickStyle.None;
            BarraVideo.Visible = false;
            BarraVideo.Scroll += BarraVideo_Scroll;
            BarraVideo.MouseUp += BarraVideo_MouseUp;
            // 
            // TimerVideo
            // 
            TimerVideo.Interval = 10;
            TimerVideo.Tick += TimerVideo_Tick;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(978, 544);
            Controls.Add(Painel);
            Controls.Add(Video);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Block Player";
            FormClosed += Form1_FormClosed;
            DragDrop += Video_DragDrop;
            DragEnter += Video_DragEnter;
            ((System.ComponentModel.ISupportInitialize)Video).EndInit();
            Painel.ResumeLayout(false);
            Painel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView Video;
        private TransparentPanel Painel;
        private TrackBar BarraVideo;
        private System.Windows.Forms.Timer TimerVideo;
        private Label TempoAtualFinal;
    }
}
