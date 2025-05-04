using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MoneyReader.Classes;
using MoneyReader.ViewModels;
using MoneyReader.Views;

namespace MoneyReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CsvStatementReader CsvStatementReader { get; }
        public ConfiguratorVM ConfiguratorVM { get; }

        public MainWindow()
        {
            CsvStatementReader = new CsvStatementReader();
            ConfiguratorVM = new ConfiguratorVM();

            InitializeComponent();

            var fileSelector = new FileSelector(OnFileRead);
            ContainerElement.Children.Add(fileSelector);
        }

        public void OnFileRead(object sender, CsvStatementReader reader)
        {
            Trace.WriteLine("HELLO");

            var configurator = new Configurator(ConfiguratorVM);
            ContainerElement.Children.Add(configurator);
        }
    }
}