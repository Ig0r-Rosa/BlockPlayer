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
            BackgroundInterface = new Panel();
            BotaoStop = new Button();
            VolumeTexto = new Label();
            VolumeVideo = new TrackBar();
            TimerVideo = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            Painel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).BeginInit();
            BackgroundInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).BeginInit();
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
            Painel.Controls.Add(BackgroundInterface);
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
            TempoAtualFinal.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempoAtualFinal.ForeColor = Color.Black;
            TempoAtualFinal.Location = new Point(12, 18);
            TempoAtualFinal.Name = "TempoAtualFinal";
            TempoAtualFinal.Size = new Size(179, 35);
            TempoAtualFinal.TabIndex = 2;
            TempoAtualFinal.Text = "00:00 / 00:00";
            TempoAtualFinal.Visible = false;
            // 
            // BarraVideo
            // 
            BarraVideo.BackColor = Color.FromArgb(64, 64, 64);
            BarraVideo.Dock = DockStyle.Top;
            BarraVideo.LargeChange = 10;
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
            // BackgroundInterface
            // 
            BackgroundInterface.BackColor = Color.White;
            BackgroundInterface.Controls.Add(BotaoStop);
            BackgroundInterface.Controls.Add(VolumeTexto);
            BackgroundInterface.Controls.Add(VolumeVideo);
            BackgroundInterface.ForeColor = Color.Black;
            BackgroundInterface.Location = new Point(3, 3);
            BackgroundInterface.Name = "BackgroundInterface";
            BackgroundInterface.Size = new Size(212, 150);
            BackgroundInterface.TabIndex = 4;
            BackgroundInterface.Visible = false;
            // 
            // BotaoStop
            // 
            BotaoStop.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BotaoStop.Location = new Point(9, 65);
            BotaoStop.Name = "BotaoStop";
            BotaoStop.Size = new Size(91, 34);
            BotaoStop.TabIndex = 6;
            BotaoStop.Text = "🔲";
            BotaoStop.UseVisualStyleBackColor = true;
            BotaoStop.Click += BotaoStop_Click;
            // 
            // VolumeTexto
            // 
            VolumeTexto.AutoSize = true;
            VolumeTexto.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VolumeTexto.ForeColor = Color.Black;
            VolumeTexto.Location = new Point(165, 118);
            VolumeTexto.Name = "VolumeTexto";
            VolumeTexto.Size = new Size(40, 23);
            VolumeTexto.TabIndex = 5;
            VolumeTexto.Text = "100";
            VolumeTexto.Visible = false;
            // 
            // VolumeVideo
            // 
            VolumeVideo.BackColor = Color.FromArgb(64, 64, 64);
            VolumeVideo.Location = new Point(9, 118);
            VolumeVideo.Maximum = 100;
            VolumeVideo.MaximumSize = new Size(150, 20);
            VolumeVideo.Name = "VolumeVideo";
            VolumeVideo.Size = new Size(150, 20);
            VolumeVideo.TabIndex = 3;
            VolumeVideo.Visible = false;
            VolumeVideo.Scroll += VolumeVideo_Scroll;
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
            BackgroundInterface.ResumeLayout(false);
            BackgroundInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView Video;
        private TransparentPanel Painel;
        private TrackBar BarraVideo;
        private System.Windows.Forms.Timer TimerVideo;
        private Label TempoAtualFinal;
        private TrackBar VolumeVideo;
        private Panel BackgroundInterface;
        private Label VolumeTexto;
        private Button BotaoStop;
    }
}
