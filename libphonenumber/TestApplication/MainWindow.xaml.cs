using libphonenumber;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.Instance;

        public MainWindow()
        {
            InitializeComponent();

            string germanNumberStr = "089123456";
            PhoneNumber number;
            try
            {
                number = PhoneNumberUtil.Instance.Parse(germanNumberStr, "DE");
            }
            catch (NumberParseException e)
            {
                throw;
            }

            if (number.IsValidNumber)
            {
                // Produces "+49 89 123456"
                Debug.WriteLine(number.Format(PhoneNumberUtil.PhoneNumberFormat.INTERNATIONAL));
                // Produces "089 123456"
                Debug.WriteLine(number.Format(PhoneNumberUtil.PhoneNumberFormat.NATIONAL));
                // Produces "+4989123456"
                Debug.WriteLine(number.Format(PhoneNumberUtil.PhoneNumberFormat.E164));

                // Produces "011 49 89 123456", the number when it is dialed in the United States.
                Debug.WriteLine(number.FormatOutOfCountryCallingNumber("US"));
            }

            AsYouTypeFormatter formatter = PhoneNumberUtil.Instance.GetAsYouTypeFormatter("DE");
            Debug.WriteLine(formatter.InputDigit('0'));  // Outputs "0"
            Debug.WriteLine(formatter.InputDigit('8'));  // Outputs "08"
            Debug.WriteLine(formatter.InputDigit('9'));  // Outputs "089"
            Debug.WriteLine(formatter.InputDigit('1'));  // Outputs "089 1"
            Debug.WriteLine(formatter.InputDigit('2'));  // Outputs "089 12"
            Debug.WriteLine(formatter.InputDigit('3'));  // Outputs "089 123"
            Debug.WriteLine(formatter.InputDigit('4'));  // Outputs "089 1234"
            Debug.WriteLine(formatter.InputDigit('5'));  // Outputs "089 12345"
            Debug.WriteLine(formatter.InputDigit('6'));  // Outputs "089 123456"
         
            // Outputs "Munich"
            Debug.WriteLine(number.GetDescriptionForNumber(Locale.ENGLISH));
            // Outputs "München"
            Debug.WriteLine(number.GetDescriptionForNumber(Locale.GERMAN));
            // Outputs "Munich"
            Debug.WriteLine(number.GetDescriptionForNumber(Locale.ITALIAN));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            string region = "DE";

            AsYouTypeFormatter asYouTypeFormatter = phoneNumberUtil.GetAsYouTypeFormatter(region);
            TextBox box = (TextBox)sender;

            var change = e.Changes.First();

            int position = change.AddedLength > 0 ? change.Offset : change.Offset - change.RemovedLength;
            int new_position = position;
            string text = "";

            int index = 0;
            foreach (var item in box.Text)
            {
                if (index++ == position)
                {
                    if (Char.IsDigit(item) || item == '+')
                    {
                        text = asYouTypeFormatter.InputDigitAndRememberPosition(item);
                        new_position = asYouTypeFormatter.RememberedPosition;
                    }
                }
                else
                    if (Char.IsDigit(item) || item == '+')
                        text = asYouTypeFormatter.InputDigit(item);
            }

            box.Text = text;
            box.SelectionStart = new_position;
            box.SelectionLength = 0;

            if (textBlock_valid != null && textBlock_type != null)
            {
                if (phoneNumberUtil.IsPossibleNumber(box.Text, region))
                {
                    var number = phoneNumberUtil.Parse(box.Text, region);
                    if (phoneNumberUtil.IsValidNumber(number))
                    {
                        textBlock_valid.Text = "VALID";
                        textBlock_type.Text = phoneNumberUtil.GetNumberType(number).ToString();

                    }
                    else
                    {
                        textBlock_valid.Text = "INVALID";
                        textBlock_type.Text = null;
                    }
                }
                else
                {
                    textBlock_valid.Text = "INVALID";
                    textBlock_type.Text = null;
                }
            }

            e.Handled = true;
            */
        }
    }
}
