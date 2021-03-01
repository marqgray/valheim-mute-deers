using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace KeezyBetterWolves
{
    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves
    {
        private void Awake()
        {
            Debug.Log("Keezy's Better Wolves 0.1.0.0");
        }

        [HarmonyPatch(typeof(Character), "Awake")]
        [HarmonyPostfix]
        private static bool CharacterAwakePatch()
        {
            throw new NotImplementedException();
        }
    }
}