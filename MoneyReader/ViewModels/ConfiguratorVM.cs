using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyReader.Utils;
using MoneyReader.Classes;

namespace MoneyReader.ViewModels
{
    public class ConfiguratorVM : BaseVM
    {

        private ObservableCollection<string> _ignoredPrefixes = [];
        public ObservableCollection<string> IgnoredPrefixes { get =>  _ignoredPrefixes; }

        // TODO: switch to listener on _ignoredPrefixes instead of direct calls
        public bool HasIgnoredPrefixes {  get => _ignoredPrefixes.Count != 0; }

        public void AddIgnoredPrefix(string prefix)
        {
            bool hadNoPrefixes = _ignoredPrefixes.Count == 0;
            _ignoredPrefixes.Add(prefix);

            if (hadNoPrefixes)
            {
                OnPropertyChanged(nameof(HasIgnoredPrefixes));
            }
        }

        public void RemoveIgnoredPrefix(string prefix)
        {
            _ignoredPrefixes.Remove(prefix);

            bool hasNoPrefixes = _ignoredPrefixes.Count == 0;
            if (hasNoPrefixes)
            {
                OnPropertyChanged(nameof(HasIgnoredPrefixes));
            }
        }

        private ObservableCollection<Category> _categories = [];

        public ObservableCollection<Category> Categories { get => _categories; }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void RemoveCategory(string categoryName)
        {
            var category = _categories.First(c => c.Name == categoryName);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}
