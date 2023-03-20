using CheckRequestSecurity.Tools;
using CheckRequestSecurity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CheckRequestSecurity.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private Page currentpage;

        public MainWindowVM()
        {
            CurrentPage = new SignInPage(this);
        }
        public Page CurrentPage
        { 
            get => currentpage;
            set
            {
                currentpage = value;
                SignalChanged();
            }
            
        }
    }
}
