using System;
using System.Collections.Generic;
using BenamixTest.ViewModels;
using Xamarin.Forms;

namespace BenamixTest.Views
{
    public partial class ViewList : BaseView
    {
        public ViewList()
        {
            InitializeComponent();
            BindingContext = new ViewModelList();
        }
    }
}
