using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Projectiles
{
    public class ThrownGravity : ThrownPotion
    {
        public override void SetDefaults()
        {
            base.color = new Color(45, 25, 52);
            base.buff = ModContent.BuffType<Buffs.GravityBuff>();
            base.duration = 600;
            base.SetDefaults();
            
        }
    }
}