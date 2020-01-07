using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsTweaks
{
    public static class WindowsThemes
    {
        public static void CreateAeroLite()
        {
            string ThemesDir = @"C:\Windows\Resources\Themes";
            File.Copy(Path.Combine(ThemesDir, "aero.theme"), Path.Combine(ThemesDir, "aerolite.theme"));

            var lines = File.ReadAllLines(Path.Combine(ThemesDir, "aerolite.theme"));

            for (int i = 0; i < lines.Length; i++)
            {
                var fields = lines[i].Split(' ');
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals(@"DisplayName=@%SystemRoot%\System32\themeui.dll,-2013"))
                {
                    lines[i] = "Displayname=Aero Lite";
                }

                if (lines[i].Equals(@"Path=%ResourceDir%\Themes\Aero\Aero.msstyles"))
                {
                    lines[i] = @"Path=%ResourceDir%\Themes\Aero\AeroLite.msstyles";
                }
            }

            File.WriteAllLines("Test.theme", lines);
        }

        public static void SetAeroLite(bool IsEnabled)
        {

        }
    }
}
