
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinFormsMapsDemo
{
	public class MapPage : ContentPage
    {
        Map _Map;
		double lat, lang;
		public MapPage(double lat, double lang)
        {
            _Map = new Map()
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

			this.lat = lat;
			this.lang = lang;
			  
           

        }

        public async Task MakeMap()
        {
			
			Pin pin = new Pin () {
				Position = new Position (lat, lang),//51.503, -0.12759),
				Label = "Some Pin!"
			};



            _Map.Pins.Clear();

            _Map.Pins.Add(pin);

            _Map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(5)));

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: _Map,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            Content = relativeLayout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await this.MakeMap();
        }
    }
}

