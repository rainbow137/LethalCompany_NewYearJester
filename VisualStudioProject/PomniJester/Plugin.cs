using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace NewYearJester
{
    [BepInPlugin("rainbow137-NewYearJester", "NewYearJester", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static new ManualLogSource Logger;

        public static Texture JesterTexture;
        public static GameObject JesterHat; // needs shader swapped out at runtime
        public static AudioClip JesterClip;
        public static AudioClip JesterPopClip;
        public static AudioClip JesterScreamingClip;
        public static AudioClip JesterKillingClip;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin rainbow137-NewYearJester is loaded!");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            LoadAssets();
        }

        private void LoadAssets()
        {
            // find file named "jestervisuals" in the same folder as the plugin
            var bundlePath = Path.Join(Path.GetDirectoryName(this.Info.Location), "jestervisuals");
            var bundle = AssetBundle.LoadFromFile(bundlePath);
            var asset = bundle.LoadAsset<GameObject>("assets/prefabs/jestervisuals.prefab");

            // little hardcoded, didn't wanna set up a class
            JesterTexture = asset.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.mainTexture;
            JesterHat = asset.transform.GetChild(1).gameObject;
            JesterClip = asset.transform.GetChild(2).GetComponent<AudioSource>().clip;
            JesterPopClip = asset.transform.GetChild(3).GetComponent<AudioSource>().clip;
            JesterScreamingClip = asset.transform.GetChild(4).GetComponent<AudioSource>().clip;
            JesterKillingClip = asset.transform.GetChild(5).GetComponent<AudioSource>().clip;
        }
    }
}