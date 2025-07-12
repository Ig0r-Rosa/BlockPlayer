
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
    }
}
