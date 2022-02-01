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

namespace alap_engine_VLSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        class IP
        {
            public double ElsoOktett;
            public double MasodikOktett;
            public double HarmadikOktett;
            public double NegyedikOktett;
            public double
                Maszk;
            public int hostSzam;
            public IP(string ElsoOktett, string MasodikOktett, string HarmadikOktett, string NegyedikOktett, string Maszk, string hostSzam)
            {
                this.ElsoOktett = byte.Parse(ElsoOktett);
                this.MasodikOktett = byte.Parse(MasodikOktett);
                this.HarmadikOktett = byte.Parse(HarmadikOktett);
                this.NegyedikOktett = byte.Parse(NegyedikOktett);
                this.Maszk = byte.Parse(Maszk);
                this.hostSzam = int.Parse(hostSzam);
            }
            public string Kiiratas { get => $"{ElsoOktett}.{MasodikOktett}.{HarmadikOktett}.{NegyedikOktett} / {Maszk}"; }
        }
        private void egyenloResz_Checked(object sender, RoutedEventArgs e)
        {
            numberOfHostLabel.Visibility = Visibility.Visible;
            numberOfHostL.Visibility = Visibility.Visible;
            array.Visibility = Visibility.Visible;
            arrayTb.Visibility = Visibility.Visible;
            numberOfHostK.Visibility = Visibility.Hidden;
            numberOfHostKTb.Visibility = Visibility.Hidden;
            hostName.Visibility = Visibility.Hidden;
            hostNameLable.Visibility = Visibility.Hidden;
            hostNameLb.Visibility = Visibility.Hidden;
            hostNameTb.Visibility = Visibility.Hidden;
            hozzadasGomb.Visibility = Visibility.Hidden;
        }
        private void kulonbozoMeret_Checked(object sender, RoutedEventArgs e)
        {
            numberOfHostLabel.Visibility = Visibility.Hidden;
            numberOfHostL.Visibility = Visibility.Hidden;
            array.Visibility = Visibility.Hidden;
            arrayTb.Visibility = Visibility.Hidden;
            numberOfHostK.Visibility = Visibility.Visible;
            numberOfHostKTb.Visibility = Visibility.Visible;
            hostName.Visibility = Visibility.Visible;
            hostNameLable.Visibility = Visibility.Visible;
            hostNameLb.Visibility = Visibility.Visible;
            hostNameTb.Visibility = Visibility.Visible;
            hozzadasGomb.Visibility = Visibility.Visible;
        }

        private void generalas_Click(object sender, RoutedEventArgs e)
        {
            if (egyenloResz.IsChecked == true)
            {
                IP generalasIP = new IP(elsoOktett.Text, masodikOktett.Text, harmadikOktett.Text, negyedikOktett.Text, maszk.Text, "0");
                int hatvany = 1;
                int seged = 2;
                while (Math.Pow(seged, hatvany) <= int.Parse(arrayTb.Text))
                {
                    hatvany++;
                }
                int keszMaszk = 32 - hatvany;
                string kiirasok = $"Host neve\tHálózati cím\tKiosztható címek\tSzórási cím\tHálózati maszk\tHálózati maszk decimális formában\n";
                teszt.Content = kiirasok;
                string halozatiCim = generalasIP.Kiiratas;
                //double[] kioszthatoCimek = { int.Parse(halozatiCim.Split('.')[3] + 1), int.Parse(halozatiCim.Split('.')[3]) + Math.Pow(seged, hatvany) - 2 };
                //double szorasiCim = int.Parse(halozatiCim.Split('.')[3]) + Math.Pow(seged, hatvany) - 1;
                for (int i = 0; i < int.Parse(arrayTb.Text); i++)
                {
                    kiirasok += $"Host{i + 1}\t{halozatiCim}\t";
                    generalasIP.NegyedikOktett = int.Parse(halozatiCim.Split('.')[3] + 1);
                    kiirasok += $"{generalasIP.Kiiratas}\t";
                    generalasIP.NegyedikOktett = int.Parse(halozatiCim.Split('.')[3]) + Math.Pow(seged, hatvany) - 2;
                    kiirasok += $"{generalasIP.Kiiratas}\t";
                    generalasIP.NegyedikOktett = int.Parse(halozatiCim.Split('.')[3]) + Math.Pow(seged, hatvany) - 1;
                    kiirasok += $"{generalasIP.Kiiratas}\t\\{keszMaszk}\t{Decimalis(keszMaszk)}\n";
                }
            }
            else
            {

            }
        }
        private string Decimalis(int maszk)
        {
            string decimalisMaszk;
            for (int i = 1; i < 5; i++)
            {
                int[] oktettMaszk = { 0, 0, 0, 0 }; 
                if(maszk / 8 == 3)
                {
                    int maradekMaszk = maszk - maszk % 8;
                    oktettMaszk[0] = 255;
                    oktettMaszk[1] = 255;
                    oktettMaszk[2] = 255;
                    oktettMaszk[3] = Vizsgalat(maradekMaszk);
                }
                    
            }
            decimalisMaszk = $"{}";
            return decimalisMaszk;
        }

        private int Vizsgalat(int vizsgalando)
        {
                switch (vizsgalando)
                {
                        case 1: return 128; 
                            break;
                        case 2: return 192; 
                            break;
                        case 3: return 224; 
                            break;
                        case 4: return 240; 
                            break;
                        case 5: return 248; 
                            break;
                        case 6: return 252; 
                            break;
                        case 7: return 254; 
                            break;
                        default:
                            break;
                }
        }
    }
}
