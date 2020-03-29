using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UserForm {
    /// <summary>
    /// Interaction logic for TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl {
        #region Constructors

        public TextBoxWithErrorProvider() {
            InitializeComponent();
            border.BorderBrush = BrushForAll;
        }

        #endregion Constructors


        #region Properties

        public string Text {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public Brush TextBoxBorderBrush {
            get { return border.BorderBrush; }
            set { border.BorderBrush = value; }
        }

        public static Brush BrushForAll { get; set; } = Brushes.Red;

        #endregion Properties


        #region Methods

        public void SetError( string errorText ) {
            textBlockToolTip.Text = errorText;

            if(errorText == "") {
                border.BorderThickness = new Thickness(0);
                toolTip.Visibility = Visibility.Hidden;
            }
            else {
                border.BorderThickness = new Thickness(1);
                toolTip.Visibility = Visibility.Visible;
            }
        }

        public void SetFocus() {
            textBox.Focus();
        }

        #endregion Methods
    }
}
