using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        LinqToSQLDataContext db;
        public MainWindow()
        {
            // NA RAZIE KOD BAZY DANYCH TUTAJ BO NIE WIEM GDZIE DO DAC, POTEM ZREFAKTORYZUJEMU ;)

            // https://www.tutorialspoint.com/linq/linq_sql.htm

            SqlConnection conn = new SqlConnection(
            "Data Source = .\\SQLEXPRESS; Initial Catalog=Pizzeria;User ID=Pizzus;Password=pizza");
            db = new LinqToSQLDataContext(conn);





            dodajProdukt(db, "Hawajska SREDNIA", 10, 29.98);





            InitializeComponent();

            Loaded += Window_Loaded;

        }

        public void dodajProdukt(LinqToSQLDataContext db, string nazwa, int Ilosc, double cena)
        {

            try
            {
                db.InsertProdukt(nazwa, Ilosc, cena);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }



            db.SubmitChanges();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (nazwa_box.Text == ""||cena_box.Text == ""|| ilosc_box.Text == "")
            {
                MessageBox.Show("Uzupelnij nazwe, cene i ilosc", "Error", MessageBoxButton.OK);

            }
            else
            {
                dodajProdukt(db, nazwa_box.Text, Int32.Parse(ilosc_box.Text),Convert.ToDouble(cena_box.Text));
                fillTablie();
            }
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            if (produktid_box.Text=="")
            {
                MessageBox.Show("Podaj Id produktu do usuniecia", "Error", MessageBoxButton.OK);
            }
            else
            {
                usunProdukt(Int32.Parse(produktid_box.Text));
                fillTablie();
            }
            
        }

        private void usunProdukt(int id)
        {

            var delete =
            from Produkty in db.Produkties
            where Produkty.IdProduktu == id
            select Produkty;

            foreach (var produkt in delete)
            {
                db.Produkties.DeleteOnSubmit(produkt);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
        }

        private void fillTablie()
        {
            Table<Produkty> Produkty = db.GetTable<Produkty>();
            var q =
               from p in Produkty
               select p;
            dataGrid.ItemsSource = q.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillTablie();   
        }

        private void zmien_Click(object sender, RoutedEventArgs e)
        {
            if (produktid_box.Text == ""| nazwa_box.Text == "" || cena_box.Text == "" || ilosc_box.Text == "")
            {
                MessageBox.Show("Wybierz id produktu do zmiany i uzupelnij wszystkie pola", "Error", MessageBoxButton.OK);
            }
            else
            {
                Produkty result = (from p in db.Produkties
                                   where p.IdProduktu == Int32.Parse(produktid_box.Text)
                                   select p).SingleOrDefault();

                result.Ilosc = Int32.Parse(ilosc_box.Text);
                result.Nazwa = nazwa_box.Text;
                result.cena = Int32.Parse(cena_box.Text);

                db.SubmitChanges();
            }

            
        }

    }


   


}