using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace wpf_adfgvx
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

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string[] kodTabla = File.ReadAllLines(dialog.FileName);
                lblKodtabla.Content = "";
                foreach (string sor in kodTabla)
                {
                    foreach (char ch in sor)
                    {
                        lblKodtabla.Content += ch + "  ";
                    }
                    lblKodtabla.Content += "\n";
                }

                lstEredmeny.Items.Clear();

                if (kodTabla.Length != 6)
                {
                    lstEredmeny.Items.Add("Hiba a mátrix méretében");
                }
                else
                {
                    foreach (string sor in kodTabla)
                    {
                        if (sor.Length != 6)
                        {
                            lstEredmeny.Items.Add("Hiba a mátrix méretében");
                            break;
                        }
                    }
                }

                foreach (string sor in kodTabla)
                {
                    foreach (char ch in sor)
                    {
                        if (!((ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9')))
                        {
                            lstEredmeny.Items.Add(string.Format("Hibás karakter ({0}) van a mátrixban", ch));
                        }
                    }
                }

                Dictionary<char, int> stat = new Dictionary<char, int>();
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    stat.Add(ch, 0);
                }
                for (char ch = '0'; ch <= '9'; ch++)
                {
                    stat.Add(ch, 0);
                }

                foreach (string sor in kodTabla)
                {
                    foreach (char ch in sor)
                    {
                        if ((ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9'))
                        {
                            stat[ch]++;
                        }
                    }
                }
                foreach (var item in stat)
                {
                    if (item.Value != 1)
                    {
                        lstEredmeny.Items.Add(string.Format("A(z) {0} karakter {1}x szerepel a mátrixban",
                                              item.Key, item.Value));
                    }
                }

                if (lstEredmeny.Items.Count == 0)
                {
                    lstEredmeny.Items.Add("A mátrix megfelelő!");
                }
            }
        }
    }
}