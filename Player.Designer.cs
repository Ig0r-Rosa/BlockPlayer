namespace BlockPlayer
{
    partial class Janela
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
            VolumeTexto = new Label();
            VolumeVideo = new TrackBar();
            TempoAtualFinal = new Label();
            BarraVideo = new TrackBar();
            TimerVideo = new System.Windows.Forms.Timer(components);
            PainelSemVideo = new Panel();
            ContinuarAssistindo = new ListView();
            BotaoContinuarAssistindo = new Button();
            BotaoAjuda = new Button();
            BotaoAjusteMinipayer = new Button();
            BotaoEscolhaVideo = new Button();
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            Painel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).BeginInit();
            PainelSemVideo.SuspendLayout();
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
            Video.Visible = false;
            Video.DragDrop += Video_DragDrop;
            Video.DragEnter += Video_DragEnter;
            // 
            // Painel
            // 
            Painel.BackColor = Color.Transparent;
            Painel.Controls.Add(VolumeTexto);
            Painel.Controls.Add(VolumeVideo);
            Painel.Controls.Add(TempoAtualFinal);
            Painel.Controls.Add(BarraVideo);
            Painel.Dock = DockStyle.Fill;
            Painel.ForeColor = Color.Transparent;
            Painel.Location = new Point(0, 0);
            Painel.Name = "Painel";
            Painel.Size = new Size(978, 544);
            Painel.TabIndex = 1;
            Painel.Click += Painel_Click;
            Painel.DoubleClick += Painel_DoubleClick;
            // 
            // VolumeTexto
            // 
            VolumeTexto.AutoSize = true;
            VolumeTexto.Dock = DockStyle.Right;
            VolumeTexto.Font = new Font("Times New Roman", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            VolumeTexto.ForeColor = Color.Black;
            VolumeTexto.Location = new Point(788, 20);
            VolumeTexto.Name = "VolumeTexto";
            VolumeTexto.Size = new Size(40, 23);
            VolumeTexto.TabIndex = 5;
            VolumeTexto.Text = "100";
            VolumeTexto.Visible = false;
            // 
            // VolumeVideo
            // 
            VolumeVideo.BackColor = Color.FromArgb(64, 64, 64);
            VolumeVideo.Dock = DockStyle.Right;
            VolumeVideo.Location = new Point(828, 20);
            VolumeVideo.Maximum = 100;
            VolumeVideo.MaximumSize = new Size(150, 25);
            VolumeVideo.Name = "VolumeVideo";
            VolumeVideo.Size = new Size(150, 25);
            VolumeVideo.TabIndex = 3;
            VolumeVideo.Value = 50;
            VolumeVideo.Visible = false;
            VolumeVideo.Scroll += VolumeVideo_Scroll;
            // 
            // TempoAtualFinal
            // 
            TempoAtualFinal.AutoSize = true;
            TempoAtualFinal.Dock = DockStyle.Left;
            TempoAtualFinal.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempoAtualFinal.ForeColor = Color.Black;
            TempoAtualFinal.Location = new Point(0, 20);
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
            BarraVideo.MaximumSize = new Size(0, 20);
            BarraVideo.Name = "BarraVideo";
            BarraVideo.Size = new Size(978, 20);
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
            // PainelSemVideo
            // 
            PainelSemVideo.Controls.Add(ContinuarAssistindo);
            PainelSemVideo.Controls.Add(BotaoContinuarAssistindo);
            PainelSemVideo.Controls.Add(BotaoAjuda);
            PainelSemVideo.Controls.Add(BotaoAjusteMinipayer);
            PainelSemVideo.Controls.Add(BotaoEscolhaVideo);
            PainelSemVideo.Dock = DockStyle.Fill;
            PainelSemVideo.Location = new Point(0, 0);
            PainelSemVideo.Name = "PainelSemVideo";
            PainelSemVideo.Size = new Size(978, 544);
            PainelSemVideo.TabIndex = 2;
            // 
            // ContinuarAssistindo
            // 
            ContinuarAssistindo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContinuarAssistindo.Location = new Point(100, 86);
            ContinuarAssistindo.Name = "ContinuarAssistindo";
            ContinuarAssistindo.Size = new Size(779, 372);
            ContinuarAssistindo.TabIndex = 5;
            ContinuarAssistindo.UseCompatibleStateImageBehavior = false;
            // 
            // BotaoContinuarAssistindo
            // 
            BotaoContinuarAssistindo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BotaoContinuarAssistindo.ForeColor = SystemColors.ActiveCaptionText;
            BotaoContinuarAssistindo.Location = new Point(878, 464);
            BotaoContinuarAssistindo.MaximumSize = new Size(100, 80);
            BotaoContinuarAssistindo.Name = "BotaoContinuarAssistindo";
            BotaoContinuarAssistindo.Size = new Size(100, 80);
            BotaoContinuarAssistindo.TabIndex = 4;
            BotaoContinuarAssistindo.Text = "Continuar assistindo";
            BotaoContinuarAssistindo.UseVisualStyleBackColor = true;
            // 
            // BotaoAjuda
            // 
            BotaoAjuda.Dock = DockStyle.Left;
            BotaoAjuda.ForeColor = SystemColors.ActiveCaptionText;
            BotaoAjuda.Location = new Point(200, 0);
            BotaoAjuda.MaximumSize = new Size(100, 80);
            BotaoAjuda.Name = "BotaoAjuda";
            BotaoAjuda.Size = new Size(100, 80);
            BotaoAjuda.TabIndex = 2;
            BotaoAjuda.Text = "Ajuda";
            BotaoAjuda.UseVisualStyleBackColor = true;
            // 
            // BotaoAjusteMinipayer
            // 
            BotaoAjusteMinipayer.Dock = DockStyle.Left;
            BotaoAjusteMinipayer.ForeColor = SystemColors.ActiveCaptionText;
            BotaoAjusteMinipayer.Location = new Point(100, 0);
            BotaoAjusteMinipayer.MaximumSize = new Size(100, 80);
            BotaoAjusteMinipayer.Name = "BotaoAjusteMinipayer";
            BotaoAjusteMinipayer.Size = new Size(100, 80);
            BotaoAjusteMinipayer.TabIndex = 1;
            BotaoAjusteMinipayer.Text = "Ajustar o miniplayer";
            BotaoAjusteMinipayer.UseVisualStyleBackColor = true;
            BotaoAjusteMinipayer.Click += BotaoAjusteMinipayer_Click;
            // 
            // BotaoEscolhaVideo
            // 
            BotaoEscolhaVideo.Dock = DockStyle.Left;
            BotaoEscolhaVideo.ForeColor = SystemColors.ActiveCaptionText;
            BotaoEscolhaVideo.Location = new Point(0, 0);
            BotaoEscolhaVideo.MaximumSize = new Size(100, 80);
            BotaoEscolhaVideo.Name = "BotaoEscolhaVideo";
            BotaoEscolhaVideo.Size = new Size(100, 80);
            BotaoEscolhaVideo.TabIndex = 0;
            BotaoEscolhaVideo.Text = "Escolha o vídeo";
            BotaoEscolhaVideo.UseVisualStyleBackColor = true;
            BotaoEscolhaVideo.Click += BotaoEscolhaVideo_Click;
            // 
            // Janela
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(978, 544);
            Controls.Add(PainelSemVideo);
            Controls.Add(Painel);
            Controls.Add(Video);
            ForeColor = SystemColors.Control;
            KeyPreview = true;
            MinimumSize = new Size(600, 300);
            Name = "Janela";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Block Player";
            FormClosed += Form1_FormClosed;
            DragDrop += Video_DragDrop;
            DragEnter += Video_DragEnter;
            ((System.ComponentModel.ISupportInitialize)Video).EndInit();
            Painel.ResumeLayout(false);
            Painel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).EndInit();
            PainelSemVideo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView Video;
        private TransparentPanel Painel;
        private TrackBar BarraVideo;
        private System.Windows.Forms.Timer TimerVideo;
        private Label VolumeTexto;
        private TrackBar VolumeVideo;
        private Label TempoAtualFinal;
        private Panel PainelSemVideo;
        private Button BotaoEscolhaVideo;
        private Button BotaoAjusteMinipayer;
        private Button BotaoAjuda;
        private Button BotaoContinuarAssistindo;
        private ListView ContinuarAssistindo;
    }
}
