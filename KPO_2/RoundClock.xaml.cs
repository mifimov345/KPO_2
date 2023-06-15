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
    public partial class RoundClock : UserControl, INotifyPropertyChanged
    {
        private DispatcherTimer timer;
        public RoundClock()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100); // Update every 10 milliseconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double SecAngle
        {
            get { return _secAngle; }
            set
            {
                _secAngle = value;
                OnPropertyChanged("SecAngle");
            }
        }

        private double _secAngle;
        private double _minAngle;
        private double _hourAngle;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double seconds = DateTime.Now.Second + DateTime.Now.Millisecond / 1000.0;
            SecAngle = seconds / 60.0 * 360.0;

            double minutes = DateTime.Now.Minute + seconds / 60.0;
            MinAngle = minutes / 60.0 * 360.0;

            double hours = DateTime.Now.Hour % 12 + minutes / 60.0;
            HourAngle = hours / 12.0 * 360.0;
        }
        public double MinAngle
        {
            get { return _minAngle; }
            set
            {
                _minAngle = value;
                OnPropertyChanged("MinAngle");
            }
        }

        public double HourAngle
        {
            get { return _hourAngle; }
            set
            {
                _hourAngle = value;
                OnPropertyChanged("HourAngle");
            }
        }
    }
}
