using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Projectiles
{
    public class ThrownMana : ThrownPotion
    {
        public override void SetDefaults()
        {
            base.color = new Color(0, 0, 255);
            base.buff = ModContent.BuffType<Buffs.ExtraManaBuff>();
            base.duration = 600;
            base.SetDefaults();
            
        }
    }
}