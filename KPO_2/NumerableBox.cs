using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace KPO_2
{
    public class NumerableBox : TextBox
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            Regex r = new Regex("[^0-9]+");
            bool isChanged = false;

            if (Text.Length == 2 && !r.IsMatch(e.Text))
            {
                if (Int32.Parse(Text + e.Text) > 255)
                {
                    Text = "255";
                    isChanged = true;
                }
            }

            if (Text.Length >= 3 || isChanged)
            {
                e.Handled = true;
                Select(3, 0);
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
            else if ((e.Key == Key.C || e.Key == Key.X ) & SelectedText.Length == Text.Length)
            {
                e.Handled = true;
                Text = "0";
            }
            else if ((e.Key == Key.C || e.Key == Key.X) & SelectedText.Length != Text.Length)
            {
                e.Handled = true;
                Select(3 - SelectedText.Length,0);
            }
            else if (e.Key == Key.Delete && SelectedText.Length == Text.Length)
            {
                e.Handled = true;
                Text = "0";
            }
            else if (e.Key == Key.Back & Text.Length == 1 & Text == "0" || e.Key == Key.Delete & Text.Length == 1 & Text == "0")
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Back)
            {
                if (Text.Length == 1 || SelectedText.Length == Text.Length)
                {
                    Text = "0";
                }
            }
            
        }

        private void AddCopyingHandler(object sender, DataObjectCopyingEventArgs e)
        {
            if (SelectedText.Length == Text.Length)
            {
                e.CancelCommand();
                Text = "0";
            }
            else
            {
                e.CancelCommand();
                Select(3 - SelectedText.Length, 0);
            }

        }

        protected override void OnSelectionChanged(RoutedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            DataObject.AddPastingHandler(this, PasteHandler);
            DataObject.RemoveCopyingHandler(this, CopyHandler);
            DataObject.AddCopyingHandler(this, CopyHandler);
        }

        private void CopyHandler(object sender, DataObjectCopyingEventArgs e)
        {
            string CopyText = e.DataObject.GetData(typeof(string)) as string;
        }

        private void PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool textOK = false;
            Regex r = new Regex(@"[^0-9]+");
            string pasteText = "";

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string temp = "";
                pasteText = e.DataObject.GetData(typeof(string)) as string;
                string[] result = Regex.Split(pasteText, @"[^0-9]+");
                for (int i = 0; i < result.Length; i++)
                {
                    temp += result[i];
                }
                if (temp.Length == 2)
                {
                    if (Int32.Parse(Text.Substring(0,Text.Length-SelectedText.Length) + Int32.Parse(temp)) > 255)
                    {
                        Text = "255";
                    }
                    else
                    {
                        Text = Int32.Parse(Text.Substring(0, Text.Length - SelectedText.Length) + Int32.Parse(temp)).ToString();
                        //Select(3, 0);
                    }
                }

                if (temp.Length >= 3)
                {
                    if (Int32.Parse(temp.Substring(0,3)) > 255)
                    {
                        Text = "255";
                    }
                    else
                    {
                        Text = Int32.Parse(Text.Substring(0, Text.Length - SelectedText.Length) + Int32.Parse(temp)).ToString();
                        //Select(3, 0);
                    }
                }
            }
            e.CancelCommand();
        }
    }
}
