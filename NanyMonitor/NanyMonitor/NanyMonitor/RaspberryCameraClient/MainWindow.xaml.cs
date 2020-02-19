using System;
using System.IO.Pipes;
using System.Threading;
using System.Windows;
using Eneter.Messaging.MessagingSystems.MessagingSystemBase;
using Eneter.Messaging.MessagingSystems.TcpMessagingSystem;
using Eneter.Messaging.MessagingSystems.UdpMessagingSystem;
using VLC;


namespace RaspberryCameraClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDuplexOutputChannel myVideoChannel;

        // VLC will read video from the named pipe.
        private NamedPipeServerStream myVideoPipe;

        private VlcInstance myVlcInstance;
        private VlcMediaPlayer myPlayer;

        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Forms.Panel aVideoPanel = new System.Windows.Forms.Panel();
            aVideoPanel.BackColor = System.Drawing.Color.Black;
            VideoWindow.Child = aVideoPanel;

            // If not installed in Provide path to your VLC.
            myVlcInstance = new VlcInstance(@"c:\Program Files\VideoLAN\VLC\");

            // Use TCP messaging.
            // You can try to use UDP or WebSockets too.
            myVideoChannel = new TcpMessagingSystemFactory()
            //myVideoChannel = new UdpMessagingSystemFactory()
                // Note: Provide address of your service here.
                .CreateDuplexOutputChannel("tcp://172.20.10.9:8093/");
            myVideoChannel.ResponseMessageReceived += OnResponseMessageReceived;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StopCapturing();
        }

        private void OnStartCapturingButtonClick(object sender, RoutedEventArgs e)
        {
            StartCapturing();
        }

        private void OnStopCapturingButtonClick(object sender, RoutedEventArgs e)
        {
            StopCapturing();
        }

        private void StartCapturing()
        {
            // Use unique name for the pipe.
            string aVideoPipeName = Guid.NewGuid().ToString();

            // Open pipe that will be read by VLC.
            myVideoPipe = new NamedPipeServerStream(@"\" + aVideoPipeName, PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 32764);
            ManualResetEvent aVlcConnectedPipe = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(x =>
            {
                myVideoPipe.WaitForConnection();

                // Indicate VLC has connected the pipe.
                aVlcConnectedPipe.Set();
            });

            // VLC connects the pipe and starts playing.
            using (VlcMedia aMedia = new VlcMedia(myVlcInstance, @"stream://\\\.\pipe\" + aVideoPipeName))
            {
                // Setup VLC so that it can process raw h264 data (i.e. not in mp4 container)
                aMedia.AddOption(":demux=H264");

                myPlayer = new VlcMediaPlayer(aMedia);
                myPlayer.Drawable = VideoWindow.Child.Handle;

                // Note: This will connect the pipe and read the video.
                myPlayer.Play();
            }

            // Wait until VLC connects the pipe so that it is ready to receive the stream.
            if (!aVlcConnectedPipe.WaitOne(5000))
            {
                throw new TimeoutException("VLC did not open connection with the pipe.");
            }

            // Open connection with service running on Raspberry.
            myVideoChannel.OpenConnection();
        }

        private void StopCapturing()
        {
            // Close connection with the service on Raspberry.
            myVideoChannel.CloseConnection();

            // Close the video pipe.
            if (myVideoPipe != null)
            {
                myVideoPipe.Close();
                myVideoPipe = null;
            }

            // Stop VLC.
            if (myPlayer != null)
            {
                myPlayer.Dispose();
                myPlayer = null;
            }
        }

        private void OnResponseMessageReceived(object sender, DuplexChannelMessageEventArgs e)
        {
            byte[] aVideoData = (byte[])e.Message;

            // Forward received data to the named pipe so that VLC can process it.
            myVideoPipe.Write(aVideoData, 0, aVideoData.Length);
        }
    }
}
