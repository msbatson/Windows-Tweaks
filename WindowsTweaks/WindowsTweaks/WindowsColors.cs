using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTweaks
{
    public static class WindowsColors
    {
        public static object CurrentActiveColor = "";
        public static object CurrentInactiveColor = "";
        public static object CurrentStartState = 1;

        public static void ReadCurrentRegistrySettings()
        {
            //HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows")
                .OpenSubKey("DWM");

            CurrentActiveColor = key.GetValue("AccentColor");
            CurrentInactiveColor = key.GetValue("AccentColorInactive");

            //HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows")
                .OpenSubKey("CurrentVersion").OpenSubKey("Themes").OpenSubKey("Personalize");

            CurrentStartState = (int)key.GetValue("ColorPrevalence");

            Console.WriteLine("Active Accent: " + CurrentActiveColor + "\nInactive Accent: " + CurrentInactiveColor + "\nStart State: " + CurrentStartState);
        }

        //reverses the hexcode to the BBGGRRAA format that Windows uses
        public static string ReverseHex(string hex)
        {
            string result = "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if (i % 2 == 0 && i != 0)
                    sb.Append(' ');
                sb.Append(hex[i]);
            }

            result = sb.ToString();

            string[] temp = result.Split(' ');

            result = temp[2] + temp[1] + temp[0];

            return result;
        }

        public static void SetInactiveColor(object color)
        {
            //HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE", true).OpenSubKey("Microsoft", true).OpenSubKey("Windows").OpenSubKey("DWM", true);

            //ff9408 for light blue
            key.SetValue("AccentColorInactive", color, RegistryValueKind.DWord);
            Console.WriteLine("Key written");

            key.Close();
        }

        public static void SetActiveColor(object color)
        {
            //HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE", true).OpenSubKey("Microsoft", true).OpenSubKey("Windows").OpenSubKey("DWM", true);

            //ff9408 for light blue
            key.SetValue("AccentColor", color, RegistryValueKind.DWord);
            Console.WriteLine(key.GetValue("AccentColor"));
            Console.WriteLine("Key written: " + color);

            key.Close();
        }

        public static void SetStartMenu(int DWORDValue)
        {
            //HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE", true).OpenSubKey("Microsoft", true).OpenSubKey("Windows").OpenSubKey("CurrentVersion", true).OpenSubKey("Themes", true).OpenSubKey("Personalize", true);
            if (DWORDValue == 2)
            {
                key.SetValue("ColorPrevalence", 2, RegistryValueKind.DWord);
                Console.WriteLine("Key written: " + 2);
            }
            else if (DWORDValue == 1)
            {
                key.SetValue("ColorPrevalence", 1, RegistryValueKind.DWord);
                Console.WriteLine("Key written: " + 1);
            }
        }


    }
}
