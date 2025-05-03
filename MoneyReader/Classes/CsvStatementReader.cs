using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Win32;

namespace MoneyReader.Classes
{
    public class CsvStatementReader
    {
        private List<Statement> _statements = [];
        public async Task<bool> AskForFile()
        {
            var fileDialog = new OpenFileDialog() { Filter = "CSV documents (.csv)|*.csv" };

            bool? result = fileDialog.ShowDialog();

            if (result != true)
            {
                return false;
            }

            string filename = fileDialog.FileName;
            string fileContents = await System.IO.File.ReadAllTextAsync(filename);

            return ParseText(fileContents);
        }
        private bool ParseText(string fileText)
        {
            var statementLines = fileText.Split("\n");

            foreach (var line in statementLines)
            {
                var text = line.Trim();

                if (String.IsNullOrEmpty(text))
                {
                    continue;
                }
                var parsedItems = text.Split(",");

                if (parsedItems.Length != 4)
                {
                    Trace.WriteLine($"Line is not valid: {text}; length: {parsedItems.Length}");
                    return false;
                }

                var date = DateTime.Parse(parsedItems[0]);
                var merchant = parsedItems[1];
                var isSpending = parsedItems[2].StartsWith('-');
                var transaction = Math.Abs(Decimal.Parse(parsedItems[3].Replace("$", "")));
                var balance = Decimal.Parse(parsedItems[3].Replace("$", ""));

                _statements.Add(new Statement(date, merchant, transaction, isSpending, balance));
            }

            return true;
        }


    }
}
