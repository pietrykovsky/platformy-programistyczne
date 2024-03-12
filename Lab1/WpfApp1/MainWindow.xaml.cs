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

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        private int _numberofitems;
        private int _seed;
        private int _capacity;
        private string _resultlist;
        private string _instacelist;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _numberofitems = int.Parse(Content1.Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            _seed = int.Parse(Content2.Text);
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            _capacity = int.Parse(Content3.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var problem = new Problem(_numberofitems, _seed);
            var result = problem.Solve(_capacity);
            _resultlist = result.ItemsToString();
            _instacelist = problem.ItemsToString();


            list1.ItemsSource = _resultlist;
            list2.ItemsSource = _instacelist;

        }
    }
}