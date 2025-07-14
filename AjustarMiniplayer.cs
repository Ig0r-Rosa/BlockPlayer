using BlockPlayer.Properties;

namespace BlockPlayer
{
    public partial class AjustarMiniplayer : Form
    {
        private TrackBar trackBarOpacidade;
        private Button btnSalvar;
        private Label lblPorcentagem;

        public AjustarMiniplayer()
        {
            InitializeComponent();
            this.Left = Properties.Settings.Default.MiniplayerX;
            this.Top = Properties.Settings.Default.MiniplayerY;
            this.Opacity = Properties.Settings.Default.MiniplayerOpacity;
            int opacidade = (int)(Properties.Settings.Default.MiniplayerOpacity * 100);
            if (opacidade < 50 || opacidade > 100)
            {
                opacidade = 50;
            }
            BarraOpacidadeMiniplayer.Value = opacidade;
            this.Width = Properties.Settings.Default.MiniplayerSizeX;
            this.Height = Properties.Settings.Default.MiniplayerSizeY;
        }

        private void BotaoConfirmarAjustes_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.MiniplayerX = this.Left;
            Properties.Settings.Default.MiniplayerY = this.Top;
            Properties.Settings.Default.MiniplayerOpacity = (double)BarraOpacidadeMiniplayer.Value / 100;
            Properties.Settings.Default.MiniplayerSizeX = this.Width;
            Properties.Settings.Default.MiniplayerSizeY = this.Height;
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void BarraOpacidadeMiniplayer_Scroll(object sender, EventArgs e)
        {
            this.Opacity = (double)BarraOpacidadeMiniplayer.Value / 100;
        }
    }
}
