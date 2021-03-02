using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace KeezyBetterWolves
{
    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        private void Awake()
        {
            Debug.Log("Keezy's Better Wolves 0.1.0.0");
            new Harmony("KeezyBetterWolves.Harmony").PatchAll();
        }

        [HarmonyPatch(typeof(Character), "Awake")]
        private class CharacterAwakePatch
        {
            [HarmonyPostfix]
            private static void Postfix(ref Character __instance)
            {
            }
        }
    }
}