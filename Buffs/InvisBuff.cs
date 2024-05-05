using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Buffs
{
    public class InvisBuff : SplashBuff
    { 
        public override void SetStaticDefaults()
        {
            base.color = new Color(255, 255, 255, 0);
            base.SetStaticDefaults();
        }

        /// <summary>
        /// Updates the opacity of the NPC.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
            if (npc.buffTime[buffIndex] > 0)
            {
                npc.alpha = 245 + (int)(10 * Math.Cos(2 * Math.PI * Main.time / 180));
            }
            else
            {
                npc.alpha = npc.GetGlobalNPC<ModifyNPC>().oldAlpha;
            }
        }
    }
}