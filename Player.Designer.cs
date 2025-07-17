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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Janela));
            Video = new LibVLCSharp.WinForms.VideoView();
            Painel = new TransparentPanel();
            VolumeTexto = new Label();
            VolumeVideo = new TrackBar();
            TempoAtualFinal = new Label();
            BarraVideo = new TrackBar();
            TimerVideo = new System.Windows.Forms.Timer(components);
            PainelSemVideo = new Panel();
            pictureBox1 = new PictureBox();
            ContinuarAssistindo = new ListView();
            BotaoContinuarAssistindo = new Button();
            BotaoAjuda = new Button();
            BotaoAjusteMinipayer = new Button();
            BotaoEscolhaVideo = new Button();
            panel1 = new Panel();
            ApagarContinuarAssistindo = new Button();
            TempoVideoContinuarAssistindo = new Label();
            NomeVideoContinuarAssistindo = new Label();
            ProgressoVideoContinuarAssistindo = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            Painel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).BeginInit();
            PainelSemVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
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
            PainelSemVideo.BackColor = Color.FromArgb(21, 23, 66);
            PainelSemVideo.Controls.Add(pictureBox1);
            PainelSemVideo.Controls.Add(ContinuarAssistindo);
            PainelSemVideo.Controls.Add(BotaoContinuarAssistindo);
            PainelSemVideo.Controls.Add(BotaoAjuda);
            PainelSemVideo.Controls.Add(BotaoAjusteMinipayer);
            PainelSemVideo.Controls.Add(BotaoEscolhaVideo);
            PainelSemVideo.Controls.Add(panel1);
            PainelSemVideo.Dock = DockStyle.Fill;
            PainelSemVideo.Location = new Point(0, 0);
            PainelSemVideo.Name = "PainelSemVideo";
            PainelSemVideo.Padding = new Padding(20);
            PainelSemVideo.Size = new Size(978, 544);
            PainelSemVideo.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = DockStyle.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(878, 20);
            pictureBox1.MaximumSize = new Size(80, 80);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // ContinuarAssistindo
            // 
            ContinuarAssistindo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContinuarAssistindo.BackColor = SystemColors.Control;
            ContinuarAssistindo.Location = new Point(100, 125);
            ContinuarAssistindo.Name = "ContinuarAssistindo";
            ContinuarAssistindo.Size = new Size(777, 287);
            ContinuarAssistindo.TabIndex = 5;
            ContinuarAssistindo.UseCompatibleStateImageBehavior = false;
            ContinuarAssistindo.SelectedIndexChanged += ContinuarAssistindo_SelectedIndexChanged;
            // 
            // BotaoContinuarAssistindo
            // 
            BotaoContinuarAssistindo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BotaoContinuarAssistindo.BackColor = Color.SteelBlue;
            BotaoContinuarAssistindo.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoContinuarAssistindo.FlatStyle = FlatStyle.Flat;
            BotaoContinuarAssistindo.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BotaoContinuarAssistindo.ForeColor = SystemColors.ActiveCaptionText;
            BotaoContinuarAssistindo.Location = new Point(805, 441);
            BotaoContinuarAssistindo.MaximumSize = new Size(150, 80);
            BotaoContinuarAssistindo.Name = "BotaoContinuarAssistindo";
            BotaoContinuarAssistindo.Size = new Size(150, 80);
            BotaoContinuarAssistindo.TabIndex = 4;
            BotaoContinuarAssistindo.Text = "Continuar assistindo";
            BotaoContinuarAssistindo.UseVisualStyleBackColor = false;
            BotaoContinuarAssistindo.Click += BotaoContinuarAssistindo_Click;
            // 
            // BotaoAjuda
            // 
            BotaoAjuda.BackColor = Color.SteelBlue;
            BotaoAjuda.Dock = DockStyle.Left;
            BotaoAjuda.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoAjuda.FlatStyle = FlatStyle.Flat;
            BotaoAjuda.Font = new Font("Times New Roman", 12F);
            BotaoAjuda.ForeColor = SystemColors.ActiveCaptionText;
            BotaoAjuda.Location = new Point(320, 20);
            BotaoAjuda.MaximumSize = new Size(150, 80);
            BotaoAjuda.Name = "BotaoAjuda";
            BotaoAjuda.Size = new Size(150, 80);
            BotaoAjuda.TabIndex = 2;
            BotaoAjuda.Text = "Ajuda\r\n";
            BotaoAjuda.UseVisualStyleBackColor = false;
            // 
            // BotaoAjusteMinipayer
            // 
            BotaoAjusteMinipayer.BackColor = Color.SteelBlue;
            BotaoAjusteMinipayer.Dock = DockStyle.Left;
            BotaoAjusteMinipayer.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoAjusteMinipayer.FlatStyle = FlatStyle.Flat;
            BotaoAjusteMinipayer.Font = new Font("Times New Roman", 12F);
            BotaoAjusteMinipayer.ForeColor = SystemColors.ActiveCaptionText;
            BotaoAjusteMinipayer.Location = new Point(170, 20);
            BotaoAjusteMinipayer.MaximumSize = new Size(150, 80);
            BotaoAjusteMinipayer.Name = "BotaoAjusteMinipayer";
            BotaoAjusteMinipayer.Size = new Size(150, 80);
            BotaoAjusteMinipayer.TabIndex = 1;
            BotaoAjusteMinipayer.Text = "Ajustar miniplayer";
            BotaoAjusteMinipayer.UseVisualStyleBackColor = false;
            BotaoAjusteMinipayer.Click += BotaoAjusteMinipayer_Click;
            // 
            // BotaoEscolhaVideo
            // 
            BotaoEscolhaVideo.BackColor = Color.SteelBlue;
            BotaoEscolhaVideo.Dock = DockStyle.Left;
            BotaoEscolhaVideo.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoEscolhaVideo.FlatStyle = FlatStyle.Flat;
            BotaoEscolhaVideo.Font = new Font("Times New Roman", 12F);
            BotaoEscolhaVideo.ForeColor = SystemColors.ActiveCaptionText;
            BotaoEscolhaVideo.Location = new Point(20, 20);
            BotaoEscolhaVideo.MaximumSize = new Size(150, 80);
            BotaoEscolhaVideo.Name = "BotaoEscolhaVideo";
            BotaoEscolhaVideo.Size = new Size(150, 80);
            BotaoEscolhaVideo.TabIndex = 0;
            BotaoEscolhaVideo.Text = "Escolher \r\nvídeo";
            BotaoEscolhaVideo.UseVisualStyleBackColor = false;
            BotaoEscolhaVideo.Click += BotaoEscolhaVideo_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(ApagarContinuarAssistindo);
            panel1.Controls.Add(TempoVideoContinuarAssistindo);
            panel1.Controls.Add(NomeVideoContinuarAssistindo);
            panel1.Controls.Add(ProgressoVideoContinuarAssistindo);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(20, 421);
            panel1.Name = "panel1";
            panel1.Size = new Size(938, 103);
            panel1.TabIndex = 9;
            // 
            // ApagarContinuarAssistindo
            // 
            ApagarContinuarAssistindo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ApagarContinuarAssistindo.BackColor = Color.Red;
            ApagarContinuarAssistindo.FlatStyle = FlatStyle.Flat;
            ApagarContinuarAssistindo.Location = new Point(739, 60);
            ApagarContinuarAssistindo.MaximumSize = new Size(40, 40);
            ApagarContinuarAssistindo.Name = "ApagarContinuarAssistindo";
            ApagarContinuarAssistindo.Size = new Size(40, 40);
            ApagarContinuarAssistindo.TabIndex = 11;
            ApagarContinuarAssistindo.Text = "🗑️";
            ApagarContinuarAssistindo.UseVisualStyleBackColor = false;
            ApagarContinuarAssistindo.Click += ApagarContinuarAssistindo_Click;
            // 
            // TempoVideoContinuarAssistindo
            // 
            TempoVideoContinuarAssistindo.AutoSize = true;
            TempoVideoContinuarAssistindo.Dock = DockStyle.Bottom;
            TempoVideoContinuarAssistindo.Font = new Font("Times New Roman", 12F);
            TempoVideoContinuarAssistindo.Location = new Point(0, 42);
            TempoVideoContinuarAssistindo.Name = "TempoVideoContinuarAssistindo";
            TempoVideoContinuarAssistindo.Size = new Size(139, 27);
            TempoVideoContinuarAssistindo.TabIndex = 8;
            TempoVideoContinuarAssistindo.Text = "00:00 / 00:00";
            TempoVideoContinuarAssistindo.Visible = false;
            // 
            // NomeVideoContinuarAssistindo
            // 
            NomeVideoContinuarAssistindo.AutoSize = true;
            NomeVideoContinuarAssistindo.Dock = DockStyle.Top;
            NomeVideoContinuarAssistindo.Font = new Font("Times New Roman", 12F);
            NomeVideoContinuarAssistindo.Location = new Point(0, 0);
            NomeVideoContinuarAssistindo.MaximumSize = new Size(700, 80);
            NomeVideoContinuarAssistindo.Name = "NomeVideoContinuarAssistindo";
            NomeVideoContinuarAssistindo.Size = new Size(69, 27);
            NomeVideoContinuarAssistindo.TabIndex = 7;
            NomeVideoContinuarAssistindo.Text = "Vídeo";
            NomeVideoContinuarAssistindo.Visible = false;
            // 
            // ProgressoVideoContinuarAssistindo
            // 
            ProgressoVideoContinuarAssistindo.BackColor = SystemColors.Control;
            ProgressoVideoContinuarAssistindo.Dock = DockStyle.Bottom;
            ProgressoVideoContinuarAssistindo.Location = new Point(0, 69);
            ProgressoVideoContinuarAssistindo.MaximumSize = new Size(600, 34);
            ProgressoVideoContinuarAssistindo.Name = "ProgressoVideoContinuarAssistindo";
            ProgressoVideoContinuarAssistindo.Size = new Size(600, 34);
            ProgressoVideoContinuarAssistindo.TabIndex = 6;
            ProgressoVideoContinuarAssistindo.Visible = false;
            // 
            // Janela
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.Black;
            ClientSize = new Size(978, 544);
            Controls.Add(PainelSemVideo);
            Controls.Add(Painel);
            Controls.Add(Video);
            ForeColor = SystemColors.Control;
            KeyPreview = true;
            MinimumSize = new Size(850, 450);
            Name = "Janela";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Block Player";
            TopMost = true;
            FormClosed += Player_FormClosed;
            DragDrop += Video_DragDrop;
            DragEnter += Video_DragEnter;
            ((System.ComponentModel.ISupportInitialize)Video).EndInit();
            Painel.ResumeLayout(false);
            Painel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)VolumeVideo).EndInit();
            ((System.ComponentModel.ISupportInitialize)BarraVideo).EndInit();
            PainelSemVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ProgressBar ProgressoVideoContinuarAssistindo;
        private Label NomeVideoContinuarAssistindo;
        private Label TempoVideoContinuarAssistindo;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Button ApagarContinuarAssistindo;
    }
}
