using HelloWorld.Models;
using HelloWorld.Persistance;
using HelloWorld.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HelloWorld.ViewModels
{
    public class ViewTask : BaseViewModel
    {
        public ObservableCollection<AcronymGroup> AcronymList { get; private set; }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing;  }
            set { SetValue(ref isRefreshing, value); }
        }

        public ICommand RefreshAcronymListCommand { get; private set; }
        public ICommand RemoveAcronymCommand { get; private set; }
        public ICommand TappedAcronymCommand { get; private set; }
        public ICommand SelectedAcronymCommand { get; private set; }
        public ICommand AddAcronymCommand { get; private set; }


        private INavigation _navigation;

        public ViewTask(INavigation navigation)
        {
            _navigation = navigation;

            RefreshAcronymListCommand = new Command(() => RefresLissView());
            RemoveAcronymCommand = new Command<Acronym>(acronym => RemoveAcronymFromCollection(acronym));
            TappedAcronymCommand = new Command<Acronym>(acronym => TappedItem(acronym));
            SelectedAcronymCommand = new Command<Acronym>(acronym => SelectedItem(acronym));
            AddAcronymCommand = new Command(() => AddAcronym());

            var acronymsFromDatabae = DatabaseManager.Instance.GetALL<Acronym>();

            AcronymList = new ObservableCollection<AcronymGroup>
            {
                new AcronymGroup("Sieci Komputerowe", acronymsFromDatabae.Where(c => c.Type == Category.Sieci).ToList()),
                new AcronymGroup("Systemy Operacyjne", acronymsFromDatabae.Where(c => c.Type == Category.Systemy).ToList()),
       
            };
        }

        private void AddAcronym()
        {
            _navigation.PushModalAsync(new AddAcronymPage(AcronymList));
        }

        public void NavigateToAcronymDeatilPage(Acronym acronym)
        {
            _navigation.PushAsync(new AcronymDetailPage(acronym));
        }

        private void TappedItem(Acronym acronym)
        {
            App.Current.MainPage.DisplayAlert("Szczególy", $"Skrót: {acronym.Name}\n\n Angielski: {acronym.TranslationEnglish}\n\n Polski: {acronym.TranslationPolish}", "Ok");
        }

        private async Task SelectedItem(Acronym acronym)
        {


            using (var stream = await FileSystem.OpenAppPackageFileAsync(acronym.Name))
            {
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync($"Skrót: {acronym.Name} Angielski: {acronym.TranslationEnglish} Polski: {acronym.TranslationPolish}");
                }
            }
        }

        private void RemoveAcronymFromCollection(Acronym acronym)
        {
            foreach (var acronymGroup in AcronymList)
            {
                if (acronymGroup.Contains(acronym))
                {
                    acronymGroup.Remove(acronym);
                }
            }

            DatabaseManager.Instance.RemoveFromDatabase<Acronym>(acronym.Id);
        }

        private void RefresLissView()
        {
            IsRefreshing = true;
            var acronymList = new List<Acronym>
            {
                new Acronym()
                {
                    Name = "Maciej",
                    TranslationPolish="Komorowski",
                    TranslationEnglish="308119"
                },
            };

            var acronymGroup = new AcronymGroup("Projekt zaliczeniowy :", acronymList);
            AcronymList.Add(acronymGroup);
            IsRefreshing = false;
        }
    }
}
