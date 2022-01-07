using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LampSwitch
{
    public class LampStatusViewModel : NotifyObject
    {
        private int _currentLevel = 0;
        public int CurrentLevel { 
            get => _currentLevel; 
            set => SetProperty(ref _currentLevel, value); 
        }

        private int _currentPosition = 0;
        public int CurrentPosition
        {
            get => _currentPosition;
            set => SetProperty(ref _currentPosition, value);
        }

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
    }
}
