﻿using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Alexandria.ItemAPI;
using Alexandria.Misc;
using JuneLib.Items;

namespace Oddments
{
    public class SafetyScissors : PassiveItem
    {
        public static OddItemTemplate template = new OddItemTemplate(typeof(SafetyScissors))
        {
            Name = "Safety Scissors",
            Description = "Shoot the Fuse!", //https://www.youtube.com/watch?v=4Emb7zasmRc
            SpriteResource = $"{Module.SPRITE_PATH}/safetyscissors.png",
            LongDescription = "Chests will no longer spawn with fuses\n\nEven a blade as blunt as this incurrs the wrath of the jammed",
            Quality = ItemQuality.D,
            PostInitAction = item =>
            {
                Hook hook = new Hook(typeof(Chest).GetMethod("RoomEntered", BindingFlags.Instance | BindingFlags.NonPublic), typeof(SafetyScissors).GetMethod("PreventFuses"));
                item.AddPassiveStatModifier(PlayerStats.StatType.Curse, 1f);
            }
        };

        public static void PreventFuses(Action<Chest, PlayerController> orig, Chest self, PlayerController enterer)
        {
            if (IsFlagSetAtAll(typeof(SafetyScissors))
                && (self.lootTable == null || !self.lootTable.CompletesSynergy))
            {
                self.PreventFuse = true;
            }
            orig(self, enterer);
        }

        public override void Pickup(PlayerController player)
        {
            player.AddFlagsToPlayer(GetType());
            base.Pickup(player);
        }

        public override void DisableEffect(PlayerController player)
        {
            player.RemoveFlagsFromPlayer(GetType());
            base.DisableEffect(player);
        }
    }
}
