using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy Stronaglowna.xaml
    /// </summary>
    public partial class Stronaglowna : Window
    {
        public Stronaglowna()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            new Danedziecka().Show();
            this.Close();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            new Nadzór().Show();
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
    }
}
