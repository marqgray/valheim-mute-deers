using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace KeezyBetterWolves
{
    public delegate void CharacterAwakeListener(Character character);

    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves : BaseUnityPlugin
    {
        private void Awake()
        {
            CharacterAwakeEvent += Wolf.MuteTamedWolfListener;
            new Harmony("KeezyBetterWolves.Harmony").PatchAll();
        }

        public static event CharacterAwakeListener CharacterAwakeEvent;

        [HarmonyPatch(typeof(Character), "Awake")]
        private class CharacterAwakePatch
        {
            [HarmonyPostfix]
            private static void Postfix(ref Character __instance)
            {
                CharacterAwakeEvent?.Invoke(__instance);
            }
        }
    }
}