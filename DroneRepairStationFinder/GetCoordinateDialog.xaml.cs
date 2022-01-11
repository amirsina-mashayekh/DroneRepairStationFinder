using System;
using System.Windows;
using System.Windows.Controls;

namespace DroneRepairStationFinder
{
    /// <summary>
    /// Displays a dialog to enter coordinates.
    /// </summary>
    public partial class GetCoordinateDialog : Window
    {
        /// <summary>
        /// Initializes new instance of <see cref="GetCoordinateDialog"/>
        /// based on specified coordinate system.
        /// </summary>
        /// <param name="XYZ">
        /// Whether coordinate is in X,Y,Z system (<c>true</c>)
        /// or LAT,LONG,ALT system (<c>false</c>).
        /// </param>
        public GetCoordinateDialog(bool XYZ, Window owner)
        {
            InitializeComponent();
            Loaded += GetCoordinateDialog_Loaded;

            Owner = owner;
            if (XYZ)
            {
                LAT_X_Label.Text = "X:";
                LONG_Y_Label.Text = "Y:";
                ALT_Z_Label.Text = "Z:";
            }
        }

        /// <summary>
        /// Initializes new instance of <see cref="GetCoordinateDialog"/>
        /// based on specified coordinate system and with specified message and caption.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="string"/> that specifies the title bar caption to display.</param>
        /// <param name="XYZ">
        /// Whether coordinate is in X,Y,Z system (<c>true</c>)
        /// or LAT,LONG,ALT system (<c>false</c>).
        /// </param>
        public GetCoordinateDialog(string message, string caption, bool XYZ, Window owner) : this(XYZ, owner)
        {
            Title = caption;
            Message.Text = message;
        }

        /// <summary>
        /// Gets the latitude or X parameter.
        /// </summary>
        public double LAT_X => double.Parse(LAT_X_Input.Text);

        /// <summary>
        /// Gets the longitude or Y parameter.
        /// </summary>
        public double LONG_Y => double.Parse(LONG_Y_Input.Text);

        /// <summary>
        /// Gets the altitude or Z parameter.
        /// </summary>
        public double ALT_Z => double.Parse(ALT_Z_Input.Text);

        private void GetCoordinateDialog_Loaded(object sender, RoutedEventArgs e)
        {
            LAT_X_Input.Focus();
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            try { _ = LAT_X; }
            catch (FormatException)
            {
                error = true;
                LAT_X_Input.Focus();
            }

            try { _ = LONG_Y; }
            catch (FormatException)
            {
                error = true;
                LONG_Y_Input.Focus();
            }

            try { _ = ALT_Z; }
            catch (FormatException)
            {
                error = true;
                ALT_Z_Input.Focus();
            }

            if (error)
            {
                MessageBox.Show("Value of one or more fields is invalid.\nPlease enter decimal numbers.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}