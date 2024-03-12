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
            _itemsCount = !string.IsNullOrEmpty(ItemsCountTextBox.Text) ? int.Parse(ItemsCountTextBox.Text) : 0;
        }

        private void SeedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _seed = !string.IsNullOrEmpty(SeedTextBox.Text) ? int.Parse(SeedTextBox.Text) : 0;
        }

        private void CapacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _capacity = !string.IsNullOrEmpty(CapacityTextBox.Text) ? int.Parse(CapacityTextBox.Text) : 0;
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