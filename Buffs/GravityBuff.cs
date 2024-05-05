using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Buffs
{
    public class GravityBuff : SplashBuff
    {
        public override void SetStaticDefaults()
        {
            base.color = new Color(45, 25, 52, 100);
            base.SetStaticDefaults();
        }

        /// <summary>
        /// Updates the direction of gravity and rotation.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.type != 488)
            {
                float oldSpriteDirection = npc.spriteDirection;
                if (npc.buffTime[buffIndex] > 0)
                {
                    npc.rotation = (float)Math.PI;
                    if (npc.buffTime[buffIndex] == 179)
                    {
                        npc.spriteDirection *= -1;
                    }
                    npc.GravityMultiplier *= -1f;               
                }
                else
                {
                    npc.rotation = (float)(Math.PI * 2);
                    npc.spriteDirection *= -1;
                    npc.GravityMultiplier *= -1f;
                }
            }
            base.Update(npc, ref buffIndex);
        }
    }
}