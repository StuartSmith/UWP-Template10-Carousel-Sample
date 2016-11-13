
using System;
using System.Linq;

using Template10.Mvvm;

using UWP_Template10_Carousel_Sample.Models;
using System.Reflection;

namespace UWP_Template10_Carousel_Sample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {            
            LoadDesignTimeData();
        }

        System.Collections.ObjectModel.ObservableCollection<ColorInfo> _Colors = new System.Collections.ObjectModel.ObservableCollection<ColorInfo>();
        public System.Collections.ObjectModel.ObservableCollection<Models.ColorInfo> Colors { get { return _Colors; } }

        Models.ColorInfo _SelectedColor = default(Models.ColorInfo);
        public Models.ColorInfo SelectedColor
        {
            get { return _SelectedColor; }
            set
            {
                // base.SetProperty(ref _SelectedColor, value);
                foreach (var item in this.Colors.Where(x => x.Selected))
                    item.Selected = false;
                if (value != null)
                {
                    value.Selected = true;
                    if (_SelectedColor != value)
                    {
                        _SelectedColor = value;
                        base.RaisePropertyChanged("SelectedColor");
                    }


                }
            }
        }


        public void LoadDesignTimeData()
        {
            var colors = typeof(Windows.UI.Colors).GetRuntimeProperties()
                .Select(x => new Models.ColorInfo { Name = x.Name, Color = (Windows.UI.Color)x.GetValue(null) });
            this.Colors.Clear();
            foreach (var color in colors.OrderBy(x => Guid.NewGuid()).Take(10))
            {
                color.MainPageViewModel = this;
                this.Colors.Add(color);
            }
            this.SelectedColor = this.Colors[1];
        }



        DelegateCommand _LoadDataCommand = null;
        public DelegateCommand LoadDataCommand => _LoadDataCommand ?? (_LoadDataCommand = new DelegateCommand(() => LoadDesignTimeData(), () => true));

        DelegateCommand<ColorInfo> _SelectCommand = null;
        public DelegateCommand<ColorInfo> SelectCommand => _SelectCommand ?? (_SelectCommand = new DelegateCommand<ColorInfo>((o) => { this.SelectedColor = o; }, (o) => true));


    }
}

