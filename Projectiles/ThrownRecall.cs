using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Projectiles
{
    public class ThrownRecall : ThrownPotion
    { 
        public override void SetDefaults()
        {
            base.color = new Color(37, 245, 252);
            base.buff = ModContent.BuffType<Buffs.RecallBuff>();
            base.duration = 600;
            base.SetDefaults();    
        }
    }
}