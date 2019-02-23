using System;
namespace BenamixTest.ViewModels
{
    public class ViewModelElement : BaseViewModel
    {
        public ViewModelElement()
        {
        }

        private string _bidsItem;
        public string BidsItem
        {
            get { return _bidsItem; }
            set
            {
                _bidsItem = value;
                OnPropertyChanged();
            }
        }

        private string _asksItem;
        public string AsksItem
        {
            get { return _asksItem; }
            set
            {
                _asksItem = value;
                OnPropertyChanged();
            }
        }
    }
}
