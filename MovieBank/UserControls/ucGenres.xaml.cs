using System.Linq;
using MovieBank.Models;
using System.Windows.Controls;

namespace MovieBank.UserControls
{
    public partial class ucGenres : UserControl
    {
        MovieBankDB _db = new MovieBankDB();
        public ucGenres()
        {
            InitializeComponent();
            GrdList.ItemsSource = _db.Genres.ToList();
        }
    }
}