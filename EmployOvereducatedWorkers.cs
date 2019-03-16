using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployOvereducatedWorkers
{
    public class EmployOvereducatedWorkers : IUserMod
    {
        public static bool IsEnabled = false;

        public string Name
        {
            get { return "Employ Overeducated Workers V2"; }
        }

        public string Description
        {
            get { return "This mod aims at helping low-level buildings to employ overeducated workers."; }
        }

        public void OnEnabled()
        {
            IsEnabled = true;
            FileStream fs = File.Create("EmployOvereducatedWorkers.txt");
            fs.Close();
        }

        public void OnDisabled()
        {
            IsEnabled = false;
        }

    }
}
