using System;
using BepInEx;
using HarmonyLib;

namespace KeezyBetterWolves
{
    [BepInPlugin("KeezyBetterWolves", "Keezy's Better Wolves", "0.1.0.0")]
    public class KeezyBetterWolves
    {
        [HarmonyPatch(typeof(SpawnSystem.SpawnData))]
        [HarmonyPatch(new[] {typeof(SpawnSystem.SpawnData)})]
        [HarmonyPostfix]
        static bool WolfSpawnPatch()
        {
            throw new NotImplementedException();
        }
        
    }
}