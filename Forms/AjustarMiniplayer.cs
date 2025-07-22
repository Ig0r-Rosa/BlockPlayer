using BlockPlayer.Propriedades;

namespace BlockPlayer
{
    public partial class AjustarMiniplayer : Form
    {
        private bool isDragging = false;
        private Point dragStartPoint;

        public AjustarMiniplayer()
        {
            InitializeComponent();
            this.Left = Propriedades.Settings.Default.MiniplayerX;
            this.Top = Propriedades.Settings.Default.MiniplayerY;
            this.Opacity = Propriedades.Settings.Default.MiniplayerOpacity;
            int opacidade = (int)(Propriedades.Settings.Default.MiniplayerOpacity * 100);
            if (opacidade < 50 || opacidade > 100)
            {
                opacidade = 50;
            }
            BarraOpacidadeMiniplayer.Value = opacidade;
            this.Width = Propriedades.Settings.Default.MiniplayerSizeX;
            this.Height = Propriedades.Settings.Default.MiniplayerSizeY;
        }

        private void BotaoConfirmarAjustes_Click(object sender, EventArgs e)
        {
            Propriedades.Settings.Default.MiniplayerX = this.Left;
            Propriedades.Settings.Default.MiniplayerY = this.Top;
            Propriedades.Settings.Default.MiniplayerOpacity = (double)BarraOpacidadeMiniplayer.Value / 100;
            Propriedades.Settings.Default.MiniplayerSizeX = this.Width;
            Propriedades.Settings.Default.MiniplayerSizeY = this.Height;
            Propriedades.Settings.Default.Save();

            this.Close();
        }

        private void BarraOpacidadeMiniplayer_Scroll(object sender, EventArgs e)
        {
            this.Opacity = (double)BarraOpacidadeMiniplayer.Value / 100;
        }

        private void AjustarMiniplayer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void AjustarMiniplayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - dragStartPoint.X;
                this.Top += e.Y - dragStartPoint.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
        }
    }
}
