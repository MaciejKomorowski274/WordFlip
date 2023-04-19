using HelloWorld.Models;
using HelloWorld.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HelloWorld.ViewModels
{

    public class AddAcronymViewModel : BaseViewModel
    {
        public List<string> AcronymTypes { get; set; } = new List<string>();
        public ObservableCollection<AcronymGroup> AcronymGroupList { get; set; } = new ObservableCollection<AcronymGroup>();
        public Dictionary<string, Category> AcronymTypeDictionary { get; set; } = new Dictionary<string, Category>();
        public INavigation Navigation { get; set; }

        private string _name;
        public string Name {
            get { return _name; }
            set { SetValue<string>(ref _name, value); } 
        }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set { SetValue<string>(ref _selectedType, value); }
        }

        private string _translationPolish;
        public string TranslationPolish
        {
            get { return _translationPolish; }
            set { SetValue<string>(ref _translationPolish, value); }
        }

        private string _translationEnglish;
        public string TranslationEnglish
        {
            get { return _translationEnglish; }
            set { SetValue<string>(ref _translationEnglish, value); }
        }

        


        public ICommand SaveAcronymCommand { get; set; }

        public AddAcronymViewModel(INavigation navigation, ObservableCollection<AcronymGroup> acronymGroup)
        {
            AcronymGroupList = acronymGroup;
            Navigation = navigation;
            FilAcronymTypePicker();
            SaveAcronymCommand = new Command(async () => await AddAcronymToDatabaseAsync());
        }

        private void FilAcronymTypePicker()
        {
            foreach (Category category in (Category[])Enum.GetValues(typeof(Category)))
            {
                AcronymTypes.Add(category.ToString());
                AcronymTypeDictionary.Add(category.ToString(), category);
            }
        }

        private async Task AddAcronymToDatabaseAsync()
        {
            var acronym = new Acronym();
            acronym.Name = Name;
            acronym.TranslationPolish = TranslationPolish;
            acronym.TranslationEnglish = TranslationEnglish;
            acronym.Type = AcronymTypeDictionary[SelectedType];

            DatabaseManager.Instance.Add<Acronym>(acronym);

            foreach (var acronymGroup in AcronymGroupList)
            {
                if (acronymGroup[0].Type == acronym.Type)
                {
                    acronymGroup.Add(acronym);
                }
            }

            await Navigation.PopModalAsync();
        }
    }
}
