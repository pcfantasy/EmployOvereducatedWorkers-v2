using Harmony;
using System.Reflection;
using System;

namespace EmployOvereducatedWorkers
{
    internal static class HarmonyDetours
    {
        private static void ConditionalPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix, HarmonyMethod postfix)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (harmony.GetPatchInfo(method)?.Owners?.Contains(harmony.Id) == true)
            {
                DebugLog.LogToFileOnly("Harmony patches already present for {0}" + fullMethodName.ToString());
            }
            else
            {
                DebugLog.LogToFileOnly("Patching {0}..." + fullMethodName.ToString());
                harmony.Patch(method, prefix, postfix);
            }
        }

        private static void ConditionalUnPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (prefix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Prefix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Prefix);
            }
            if (postfix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Postfix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Postfix);
            }
        }

        public static void Apply()
        {
            var harmony = HarmonyInstance.Create("EmployOvereducatedWorkers");
            //1
            var transferManagerMatchOffers = typeof(TransferManager).GetMethod("MatchOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var transferManagerMatchOffersPrefix = typeof(CustomTransferManager).GetMethod("TransferManagerMatchOffersPrefix");
            harmony.ConditionalPatch(transferManagerMatchOffers,
                new HarmonyMethod(transferManagerMatchOffersPrefix),
                null);
            Loader.HarmonyDetourFailed = false;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            var harmony = HarmonyInstance.Create("EmployOvereducatedWorkers");
            //1
            var transferManagerMatchOffers = typeof(TransferManager).GetMethod("MatchOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var transferManagerMatchOffersPrefix = typeof(CustomTransferManager).GetMethod("TransferManagerMatchOffersPrefix");
            harmony.ConditionalUnPatch(transferManagerMatchOffers,
                new HarmonyMethod(transferManagerMatchOffersPrefix),
                null);
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
