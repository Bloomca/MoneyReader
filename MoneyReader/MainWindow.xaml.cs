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
        private ConfiguratorVM _configuratorVM { get; }

        public MainWindow()
        {
            _configuratorVM = new ConfiguratorVM();

            InitializeComponent();

            var fileSelector = new FileSelector(OnFileRead);
            ContainerElement.Children.Add(fileSelector);
        }

        public void OnFileRead(object sender, CsvStatementReader reader)
        {
            var configurator = new Configurator(_configuratorVM);
            Grid.SetRow(configurator, 2);
            ContainerElement.Children.Add(configurator);

            var analyzerVM = new AnalyzerVM(_configuratorVM, reader);
            var analyzer = new Analyzer(analyzerVM);
            Grid.SetRow(analyzer, 4);

            ContainerElement.Children.Add(analyzer);
        }
    }
}