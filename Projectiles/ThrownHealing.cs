using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Projectiles
{
    public class ThrownHealing : ThrownPotion
    {
        public override void SetDefaults()
        {
            base.color = new Color(255, 0, 0);
            base.buff = ModContent.BuffType<Buffs.ExtraLifeBuff>();
            base.duration = 600; 
            base.SetDefaults();
        }
    }
}