﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Oddments
{
    public class TwoDollarCoin : PassiveItem
    {
        public static ItemTemplate template = new ItemTemplate(typeof(TwoDollarCoin))
        {
            Name = "Two Dollar Coin",
            Description = "Gold piece",
            LongDescription = "Even with it's age, a chunk of gold like this is still worth a fair amount.",
            SpriteResource = $"{Module.ASSEMBLY_NAME}/Resources/Sprites/twodollarcoin.png",
            Quality = ItemQuality.D
        };

        public override void Pickup(PlayerController player)
        {
            if (!PickedUp)
            {
                player.carriedConsumables.Currency += 100;
            }
            base.Pickup(player);
        }
    }
}
