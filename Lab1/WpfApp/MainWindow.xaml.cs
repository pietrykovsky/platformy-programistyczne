using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConsoleApp;

namespace WpfApp
{

    public partial class MainWindow : Window
    {
        private int _itemsCount;
        private int _seed;
        private int _capacity;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ItemsCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(ItemsCountTextBox.Text, out int value) && value >= 0)
            {
                _itemsCount = value;
                ItemsCountTextBox.Background = new SolidColorBrush(Colors.Pink);
            }
            else
            {
                _itemsCount = 0;
                ItemsCountTextBox.Background = new SolidColorBrush(Colors.LightPink);
                MessageBox.Show("Wprowadzona wartość dla liczby przedmiotów jest nieprawidłowa. Proszę wprowadzić liczbę całkowitą większą lub równą 0.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SeedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(SeedTextBox.Text, out int value) && value >= 0)
            {
                _seed = value;
                SeedTextBox.Background = new SolidColorBrush(Colors.Pink);
            }
            else
            {
                _seed = 0;
                SeedTextBox.Background = new SolidColorBrush(Colors.LightPink);
                MessageBox.Show("Wprowadzona wartość dla ziarna generatora liczb jest nieprawidłowa. Proszę wprowadzić liczbę całkowitą większą lub równą 0.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CapacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CapacityTextBox.Text, out int value) && value >= 0)
            {
                _capacity = value;
                CapacityTextBox.Background = new SolidColorBrush(Colors.Pink);
            }
            else
            {
                _capacity = 0;
                CapacityTextBox.Background = new SolidColorBrush(Colors.LightPink);
                MessageBox.Show("Wprowadzona wartość dla pojemności jest nieprawidłowa. Proszę wprowadzić liczbę całkowitą większą lub równą 0.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var problem = new Problem(_itemsCount, _seed);
            var result = problem.Solve(_capacity);
            ResultList.ItemsSource = result.ItemsToListOfStrings();
            ItemsList.ItemsSource = problem.ItemsToListOfStrings();
        }
    }
}