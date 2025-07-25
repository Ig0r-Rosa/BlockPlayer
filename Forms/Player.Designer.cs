using BlockPlayer.Classes;

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
            PainelInterface = new Panel();
            PainelVolume = new Panel();
            VolumeVideo = new DoubleBufferedPanel();
            VolumeTexto = new Label();
            TempoAtualFinal = new Label();
            BarraVideo = new DoubleBufferedPanel();
            TimerVideo = new System.Windows.Forms.Timer(components);
            PainelSemVideo = new Panel();
            ApagarContinuarAssistindo = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            BotaoContinuarAssistindo = new Button();
            BotaoAjusteMinipayer = new Button();
            ContinuarAssistindo = new FlowLayoutPanel();
            panel4 = new Panel();
            panel3 = new Panel();
            BotaoEscolhaVideo = new Button();
            panel1 = new Panel();
            TempoVideoContinuarAssistindo = new Label();
            NomeVideoContinuarAssistindo = new Label();
            ProgressoVideoContinuarAssistindo = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)Video).BeginInit();
            Painel.SuspendLayout();
            PainelInterface.SuspendLayout();
            PainelVolume.SuspendLayout();
            PainelSemVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ContinuarAssistindo.SuspendLayout();
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
            Video.Size = new Size(1055, 544);
            Video.TabIndex = 0;
            Video.Text = "videoView1";
            Video.Visible = false;
            Video.DragDrop += Video_DragDrop;
            Video.DragEnter += Video_DragEnter;
            // 
            // Painel
            // 
            Painel.BackColor = Color.Transparent;
            Painel.Controls.Add(PainelInterface);
            Painel.Dock = DockStyle.Fill;
            Painel.ForeColor = Color.Transparent;
            Painel.Location = new Point(0, 0);
            Painel.Name = "Painel";
            Painel.Size = new Size(1055, 544);
            Painel.TabIndex = 1;
            Painel.Click += Painel_Click;
            Painel.DoubleClick += Painel_DoubleClick;
            // 
            // PainelInterface
            // 
            PainelInterface.Controls.Add(PainelVolume);
            PainelInterface.Controls.Add(TempoAtualFinal);
            PainelInterface.Controls.Add(BarraVideo);
            PainelInterface.Dock = DockStyle.Top;
            PainelInterface.Location = new Point(0, 0);
            PainelInterface.Name = "PainelInterface";
            PainelInterface.Padding = new Padding(10, 15, 10, 10);
            PainelInterface.Size = new Size(1055, 84);
            PainelInterface.TabIndex = 6;
            // 
            // PainelVolume
            // 
            PainelVolume.AutoSize = true;
            PainelVolume.Controls.Add(VolumeVideo);
            PainelVolume.Controls.Add(VolumeTexto);
            PainelVolume.Dock = DockStyle.Right;
            PainelVolume.Location = new Point(839, 15);
            PainelVolume.Name = "PainelVolume";
            PainelVolume.Padding = new Padding(10);
            PainelVolume.Size = new Size(206, 44);
            PainelVolume.TabIndex = 6;
            // 
            // VolumeVideo
            // 
            VolumeVideo.BackColor = Color.DimGray;
            VolumeVideo.Dock = DockStyle.Right;
            VolumeVideo.Location = new Point(46, 10);
            VolumeVideo.MaximumSize = new Size(150, 15);
            VolumeVideo.Name = "VolumeVideo";
            VolumeVideo.Size = new Size(150, 15);
            VolumeVideo.TabIndex = 7;
            VolumeVideo.MouseDown += VolumeVideo_MouseDown;
            VolumeVideo.MouseMove += VolumeVideo_MouseMove;
            VolumeVideo.MouseUp += VolumeVideo_MouseUp;
            // 
            // VolumeTexto
            // 
            VolumeTexto.AutoSize = true;
            VolumeTexto.Dock = DockStyle.Left;
            VolumeTexto.Font = new Font("Times New Roman", 8.5F, FontStyle.Bold);
            VolumeTexto.ForeColor = Color.White;
            VolumeTexto.Location = new Point(10, 10);
            VolumeTexto.MaximumSize = new Size(0, 15);
            VolumeTexto.Name = "VolumeTexto";
            VolumeTexto.Size = new Size(36, 15);
            VolumeTexto.TabIndex = 5;
            VolumeTexto.Text = "100";
            VolumeTexto.Visible = false;
            // 
            // TempoAtualFinal
            // 
            TempoAtualFinal.AutoSize = true;
            TempoAtualFinal.Dock = DockStyle.Left;
            TempoAtualFinal.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempoAtualFinal.ForeColor = Color.White;
            TempoAtualFinal.Location = new Point(10, 15);
            TempoAtualFinal.Name = "TempoAtualFinal";
            TempoAtualFinal.Size = new Size(179, 35);
            TempoAtualFinal.TabIndex = 2;
            TempoAtualFinal.Text = "00:00 / 00:00";
            TempoAtualFinal.Visible = false;
            // 
            // BarraVideo
            // 
            BarraVideo.BackColor = Color.DimGray;
            BarraVideo.Dock = DockStyle.Bottom;
            BarraVideo.Location = new Point(10, 59);
            BarraVideo.MaximumSize = new Size(0, 15);
            BarraVideo.Name = "BarraVideo";
            BarraVideo.Size = new Size(1035, 15);
            BarraVideo.TabIndex = 8;
            BarraVideo.MouseDown += BarraVideo_MouseDown;
            BarraVideo.MouseMove += BarraVideo_MouseMove;
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
            PainelSemVideo.Controls.Add(ApagarContinuarAssistindo);
            PainelSemVideo.Controls.Add(panel2);
            PainelSemVideo.Controls.Add(pictureBox1);
            PainelSemVideo.Controls.Add(BotaoContinuarAssistindo);
            PainelSemVideo.Controls.Add(BotaoAjusteMinipayer);
            PainelSemVideo.Controls.Add(ContinuarAssistindo);
            PainelSemVideo.Controls.Add(panel3);
            PainelSemVideo.Controls.Add(BotaoEscolhaVideo);
            PainelSemVideo.Controls.Add(panel1);
            PainelSemVideo.Dock = DockStyle.Fill;
            PainelSemVideo.Location = new Point(0, 0);
            PainelSemVideo.Name = "PainelSemVideo";
            PainelSemVideo.Padding = new Padding(20);
            PainelSemVideo.Size = new Size(1055, 544);
            PainelSemVideo.TabIndex = 2;
            // 
            // ApagarContinuarAssistindo
            // 
            ApagarContinuarAssistindo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ApagarContinuarAssistindo.BackColor = Color.Red;
            ApagarContinuarAssistindo.Cursor = Cursors.Hand;
            ApagarContinuarAssistindo.FlatAppearance.BorderColor = Color.Black;
            ApagarContinuarAssistindo.FlatAppearance.BorderSize = 0;
            ApagarContinuarAssistindo.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 128);
            ApagarContinuarAssistindo.FlatStyle = FlatStyle.Flat;
            ApagarContinuarAssistindo.Location = new Point(816, 481);
            ApagarContinuarAssistindo.MaximumSize = new Size(40, 40);
            ApagarContinuarAssistindo.Name = "ApagarContinuarAssistindo";
            ApagarContinuarAssistindo.Size = new Size(40, 40);
            ApagarContinuarAssistindo.TabIndex = 11;
            ApagarContinuarAssistindo.Text = "🗑️";
            ApagarContinuarAssistindo.UseVisualStyleBackColor = false;
            ApagarContinuarAssistindo.Click += ApagarContinuarAssistindo_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(340, 20);
            panel2.MaximumSize = new Size(20, 80);
            panel2.Name = "panel2";
            panel2.Size = new Size(20, 80);
            panel2.TabIndex = 12;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = DockStyle.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(955, 20);
            pictureBox1.MaximumSize = new Size(80, 80);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // BotaoContinuarAssistindo
            // 
            BotaoContinuarAssistindo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BotaoContinuarAssistindo.BackColor = Color.SteelBlue;
            BotaoContinuarAssistindo.Cursor = Cursors.Hand;
            BotaoContinuarAssistindo.FlatAppearance.BorderSize = 0;
            BotaoContinuarAssistindo.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoContinuarAssistindo.FlatStyle = FlatStyle.Flat;
            BotaoContinuarAssistindo.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BotaoContinuarAssistindo.ForeColor = SystemColors.ActiveCaptionText;
            BotaoContinuarAssistindo.Location = new Point(882, 441);
            BotaoContinuarAssistindo.MaximumSize = new Size(150, 80);
            BotaoContinuarAssistindo.Name = "BotaoContinuarAssistindo";
            BotaoContinuarAssistindo.Size = new Size(150, 80);
            BotaoContinuarAssistindo.TabIndex = 4;
            BotaoContinuarAssistindo.Text = "Continuar assistindo";
            BotaoContinuarAssistindo.UseVisualStyleBackColor = false;
            BotaoContinuarAssistindo.Click += BotaoContinuarAssistindo_Click;
            // 
            // BotaoAjusteMinipayer
            // 
            BotaoAjusteMinipayer.BackColor = Color.SteelBlue;
            BotaoAjusteMinipayer.Cursor = Cursors.Hand;
            BotaoAjusteMinipayer.Dock = DockStyle.Left;
            BotaoAjusteMinipayer.FlatAppearance.BorderSize = 0;
            BotaoAjusteMinipayer.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            BotaoAjusteMinipayer.FlatStyle = FlatStyle.Flat;
            BotaoAjusteMinipayer.Font = new Font("Times New Roman", 12F);
            BotaoAjusteMinipayer.ForeColor = SystemColors.ActiveCaptionText;
            BotaoAjusteMinipayer.Location = new Point(190, 20);
            BotaoAjusteMinipayer.MaximumSize = new Size(150, 80);
            BotaoAjusteMinipayer.Name = "BotaoAjusteMinipayer";
            BotaoAjusteMinipayer.Size = new Size(150, 80);
            BotaoAjusteMinipayer.TabIndex = 1;
            BotaoAjusteMinipayer.Text = "Ajustar miniplayer";
            BotaoAjusteMinipayer.UseVisualStyleBackColor = false;
            BotaoAjusteMinipayer.Click += BotaoAjusteMinipayer_Click;
            // 
            // ContinuarAssistindo
            // 
            ContinuarAssistindo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ContinuarAssistindo.AutoScroll = true;
            ContinuarAssistindo.BackColor = Color.White;
            ContinuarAssistindo.Controls.Add(panel4);
            ContinuarAssistindo.Location = new Point(100, 125);
            ContinuarAssistindo.Name = "ContinuarAssistindo";
            ContinuarAssistindo.Size = new Size(866, 293);
            ContinuarAssistindo.TabIndex = 11;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Transparent;
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(0, 0);
            panel4.TabIndex = 14;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(170, 20);
            panel3.MaximumSize = new Size(20, 80);
            panel3.Name = "panel3";
            panel3.Size = new Size(20, 80);
            panel3.TabIndex = 13;
            // 
            // BotaoEscolhaVideo
            // 
            BotaoEscolhaVideo.BackColor = Color.SteelBlue;
            BotaoEscolhaVideo.Cursor = Cursors.Hand;
            BotaoEscolhaVideo.Dock = DockStyle.Left;
            BotaoEscolhaVideo.FlatAppearance.BorderSize = 0;
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
            panel1.Controls.Add(TempoVideoContinuarAssistindo);
            panel1.Controls.Add(NomeVideoContinuarAssistindo);
            panel1.Controls.Add(ProgressoVideoContinuarAssistindo);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(20, 424);
            panel1.Name = "panel1";
            panel1.Size = new Size(1015, 100);
            panel1.TabIndex = 9;
            // 
            // TempoVideoContinuarAssistindo
            // 
            TempoVideoContinuarAssistindo.AutoEllipsis = true;
            TempoVideoContinuarAssistindo.Dock = DockStyle.Bottom;
            TempoVideoContinuarAssistindo.Font = new Font("Times New Roman", 12F);
            TempoVideoContinuarAssistindo.Location = new Point(0, 39);
            TempoVideoContinuarAssistindo.MaximumSize = new Size(500, 80);
            TempoVideoContinuarAssistindo.Name = "TempoVideoContinuarAssistindo";
            TempoVideoContinuarAssistindo.Size = new Size(500, 27);
            TempoVideoContinuarAssistindo.TabIndex = 8;
            TempoVideoContinuarAssistindo.Text = "00:00 / 00:00";
            TempoVideoContinuarAssistindo.Visible = false;
            // 
            // NomeVideoContinuarAssistindo
            // 
            NomeVideoContinuarAssistindo.AutoEllipsis = true;
            NomeVideoContinuarAssistindo.Dock = DockStyle.Top;
            NomeVideoContinuarAssistindo.Font = new Font("Times New Roman", 12F);
            NomeVideoContinuarAssistindo.Location = new Point(0, 0);
            NomeVideoContinuarAssistindo.MaximumSize = new Size(700, 80);
            NomeVideoContinuarAssistindo.Name = "NomeVideoContinuarAssistindo";
            NomeVideoContinuarAssistindo.Size = new Size(700, 27);
            NomeVideoContinuarAssistindo.TabIndex = 7;
            NomeVideoContinuarAssistindo.Text = "Vídeo";
            NomeVideoContinuarAssistindo.Visible = false;
            // 
            // ProgressoVideoContinuarAssistindo
            // 
            ProgressoVideoContinuarAssistindo.BackColor = SystemColors.Control;
            ProgressoVideoContinuarAssistindo.Dock = DockStyle.Bottom;
            ProgressoVideoContinuarAssistindo.Location = new Point(0, 66);
            ProgressoVideoContinuarAssistindo.MaximumSize = new Size(650, 80);
            ProgressoVideoContinuarAssistindo.Name = "ProgressoVideoContinuarAssistindo";
            ProgressoVideoContinuarAssistindo.Size = new Size(650, 34);
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
            ClientSize = new Size(1055, 544);
            Controls.Add(PainelSemVideo);
            Controls.Add(Painel);
            Controls.Add(Video);
            ForeColor = SystemColors.Control;
            KeyPreview = true;
            MinimumSize = new Size(930, 450);
            Name = "Janela";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Block Player";
            TopMost = true;
            FormClosed += Player_FormClosed;
            DragDrop += Video_DragDrop;
            DragEnter += Video_DragEnter;
            ((System.ComponentModel.ISupportInitialize)Video).EndInit();
            Painel.ResumeLayout(false);
            PainelInterface.ResumeLayout(false);
            PainelInterface.PerformLayout();
            PainelVolume.ResumeLayout(false);
            PainelVolume.PerformLayout();
            PainelSemVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ContinuarAssistindo.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView Video;
        private TransparentPanel Painel;
        private System.Windows.Forms.Timer TimerVideo;
        private Label TempoAtualFinal;
        private Panel PainelSemVideo;
        private Button BotaoEscolhaVideo;
        private Button BotaoAjusteMinipayer;
        private Button BotaoContinuarAssistindo;
        private ProgressBar ProgressoVideoContinuarAssistindo;
        private Label NomeVideoContinuarAssistindo;
        private Label TempoVideoContinuarAssistindo;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Button ApagarContinuarAssistindo;
        private FlowLayoutPanel ContinuarAssistindo;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel PainelInterface;
        private DoubleBufferedPanel BarraVideo;
        private Panel PainelVolume;
        private DoubleBufferedPanel VolumeVideo;
        private Label VolumeTexto;
    }
}
