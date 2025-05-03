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
using MoneyReader.Classes;

namespace MoneyReader.Views
{
    /// <summary>
    /// Interaction logic for Configurator.xaml
    /// </summary>
    public partial class Configurator : UserControl
    {
        public ConfiguratorVM VM { get; }
        public Configurator()
        {
            InitializeComponent();
            this.DataContext = this;

            VM = new ConfiguratorVM();
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

        private void OnCategoryAdd(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryTextBox.Text;

            if (String.IsNullOrEmpty(categoryName))
            {
                // Simply ignore the button click for now
                return;
            }

            CategoryMatchingType type = RadioContains.IsChecked == true ? CategoryMatchingType.Contains : CategoryMatchingType.StartsWith;

            VM.AddCategory(new Category(categoryName, type));
        }

        private void OnRemoveCategoryClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: string categoryToRemove })
            {
                VM.RemoveCategory(categoryToRemove);
            }
        }
    }
}
