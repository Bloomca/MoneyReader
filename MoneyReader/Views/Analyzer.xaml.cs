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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MoneyReader.ViewModels;

namespace MoneyReader.Views
{
    /// <summary>
    /// Interaction logic for Analyzer.xaml
    /// </summary>
    public partial class Analyzer : UserControl
    {
        public AnalyzerVM VM { get; }

        public Analyzer(AnalyzerVM analyzerVM)
        {
            InitializeComponent();
            this.DataContext = this;
            VM = analyzerVM;

            Trace.WriteLine(VM.Data.Count);
        }
    }
}
