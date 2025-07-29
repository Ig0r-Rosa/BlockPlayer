
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
                    if (_videoFinalizado)
                    {
                        // Seleciona o painel do video atual
                        // Procura o painel do vídeo atual na lista "ContinuarAssistindo"
                        foreach (Control ctrl in ContinuarAssistindo.Controls)
                        {
                            if (ctrl is Panel painel && painel.Tag is VideoInfo info && info.Caminho == _mediaPlayer?.Media?.Mrl)
                            {
                                painelSelecionado = painel;
                                break;
                            }
                        }

                        if(painelSelecionado != null)
                        {
                            ApagarContinuarAssistindoSelecionado();
                        }
                    }
                    else
                    {
                        SalvarContinuarAssistindo();
                    }
                    NomeVideoContinuarAssistindo.Visible = false;
                    ProgressoVideoContinuarAssistindo.Visible = false;
                    TempoVideoContinuarAssistindo.Visible = false;
                    _videoFinalizado = false;
                    _mediaPlayer.Stop();
                    _mediaPlayer.Media = null;
                    AtualizarVisibilidadeVideo(false);
                    return true;

                case Keys.Space:
                    Pause();
                    return true;

                case Keys.Right:
                    if(_mediaPlayer.Time + 5000 < _mediaPlayer.Length)
                    {
                        _mediaPlayer.Time += 5000;
                    }
                    else
                    {
                        _mediaPlayer.Time = _mediaPlayer.Length;
                    }
                    AtualizarTempoVideo();
                    BarraVideo.Invalidate();
                    return true;

                case Keys.Left:
                    _mediaPlayer.Time = Math.Max(0, _mediaPlayer.Time - 5000);
                    AtualizarTempoVideo();
                    BarraVideo.Invalidate();
                    return true;

                case Keys.Up:
                    VolumeAtual = Math.Min(VolumeAtual + 5, VolumeMaximo);
                    AtualizarVolume();
                    VolumeVideo.Invalidate();
                    return true;

                case Keys.Down:
                    VolumeAtual = Math.Max(VolumeAtual - 5, 0);
                    AtualizarVolume();
                    VolumeVideo.Invalidate();
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
