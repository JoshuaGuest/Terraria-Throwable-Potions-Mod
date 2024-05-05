using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Buffs
{
    /// <summary>
    /// Defines the behaviour for all thrown-potion status effects.
    /// </summary>
    public abstract class SplashBuff : ModBuff
    {
        // The color of the particle effects and tinting.
        public Color color;

        // Lower bound the range of splashbuff types
        private int lowerBound = ModContent.BuffType<Buffs.ExtraLifeBuff>();
        // Upper bound the range of splashbuff types
        private int upperBound = ModContent.BuffType<Buffs.RecallBuff>();

        /// <summary>
        /// Sets the static properites of the status effect.
        /// </summary>
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        /// <summary>
        /// Updates NPC color and particle effects. 
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        public override void Update(NPC npc, ref int buffIndex)
        {  
            // Updates the tint of the NPC only if the current buff instance is the most recently applied.
            if (IsLastSplashBuff(npc, ref buffIndex))
            {
                ChangeColor(npc, ref buffIndex);
            }
            DripParticles(npc);  
        }

        /// <summary>
        /// Checks whether the current status effect instance is the most recently applied.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>
        /// <returns>true if the instance in the most recently applied, false otherwise.</returns>
        public bool IsLastSplashBuff(NPC npc, ref int buffIndex)
        {
            for (int i = buffIndex + 1; i < npc.buffType.Length; i++)
            {
                if (npc.buffType[i] >= lowerBound && npc.buffType[i] <= upperBound)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Applies a tint to the effected NPC for the duration of the status effect.
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buffIndex"></param>               
        public void ChangeColor(NPC npc, ref int buffIndex)
        {
            npc.color = (ModContent.BuffType<InvisBuff>() != npc.buffType[buffIndex]) ? color : new Color(1, 1, 1, 0);
            if (npc.buffType[1] == 0 && npc.buffTime[buffIndex] == 0)
            {
                /* 
                    Any slime with name "[color] slime" has its internal ID updated to 1 (BlueSlime.type) for runtime. 
                    To differentiate them for recoloring, the original ID has to be checked. 
                    Below, only green and purple slimes are considered. WIP 
                */
                switch (npc.netID)
                {
                    case -7:
                        npc.color = new Color(192, 0, 0, 128); //Make Purple
                        break;

                    case -3:
                        npc.color = new Color(0, 220, 40, 100);
                        break;

                    default:
                        npc.color = npc.GetGlobalNPC<ModifyNPC>().oldColor;
                        break;
                }
            }
        }

        /// <summary>
        /// Generates particles that drip off the effected NPC.
        /// </summary>
        /// <param name="entity"></param>
        public void DripParticles(Entity entity)
        {
            if (Main.rand.Next(1, 21) == 1)
            {
                Vector2 delta = new Vector2(Main.rand.Next(0, entity.width), Main.rand.Next(0, (int)(entity.height/2.5)));
                Vector2 dustPostion = entity.position + delta;
                int dustIndex = Dust.NewDust(dustPostion, 1, 1, 4, 0f, 0f, 140, color, 0.6f);
                Main.dust[dustIndex].velocity = new Vector2(0f, 0f);
            }
        }
    }

    /// <summary>
    /// Modifies all NPCs to accomodate the new functionality.
    /// </summary>
    public class ModifyNPC : GlobalNPC
    {
        // Adds a unique instance of the folowing variables to every NPC.
        public override bool InstancePerEntity => true;

        // Default NPC color for reference.
        public Color oldColor;
        // Default NPC alpha for reference.
        public int oldAlpha;
        // Stores whether the NPC is effected with the Extra-Life Buff.
        public bool lifeBuff;
        // Stores whether the NPC is effected with the Extra-Mana Buff.
        public bool manaBuff;

        /// <summary>
        /// Sets the new properties for all NPCs. 
        /// </summary>
        /// <param name="npc"></param>
        public override void SetDefaults(NPC npc)
        {
            oldColor = npc.color;
            oldAlpha = npc.alpha;
        }

        /// <summary>
        /// Activates specific status effects on NPC death.
        /// </summary>
        /// <param name="npc"></param>
        public override void OnKill(NPC npc)
        {
            if (lifeBuff)
            {
                for (int i = 0; i < 10; i++)
                {
                    Item.NewItem(null, npc.getRect(), 58);
                }
            }
            if (manaBuff)
            {
                for (int i = 0; i < 10; i++)
                {
                    Item.NewItem(null, npc.getRect(), 184);
                }
            }
        }
    }
}