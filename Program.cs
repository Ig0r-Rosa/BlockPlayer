using System.Threading;
using System.Windows.Forms;
using System.IO.Pipes;
using System.Text;

namespace BlockPlayer
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "BlockPlayerSingletonMutex");

        [STAThread]
        static void Main(string[] args)
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                // J� existe inst�ncia rodando, envie o caminho do v�deo para ela e saia
                if (args.Length > 0)
                {
                    using (var client = new NamedPipeClientStream("BlockPlayerPipe"))
                    {
                        try
                        {
                            client.Connect(500);
                            var data = Encoding.UTF8.GetBytes(args[0]);
                            client.Write(data, 0, data.Length);
                        }
                        catch
                        {
                            // Caso n�o conecte, ignore e feche normalmente
                        }
                    }
                }
                return; // Sai da nova inst�ncia
            }

            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Janela janela = new Janela();

            // Starta um servidor de NamedPipe para receber novos v�deos
            var serverThread = new Thread(() => ServerPipeLoop(janela));
            serverThread.IsBackground = true;
            serverThread.Start();

            if (args.Length > 0)
            {
                janela.AbrirVideo(args[0]);
            }

            Application.Run(janela);

            mutex.ReleaseMutex();
        }

        private static void ServerPipeLoop(Janela janela)
        {
            while (true)
            {
                try
                {
                    using (var server = new NamedPipeServerStream("BlockPlayerPipe"))
                    {
                        server.WaitForConnection();

                        byte[] buffer = new byte[1024];
                        int bytesRead = server.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string videoPath = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            // Invoca o m�todo AbrirVideo na thread do formul�rio
                            janela?.Invoke(new Action(() =>
                            {
                                janela.AbrirVideo(videoPath);
                                janela.BringToFront();
                                janela.Activate();
                            }));
                        }
                    }
                }
                catch
                {
                    // Pode ignorar erros e continuar o loop
                }
            }
        }
    }
}
