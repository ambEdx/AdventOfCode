using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.ViewModels
{
    [ObservableObject]
    public partial class MainPageViewModel
    {
        public MainPageViewModel()
        {
            Splash = "Advent of Code";
        }
        [ObservableProperty]
        private string _splash;
    }
}
