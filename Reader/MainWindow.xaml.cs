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
using System.Xml;

namespace Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlNode body;
        XmlNode description;
        XmlNode data;

        String encoding;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpenBook_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new()
            {
                Filter = "fb2 files (*.fb2)|*.fb2"
            };
            if (openFile.ShowDialog() is false)
            {
                return;
            }

            LoadBook(openFile.FileName);
        }

        private void LoadBook(String file)
        {
            try
            {
                XmlDocument fullFile = new();
                fullFile.Load(file);
                
                encoding = fullFile.FirstChild.Attributes["encoding"].Value ?? "UTF-8";
                
                var fullBook = fullFile.LastChild;
                
                description = fullBook.FirstChild;
                body = description.NextSibling;
                data = body.NextSibling;
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Ошибка при открытии файла\n{exc.Message}");
            }
        }
    }
}
