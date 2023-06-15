using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace KPO_2
{
    public partial class Palette : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private SolidColorBrush selectedColor;
        public SolidColorBrush SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                OnPropertyChanged();
            }
        }
        private string r, g, b;
        private string hex;
        public string R
        {
            get => r;
            set
            {
                if (r != value)
                {
                    r = value;
                    UpdateHex();
                    UpdateColor();
                    OnPropertyChanged();
                }
            }
        }
        public string G
        {
            get => g;
            set
            {
                if (g != value)
                {
                    g = value;
                    UpdateHex();
                    UpdateColor();
                    OnPropertyChanged();
                }
            }
        }
        public string B
        {
            get => b;
            set
            {
                if (b != value)
                {
                    b = value;
                    UpdateHex();
                    UpdateColor();
                    OnPropertyChanged();
                }
            }
        }

        public string Hex
        {
            get => hex;
            set
            {
                if (hex != value)
                {
                    hex = value;
                    UpdateRGB();
                    UpdateColor();
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateHex()
        {
            string RR = "";
            string GG = "";
            string BB = "";
            if (!string.IsNullOrEmpty(R))
                RR = string.Format($"{Convert.ToByte(R):X2}");
            if (!string.IsNullOrEmpty(G))
                GG = string.Format($"{Convert.ToByte(G):X2}");
            if (!string.IsNullOrEmpty(B))
                BB = string.Format($"{Convert.ToByte(B):X2}");

            Hex = string.Format($"#{RR}{GG}{BB}");
        }

        private void UpdateRGB()
        {
            string hexString = Hex;
            if (hexString.StartsWith("#"))
            {
                hexString = hexString.Substring(1);
            }
            if (hexString.Length == 6)
            {
                R = byte.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber).ToString();
                G = byte.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber).ToString();
                B = byte.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber).ToString();
            }
        }

        private void UpdateColor()
        {
            SelectedColor = new SolidColorBrush(Color.FromRgb(Convert.ToByte(R), Convert.ToByte(G), Convert.ToByte(B)));
            ColorVisualiser.Background = SelectedColor;
        }
        public Palette()
        {
            InitializeComponent();
        }
    }
}
