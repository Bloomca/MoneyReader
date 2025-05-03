using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyReader.Classes
{
    public enum CategoryMatchingType
    {
        Contains,
        StartsWith
    }

    public class Category
    {
        public string Name { get; }

        public CategoryMatchingType Type { get; }

        public Category(string name, CategoryMatchingType type)
        {
            Name = name;
            Type = type;
        }
    }
}
