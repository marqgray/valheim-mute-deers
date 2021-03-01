using System;
using BepInEx;
using HarmonyLib;

namespace KeezyBetterWolves
{
    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves
    {
        [HarmonyPatch(typeof(Character), "Awake")]
        [HarmonyPostfix]
        static bool CharacterAwakePatch()
        {
            throw new NotImplementedException();
        }
        
    }
}