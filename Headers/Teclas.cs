
using BlockPlayer.Classes;

namespace BlockPlayer
{
    public partial class Janela : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    NomeVideoContinuarAssistindo.Visible = false;
                    ProgressoVideoContinuarAssistindo.Visible = false;
                    TempoVideoContinuarAssistindo.Visible = false;
                    SalvarContinuarAssistindo();
                    _mediaPlayer.Stop();
                    _mediaPlayer.Media = null;
                    AtualizarVisibilidadeVideo(false);
                    return true;

                case Keys.Space:
                    Pause();
                    return true;

                case Keys.Right:
                    _mediaPlayer.Time += 5000;
                    AtualizarTempoVideo();
                    return true;

                case Keys.Left:
                    _mediaPlayer.Time = Math.Max(0, _mediaPlayer.Time - 5000);
                    AtualizarTempoVideo();
                    return true;

                case Keys.Up:
                    VolumeVideo.Value = Math.Min(VolumeVideo.Value + 5, VolumeVideo.Maximum);
                    AtualizarVolume();
                    return true;

                case Keys.Down:
                    VolumeVideo.Value = Math.Max(VolumeVideo.Value - 5, VolumeVideo.Minimum);
                    AtualizarVolume();
                    return true;

                case Keys.Enter:
                    AlternarMaximizado();
                    return true;

                case Keys.F11:
                    AlternarFullscreen();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ConfigHotKeys()
        {
            _hotkey = new GlobalHotkey(this, 1, Keys.M);
            _hotkey.OnHotkeyPressed += AlternarMiniplayer;

            _hotkeyVoltar = new GlobalHotkey(this, 2, Keys.Oemcomma);
            _hotkeyVoltar.OnHotkeyPressed += () =>
            {
                if (_miniplayer != null && _miniplayer.Visible)
                {
                    AjustarTempo(-10000);
                }
            };

            _hotkeyAvancar = new GlobalHotkey(this, 3, Keys.OemPeriod);
            _hotkeyAvancar.OnHotkeyPressed += () =>
            {
                if (_miniplayer != null && _miniplayer.Visible)
                {
                    AjustarTempo(10000);
                }
            };
        }
    }
}
