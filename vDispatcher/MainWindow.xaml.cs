using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace vDispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public ObservableCollection<Incident> actualInc { get; set; }
        public List<Incident> incData = new List<Incident> 
        {
            new Incident(IncType.wypadek, 2, 4, 
                "Czołowe zderzenie ciężarówki i dwóch samochodów osobowych.", 
                new DispatcherTimer()),
            new Incident(IncType.wypadek, 2, 0,
                "Stłuczka dwóch samochodów osobowych.",
                new DispatcherTimer()),
            new Incident(IncType.pozar, 0, 0, 
                "Pożar samochodu.", 
                new DispatcherTimer()),
            new Incident(IncType.pozar, 5, 1,
                "Pożar mieszkania w bloku.",
                new DispatcherTimer()),
            new Incident(IncType.kradziez, 0, 0, 
                "Włamanie do sklepu spożywczego.", 
                new DispatcherTimer()),
            new Incident(IncType.transport, 0, 0, 
                "Pilny transport krwi.", 
                new DispatcherTimer()),
            new Incident(IncType.pobicie, 10, 0, 
                "Bójka pseudokibiców na stadionie", 
                new DispatcherTimer())
        };

        public List<Police> policeData = new List<Police>
        {
            new Police(PoliceType.drogowka, 
                new List<IncType> { IncType.wypadek}, 
                120, 2),
            new Police(PoliceType.kryminalna, 
                new List<IncType> { IncType.pobicie, IncType.zabojstwo, IncType.kradziez}, 
                180, 2)
        };

        public List<FireBrigade> fireBrigadeData = new List<FireBrigade>
        {
            new FireBrigade (FireBrigadeType.drogowa, 
                new List<IncType> { IncType.wypadek}, 
                150, 200),
            new FireBrigade (FireBrigadeType.gasnicza, 
                new List<IncType> { IncType.pozar},
                160, 1000)
        };

        public List<Emergency> emergencyData = new List<Emergency>
        {
            new Emergency (EmergencyType.ratunkowa, 
                new List<IncType> { IncType.wypadek, IncType.pobicie, IncType.pozar}, 
                100, 2),
            new Emergency (EmergencyType.transportowa, 
                new List<IncType> { IncType.transport}, 
                160, 2)
        };

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public DispatcherTimer endTimer = new DispatcherTimer();
        public bool integrityCheck = true;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            actualInc = new ObservableCollection<Incident>();

            this.listBoxPolice.ItemsSource = Enum.GetValues(typeof(PoliceType));
            this.listBoxFireBrigade.ItemsSource = Enum.GetValues(typeof(FireBrigadeType));
            this.listBoxEmergency.ItemsSource = Enum.GetValues(typeof(EmergencyType));

            startDispatcherTimer();
        }

        public void startDispatcherTimer()
        {
            Random newIncTime = new Random();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            endTimer.Tick += endTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, newIncTime.Next(10,10));
            endTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            endTimer.Start();
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Random randomID = new Random();
            int newIncID = randomID.Next(0, 5);
            Incident incident = new Incident(incData[newIncID].incType, 
                incData[newIncID].woundedNumber, 
                incData[newIncID].deadNumber, 
                incData[newIncID].description, 
                incData[newIncID].incTimer);
            actualInc.Add(incident);
        }

        public void endTimer_Tick(object sender, EventArgs e)
        {
            int a = actualInc.Count;
            for (int i = 0; i < a - 1; i++)
            {
                actualInc[i].endTimer = actualInc[i].endTimer - 1;
            }
        }

        private void listBoxInc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                incDescTextBlock.Text =
                    "Typ: " + actualInc[listBoxInc.SelectedIndex].incType
                    + "\nOpis: " + actualInc[listBoxInc.SelectedIndex].description
                    + "\nCzas [s]: " + actualInc[listBoxInc.SelectedIndex].endTimer
                    + "\nLiczba rannych: " + actualInc[listBoxInc.SelectedIndex].woundedNumber
                    + "\nLiczba zabitych: " + actualInc[listBoxInc.SelectedIndex].deadNumber;
            }
            catch (Exception)
            {
                incDescTextBlock.Text = " ";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            integrityCheck = true;
            try
            {
                if (actualInc[listBoxInc.SelectedIndex].endTimer
                    >= policeData[listBoxPolice.SelectedIndex].actionTimer
                    && actualInc[listBoxInc.SelectedIndex].endTimer
                    >= fireBrigadeData[listBoxFireBrigade.SelectedIndex].actionTimer
                    && actualInc[listBoxInc.SelectedIndex].endTimer
                    >= emergencyData[listBoxEmergency.SelectedIndex].actionTimer
                    )
                {
                    actualInc.Remove(actualInc[listBoxInc.SelectedIndex]);
                }
                else
                {
                    MessageBox.Show("Za malo czasu na dotarcie");
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                if (listBoxInc.SelectedIndex == -1)
                {
                    MessageBox.Show("Zaznacz incydent");
                    integrityCheck = false;
                }
                if (listBoxPolice.SelectedIndex == -1 
                    && policeDisableCheckBox.IsChecked == false)
                {
                    MessageBox.Show("Zaznacz policję");
                    integrityCheck = false;
                }
                if (listBoxFireBrigade.SelectedIndex == -1 
                    && fireDisableCheckBox.IsChecked == false)
                {
                    MessageBox.Show("Zaznacz straz");
                    integrityCheck = false;
                }
                if (listBoxEmergency.SelectedIndex == -1 
                    && emergencyDisableCheckBox.IsChecked == false)
                {
                    MessageBox.Show("Zaznacz pogotowie");
                    integrityCheck = false;
                }
                if (listBoxPolice.SelectedIndex != -1)
                {
                    if (policeData[listBoxPolice.SelectedIndex].capabilities.
                        Contains(actualInc[listBoxInc.SelectedIndex].incType)
                        && policeDisableCheckBox.IsChecked == false )
                    {
                        integrityCheck = true;
                    }
                    else
                    {
                        MessageBox.Show("Wybrales zly typ policji");
                        integrityCheck = false;
                    }
                }
                if (listBoxFireBrigade.SelectedIndex != -1)
                {
                    if (fireBrigadeData[listBoxFireBrigade.SelectedIndex].capabilities.
                        Contains(actualInc[listBoxInc.SelectedIndex].incType)
                        && fireDisableCheckBox.IsChecked == false)
                    {
                        integrityCheck = true;
                    }
                    else
                    {
                        MessageBox.Show("Wybrales zly typ strazy");
                        integrityCheck = false;
                    }
                }
                if (listBoxEmergency.SelectedIndex != -1)
                {
                    if (emergencyData[listBoxEmergency.SelectedIndex].capabilities.
                        Contains(actualInc[listBoxInc.SelectedIndex].incType)
                        && emergencyDisableCheckBox.IsChecked == false )
                    {
                        integrityCheck = true;
                    }
                    else
                    {
                        MessageBox.Show("Wybrales zly typ pogotowia");
                        integrityCheck = false;
                    }
                }
                if (integrityCheck == true)
                {
                    actualInc.Remove(actualInc[listBoxInc.SelectedIndex]);
                }
            }        
        }

        private void listBoxPolice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string policeCaps = " ";
                for (int k = 0; 
                    k < policeData[listBoxPolice.SelectedIndex].capabilities.Count; 
                    k++)
                {
                    policeCaps = policeCaps + "\n" 
                    + policeData[listBoxPolice.SelectedIndex].capabilities[k];
                }
                policeDescTextBlock.Text = "Typ: " 
                + policeData[listBoxPolice.SelectedIndex].policeType + "\n"
                + "Możliwości: " + policeCaps + "\n"
                + "Liczba pasażerów: " 
                + policeData[listBoxPolice.SelectedIndex].passengersNumber + "\n"
                + "Czas [s]: " + policeData[listBoxPolice.SelectedIndex].actionTimer + "\n";
            }
            catch (Exception)
            {
                policeDescTextBlock.Text = " ";
            }
        }

        private void listBoxFireBrigade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fireCaps = " ";
                for (int k = 0; 
                    k < fireBrigadeData[listBoxFireBrigade.SelectedIndex].capabilities.Count; 
                    k++)
                {
                    fireCaps = fireCaps + "\n"
                    + fireBrigadeData[listBoxFireBrigade.SelectedIndex].capabilities[k];
                }
                fireDescTextBlock.Text = "Typ: " 
                + fireBrigadeData[listBoxFireBrigade.SelectedIndex].fireBrigadeType + "\n"
                + "Możliwości: " + fireCaps + "\n"
                + "Pojemność zbiornika [L]: "
                + fireBrigadeData[listBoxFireBrigade.SelectedIndex].tankCapacity + "\n"
                + "Czas [s]: " + fireBrigadeData[listBoxFireBrigade.SelectedIndex].actionTimer + "\n";
            }
            catch (Exception)
            {
                fireDescTextBlock.Text = " ";
            }
        }

        private void listBoxEmergency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string emergencyCaps = " ";
                for (int k = 0; 
                    k < emergencyData[listBoxEmergency.SelectedIndex].capabilities.Count; 
                    k++)
                {
                    emergencyCaps = emergencyCaps + "\n"
                    + emergencyData[listBoxEmergency.SelectedIndex].capabilities[k];
                }
                emergencyDescTextBlock.Text = "Typ: " 
                + emergencyData[listBoxEmergency.SelectedIndex].emergencyType + "\n"
                + "Możliwości: " + emergencyCaps + "\n"
                + "Ilość noszy: " 
                + emergencyData[listBoxEmergency.SelectedIndex].stretcherNumber + "\n"
                + "Czas [s]: " + emergencyData[listBoxEmergency.SelectedIndex].actionTimer + "\n";
            }
            catch (Exception)
            {
                emergencyDescTextBlock.Text = " ";
            }
        }

        private void policeDisableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            listBoxPolice.SelectedIndex = -1;
            listBoxPolice.IsEnabled = false;
            policeListLabel.IsEnabled = false;
            policeDescLabel.IsEnabled = false;
            policeDescTextBlock.IsEnabled = false;
        }
        private void policeDisableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {            
            listBoxPolice.IsEnabled = true;
            policeListLabel.IsEnabled = true;
            policeDescLabel.IsEnabled = true;
            policeDescTextBlock.IsEnabled = true;
        }
        private void fireDisableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            listBoxFireBrigade.SelectedIndex = -1;
            listBoxFireBrigade.IsEnabled = false;
            fireListLabel.IsEnabled = false;
            fireDescLabel.IsEnabled = false;
            fireDescTextBlock.IsEnabled = false;
        }
        private void fireDisableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {            
            listBoxFireBrigade.IsEnabled = true;
            fireListLabel.IsEnabled = true;
            fireDescLabel.IsEnabled = true;
            fireDescTextBlock.IsEnabled = true;
        }
        private void emergencyDisableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            listBoxEmergency.SelectedIndex = -1;
            listBoxEmergency.IsEnabled = false;
            emergencyListLabel.IsEnabled = false;
            emergencyDescLabel.IsEnabled = false;
            emergencyDescTextBlock.IsEnabled = false;
        }
        private void emergencyDisableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {            
            listBoxEmergency.IsEnabled = true;
            emergencyListLabel.IsEnabled = true;
            emergencyDescLabel.IsEnabled = true;
            emergencyDescTextBlock.IsEnabled = true;
        }
    }
}