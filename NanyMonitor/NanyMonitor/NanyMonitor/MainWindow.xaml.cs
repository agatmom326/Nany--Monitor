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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NanyMonitor
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

   

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

            //jesli konto już istnieje new Stronaglowna().Show();
           

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btnlog_Click(object sender, RoutedEventArgs e)
        {
            new Danedziecka().Show();
            this.Close();
        }
    }
}
