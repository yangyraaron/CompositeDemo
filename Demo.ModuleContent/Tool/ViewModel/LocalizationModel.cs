using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using Demo.Infrastructure;
using System.Globalization;

namespace Demo.Content
{
    public class LocalizationModel:NotificationObject
    {
        #region properties

        public ObservableCollection<LanguageModel> OriginalLanguages
        {
            get
            {
                var langs = DataManager.Current.GetWeakReferenceItemFunc
                    <string, ObservableCollection<LanguageModel>>
                        ("Multi-Language", LoadAllLanguages);
                return langs;
            }
        }

        protected LanguageModel _targetLanguage;
        public LanguageModel TargetLanguage
        {
            get { return _targetLanguage; }
            set
            {
                if (_targetLanguage == value)
                    return;
                _targetLanguage = value;
                RaisePropertyChanged("TargetLanguage");
            }
        }

        #endregion

        #region Methods

        protected ObservableCollection<LanguageModel> LoadAllLanguages()
        {

            var langs = from c in CultureInfo.GetCultures(CultureTypes.AllCultures)
                        orderby c.EnglishName
                        where c.Name.Length > 2
                        select new LanguageModel { Description = c.EnglishName, Name = c.Name };
            return new ObservableCollection<LanguageModel>(langs);
        }
        #endregion
    }
}
