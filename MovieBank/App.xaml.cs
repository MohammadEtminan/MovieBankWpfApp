using System.Windows;
using MovieWpfApp.Utility;

namespace MovieBank
{
    public partial class App : Application
    {
        public App()
        {
            PersianCulture culture = new PersianCulture();
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}