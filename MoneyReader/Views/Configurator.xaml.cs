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
using MoneyReader.ViewModels;

namespace MoneyReader.Views
{
    /// <summary>
    /// Interaction logic for Configurator.xaml
    /// </summary>
    public partial class Configurator : UserControl
    {
        public ConfiguratorVM VM { get; } = new ConfiguratorVM();
        public Configurator()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OnAddPrefixClick(object sender, RoutedEventArgs e)
        {
            string prefix = IgnoreTextBox.Text;

            if (String.IsNullOrEmpty(prefix))
            {
                // Simply ignore the button click for now
                return;
            }

            VM.AddIgnoredPrefix(prefix);
            IgnoreTextBox.Clear();
        }

        private void OnRemovePrefixClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: string prefixToRemove })
            {
                VM.RemoveIgnoredPrefix(prefixToRemove);
            }
        }
    }
}
