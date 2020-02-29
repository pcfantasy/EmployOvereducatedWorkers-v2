using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System;
using System.Linq;
using ColossalFramework.Math;
using System.Collections.Generic;

namespace EmployOvereducatedWorkers
{
    public class Loader : LoadingExtensionBase
    {
        public static bool DetourInited = false;
        public static bool HarmonyDetourInited = false;
        public static bool HarmonyDetourFailed = true;
        public static LoadMode CurrentLoadMode;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

        }

        public void HarmonyInitDetour()
        {
            if (!HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Init harmony detours");
                HarmonyDetours.Apply();
                HarmonyDetourInited = true;
            }
        }

        public void HarmonyRevertDetour()
        {
            if (HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Revert harmony detours");
                HarmonyDetours.DeApply();
                HarmonyDetourInited = false;
                HarmonyDetourFailed = true;
            }
        }

        public void InitDetour()
        {
            if (!DetourInited)
            {
                DebugLog.LogToFileOnly("Init detours");
                DetourInited = true;
            }
        }

        public void RevertDetour()
        {
            if (DetourInited)
            {
                DebugLog.LogToFileOnly("Revert detours");
                DetourInited = false;
            }
            EmployOvereducatedWorkersThreading.isFirstTime = true;
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }
    }
}

