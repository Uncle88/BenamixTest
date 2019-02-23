using System;
using BenamixTest.ViewModels;
using Xamarin.Forms;

namespace BenamixTest.Views
{
    public class BaseView : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null && BindingContext is BaseViewModel)
            {
                ((BaseViewModel)BindingContext).Initialize();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext != null && BindingContext is BaseViewModel)
            {
                ((BaseViewModel)BindingContext).Deinitialize();
            }
        }
    }
}
