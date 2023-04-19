using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HelloWorld.Models
{
    public class AcronymGroup : ObservableCollection<Acronym>
    {
        public string Title { get; set; }
        public List<Acronym> ListAcronym { get; set; }
        private bool _isShow;

        public AcronymGroup(string title, List<Acronym> listacronym)
        {
            Title = title;
            ListAcronym = listacronym;

            _isShow = true;

            foreach (var acronym in listacronym)
            {
                base.Add(acronym);
            }
        }

        public void ShowOrHideElements()
        {
            if (_isShow)
            {
                base.Clear();
            }
            else
            {
                foreach (var acronym in ListAcronym)
                {
                    base.Add(acronym);
                }
            }

            _isShow = !_isShow;
        }
    }
}
