using System;
using System.Collections.Generic;
using BenamixTest.ViewModels;
using Xamarin.Forms;

namespace BenamixTest.Views
{
    public partial class ViewElements : BaseView
    {
        public ViewElements()
        {
            InitializeComponent();
            BindingContext = new ViewModelElement();
        }
    }
}
