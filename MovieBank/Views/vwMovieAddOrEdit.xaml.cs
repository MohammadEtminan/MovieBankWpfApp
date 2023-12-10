using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using MovieBank.Models;
using MovieBank.Utility;
using System.Windows.Media.Imaging;

namespace MovieBank.Views
{
    public partial class vwMovieAddOrEdit : Window
    {
        public Movies Movie = new Movies();
        private MovieBankDB _db = new MovieBankDB();
        OpenFileDialog _dialog = new OpenFileDialog();
        public vwMovieAddOrEdit()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void VwMovieAddOrEdit_Loaded(object sender, RoutedEventArgs e)
        {
            var directors = _db.Directors.ToList();
            directors.Insert(0, new Directors() { Id = 0, FullName = "-- انتخاب کنید --" });
            CboDirector.ItemsSource = directors;
            CboDirector.SelectedIndex = 0;
            this.DataContext = Movie;
            if (!string.IsNullOrEmpty(Movie.Poster))
            {
                ImgBackground.Source = ImgPoster.Source = new BitmapImage(new Uri(Variable.ImageFullPath + Movie.Poster));
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_dialog != null && !string.IsNullOrEmpty(_dialog.FileName))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "Images\\Movie\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(Movie.Poster) && File.Exists(Variable.ImageFullPath + Movie.Poster))
                    File.Delete(Variable.ImageFullPath + Movie.Poster);

                var imageName = Guid.NewGuid().ToString().Replace("-", "");
                var ext = Path.GetExtension(_dialog.SafeFileName);
                var fullImageName = imageName + ext;
                File.Copy(_dialog.FileName, path + fullImageName);
                Movie.Poster = fullImageName;
            }
            if (Movie.Id == 0)
            {
                Movie.CreateDate = DateTime.Now;
                _db.Movies.Add(Movie);
            }
            else
            {
                var model = _db.Movies.Single(c => c.Id == Movie.Id);
                model.Title = Movie.Title;
                model.Tagline = Movie.Tagline;
                model.Cast = Movie.Cast;
                model.Poster = Movie.Poster;
                model.Description = Movie.Description;
                model.DirectorId = Movie.DirectorId;
                model.ImdbRate = Movie.ImdbRate;
                model.Year = Movie.Year;
            }

            _db.SaveChanges();
            this.DialogResult = true;
        }

        private void BtnPoster_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Filter = "Jpg files (*.jpg)|*.jpg|PNG files (*.png)|*.png";
            if (_dialog.ShowDialog() == true)
            {
                TxtPosterName.Content = _dialog.FileName;
                ImgBackground.Source = ImgPoster.Source = new BitmapImage(new Uri(_dialog.FileName));
            }
        }
    }
}