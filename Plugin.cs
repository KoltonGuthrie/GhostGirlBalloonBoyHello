using BepInEx;
using BepInEx.Logging;
using GhostGirlBalloonBoyHello.Patches;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GhostGirlBalloonBoyHello
{

    [BepInPlugin(modGUID, modName, modVersion)]
    public class GhostGirlBalloonBoyHelloBase : BaseUnityPlugin
    {
        private const string modGUID = "Kolton12O.GhostGirlBalloonBoyHello";
        private const string modName = "GhostGirlBalloonBoyHello";
        private const string modVersion = "1.0.2";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static GhostGirlBalloonBoyHelloBase Instance;

        internal ManualLogSource mls;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("The Ghost Girl Balloon Boy Hello has awaken!");

            harmony.PatchAll(typeof(DressGirlAIPatch));

            SoundFX = new List<AudioClip>();
            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd((modName + ".dll").ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "balloonboyhello");

            if (Bundle != null)
            {
                mls.LogInfo("Successfully loaded asset bundle! :)");
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            } else
            {
                mls.LogError("Failed to load asset bundle! :(");
            }
        }
    }
}
