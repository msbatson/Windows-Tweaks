using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace WindowsTweaks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SelectionBox.SelectedIndex = 0;

            //Incomplete, hiding until it is finished
            SelectionBox.Items.Remove(SelectionBox.Items[1]);

            WindowsColors.ReadCurrentRegistrySettings();

            if (!IsAdministrator())
            {
                btnCreateAeroLite.IsEnabled = false;
            }
        }

        //Some tweaks may require admin, rather that forcing the application to run as an admin I'd rather just lock it down and
        //not show those options
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionBox.SelectedIndex.Equals("Window Colors"))
            {
                WindowsColorsTab.Visibility = Visibility.Visible;
                WindowsThemesTab.Visibility = Visibility.Hidden;
            }
            else if (SelectionBox.SelectedItem.Equals("Windows Themes"))
            {
                WindowsColorsTab.Visibility = Visibility.Hidden;
                WindowsThemesTab.Visibility = Visibility.Visible;
            }
        }



        private void ColorTile_Click(object sender, RoutedEventArgs e)
        {
            Button[] ColorButtons = { Tile1, Tile2, Tile3, Tile4, Tile5, Tile6, Tile7, Tile8,
                                      Tile9, Tile10, Tile11, Tile12, Tile13, Tile14, Tile15, Tile16,
                                      Tile17, Tile18, Tile19, Tile20, Tile21, Tile22, Tile23, Tile24,
                                      Tile25, Tile26, Tile27, Tile28, Tile29, Tile30, Tile31, Tile32,
                                      Tile33, Tile34, Tile35, Tile36, Tile37, Tile38, Tile39, Tile40,
                                      Tile41, Tile42, Tile43, Tile44, Tile45, Tile46, Tile47, Tile48 };

            /* Leftover from before I figured out Visual Studio was adding "FF" and throwing the conversion off, retaining in case 
               I need it someday
               string[] hexCodes = { "FFB900", "FF8C00", "F7630C", "CA5010", "DA3B01", "EF6950", "D13438", "FF4343",
                                  "E74856", "E81123", "EA005E", "C30052", "e3008c", "BF0077", "C239B3", "9A0089",
                                  "0078D7", "0063B1", "8E8CD8", "6B69D6", "8764B8", "744DA9", "B146C2", "881798",
                                  "0099BC", "2D7D9A", "00B7C3", "038387", "00B294", "018574", "00CC6A", "10893E",
                                  "7A7574", "5D5A58", "68768A", "515C6B", "567C73", "486860", "498205", "107C10",
                                  "767676", "4C4A48", "69797E", "4A5459", "647C64", "525E54", "847545", "7E735F" };*/

            for (int i = 0; i < ColorButtons.Count(); i++)
            {
                if (((FrameworkElement)e.Source).Name == ColorButtons[i].Name)
                {
                    //Before a color is able to be used it is VITAL that the following is done
                    //Remove the # and the additional "FF" that Visual Studio is adding to the background colors to make it a 
                    //full hex code, convert any letters to lower, and then reverse that before converting it to an Int for use with 
                    //the registry
                    if ((bool)chkActiveAccent.IsChecked)
                    {
                        WindowsColors.SetActiveColor(Convert.ToInt32(WindowsColors.ReverseHex(ColorButtons[i].Background.ToString().Replace("#","").Remove(0,2)).ToLower(), 16));
                    }
                    else if (!(bool)chkActiveAccent.IsChecked)
                    {
                        WindowsColors.SetInactiveColor(Convert.ToInt32(WindowsColors.ReverseHex(ColorButtons[i].Background.ToString().Replace("#", "").Remove(0, 2)).ToLower(), 16));
                    }
                    break;
                }
            }
        }

        private void ChkDarkStart_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chkDarkStart.IsChecked)
            {
                WindowsColors.SetStartMenu(2);
            }
            else if (!(bool)chkDarkStart.IsChecked)
            {
                WindowsColors.SetStartMenu(1);
            }
        }

        private void ChkAeroLite_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CustomColor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
