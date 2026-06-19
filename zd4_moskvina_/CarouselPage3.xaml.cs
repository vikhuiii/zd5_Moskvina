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
    public partial class CarouselPage3 : ContentPage, ICarouselPageWithData
    {
        private string username = "";
        private string selectedItem = "";

        public CarouselPage3()
        {
            InitializeComponent();
        }

        public void SetUserData(string username)
        {
            this.username = username;
            userLabel.Text = string.IsNullOrEmpty(username) ? "Гость" : $"Пользователь: {username}";
        }


        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownPicker.SelectedIndex >= 0)
                selectedValueLabel.Text = dropdownPicker.SelectedItem?.ToString() ?? string.Empty;
            else
                selectedValueLabel.Text = string.Empty;
        }

        // Метод для получения выбранного элемента
        public string GetSelectedItem()
        {
            return selectedItem;
        }

        // Метод для получения расшифровки выбранного элемента
        public string GetSelectedItemDescription()
        {
            if (string.IsNullOrEmpty(selectedItem))
                return "Элемент не выбран";

            if (selectedItem.Contains("Select one"))
                return "Начальный уровень";
            else if (selectedItem.Contains("Select two"))
                return "Средний уровень";
            else if (selectedItem.Contains("Select three"))
                return "Продвинутый уровень";
            else if (selectedItem.Contains("Select four"))
                return "Полный функционал";

            return "Описание не найдено";
        }
    }
}