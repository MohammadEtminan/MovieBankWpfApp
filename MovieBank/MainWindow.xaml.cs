using System;
using System.IO;
using System.Linq;
using System.Windows;
using MovieBank.Views;
using MovieBank.Models;
using MovieBank.Utility;
using System.Windows.Input;
using MovieBank.UserControls;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovieBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MovieBankDB _db = new MovieBankDB();
        Movies _movies = new Movies();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _movies;
            LoadMovies();
        }

        private void LoadMovies()
        {
            SpMovieList.Children.Clear();
            foreach (var movie in _db.Movies)
            {
                var path = Variable.ImageFullPath;
                BitmapImage poster = null;
                if (!string.IsNullOrEmpty(movie.Poster) && File.Exists(path + movie.Poster))
                    poster = new BitmapImage(new Uri(path + movie.Poster));
                else
                    poster = new BitmapImage(new Uri(Variable.ApplicationPath + Variable.DefaultPoster));

                var uc = new ucImageWithBorder() { Value = movie, Source = poster };
                uc.MouseWheel += Child_MouseWheel;
                uc.MouseDown += Child_MouseDown;
                SpMovieList.Children.Add(uc);
            }
        }

        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                SvMovieList.LineLeft();
            else
                SvMovieList.LineRight();
        }

        private void Child_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainGridPanel.Visibility != Visibility.Visible)
            {
                ImgBackground.Visibility = Visibility.Hidden;
                MainGridPanel.Visibility = Visibility.Visible;
            }

            var uc = (UserControl)sender;
            if (uc.Content is Border border)
                if (border.Tag is Movies movie)
                {
                    this.DataContext = movie;
                    _movies = movie;
                }
                else
                    MessageBox.Show($"Tag Value: {border.Tag}");
        }

        private void RecTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

            if (e.MiddleButton == MouseButtonState.Pressed)
                this.WindowState = this.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtmMinus_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMoveRight_Click(object sender, RoutedEventArgs e)
        {
            SvMovieList.LineRight();
        }

        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            SvMovieList.LineLeft();
        }

        private void BtnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            var vw = new vwMovieAddOrEdit() { Owner = this };
            if (vw.ShowDialog() == true)
                LoadMovies();
        }


        private void BtnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            var vw = new vwMovieAddOrEdit() { Owner = this, Movie = _movies };
            vw.Title = $"ویرایش {_movies.Title}";
            vw.BtnSave.Content = "ویرایش";
            var result = vw.ShowDialog();
            DataContext = _movies = _db.Movies.AsNoTracking().Single(c => c.Id == _movies.Id);
            if (vw.ShowDialog() == true)
                LoadMovies();
        }

        private void BtnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            if (_movies != null)
            {
                _db.Movies.Remove(_movies);
                _db.SaveChanges();
                ImgBackground.Visibility = Visibility.Visible;
                MainGridPanel.Visibility = Visibility.Hidden;
                LoadMovies();
            }
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            var vw = new vwConfig() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner};
            vw.ShowDialog();
        }
    }
}