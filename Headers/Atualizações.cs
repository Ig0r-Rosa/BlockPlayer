
    namespace BlockPlayer
    {
        public partial class Janela : Form
        {
            private string FormatarTempo(long ms)
            {
                var totalSeconds = ms / 1000;
                var minutes = totalSeconds / 60;
                var seconds = totalSeconds % 60;
                return $"{minutes:D2}:{seconds:D2}";
            }

            public void AtualizarTempoVideo()
            {
                if (_mediaPlayer.Length > 0)
                {
                    long pos = _mediaPlayer.Time;
                    long len = _mediaPlayer.Length;
                    TempoAtualFinal.Text = $"{FormatarTempo(pos)} / {FormatarTempo(len)}";
                }
            }

            public void AtualizarVolume()
            {
                _mediaPlayer.Volume = VolumeVideo.Value;
                VolumeTexto.Text = VolumeVideo.Value.ToString();
            }

            private void AjustarTempo(int deltaMs)
            {
                if (_mediaPlayer != null && _mediaPlayer.Media != null)
                {
                    long novoTempo = Math.Max(0, _mediaPlayer.Time + deltaMs);
                    _mediaPlayer.Time = novoTempo;
                    AtualizarTempoVideo();
                }
            }

            private void HotKeysUnregister()
            {
                _hotkey?.Unregister();
                _hotkeyVoltar?.Unregister();
                _hotkeyAvancar?.Unregister();
            }
        }
    }
