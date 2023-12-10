using System;
using System.Linq;
using System.Windows;
using MovieBank.Models;
using System.Data.Entity;
using System.Windows.Controls;

namespace MovieBank.UserControls
{
    public partial class ucDirectors : UserControl
    {
        Directors _directors = new Directors();
        MovieBankDB _db = new MovieBankDB();
        public ucDirectors()
        {
            InitializeComponent();
            LoadGrid();
            SpAddDirector.DataContext = _directors;
        }

        private void BtnAddDirector_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _db.Directors.Add(_directors);
                _db.SaveChanges();
                WPFCustomMessageBox.CustomMessageBox.Show("رکورد جدید با موفقیت ثبت گردید");
                LoadGrid();
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or display an error message
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadGrid()
        {
            try
            {
                GrdList.ItemsSource = _db.Directors.ToList();
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or display an error message
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn)
                    if (btn.Tag is Directors director)
                    {
                        _db.SaveChanges();
                        WPFCustomMessageBox.CustomMessageBox.Show("رکورد مورد نظر با موفقیت ویرایش شد");
                        LoadGrid();
                    }
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or display an error message
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WPFCustomMessageBox.CustomMessageBox.ShowYesNo("آیا از حذف این رکورد اطمینان دارید؟", "حذف", "بلی", "خیر") == MessageBoxResult.Yes)
                    if (sender is Button btn)
                        if (btn.Tag is Directors director)
                        {
                            _db.Entry(director).State = EntityState.Deleted;
                            _db.SaveChanges();
                            WPFCustomMessageBox.CustomMessageBox.Show("رکورد مورد نظر با موفقیت حذف گردید");
                            LoadGrid();
                        }
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or display an error message
                MessageBox.Show(ex.Message);
            }
        }
    }
}