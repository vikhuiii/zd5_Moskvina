using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zd4_moskvina_
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselPage4 : ContentPage, ICarouselPageWithData
    {
        private string username = "";
        private MainPage parentCarousel;

        public CarouselPage4()
        {
            InitializeComponent();
        }

        public void SetUserData(string username)
        {
            this.username = username;
            usernameDisplay.Text = string.IsNullOrEmpty(username) ? "Гость" : username;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Получаем ссылку на родительский CarouselPage
            parentCarousel = this.Parent as MainPage;
        }

        private void OnStaticResultClicked(object sender, EventArgs e)
        {
            try
            {
                // Получаем данные с других страниц CarouselPage
                string selectedItem = "";
                string description = "";

                if (parentCarousel != null)
                {
                    // Ищем страницу 3 (Dropdown и Switch)
                    foreach (var page in parentCarousel.Children)
                    {
                        if (page is CarouselPage3 page3)
                        {
                            selectedItem = page3.GetSelectedItem();
                            description = page3.GetSelectedItemDescription();
                        }

                        
                    }
                }

                // Отображаем результат
                selectedItemDisplay.Text = string.IsNullOrEmpty(selectedItem) ? "Элемент не выбран" : selectedItem;
                descriptionDisplay.Text = string.IsNullOrEmpty(description) ? "Расшифровка не найдена" : description;
                usernameDisplay.Text = string.IsNullOrEmpty(username) ? "Гость" : username;

                // Показываем фрейм с результатом
                resultFrame.IsVisible = true;

                // Анимация появления
                resultFrame.FadeTo(1, 500);
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Произошла ошибка: " + ex.Message, "OK");
            }
        }
    }
}