using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Chess2.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<CellViewModel> Cells { get; set; }
            = new ObservableCollection<CellViewModel>();
    }
}
