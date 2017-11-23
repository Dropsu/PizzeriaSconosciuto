using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PizzeriaSconosciuto
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // NA RAZIE KOD BAZY DANYCH TUTAJ BO NIE WIEM GDZIE DO DAC, POTEM ZREFAKTORYZUJEMU ;)

            // https://www.tutorialspoint.com/linq/linq_sql.htm

            SqlConnection conn = new SqlConnection(
            "Data Source = .\\SQLEXPRESS; Initial Catalog=Pizzeria;User ID=Pizzus;Password=pizza");
            LinqToSQLDataContext db = new LinqToSQLDataContext(conn);





            dodajProdukt(db, "Hawajska SREDNIA", 10, 29.99);





            InitializeComponent();
        }

        public void dodajProdukt(LinqToSQLDataContext db, string nazwa, int Ilosc, double cena)
        {

            Produkty nowy = new Produkty();
            nowy.Nazwa = nazwa;
            nowy.cena = (float)cena;
            nowy.Ilosc = Ilosc;

            db.Produkties.InsertOnSubmit(nowy);
            db.SubmitChanges();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }


   


}