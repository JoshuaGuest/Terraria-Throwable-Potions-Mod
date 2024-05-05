using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ThrowablePotions.Buffs
{
    /// <summary>
    /// WIP. Defines the behaviour for the effect granted by the recall splash buff.
    /// </summary>
    public class RecallBuff : SplashBuff
    {
        public override void SetStaticDefaults()
        {
            base.color = new Color(37, 245, 252, 128);
            base.SetStaticDefaults();
        }

        /*
        public override void Update(Player player, ref int buffIndex)
        {
            //Empty
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //Empty
        }
        */

    }
}