using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TriangleMatrix
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (StackPanel sp in SPMatrix2.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    tb.IsEnabled = false;
                    tb.VerticalContentAlignment = VerticalAlignment.Center;
                }
            }
            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    tb.VerticalContentAlignment = VerticalAlignment.Center;
                }
            }
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    tb.Text = string.Empty;
                }
            }
            foreach (StackPanel sp in SPMatrix2.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    tb.Text = string.Empty;
                }
            }
        }

        private void BRandomize_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    tb.Text = random.Next(-50, 50).ToString();
                }
            }
        }

        private void BCalculateDiagonal_Click(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    if (string.IsNullOrWhiteSpace(tb.Text))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    if (Regex.IsMatch(tb.Text, @"^[A-Za-zА-Яа-я]+$"))
                    {
                        MessageBox.Show("В матрице могут быть только числовые значения");
                        return;
                    }
                }
            }
        }

        private void BCalculateTriangle_Click(object sender, RoutedEventArgs e)
        {
            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    if (string.IsNullOrWhiteSpace(tb.Text))
                    {
                        MessageBox.Show("Запоните все поля");
                        return;
                    }
                    if (Regex.IsMatch(tb.Text, @"^[A-Za-zА-Яа-я]+$"))
                    {
                        MessageBox.Show("В матрице могут быть только числовые значения");
                        return;
                    }
                }
            }

            double[,] matrix = new double[5, 5];

            List<string> indexes = new List<string>();

            foreach (StackPanel sp in SPMatrix1.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    indexes.Add(Regex.Replace(tb.Name, @"\D", string.Empty));
                    char[] digits = indexes.Last().ToArray<char>();
                    matrix[int.Parse(digits[0].ToString()) - 1, int.Parse(digits[1].ToString()) - 1] = int.Parse(tb.Text);
                }
            }

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    double koef = matrix[j, i] / matrix[i, i];
                    for (int k = i; k < matrix.GetLength(0); k++)
                    {
                        matrix[j, k] -= matrix[i, k] * koef;
                    }
                }
            }

            foreach (StackPanel sp in SPMatrix2.Children)
            {
                foreach (TextBox tb in sp.Children)
                {
                    string name = tb.Name;
                    string digits = Regex.Replace(name, @"\D", string.Empty);
                    char[] digitsArr = digits.ToArray<char>();
                    tb.Text = matrix[int.Parse(digitsArr[0].ToString()) - 1, int.Parse(digitsArr[1].ToString()) - 1].ToString();
                }
            }
        }
    }
}
