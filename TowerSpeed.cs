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
using BTD_Mod_Helper.Api.Helpers;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using BTD_Mod_Helper.Api.ModOptions;
using UnityEngine;
using System;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts;
using UnityEngine.InputSystem.Utilities;

[assembly: MelonInfo(typeof(TowerSpeed.TowerSpeed), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TowerSpeed
{

    public class TowerSpeed : BloonsTD6Mod
    {
        private const int CustomStartingCash = default; // Set your desired starting cash here
        private bool shiftZPressed = false;

        public override void OnApplicationStart()
        {
            ModHelper.Msg<TowerSpeed>("Custom Starting Cash Mod loaded!");
        }
        private static readonly ModSettingHotkey SetCashHotkey = new(KeyCode.F6)
        {
            displayName = "Set Custom Cash Hotkey"
        };
        public override void OnUpdate()
        {
            // Detect if Shift + Z is pressed
            if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed && Keyboard.current.zKey.wasPressedThisFrame)
            {
                shiftZPressed = !shiftZPressed; // Toggle the state
                ModHelper.Msg<TowerSpeed>($"Shift + Z toggled: {shiftZPressed}");
            }

            if (SetCashHotkey.JustPressed() && InGame.instance != null)
            {
                // Display popup to let the user set custom cash
                PopupScreen.instance.ShowSetValuePopup("Set Custom Cash",
                    "Set the custom cash amount you want.",
                    new Action<int>(cash => SetCash(cash)), CustomStartingCash);
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
        private void SetCash(int cash)
        {
            if (InGame.instance != null)
            {
                GameModel gameModel = InGame.Bridge.Model;
                gameModel.cash = cash; // Set the new custom cash value
                ModHelper.Msg<TowerSpeed>($"Custom cash set to {cash}");
            }
        }
    }
}
