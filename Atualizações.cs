
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
            if (_mediaPlayer?.Media?.Mrl != null && _mediaPlayer.Length > 0)
            {
                string path = Uri.UnescapeDataString(_mediaPlayer.Media.Mrl.Replace("file:///", "").Replace("/", "\\"));
                long tempo = _mediaPlayer.Time;
                long duracao = _mediaPlayer.Length;
                DateTime agora = DateTime.UtcNow;

                var paths = Properties.Settings.Default.VideoPaths ?? new System.Collections.Specialized.StringCollection();
                var tempos = Properties.Settings.Default.VideoTimes ?? new System.Collections.Specialized.StringCollection();
                var datas = Properties.Settings.Default.VideoDatas ?? new System.Collections.Specialized.StringCollection();
                var duracoes = Properties.Settings.Default.VideoDuracao ?? new System.Collections.Specialized.StringCollection();
                var thumbs = Properties.Settings.Default.VideoThumbs ?? new System.Collections.Specialized.StringCollection();

                // Remove se já existe para regravar
                int existingIndex = paths.IndexOf(path);
                if (existingIndex >= 0)
                {
                    paths.RemoveAt(existingIndex);
                    tempos.RemoveAt(existingIndex);
                    datas.RemoveAt(existingIndex);
                    duracoes.RemoveAt(existingIndex);
                    thumbs.RemoveAt(existingIndex);
                }

                // Salvar miniatura como .jpg
                string nomeArquivoMiniatura = Path.GetFileNameWithoutExtension(path) + ".jpg";
                string caminhoMiniatura = Path.Combine(pastaMiniaturas, nomeArquivoMiniatura);

                // Tira snapshot atual
                bool snapshotSucesso = _mediaPlayer.TakeSnapshot(0, caminhoMiniatura, 320, 180);

                // Só salva se o snapshot funcionou
                if (snapshotSucesso)
                {
                    paths.Add(path);
                    tempos.Add(tempo.ToString());
                    datas.Add(agora.ToString("o"));
                    duracoes.Add(duracao.ToString());
                    thumbs.Add(caminhoMiniatura);
                }

                // Salva atualizações
                Properties.Settings.Default.VideoPaths = paths;
                Properties.Settings.Default.VideoTimes = tempos;
                Properties.Settings.Default.VideoDatas = datas;
                Properties.Settings.Default.VideoDuracao = duracoes;
                Properties.Settings.Default.VideoThumbs = thumbs;

                Properties.Settings.Default.Save();
            }
        }
    }
}
