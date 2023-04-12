﻿using Alexandria.ItemAPI;
using JuneLib.Items;
using UnityEngine;

namespace Oddments
{
    public class PulsingBulletsItem : PassiveItem
    {
        public static OddItemTemplate template = new OddItemTemplate(typeof(PulsingBulletsItem))
        {
            Name = "Hideous Bullets",
            Description = "Pulsating flesh",
            SpriteResource = $"{Module.SPRITE_PATH}/hideousbullets.png",
            LongDescription = "Bullets will pulsate in damage and size, leading to on average larger bullets with higher damage.",
            Quality = ItemQuality.C,
            PostInitAction = item =>
            {
                item.MakeBulletMod();
            }
        };

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.PostProcessProjectile += Player_PostProcessProjectile;
        }

        public override void DisableEffect(PlayerController player)
        {
            base.DisableEffect(player);
            player.PostProcessProjectile -= Player_PostProcessProjectile;
        }

        private void Player_PostProcessProjectile(Projectile arg1, float arg2)
        {
            arg1.gameObject.AddComponent<PulsingProjectileEffect>();
            Color fleshColor = new Color(0.4f, 0, 0);
            arg1.AdjustPlayerProjectileTint(fleshColor, 1);
        }

    }
    public class PulsingProjectileEffect : BraveBehaviour
    {
        public float minSize = 0.5f;
        public float maxSize = 2f;
        public float minDamage = 0.8f;
        public float maxDamage = 1.4f;
        public float pulseSpeedMult = 0.1f;
        private float m_lastSize = 1f;
        private float m_elapsedTick;
        private float m_lastDMG = 1f;

        void Start()
        {
            if (!projectile)
            {
                return;
            }
            projectile.specRigidbody.UpdateCollidersOnScale = true;

            projectile.OnPostUpdate += Projectile_OnPostUpdate;
        }

        private void Projectile_OnPostUpdate(Projectile obj)
        {
            if (obj == null)
                return;

            m_elapsedTick += BraveTime.DeltaTime * 100f;
            float sine = Mathf.Sin(m_elapsedTick * pulseSpeedMult) / 2 + 0.5f;
            float mult = Mathf.Lerp(minSize, maxSize, sine);
            float dmgMult = Mathf.Lerp(minDamage, maxDamage, sine);
            obj.RuntimeUpdateScale((1 / m_lastSize) * mult);
            obj.baseData.damage *= (1 / m_lastDMG) * dmgMult;

            m_lastSize = mult;
            m_lastDMG = dmgMult;
        }
    }
}
