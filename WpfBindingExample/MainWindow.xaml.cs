using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfBindingExample
{
    public class TextFileObject
    {
        public string name { get; set; }
        public long totalChars { get; set; }
        public bool bearbeitet { get; set; }
        public string path { get; set; }
    }

    /// <summary>
    /// Let's produce some random objects...
    /// </summary>
    public static class FileFactory
    {
        private static string[] _names = { "autoexec.bat", "sys.log", "error.txt" };
        private static string[] _paths = { "c:/windows", "c:/program files", "c:/", "d:/downloads" };

        public static IEnumerable<TextFileObject> GenerateFiles(int count)
        {
            List<TextFileObject> result = new List<TextFileObject>();
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                TextFileObject f = new TextFileObject();
                f.name = _names[rnd.Next(_names.Length)];
                f.totalChars = rnd.Next(int.MaxValue);
                f.path = _paths[rnd.Next(_paths.Length)];
                f.bearbeitet = rnd.Next(2) == 1 ? true : false;
                result.Add(f);
            }
            return result;
        }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<TextFileObject> _source { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PopulateButton.Click += PopulateButton_Click;
            Grid.AutoGenerateColumns = true;
            // Initialize observable Datatype
            _source = new ObservableCollection<TextFileObject>();
            // Set Source
            Grid.ItemsSource = _source;
        }

        void PopulateButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in FileFactory.GenerateFiles(20))
            {
                // Add items to collection
                _source.Add(item);
            }
        }
    }
}
