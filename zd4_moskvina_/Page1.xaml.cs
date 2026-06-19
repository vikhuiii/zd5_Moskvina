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
    public partial class Page1 : ContentPage
    {
        string userData;
        // Метод для получения данных пользователя
        public void SetUserData(string username)
        {
            userData = username;
            // Можно отобразить имя пользователя где-то на странице
        }

        private void percentSl_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            percentTxt.Text = percentSl.Value.ToString("0") + "%";
            CalculateCredit();
        }

        private void ValueChanged(object sender, TextChangedEventArgs e)
        {
            CalculateCredit();
        }

        private void CalculateCredit()
        {
            try
            {
                // Сумма кредита
                if (!double.TryParse(sum.Text, out double credit) || credit <= 0)
                {
                    ClearResult();
                    return;
                }
                // Срок кредита в месяцах
                if (!int.TryParse(srok.Text, out int month) || month <= 0)
                {
                    ClearResult();
                    return;
                }
                // Получаем процентную ставку
                double annualRate = percentSl.Value / 100;
                double monthlyRate = annualRate / 12;
                // Получаем тип платежа
                string paymentType = typePay.SelectedItem?.ToString();
                double monthlyPayment = 0;
                double totalPayment = 0;
                double overPayment = 0;

                switch (paymentType)
                {
                    case "Аннуитетный":
                        CalculateAnnuit(credit, month, monthlyRate, out monthlyPayment, out totalPayment, out overPayment);
                        break;
                    case "Дифференцированный":
                        CalculateDifferentiated(credit, month, monthlyRate, out monthlyPayment, out totalPayment, out overPayment);
                        break;
                    case "Буллетный":
                        CalculateBullet(credit, month, monthlyRate, out monthlyPayment, out totalPayment, out overPayment);
                        break;
                }
                // Результаты с округлением
                monthPay.Text = monthlyPayment.ToString("F2");
                allMoney.Text = totalPayment.ToString("F2");
                overPay.Text = overPayment.ToString("F2");
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка", "Произошла ошибка: " + ex.Message, "Ok");
                ClearResult();
            }
        }

        private void CalculateAnnuit(double amount, int months, double monthlyRate, out double monthlyPayment, out double totalPayment, out double overPayment)
        {
            if (monthlyRate == 0)
            {
                monthlyPayment = amount / months;
            }
            else
            {
                double pow = Math.Pow(1 + monthlyRate, months);
                monthlyPayment = amount * (monthlyRate * pow) / (pow - 1);
            }

            totalPayment = monthlyPayment * months;
            overPayment = totalPayment - amount;
        }

        private void CalculateDifferentiated(double amount, int months, double monthlyRate, out double monthlyPayment, out double totalPayment, out double overPayment)
        {
            double principalPerMonth = amount / months;
            double firstMonthPayment = principalPerMonth + (amount * monthlyRate);
            double lastMonthPayment = principalPerMonth + (principalPerMonth * monthlyRate);
            monthlyPayment = (firstMonthPayment + lastMonthPayment) / 2;

            double totalInterest = 0;
            for (int i = 0; i < months; i++)
            {
                double remainingPrincipal = amount - (principalPerMonth * i);
                double interest = remainingPrincipal * monthlyRate;
                totalInterest += interest;
            }

            totalPayment = amount + totalInterest;
            overPayment = totalPayment - amount;
        }

        private void CalculateBullet(double amount, int months, double monthlyRate, out double monthlyPayment, out double totalPayment, out double overPayment)
        {
            double monthlyInterest = amount * monthlyRate;
            monthlyPayment = monthlyInterest;
            totalPayment = amount + (monthlyInterest * months);
            overPayment = totalPayment - amount;
        }

        private void ClearResult()
        {
            monthPay.Text = "....";
            allMoney.Text = "....";
            overPay.Text = "....";
        }

        private void typePay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateCredit();
        }
    }
}