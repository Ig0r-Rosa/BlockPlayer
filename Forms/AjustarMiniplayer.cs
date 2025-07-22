using System;
using System.Drawing;
using System.Windows.Forms;
using BlockPlayer.Propriedades;
using System.Runtime.InteropServices;

namespace BlockPlayer
{
    public partial class AjustarMiniplayer : Form
    {
        private const int resizeMargin = 8;
        private bool resizing = false;
        private ResizeDirection resizeDir = ResizeDirection.None;
        private Point lastMousePos;

        public AjustarMiniplayer()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            this.Left = Settings.Default.MiniplayerX;
            this.Top = Settings.Default.MiniplayerY;
            this.Opacity = Settings.Default.MiniplayerOpacity;
            this.Width = Settings.Default.MiniplayerSizeX;
            this.Height = Settings.Default.MiniplayerSizeY;

            int opacidade = (int)(this.Opacity * 100);
            if (opacidade < 50 || opacidade > 100) opacidade = 50;
            BarraOpacidadeMiniplayer.Value = opacidade;
        }

        private void BotaoConfirmarAjustes_Click(object sender, EventArgs e)
        {
            Settings.Default.MiniplayerX = this.Left;
            Settings.Default.MiniplayerY = this.Top;
            Settings.Default.MiniplayerOpacity = this.Opacity;
            Settings.Default.MiniplayerSizeX = this.Width;
            Settings.Default.MiniplayerSizeY = this.Height;
            Settings.Default.Save();

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
                resizeDir = GetResizeDirection(e.Location);
                if (resizeDir != ResizeDirection.None)
                {
                    resizing = true;
                    lastMousePos = e.Location;
                }
                else
                {
                    // Mover janela se não estiver redimensionando
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        private void AjustarMiniplayer_MouseUp(object sender, MouseEventArgs e)
        {
            resizing = false;
        }

        private void AjustarMiniplayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!resizing)
            {
                var dir = GetResizeDirection(e.Location);
                SetCursor(dir);
            }
            else
            {
                ResizeForm(resizeDir, e);
            }

            this.Invalidate();
            this.Update();
        }

        private ResizeDirection GetResizeDirection(Point mouse)
        {
            bool left = mouse.X < resizeMargin;
            bool right = mouse.X > this.Width - resizeMargin;
            bool top = mouse.Y < resizeMargin;
            bool bottom = mouse.Y > this.Height - resizeMargin;

            if (top && left) return ResizeDirection.TopLeft;
            if (top && right) return ResizeDirection.TopRight;
            if (bottom && left) return ResizeDirection.BottomLeft;
            if (bottom && right) return ResizeDirection.BottomRight;
            if (left) return ResizeDirection.Left;
            if (right) return ResizeDirection.Right;
            if (top) return ResizeDirection.Top;
            if (bottom) return ResizeDirection.Bottom;

            return ResizeDirection.None;
        }

        private void SetCursor(ResizeDirection direction)
        {
            switch (direction)
            {
                case ResizeDirection.Top:
                case ResizeDirection.Bottom:
                    this.Cursor = Cursors.SizeNS;
                    break;
                case ResizeDirection.Left:
                case ResizeDirection.Right:
                    this.Cursor = Cursors.SizeWE;
                    break;
                case ResizeDirection.TopLeft:
                case ResizeDirection.BottomRight:
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case ResizeDirection.TopRight:
                case ResizeDirection.BottomLeft:
                    this.Cursor = Cursors.SizeNESW;
                    break;
                default:
                    this.Cursor = Cursors.Default;
                    break;
            }
        }

        private void ResizeForm(ResizeDirection direction, MouseEventArgs e)
        {
            Point screenPos = this.PointToScreen(e.Location);
            Rectangle bounds = this.Bounds;

            switch (direction)
            {
                case ResizeDirection.TopLeft:
                    bounds.Width += bounds.X - screenPos.X;
                    bounds.Height += bounds.Y - screenPos.Y;
                    bounds.X = screenPos.X;
                    bounds.Y = screenPos.Y;
                    break;
                case ResizeDirection.Top:
                    bounds.Height += bounds.Y - screenPos.Y;
                    bounds.Y = screenPos.Y;
                    break;
                case ResizeDirection.TopRight:
                    bounds.Width = screenPos.X - bounds.X;
                    bounds.Height += bounds.Y - screenPos.Y;
                    bounds.Y = screenPos.Y;
                    break;
                case ResizeDirection.Right:
                    bounds.Width = screenPos.X - bounds.X;
                    break;
                case ResizeDirection.BottomRight:
                    bounds.Width = screenPos.X - bounds.X;
                    bounds.Height = screenPos.Y - bounds.Y;
                    break;
                case ResizeDirection.Bottom:
                    bounds.Height = screenPos.Y - bounds.Y;
                    break;
                case ResizeDirection.BottomLeft:
                    bounds.Width += bounds.X - screenPos.X;
                    bounds.X = screenPos.X;
                    bounds.Height = screenPos.Y - bounds.Y;
                    break;
                case ResizeDirection.Left:
                    bounds.Width += bounds.X - screenPos.X;
                    bounds.X = screenPos.X;
                    break;
            }

            // limites mínimos
            if (bounds.Width < 100) bounds.Width = 100;
            if (bounds.Height < 100) bounds.Height = 100;

            this.Bounds = bounds;
        }

        // Para mover a janela sem bordas:
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private enum ResizeDirection
        {
            None,
            Left,
            Right,
            Top,
            Bottom,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
    }
}
