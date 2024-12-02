using MelonLoader;
using BTD_Mod_Helper;
using TowerSpeed;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.SimulationTests;
using Il2CppAssets.Scripts.Models.Gameplay.Mods;

[assembly: MelonInfo(typeof(TowerSpeed.TowerSpeed), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TowerSpeed;

public class TowerSpeed : BloonsTD6Mod
{
    public class CustomStartingCashMod : BloonsTD6Mod
    {
        private const int CustomStartingCash = 100000; // Set your desired starting cash here

        public override void OnApplicationStart()
        {
            ModHelper.Msg<CustomStartingCashMod>("Custom Starting Cash Mod loaded!");
        }

        public override void OnNewGameModel(GameModel gameModel)
        {
            // Set the starting cash for the game
            gameModel.cash = CustomStartingCash;

            // Example of modifying other game elements if needed:
            foreach (var weapon in gameModel.GetDescendants<WeaponModel>().ToList())
            {
                weapon.rate = 0; // Modify weapon rate (example)
                weapon.customStartCooldown = 0; // Reset weapon cooldown (example)
            }

            ModHelper.Msg<CustomStartingCashMod>($"Starting cash set to {CustomStartingCash}");
        }
    }
}