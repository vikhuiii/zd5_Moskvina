using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace zd4_moskvina_
{
    public partial class MainPage : CarouselPage
    {
        private string username = "";

        public MainPage()
        {
            InitializeComponent();

            // Подписываемся на событие смены страницы
            this.CurrentPageChanged += OnCurrentPageChanged;
        }

        public void SetUserData(string username)
        {
            this.username = username;

            // Передаем данные на все страницы
            foreach (var page in this.Children)
            {
                if (page is ICarouselPageWithData pageWithData)
                {
                    pageWithData.SetUserData(username);
                }
            }
        }

        private void OnCurrentPageChanged(object sender, System.EventArgs e)
        {
            // При смене страницы обновляем данные
            var currentPage = this.CurrentPage;
            if (currentPage is ICarouselPageWithData pageWithData)
            {
                pageWithData.SetUserData(username);
            }
        }
    }

    // Интерфейс для страниц, которые принимают данные пользователя
    public interface ICarouselPageWithData
    {
        void SetUserData(string username);
    }
}