using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KPO_2
{
    public class HexBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            Regex r = new Regex("[^0-9A-Fa-f]+");
            if (Text.Length == 0)
            {
                Text = "#";
                Select(Text.Length, 0);
            }
            else if (Text.Length > 0 && Text[0] != '#')
            {
                Text = "#";
                Select(Text.Length, 0);
            }
            if (Text.Length >= 7)
            {
                e.Handled = true;
                return;
            }

            e.Handled = r.IsMatch(e.Text);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Back)
            {
                if (Text.Length == 1 || SelectedText.Length == Text.Length)
                {
                    Text = "#000000";
                }
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            DataObject.AddPastingHandler(this,PasteHandler);
        }

        private void PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool textOK = false;
            Regex r = new Regex("[^0-9A-Fa-f]+");
            string pasteText = "";

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string temp = "";
                pasteText = e.DataObject.GetData(typeof(string)) as string;
                string[] result = Regex.Split(pasteText, "[^0-9A-Fa-f,#]+");
                for (int i = 0; i < result.Length; i++)
                {
                    temp += result[i];
                }
                Text = '#' + temp;
            }
            e.CancelCommand();
        }
    }
}
