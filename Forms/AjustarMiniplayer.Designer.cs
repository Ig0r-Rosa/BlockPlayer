namespace BlockPlayer
{
    partial class AjustarMiniplayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BarraOpacidadeMiniplayer = new TrackBar();
            BotaoConfirmarAjustes = new Button();
            proporcao16p9 = new Button();
            proporcao9p16 = new Button();
            ((System.ComponentModel.ISupportInitialize)BarraOpacidadeMiniplayer).BeginInit();
            SuspendLayout();
            // 
            // BarraOpacidadeMiniplayer
            // 
            BarraOpacidadeMiniplayer.Dock = DockStyle.Top;
            BarraOpacidadeMiniplayer.Location = new Point(0, 0);
            BarraOpacidadeMiniplayer.Maximum = 100;
            BarraOpacidadeMiniplayer.Minimum = 50;
            BarraOpacidadeMiniplayer.Name = "BarraOpacidadeMiniplayer";
            BarraOpacidadeMiniplayer.Size = new Size(778, 69);
            BarraOpacidadeMiniplayer.TabIndex = 0;
            BarraOpacidadeMiniplayer.Value = 50;
            BarraOpacidadeMiniplayer.Scroll += BarraOpacidadeMiniplayer_Scroll;
            // 
            // BotaoConfirmarAjustes
            // 
            BotaoConfirmarAjustes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BotaoConfirmarAjustes.Font = new Font("Times New Roman", 10F);
            BotaoConfirmarAjustes.Location = new Point(613, 319);
            BotaoConfirmarAjustes.Name = "BotaoConfirmarAjustes";
            BotaoConfirmarAjustes.Size = new Size(153, 113);
            BotaoConfirmarAjustes.TabIndex = 1;
            BotaoConfirmarAjustes.Text = "Confirmar ajustes";
            BotaoConfirmarAjustes.UseVisualStyleBackColor = true;
            BotaoConfirmarAjustes.Click += BotaoConfirmarAjustes_Click;
            // 
            // proporcao16p9
            // 
            proporcao16p9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            proporcao16p9.Font = new Font("Times New Roman", 10F);
            proporcao16p9.Location = new Point(12, 319);
            proporcao16p9.Name = "proporcao16p9";
            proporcao16p9.Size = new Size(112, 113);
            proporcao16p9.TabIndex = 2;
            proporcao16p9.Text = "16:9";
            proporcao16p9.UseVisualStyleBackColor = true;
            proporcao16p9.Click += proporcao16p9_Click;
            // 
            // proporcao9p16
            // 
            proporcao9p16.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            proporcao9p16.Font = new Font("Times New Roman", 10F);
            proporcao9p16.Location = new Point(142, 319);
            proporcao9p16.Name = "proporcao9p16";
            proporcao9p16.Size = new Size(112, 113);
            proporcao9p16.TabIndex = 3;
            proporcao9p16.Text = "9:16";
            proporcao9p16.UseVisualStyleBackColor = true;
            proporcao9p16.Click += proporcao9p16_Click;
            // 
            // AjustarMiniplayer
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(778, 444);
            Controls.Add(proporcao9p16);
            Controls.Add(proporcao16p9);
            Controls.Add(BotaoConfirmarAjustes);
            Controls.Add(BarraOpacidadeMiniplayer);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(450, 300);
            Name = "AjustarMiniplayer";
            StartPosition = FormStartPosition.Manual;
            Text = "Ajustar Miniplayer";
            TopMost = true;
            MouseDown += AjustarMiniplayer_MouseDown;
            MouseMove += AjustarMiniplayer_MouseMove;
            MouseUp += AjustarMiniplayer_MouseUp;
            ((System.ComponentModel.ISupportInitialize)BarraOpacidadeMiniplayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar BarraOpacidadeMiniplayer;
        private Button BotaoConfirmarAjustes;
        private Button proporcao16p9;
        private Button proporcao9p16;
    }
}