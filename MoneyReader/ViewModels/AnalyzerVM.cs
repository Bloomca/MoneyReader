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
    public readonly struct DataCategory
    {
        public Category Category { get; init; }
        public List<Statement> Statements { get; init; }
    }

    public class AnalyzerVM : BaseVM
    {
        private CsvStatementReader _csvStatementReader;
        private ConfiguratorVM _configuratorVM;

        private List<DataCategory> _data = [];
        public List<DataCategory> Data { get => _data; }

        public AnalyzerVM(ConfiguratorVM configuratorVM, CsvStatementReader csvStatementReader)
        {
            _csvStatementReader = csvStatementReader;
            _configuratorVM = configuratorVM;
            configuratorVM.IgnoredPrefixes.CollectionChanged += OnCategoriesChanged;
            configuratorVM.Categories.CollectionChanged += OnCategoriesChanged;

            CalculateData();
        }

        private void OnCategoriesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalculateData();
        }

        private void CalculateData()
        {
            Dictionary<Category, List<Statement>> newData = [];
            List<Statement> uncategorizedStatements = [];

            foreach (var category in _configuratorVM.Categories)
            {
                if (category == null) continue;

                newData.Add(category, new List<Statement>());
            }

            foreach (var statement in _csvStatementReader.Statements)
            {
                if (statement == null) continue;

                string nameWithoutPrefixes = IgnorePrefixes(statement.Name);

                var matchingCategory = FindMatchingCategory(nameWithoutPrefixes);

                if (matchingCategory != null)
                {
                    newData[matchingCategory].Add(statement);
                } else
                {
                    uncategorizedStatements.Add(statement);
                }
            }

            List<DataCategory> newFormattedData = [];

            foreach (var item in newData)
            {
                newFormattedData.Add(new DataCategory() { Category = item.Key, Statements = item.Value });
            }

            newFormattedData.Add(new DataCategory()
            {
                Category = new Category("No category", CategoryMatchingType.StartsWith),
                Statements = uncategorizedStatements
            });

            _data = newFormattedData;
        }

        private string IgnorePrefixes(string statementName)
        {
            var ignoredPrefixes = _configuratorVM.IgnoredPrefixes;

            if (ignoredPrefixes == null || ignoredPrefixes.Count == 0) return statementName;

            string result = statementName.ToLower();
            foreach (var prefix in ignoredPrefixes)
            {
                string formattedPrefix = prefix.ToLower();
                if (statementName.StartsWith(formattedPrefix))
                {
                    result = statementName.Replace(formattedPrefix, "");
                }
            }

            return result;
        }

        private Category? FindMatchingCategory(string statementName)
        {
            var categories = _configuratorVM.Categories;

            if (categories == null || categories.Count == 0) { return null; }

            foreach (var category in categories)
            {
                string formattedCategoryName = category.Name.ToLower();

                if (category.Type == CategoryMatchingType.StartsWith)
                {
                    if (statementName.ToLower().StartsWith(formattedCategoryName))
                    {
                        return category;
                    }
                    else continue;
                }
                else if (category.Type == CategoryMatchingType.Contains)
                {
                    if (statementName.ToLower().Contains(formattedCategoryName))
                    {
                        return category;
                    }
                    else continue;
                }
            }

            return null;
        }

    }
}
