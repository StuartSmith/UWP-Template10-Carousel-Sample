
using Template10.Mvvm;
using Windows.UI.Xaml.Media;
using UWP_Template10_Carousel_Sample.ViewModels;


namespace UWP_Template10_Carousel_Sample.Models
{
   
        public class ColorInfo : BindableBase
        {

            public string Name { get; set; }
            public Windows.UI.Color Color { get; set; }
            public SolidColorBrush Brush { get { return new SolidColorBrush(this.Color); } }
            bool _Selected = default(bool);
            public bool Selected
            {
                get
                {
                    return _Selected;
                }
                set
                {
                    if (_Selected != value)
                    {
                        _Selected = value;
                        base.RaisePropertyChanged("Selected");
                    }

                }
            }

            public MainPageViewModel MainPageViewModel { get; set; }

            DelegateCommand<ColorInfo> _SelectCommand = null;
            public DelegateCommand<ColorInfo> SelectCommand => _SelectCommand ?? (_SelectCommand = new DelegateCommand<ColorInfo>((o) =>
            {
                this.MainPageViewModel.SelectedColor = o;
            }
            , (o) => true));
        }

  
}
