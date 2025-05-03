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
using MoneyReader.Classes;
using MoneyReader.ViewModels;

namespace MoneyReader.Views
{

    public delegate void FileReadEventHandler(object sender, CsvStatementReader reader);

    /// <summary>
    /// Interaction logic for FileSelector.xaml
    /// </summary>
    public partial class FileSelector : UserControl
    {
        public event FileReadEventHandler? FileRead;
        public FileSelectorVM VM { get; }
        public FileSelector()
        {
            InitializeComponent();
            VM = new FileSelectorVM();
            this.DataContext = this;
        }

        private async void File_Select_Click(object sender, RoutedEventArgs e)
        {
            if (VM.HasError)
            {
                VM.HasError = false;
            }

            var csvReader = new CsvStatementReader();
            var result = await csvReader.AskForFile();

            if (!result)
            {
                VM.HasError = true;
            } else
            {
                FileRead?.Invoke(this, csvReader);
            }
        }
    }
}
