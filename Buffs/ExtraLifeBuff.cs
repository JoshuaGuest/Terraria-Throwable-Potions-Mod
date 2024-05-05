using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Buffs
{
    public class ExtraLifeBuff : SplashBuff
    {
        public override void SetStaticDefaults()
        {
            base.color = new Color(192, 0, 0, 128);
            base.SetStaticDefaults();
        }

        /// <summary>
        /// Updates the lifeBuff trait.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.buffTime[buffIndex] > 0)
            {
                npc.GetGlobalNPC<ModifyNPC>().lifeBuff = true;
            }
            else
            {
                npc.GetGlobalNPC<ModifyNPC>().lifeBuff = false;
            }
            base.Update(npc, ref buffIndex);
        }
    }
}