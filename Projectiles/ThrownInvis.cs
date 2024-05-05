using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Projectiles
{
    public class ThrownInvis : ThrownPotion
    {
        public override void SetDefaults()
        {
            base.color = new Color(219, 233, 244);
            base.buff = ModContent.BuffType<Buffs.InvisBuff>();
            base.duration = 600; 
            base.SetDefaults();
        }
    }
}