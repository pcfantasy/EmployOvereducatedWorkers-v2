using ColossalFramework;
using ColossalFramework.UI;
using ICities;

namespace EmployOvereducatedWorkers
{
    public class EmployOvereducatedWorkersThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (EmployOvereducatedWorkers.IsEnabled)
                {
                    CheckDetour();
                }
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.DetourInited && Loader.HarmonyDetourInited)
            {
                isFirstTime = false;
                if (Loader.DetourInited)
                {
                    DebugLog.LogToFileOnly("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Checking detours.");
                    if (Loader.HarmonyDetourFailed)
                    {
                        string error = "EmployOvereducatedWorkers HarmonyDetourInit is failed, Send EmployOvereducatedWorkers.txt to Author.";
                        DebugLog.LogToFileOnly(error);
                        UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", error, true);
                    }
                }
            }
        }
    }
}