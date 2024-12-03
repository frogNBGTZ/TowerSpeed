using MelonLoader;
using BTD_Mod_Helper;
using TowerSpeed;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine.InputSystem;
using Il2CppAssets.Scripts.Models.Towers;
using UnityEngine.Playables;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Models.Powers;
using Il2CppNinjaKiwi.GUTS.Models;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

[assembly: MelonInfo(typeof(TowerSpeed.TowerSpeed), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TowerSpeed
{

    public class TowerSpeed : BloonsTD6Mod
    {
        private const int CustomStartingCash = 1000000000; // Set your desired starting cash here
        private bool shiftZPressed = false;

        public override void OnApplicationStart()
        {
            ModHelper.Msg<TowerSpeed>("Custom Starting Cash Mod loaded!");
        }

        public override void OnUpdate()
        {
            // Detect if Shift + Z is pressed
            if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed && Keyboard.current.zKey.wasPressedThisFrame)
            {
                shiftZPressed = !shiftZPressed; // Toggle the state
                ModHelper.Msg<TowerSpeed>($"Shift + Z toggled: {shiftZPressed}");
            }

        }

        public override void OnNewGameModel(GameModel gameModel)
        {
            // Set the starting cash for the game
            gameModel.cash = CustomStartingCash;
            ModHelper.Msg<TowerSpeed>($"Starting cash set to {CustomStartingCash}");
            // Modify weapon behavior only if Shift + Z is active
            if (shiftZPressed)
            {
                foreach (var weapon in gameModel.GetDescendants<WeaponModel>().ToList())
                {
                    weapon.rate = 0; // Modify weapon rate (example)
                    weapon.customStartCooldown = 0; // Reset weapon cooldown (example)
                }

                ModHelper.Msg<TowerSpeed>("Weapon modifications applied because Shift + Z is active.");
            }

        }
    }
}
