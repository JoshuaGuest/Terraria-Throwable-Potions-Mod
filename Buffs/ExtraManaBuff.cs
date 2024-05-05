using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Buffs
{
    public class ExtraManaBuff : SplashBuff
    {
        public override void SetStaticDefaults()
        {
            base.color = new Color(0, 0, 192, 128);
            base.SetStaticDefaults();
        }

        /// <summary>
        /// Updates the manaBuff trait.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] > 0)
            {
                npc.GetGlobalNPC<ModifyNPC>().manaBuff = true;
            }
            else
            {
                npc.GetGlobalNPC<ModifyNPC>().manaBuff = false;
            }
            base.Update(npc, ref buffIndex);
        }
    }
}