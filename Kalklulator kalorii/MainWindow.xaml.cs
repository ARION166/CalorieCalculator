using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Kalkulator_kalorii
{
    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        private List<Product> products = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeConnection();
            LoadProductsFromDatabase();
            LoadProductsFromDatabase2();
            RefreshDataGrid();
        }

        private void ProduktyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                string selectedProduct = comboBox.SelectedItem as string;
                if (selectedProduct != null)
                {
                    Product product = products.Find(p => p.Name == selectedProduct);
                    if (product != null)
                    {
                        product.OriginalProtein = product.Protein;
                        product.OriginalCarbohydrates = product.Carbohydrates;
                        product.OriginalFat = product.Fat;
                        product.OriginalTotalCalories = product.TotalCalories;
                        WybraneProduktyDataGrid.Items.Add(product);
                    }
                }
            }
            CalculateAndDisplaySum();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Podaj Dane")
            {
                textBox.Text = "";
            }
        }

        private void InitializeConnection()
        {
            string connectionString = "Data Source=(local);Initial Catalog=Produkty;Integrated Security=True;Encrypt=False";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas łączenia z bazą danych: " + ex.Message);
            }
        }

        private void LoadProductsFromDatabase2()
        {
            string query = "SELECT Nazwa FROM Produkty";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string nazwa = reader.GetString(0);
                    if (!ProduktyComboBox.Items.Contains(nazwa))
                    {
                        ProduktyComboBox.Items.Add(nazwa);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania danych z bazy danych: " + ex.Message);
            }
        }

        private void LoadProductsFromDatabase()
        {
            string query = "SELECT Nazwa, Białko, Węglowodany, Tłuszcze FROM Produkty";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string nazwa = reader.GetString(0);
                    int bialko = reader.GetInt32(1);
                    int weglowodany = reader.GetInt32(2);
                    int tluszcze = reader.GetInt32(3);
                    int totalCalories = bialko * 4 + weglowodany * 4 + tluszcze * 9;
                    Product product = new Product
                    {
                        Name = nazwa,
                        Protein = bialko,
                        Carbohydrates = weglowodany,
                        Fat = tluszcze,
                        TotalCalories = totalCalories
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania danych z bazy danych: " + ex.Message);
            }
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NazwaTextBox.Text;
            int protein, carbs, fat;
            if (!int.TryParse(BiałkoTextBox.Text, out protein) ||
                !int.TryParse(WeglowodanyTextBox.Text, out carbs) ||
                !int.TryParse(TluszczeTextBox.Text, out fat))
            {
                MessageBox.Show("Podane wartości nie są poprawne.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int totalCalories = protein * 4 + carbs * 4 + fat * 9;
            Product newProduct = new Product
            {
                Name = name,
                Protein = protein,
                Carbohydrates = carbs,
                Fat = fat,
                TotalCalories = totalCalories
            };
            AddProductToDatabase(newProduct);
            products.Add(newProduct);
            RefreshDataGrid();
            ClearInputFields();
            LoadProductsFromDatabase2();
        }

        private void AddProductToDatabase(Product product)
        {
            try
            {
                string query = "INSERT INTO Produkty (Nazwa, Białko, Węglowodany, Tłuszcze, Kalorie) VALUES (@Nazwa, @Białko, @Węglowodany, @Tłuszcze, @Kalorie)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nazwa", product.Name);
                    command.Parameters.AddWithValue("@Białko", product.Protein);
                    command.Parameters.AddWithValue("@Węglowodany", product.Carbohydrates);
                    command.Parameters.AddWithValue("@Tłuszcze", product.Fat);
                    command.Parameters.AddWithValue("@Kalorie", product.TotalCalories);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produkt został dodany do bazy danych.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas dodawania produktu do bazy danych: " + ex.Message);
            }
        }

        private void RefreshDataGrid()
        {
            ProduktyDataGrid.ItemsSource = null;
            ProduktyDataGrid.ItemsSource = new List<Product>(products);
            CalculateAndDisplaySum();
        }

        private void ClearInputFields()
        {
            NazwaTextBox.Text = "";
            BiałkoTextBox.Text = "";
            WeglowodanyTextBox.Text = "";
            TluszczeTextBox.Text = "";
        }

        private void IloscTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = "1";
            }
        }

        private void IloscTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                Product selectedProduct = (Product)WybraneProduktyDataGrid.SelectedItem;
                if (selectedProduct != null)
                {
                    int newQuantity;
                    if (int.TryParse(textBox.Text, out newQuantity))
                    {
                        selectedProduct.Protein = selectedProduct.OriginalProtein * newQuantity;
                        selectedProduct.Carbohydrates = selectedProduct.OriginalCarbohydrates * newQuantity;
                        selectedProduct.Fat = selectedProduct.OriginalFat * newQuantity;
                        selectedProduct.TotalCalories = selectedProduct.OriginalTotalCalories * newQuantity;
                    }
                }
            }
            CalculateAndDisplaySum();
        }

        private void TextBox_GotFocus2(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox.Text == "1")
                {
                    textBox.Text = "";
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "1";
                }
            }
        }

        private void UsunButton_Click(object sender, RoutedEventArgs e)
        {
            if (WybraneProduktyDataGrid.SelectedItem != null)
            {
                Product selectedProduct = (Product)WybraneProduktyDataGrid.SelectedItem;
                WybraneProduktyDataGrid.Items.Remove(selectedProduct);
                CalculateAndDisplaySum();
            }
            else
            {
                MessageBox.Show("Wybierz produkt do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UsunButton_Click2(object sender, RoutedEventArgs e)
        {
            if (ProduktyDataGrid.SelectedItem != null)
            {
                Product selectedProduct = (Product)ProduktyDataGrid.SelectedItem;
                products.Remove(selectedProduct);
                RefreshDataGrid();
                RemoveProductFromDatabase(selectedProduct);
                ProduktyComboBox.Items.Remove(selectedProduct.Name);
            }
            else
            {
                MessageBox.Show("Wybierz produkt do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculateAndDisplaySum()
        {
            int sumBialko = 0;
            int sumTluszcze = 0;
            int sumWeglowodany = 0;
            int sumKalorie = 0;

            foreach (Product product in WybraneProduktyDataGrid.Items)
            {
                sumBialko += product.Protein;
                sumTluszcze += product.Fat;
                sumWeglowodany += product.Carbohydrates;
                sumKalorie += product.TotalCalories;
            }

            SumaBialkoTextBox.Text = sumBialko.ToString();
            SumaTluszczeTextBox.Text = sumTluszcze.ToString();
            SumaWeglowodanyTextBox.Text = sumWeglowodany.ToString();
            SumaKalorieTextBox.Text = sumKalorie.ToString();
        }

        private void RemoveProductFromDatabase(Product product)
        {
            try
            {
                string query = "DELETE FROM Produkty WHERE Nazwa = @Nazwa";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nazwa", product.Name);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produkt został usunięty z bazy danych.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas usuwania produktu z bazy danych: " + ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
