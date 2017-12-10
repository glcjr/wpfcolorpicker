using System;
using System.Windows;
using System.Windows.Media;

namespace WPFColorPickerLib
{
    /// <summary>
    /// Holds a ColorPicker control, and exposes the ColorPicker SelectedColor.
    /// 
    /// Enhanced by Mark Treadwell (1/2/10) to include:
    ///  - Added ability to set ColorPicker initial color via constructor or property
    ///  - Use of Button's IsDefault and IsCancel properties
    ///  - Setting tab behavior
    /// </summary>
    public partial class ColorDialog : Window
    {
        #region Constructors

        /// <summary>
        /// Default constructor initializes to Black.
        /// </summary>
        public ColorDialog()
          : this(Colors.Black)
        { }

        /// <summary>
        /// Constructor with an initial color.
        /// </summary>
        /// <param name="initialColor">Color to set the ColorPicker to.</param>
        public ColorDialog(Color initialColor)
        {
            InitializeComponent();
            colorPicker.InitialColor = initialColor;
        }
        
        /// <summary>
        /// Constructor with a Brush as a Parameter to get the color from
        /// Added by Beaver Valley Software http://www.beavervalleysoftware.com 6-4-2016
        /// </summary>
        /// <param name="initialColor"></param>
        public ColorDialog(Brush initialColor)
        {
            BrushColor = initialColor;
        }
        /// <summary>
        /// Constructor with a string that should contain the Hex value to get the initial color from
        /// Added by Beaver Valley Software 6-2-2016
        /// </summary>
        /// <param name="hexcolor"></param>
        public ColorDialog(string hexcolor)
        {
            HexColor = hexcolor;
        }
        /// <summary>
        /// Constructor with a System.Drawing.Color as a parameter to get the initial color from
        /// Added by Beaver Valley Software 6-3-2016
        /// </summary>
        /// <param name="color"></param>
        public ColorDialog(System.Drawing.Color color)
        {
            SDNetColor = color;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets/sets the ColorDialog color.
        /// </summary>
        public Color SelectedColor
        {
            get { return colorPicker.SelectedColor; }
            set { colorPicker.InitialColor = value; }
        }
        /// <summary>
        /// Gets/Sets the color
        /// added by Beaver Valley Software 6-1-16
        /// </summary>
        public string HexColor
        {
            get { return ToHexColor(colorPicker.SelectedColor, false); }
            set { colorPicker.InitialColor = FromHexColor(value); }
        }
        /// <summary>
        /// get/set the color
        /// Added by Beaver Valley Software 6-4-16
        /// </summary>
        public Brush BrushColor
        {
            get
            {
                var converter = new System.Windows.Media.BrushConverter();
                return (Brush)converter.ConvertFromString(HexColor);
            }
            set
            {
                var converter = new System.Windows.Media.BrushConverter();
                HexColor = converter.ConvertToString(value);
            }
        }
        /// <summary>
        /// get/sets the color of the dialog
        /// Added by Beaver Valley Software 6-2-16
        /// </summary>
        public System.Drawing.Color SDNetColor
        {
            get
            {
                return System.Drawing.ColorTranslator.FromHtml(HexColor);
            }
            set
            {
                HexColor = System.Drawing.ColorTranslator.ToHtml(value);
            }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Close ColorDialog, accepting color selection.
        /// </summary>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        ///  Close ColorDialog, rejecting color selection.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion
        #region private converters
        /// <summary>
        /// Converts from a System.Windows.Media brush to a hex color
        /// added by Beaver Valley Software
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alphaChannel"></param>
        /// <returns></returns>
        public static string ToHexColor(Color color, bool alphaChannel)
        {
            return String.Format("#{0}{1}{2}{3}",
                                 alphaChannel ? color.A.ToString("X2") : String.Empty,
                                 color.R.ToString("X2"),
                                 color.G.ToString("X2"),
                                 color.B.ToString("X2"));
        }
        /// <summary>
        /// Converts a hex color string to a System.Windows.Media. Color
        /// added by Beaver Valley Software
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color FromHexColor(string hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }
        #endregion
    }
}
