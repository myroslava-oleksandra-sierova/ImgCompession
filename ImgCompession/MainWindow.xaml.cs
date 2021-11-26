using Microsoft.Win32;
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


namespace ImgCompession
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageProcessor ip;
        string input_image_name;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow p = new SettingsWindow();
            p.Show();
        }
        private void Charts_Button_Click(object sender, RoutedEventArgs e)
        {
            ChartsWindow p = new ChartsWindow();
            p.Show();
        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                input_img.Source = new BitmapImage(new Uri(op.FileName));
                input_image_name = op.FileName;
            }
        }
        private void compress_click(object sender, RoutedEventArgs e)
        {
            this.ip = new ImageProcessor();
            var out_i=ip.process(new BitmapImage(new Uri(input_image_name)), 10, "123");
            output_img.Source = out_i;

        }
    }
}
