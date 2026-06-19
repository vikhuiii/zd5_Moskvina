using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4_moskvina_
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            // Проверка на заполнение полей
            if (string.IsNullOrWhiteSpace(usernameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите имя пользователя", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите пароль", "OK");
                return;
            }

            // Переход на CarouselPage с передачей имени пользователя
            var carouselPage = new MainPage();
            carouselPage.SetUserData(usernameEntry.Text);

            // Используем Navigation для перехода
            await Navigation.PushAsync(carouselPage);
        }
    }
}