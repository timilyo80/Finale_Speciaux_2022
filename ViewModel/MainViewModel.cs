using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApp1.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public DispatcherTimer timer;
        private int currentProgress;
        public int CurrentProgress
        {
            get { return currentProgress; }
            set
            {
                if (currentProgress != value)
                {
                    currentProgress = value;
                    OnPropertyChanged();
                }
            }
        }
        private int maxProgress;
        public int MaxProgress
        {
            get { return maxProgress; }
            set
            {
                if (maxProgress != value)
                {
                    maxProgress = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, args) =>
            {
                CurrentProgress ++;
            };
        }

        
    }
}
