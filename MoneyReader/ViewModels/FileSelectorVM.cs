using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyReader.Utils;

namespace MoneyReader.ViewModels
{
    public class FileSelectorVM : BaseVM
    {
        private bool _hasError = false;

        public bool HasError
        {
            get { return _hasError; }
            set => SetProperty(ref _hasError, value);
        }
    }
}
