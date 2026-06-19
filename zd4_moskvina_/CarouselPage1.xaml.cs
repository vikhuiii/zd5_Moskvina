using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4_moskvina_
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselPage1 : ContentPage, ICarouselPageWithData
    {
        private string username = "";

        public CarouselPage1()
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