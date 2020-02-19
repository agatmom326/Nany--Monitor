using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace NanyMonitor
{
    /// <summary>
    /// Logika interakcji dla klasy Nadzór.xaml
    /// </summary>
    public partial class Nadzór : Window
    {
        public Nadzór()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            new Stronaglowna().Show();
            this.Close();

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {

            new Danedziecka().Show();
            this.Close();
        }

     

        private void btn4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Users\Monika\Desktop\NanyMonitor\NanyMonitor\NanyMonitor\RaspberryCameraClient\bin\Debug\RaspberryCameraClient.exe");
        }
    }
}
