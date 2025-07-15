
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
                int progress = (int)((double)pos / len * BarraVideo.Maximum);
                BarraVideo.Value = Math.Min(progress, BarraVideo.Maximum);
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

        private void SalvarContinuarAssistindo()
        {
            // Salvando caminho e tempo do vídeo atual
            if (_mediaPlayer?.Media?.Mrl != null && _mediaPlayer.Length > 0)
            {
                string path = _mediaPlayer.Media.Mrl.Replace("file:///", "").Replace("/", "\\");
                long tempo = _mediaPlayer.Time;

                if (Properties.Settings.Default.VideoPaths == null)
                    Properties.Settings.Default.VideoPaths = new System.Collections.Specialized.StringCollection();
                if (Properties.Settings.Default.VideoTimes == null)
                    Properties.Settings.Default.VideoTimes = new System.Collections.Specialized.StringCollection();

                // Remover duplicatas
                int existingIndex = Properties.Settings.Default.VideoPaths.IndexOf(path);
                if (existingIndex >= 0)
                {
                    Properties.Settings.Default.VideoPaths.RemoveAt(existingIndex);
                    Properties.Settings.Default.VideoTimes.RemoveAt(existingIndex);
                }

                Properties.Settings.Default.VideoPaths.Add(path);
                Properties.Settings.Default.VideoTimes.Add(tempo.ToString());

                Properties.Settings.Default.Save();


                System.Windows.Forms.Timer delay = new System.Windows.Forms.Timer { Interval = 200 };
                delay.Tick += (s, ev) =>
                {
                    delay.Stop();
                    delay.Dispose();
                };
                delay.Start();
            }
        }
    }
}
