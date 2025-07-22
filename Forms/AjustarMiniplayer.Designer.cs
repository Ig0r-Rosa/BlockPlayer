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
            BotaoConfirmarAjustes.Location = new Point(538, 319);
            BotaoConfirmarAjustes.Name = "BotaoConfirmarAjustes";
            BotaoConfirmarAjustes.Size = new Size(228, 113);
            BotaoConfirmarAjustes.TabIndex = 1;
            BotaoConfirmarAjustes.Text = "Confirmar ajustes";
            BotaoConfirmarAjustes.UseVisualStyleBackColor = true;
            BotaoConfirmarAjustes.Click += BotaoConfirmarAjustes_Click;
            // 
            // AjustarMiniplayer
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(778, 444);
            Controls.Add(BotaoConfirmarAjustes);
            Controls.Add(BarraOpacidadeMiniplayer);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(400, 250);
            Name = "AjustarMiniplayer";
            StartPosition = FormStartPosition.Manual;
            Text = "Ajustar Miniplayer";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)BarraOpacidadeMiniplayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TrackBar BarraOpacidadeMiniplayer;
        private Button BotaoConfirmarAjustes;
    }
}