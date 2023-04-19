using HelloWorld.Models;
using HelloWorld.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAcronymPage : ContentPage
    {
        public AddAcronymPage(ObservableCollection<AcronymGroup> AcronymGrouplist)
        {
            InitializeComponent();
            BindingContext = new AddAcronymViewModel(Navigation, AcronymGrouplist);
        }
    }
}