using System.Windows;
using MovieBank.UserControls;

namespace MovieBank.Views
{
    public partial class vwConfig : Window
    {
        public vwConfig()
        {
            InitializeComponent();
        }

        private void BtnDirectors_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new ucDirectors());
        }

        private void BtnMovieGenres_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new ucGenres());
        }
    }
}