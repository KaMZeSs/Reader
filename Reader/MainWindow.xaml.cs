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

        private async void LoadBook(String file)
        {
            try
            {
                XmlDocument fullFile = new();
                fullFile.Load(file);

                encoding = "UTF-8";

                var fullBook = fullFile.LastChild;

                description = fullBook.FirstChild;
                body = description.NextSibling;
                data = body.NextSibling;

                await Task.Run(() => LoadFirstPage());
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Ошибка при открытии файла\n{exc.Message}");
            }
        }

        private void LoadFirstPage()
        {
            var enumerator = body.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current as XmlNode;
                if (item.Name is "section")
                {
                    var enum_of_page = item.GetEnumerator();

                    String last_element = String.Empty; //Нужен для запоминания предыдущего элемента (если неск подряд <p>, то можно в один впихнуть)
                    StringBuilder sb = new StringBuilder();
                    while (enum_of_page.MoveNext())
                    {
                        var page_item = enum_of_page.Current as XmlNode;

                        //if (page_item.Name is "p")
                        //{
                        //    if (last_element is "p")
                        //    {
                        //        ConcatParagraphToLast(page_item.InnerText);
                        //    }
                        //    else
                        //    {
                        //        ViewNewParagraph(page_item.InnerText);
                        //    }
                        //}


                        //Вариант со StringBuilder лучше, но пролагивает
                        //Для удаления пролага можно попробовать выводить текст частями, если он большой
                        //Если, например, текст длиннее 500 символов, то разбить его по 100 и выводить в цикле, параллельно делая
                        //await Task.Run(() => Thread.Sleep(1))
                        if (page_item.Name is "p") 
                        {
                            sb.AppendLine(page_item.InnerText);
                        }
                        else
                        {
                            ViewNewParagraph(sb.ToString());
                            sb.Clear();
                        }

                        last_element = page_item.Name;
                    }
                    ViewNewParagraph(sb.ToString());
                }
            }
        }

        private void ViewNewParagraph(String text)
        {
            if ((text is null) || (text.Length is 0))
                return;
            bodyStackViewer.Dispatcher.Invoke(() =>
            {
                TextBlock textBlock = new()
                {
                    FontSize = 15,
                    Text = text,
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Margin = new Thickness(5)
                };
                bodyStackViewer.Children.Add(textBlock);
            });
            System.Threading.Thread.Sleep(1); //Без него грузит быстрее, но зависает намертво
            //Нужно делать разделение страниц, или, если параграфы продолжаются - оставлять тот же
            //bodyStackViewer.Children.Add(textBlock);
        }

        private void ConcatParagraphToLast(String text)
        {
            if (text is null)
                return;
            bodyStackViewer.Dispatcher.Invoke(() =>
            {
                var last_par = bodyStackViewer.Children[bodyStackViewer.Children.Count - 1] as TextBlock;
                last_par.Text += Environment.NewLine + text;
            });
            System.Threading.Thread.Sleep(1);
        }
    }
}
