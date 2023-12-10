using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace MovieBank.UserControls
{
    public partial class ucImageWithBorder : UserControl
    {
        #region [- DP Properties -]

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(dp: ImageSourceProperty); }
            set { SetValue(dp: ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(name: "Source", propertyType: typeof(ImageSource), ownerType: typeof(ucImageWithBorder), new PropertyMetadata(defaultValue: null));


        public object Value
        {
            get { return (object)GetValue(dp: ValueProperty); }
            set { SetValue(dp: ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(name: "Value", propertyType: typeof(object), ownerType: typeof(ucImageWithBorder), new PropertyMetadata(defaultValue: null));

        #endregion
        public ucImageWithBorder()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}