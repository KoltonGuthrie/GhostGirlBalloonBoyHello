using HarmonyLib;
using UnityEngine;

namespace GhostGirlBalloonBoyHello.Patches
{
    [HarmonyPatch(typeof(DressGirlAI))]
    internal class DressGirlAIPatch
    {
        private static int lastSeenAmount = 0;

        [HarmonyPatch(typeof(DressGirlAI), "Update")]
        [HarmonyPostfix]
        [HarmonyWrapSafe]
        static void patchUpdate(DressGirlAI __instance)
        {
            int __timesSeenByPlayer = Traverse.Create(__instance).Field("timesSeenByPlayer").GetValue<int>();
            if (__timesSeenByPlayer == 0)
            {
                lastSeenAmount = 0;
                return;
            }
            if(__timesSeenByPlayer > lastSeenAmount)
            {
                lastSeenAmount = __timesSeenByPlayer;

                AudioSource audioSource = __instance.GetComponent<AudioSource>();
                audioSource.maxDistance = 50f;
                audioSource.Stop();
                audioSource.PlayOneShot(GhostGirlBalloonBoyHelloBase.SoundFX[0]);

            }
            
        }

    }
}
