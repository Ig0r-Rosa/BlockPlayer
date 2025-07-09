using LibVLCSharp.WinForms;
using System.Windows.Forms;

namespace BlockPlayer
{
    public class VideoPlayer : VideoView
    {
        public VideoPlayer()
        {
            // Permite o controle receber foco e eventos de mouse
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus(); // Garante que o controle ganhe foco (útil para interações futuras)
        }
    }
}
