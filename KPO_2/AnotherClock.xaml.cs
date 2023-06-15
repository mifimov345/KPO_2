using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace KPO_2
{
    /// <summary>
    /// Логика взаимодействия для AnotherClock.xaml
    /// </summary>
    public partial class AnotherClock : UserControl, INotifyPropertyChanged
    {
        private DispatcherTimer timer;

        Dictionary<int, string> nums = new Dictionary<int, string>()
        {
            {0, "M 5 5 L 85 5 M 85 8 L 85 189 M 85 192 L 5 192 M 5 189 L 5 8" },
            {1, "M 85 8 L 85 189" },
            {2, "M 5 5 L 85 5 M 85 8 L 85 97 M 85 192 L 5 192 M 5 103 L 5 189 M 8 100 L 83 100" },
            {3, "M 5 5 L 85 5 M 85 8 L 85 97 M 85 192 L 5 192 M 85 103 L 85 189 M 8 100 L 83 100" },
            {4, "M 5 8 L 5 98 M 85 8 L 85 97 M 85 103 L 85 189 M 8 100 L 83 100" },
            {5, "M 5 5 L 85 5 M 85 103 L 85 189 M 85 192 L 5 192 M 5 98 L 5 8 M 8 100 L 83 100" },
            {6, "M 5 5 L 85 5 M 85 103 L 85 189 M 85 192 L 5 192 M 5 189 L 5 8 M 8 100 L 83 100" },
            {7, "M 5 5 L 85 5 M 85 8 L 85 189" },
            {8, "M 5 5 L 85 5 M 85 8 L 85 189 M 85 192 L 5 192 M 5 189 L 5 8 M 8 100 L 82 100" },
            {9, "M 5 5 L 85 5 M 85 8 L 85 189 M 85 192 L 5 192 M 5 99 L 5 8 M 8 100 L 82 100" },
        };

        public event PropertyChangedEventHandler PropertyChanged;
        public AnotherClock()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public string SecondOne
        {
            get { return _secondOne; }
            set 
            {
                _secondOne= value;
                OnPropertyChanged("SecondOne");
            }
        }
        public string SecondTwo 
        {
            get { return _secondTwo; }
            set
            {
                _secondTwo = value;
                OnPropertyChanged("SecondTwo");
            }
        }
        public string MinuteOne
        {
            get { return _minuteOne; }
            set
            {
                _minuteOne = value;
                OnPropertyChanged("MinuteOne");
            }
        }
        public string MinuteTwo
        {
            get { return _minuteTwo; }
            set
            {
                _minuteTwo = value;
                OnPropertyChanged("MinuteTwo");
            }
        }
        public string HourOne
        {
            get { return _hourOne; }
            set
            {
                _hourOne = value;
                OnPropertyChanged("HourOne");
            }
        }
        public string HourTwo
        {
            get { return _hourTwo; }
            set
            {
                _hourTwo = value;
                OnPropertyChanged("HourTwo");
            }
        }

        private string _secondOne;
        private string _secondTwo;
        private string _minuteOne;
        private string _minuteTwo;
        private string _hourOne;
        private string _hourTwo;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int second2 = ((DateTime.Now.Second + DateTime.Now.Millisecond / 1000) % 10);
            SecondTwo = nums[second2];
            int second1 = ((int)((DateTime.Now.Second + DateTime.Now.Millisecond / 1000)) / 10);
            SecondOne = nums[second1];
            int minute2 = (DateTime.Now.Minute  % 10);
            MinuteTwo= nums[minute2];
            int minute1 = ((int)(DateTime.Now.Minute  / 10));
            MinuteOne = nums[minute1];
            int hour2 = (DateTime.Now.Hour  % 10);
            HourTwo= nums[hour2];
            int hour1 = ((int)(DateTime.Now.Hour  / 10));
            HourOne = nums[hour1];
        }
    }
}
