using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asynchro
{
    //Pomoc: https://www.youtube.com/watch?v=DqjIQiZ_ql4
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //metoda wywoła się po kliknięciu przycisku
        {
            //Task.Factory.StartNew(() => WaznaMetoda("Ambroz")).ContinueWith(a => label1.Text = a.Result, TaskScheduler.FromCurrentSynchronizationContext()); //tworzy nowe zdarzenie i je uaktywnia. ContinueWith mówi o tym, ze wykona się kiedy poprzednie się zakończy. 
            //P.S Tu się wywala bo jest to Cross-Thread jak mamy samo a.Result, natomiast jeśli damy dalsza częśc to program przejrzy całą aplikacje i wyszuka do którego wątku się odnosimy. tutaj do Interfejsu Użytkownika (UI)
            //label1.Text = WaznaMetoda("Paweł"); //po kliknięciu przycisku wywoła się WaznaMetoda z name=Paweł

            WywolajWaznaMetoda(); //po kliknięciu przycisku wywołuje się private async void WywolajWaznaMetoda()
            label1.Text = "Oczekuje ...";
        }

        private async void WywolajWaznaMetoda()
        {
            var result = await WaznaMetodaAsync("Paweł"); //po wywołaniu nie moze przejść dalej jeśli ta linia nie zostanie wywołana
            label1.Text = result; //kiedy się wykona ma zmienić label1.Text
        }

        private Task<string> WaznaMetodaAsync(string name)//przemiana wynikowego stroinga na string wątek (Task string). Wywołam tą metodę, ale zwróci mi wątek, a kiedy się zakończy wątek to mam gostęp do jego wyniku
        {//aby zrobić coś asynchronicznie wystarczy dodać Async
           return Task.Factory.StartNew(() => WaznaMetoda(name));
        }

        private string WaznaMetoda(string name) //tworzymy przywatna metodę, która przyjmuje parametr string o nazwie name
        {
            Thread.Sleep(2000); //uśpienie wątku na 2 sekundy
            return "Witaj, " + name; //zwrócenie słowa Witaj oraz tego co znajduje się pod zmienną name
        
        }
    }
}
