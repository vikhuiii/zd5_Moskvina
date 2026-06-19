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
    public partial class ButtonStylesPage : ContentPage
    {


        private string username = "";
        private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>
        {
            { "Select one", "Выбран первый элемент - это базовый вариант выбора" },
            { "Select two", "Выбран второй элемент - промежуточное значение" },
            { "Select three", "Выбран третий элемент - расширенный вариант" },
            { "Select four", "Выбран четвертый элемент - максимальный вариант выбора" }
        };

        public ButtonStylesPage()
        {
            InitializeComponent();
            dropdownPicker.SelectedIndex = 0;
        }

        // Метод для получения данных пользователя
        public void SetUserData(string username)
        {
            this.username = username;
        }



        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownPicker.SelectedIndex >= 0)
            {
                string selected = dropdownPicker.Items[dropdownPicker.SelectedIndex];
                selectedValueLabel.Text = "Выбрано: " + selected;

            }
        }

        private void OnStaticResultClicked(object sender, EventArgs e)
        {
            try
            {
                // Получаем выбранное значение из списка
                string selectedItem = "Select one";
                if (dropdownPicker.SelectedIndex >= 0 && dropdownPicker.SelectedIndex < dropdownPicker.Items.Count)
                {
                    selectedItem = dropdownPicker.Items[dropdownPicker.SelectedIndex];
                }

                // Получаем максимальное значение из слайдеров
                double maxSliderValue = GetMaxSliderValue();

                // Получаем расшифровку для выбранного элемента
                string description = GetItemDescription(selectedItem);

                // Отображаем результат
                selectedItemDisplay.Text = selectedItem;
                descriptionDisplay.Text = description;
                maxSliderValueDisplay.Text = maxSliderValue.ToString("F0");
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

        private double GetMaxSliderValue()
        {
            // Получаем максимальное значение из всех слайдеров на странице
            double maxValue = 0;

            // Ищем все слайдеры на странице
            var sliders = FindSliders(this.Content);
            foreach (var slider in sliders)
            {
                if (slider.Value > maxValue)
                {
                    maxValue = slider.Value;
                }
            }

            return maxValue;
        }

        private List<Slider> FindSliders(Element element)
        {
            var result = new List<Slider>();

            if (element is Slider slider)
            {
                result.Add(slider);
            }

            if (element is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is Element childElement)
                    {
                        result.AddRange(FindSliders(childElement));
                    }
                }
            }
            else if (element is ContentView contentView && contentView.Content != null)
            {
                result.AddRange(FindSliders(contentView.Content));
            }
            else if (element is ScrollView scrollView && scrollView.Content != null)
            {
                result.AddRange(FindSliders(scrollView.Content));
            }
            else if (element is Frame frame && frame.Content != null)
            {
                result.AddRange(FindSliders(frame.Content));
            }

            return result;
        }

        private string GetItemDescription(string item)
        {
            if (itemDescriptions.ContainsKey(item))
            {
                return itemDescriptions[item];
            }
            return "Описание для данного элемента не найдено";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Скрываем результат при возврате на страницу
            resultFrame.IsVisible = false;
        }
    }
}
