﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPF_TaskManager.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChenged (string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (propertyName));
        }
    }
}
