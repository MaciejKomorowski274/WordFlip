using HelloWorld.Models;
using HelloWorld.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public ViewTask ViewModel { get; set; }
        public ListViewPage()
        {
            ViewModel = new ViewTask(Navigation);
            InitializeComponent();
            BindingContext = ViewModel;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var senderBindingContext = ((Button)sender).BindingContext;
            var acronym = (Acronym)senderBindingContext;

            ViewModel.NavigateToAcronymDeatilPage(acronym);
        }

        private void AcronymListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedAcronymCommand.Execute(e.SelectedItem);
        }

        private void AcronymListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.TappedAcronymCommand.Execute(e.Item);
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as Xamarin.Forms.MenuItem;
            var acronym = menuItem.CommandParameter as Acronym;

            ViewModel.NavigateToAcronymDeatilPage(acronym);

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var button = sender as Button;
            var acronymGroup = button.CommandParameter as AcronymGroup;

            acronymGroup.ShowOrHideElements();
        }
    }
}