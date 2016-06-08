using System;
using System.Collections.Generic;
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
using System.Timers;
using System.Windows.Threading;

namespace vDispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Incident> incData = new List<Incident> 
        {
            new Incident(IncType.wypadek, 3, 4, "aaa", new DispatcherTimer()),
            new Incident(IncType.pozar, 3, 4, "bbb", new DispatcherTimer()),
            new Incident(IncType.kradziez, 3, 4, "ccc", new DispatcherTimer()),
            new Incident(IncType.transport, 3, 4, "ddd", new DispatcherTimer()),
            new Incident(IncType.pobicie, 3, 4, "eee", new DispatcherTimer())
        };

        public List<Police> policeData = new List<Police>
        {
            new Police(PoliceType.drogowka, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 2),
            new Police(PoliceType.kryminalna, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 2)
        };

        public List<FireBrigade> fireBrigadeData = new List<FireBrigade>
        {
            new FireBrigade (FireBrigadeType.drogowa, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 200),
            new FireBrigade (FireBrigadeType.gasnicza, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 1000)
        };

        public List<Emergency> emergencyData = new List<Emergency>
        {
            new Emergency (EmergencyType.ratunkowa, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 2),
            new Emergency (EmergencyType.transportowa, new List<IncType> { IncType.wypadek}, new DispatcherTimer(), 2)
        };

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            foreach (Police police in policeData)
            {
                listBoxPolice.Items.Add(police.policeType);
            }
            foreach (FireBrigade fireBrigade in fireBrigadeData)
            {
                listBoxFireBrigade.Items.Add(fireBrigade.fireBrigadeType);
            }
            foreach (Emergency emergency in emergencyData)
            {
                listBoxEmergency.Items.Add(emergency.emergencyType);
            }
            startDispatcherTimer();

                 
        }

        public void startDispatcherTimer()
        {
            Random newIncTime = new Random();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, newIncTime.Next(2, 5));
            dispatcherTimer.Start();
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Random randomID = new Random();
            int newIncID = randomID.Next(0, 5) % 5;
            listBoxInc.Items.Add(incData[newIncID].incType);
        }

        private void listBoxInc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxInc.SelectedIndex == -1)
            {
                listBoxInc.SelectedIndex = 0;
            }

            incDescTextBlock.Text = incData[listBoxInc.SelectedIndex % 5].description;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (incData[listBoxInc.SelectedIndex % 5].incTimer.Interval 
                <= policeData[listBoxPolice.SelectedIndex].actionTimer.Interval
                && incData[listBoxInc.SelectedIndex % 5].incTimer.Interval
                <= fireBrigadeData[listBoxFireBrigade.SelectedIndex].actionTimer.Interval
                && incData[listBoxInc.SelectedIndex % 5].incTimer.Interval
                <= emergencyData[listBoxEmergency.SelectedIndex].actionTimer.Interval
                )
            {
                listBoxInc.Items.Remove(listBoxInc.SelectedItem);
            }

            else
            {
                incDescTextBlock.Text = "ducpa";
            }
            

        }

        private void listBoxPolice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            policeDescTextBlock.Text = "Typ: " + policeData[listBoxPolice.SelectedIndex].policeType + "\n"
            + "Możliwości: " + policeData[listBoxPolice.SelectedIndex].capabilities[0] + "\n"
            + "Liczba pasażerów: " + policeData[listBoxPolice.SelectedIndex].passengersNumber + "\n"
            + "Czas: " + policeData[listBoxPolice.SelectedIndex].actionTimer.Interval + "\n";
        }

        private void listBoxFireBrigade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fireDescTextBlock.Text = "Typ: " + fireBrigadeData[listBoxFireBrigade.SelectedIndex].fireBrigadeType + "\n"
            + "Możliwości: " + fireBrigadeData[listBoxFireBrigade.SelectedIndex].capabilities[0] + "\n"
            + "Pojemność zbiornika [L]: " + fireBrigadeData[listBoxFireBrigade.SelectedIndex].tankCapacity + "\n"
            + "Czas: " + fireBrigadeData[listBoxFireBrigade.SelectedIndex].actionTimer.Interval + "\n";
        }

        private void listBoxEmergency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            emergencyDescTextBlock.Text = "Typ: " + emergencyData[listBoxEmergency.SelectedIndex].emergencyType + "\n"
            + "Możliwości: " + emergencyData[listBoxEmergency.SelectedIndex].capabilities[0] + "\n"
            + "Ilość noszy: " + emergencyData[listBoxEmergency.SelectedIndex].stretcherNumber + "\n"
            + "Czas: " + emergencyData[listBoxEmergency.SelectedIndex].actionTimer.Interval + "\n";

        }
    }
}
