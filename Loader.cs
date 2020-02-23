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
        public static LoadMode CurrentLoadMode;
        public static RedirectCallsState state1;
        public static bool isMoreEffectiveTransferRunning = false;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            Loader.CurrentLoadMode = mode;
            if (EmployOvereducatedWorkers.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    DebugLog.LogToFileOnly("OnLevelLoaded");
                    Detour();
                    if (mode == LoadMode.NewGame)
                    {
                        DebugLog.LogToFileOnly("New Game");
                    }
                }
            }
        }


        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (CurrentLoadMode == LoadMode.LoadGame || CurrentLoadMode == LoadMode.NewGame)
            {
                if (EmployOvereducatedWorkers.IsEnabled)
                {
                    RevertDetour();
                }
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public void Detour()
        {
            isMoreEffectiveTransferRunning = CheckMoreEffectiveTransferIsLoaded();
            if (!isMoreEffectiveTransferRunning)
            {
                var srcMethod1 = typeof(TransferManager).GetMethod("MatchOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                var destMethod1 = typeof(CustomTransferManager).GetMethod("MatchOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                state1 = RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);
            }
        }

        public void RevertDetour()
        {
            if (!isMoreEffectiveTransferRunning)
            {
                var srcMethod1 = typeof(TransferManager).GetMethod("MatchOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                RedirectionHelper.RevertRedirect(srcMethod1, state1);
            }
        }

        private bool Check3rdPartyModLoaded(string namespaceStr, bool printAll = false)
        {
            bool thirdPartyModLoaded = false;

            var loadingWrapperLoadingExtensionsField = typeof(LoadingWrapper).GetField("m_LoadingExtensions", BindingFlags.NonPublic | BindingFlags.Instance);
            List<ILoadingExtension> loadingExtensions = (List<ILoadingExtension>)loadingWrapperLoadingExtensionsField.GetValue(Singleton<LoadingManager>.instance.m_LoadingWrapper);

            if (loadingExtensions != null)
            {
                foreach (ILoadingExtension extension in loadingExtensions)
                {
                    if (printAll)
                        DebugLog.LogToFileOnly($"Detected extension: {extension.GetType().Name} in namespace {extension.GetType().Namespace}");
                    if (extension.GetType().Namespace == null)
                        continue;

                    var nsStr = extension.GetType().Namespace.ToString();
                    if (namespaceStr.Equals(nsStr))
                    {
                        DebugLog.LogToFileOnly($"The mod '{namespaceStr}' has been detected.");
                        thirdPartyModLoaded = true;
                        break;
                    }
                }
            }
            else
            {
                DebugLog.LogToFileOnly("Could not get loading extensions");
            }

            return thirdPartyModLoaded;
        }

        private bool CheckRealCityIsLoaded()
        {
            return this.Check3rdPartyModLoaded("RealCity", false);
        }

        private bool CheckMoreEffectiveTransferIsLoaded()
        {
            return this.Check3rdPartyModLoaded("MoreEffectiveTransfer", false);
        }
    }
}

