using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenamixTest.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Initialize() { }
        public virtual void Deinitialize() { }
    }
}