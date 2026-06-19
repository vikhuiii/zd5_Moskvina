using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace zd4_moskvina_
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselPage2 : ContentPage, ICarouselPageWithData
    {
        private string username = "";

        public CarouselPage2()
        {
            InitializeComponent();
        }

        public void SetUserData(string username)
        {
            this.username = username;
            userLabel.Text = string.IsNullOrEmpty(username) ? "Гость" : $"Пользователь: {username}";
        }


    }
}