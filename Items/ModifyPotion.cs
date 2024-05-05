using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Items
{
    /// <summary>
    /// Modifies the data of all items. Enables a list of potions to be used as a projectile.
    /// </summary>
    public class ModifyPotion : GlobalItem
    {
        // Adds a unique instance of the following variables to every Item.
        public override bool InstancePerEntity => true;

        // A list of all item IDs of the potions that are to be modified. 
        public static int[] potionIDs = {28, 110, 2350, 305, 297};
        // The colors that define the particles expelled from the player on consumption.
        public Color[] drinkParticleColor; 

        /// <summary>
        /// Sets the new default properties for all potions.
        /// </summary>
        /// <param name="item"></param>
        public override void SetDefaults(Item item)
        {
            if (Array.Exists(potionIDs, element => element == item.type))
            {
                drinkParticleColor = ItemID.Sets.DrinkParticleColors[item.type];
            }
            base.SetDefaults(item);
        }

        /// <summary>
        /// Sets the item properties to those of a throwable item.
        /// </summary>
        public void SetProjectileDefaults(Item item)
        {
            if (item.type == 28) item.potion = false;
            item.UseSound = SoundID.Item7;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.shootSpeed = 8f;
            item.shoot = GetProjectile(item);
            ItemID.Sets.DrinkParticleColors[item.type] = new Color[0];
        }

        /// <summary>
        /// Sets the item properties to those of standard Terrria potion.
        /// </summary>
        public void SetItemDefaults(Item item)
        {
            if (item.type == 28) item.potion = true;
            item.useStyle = ItemUseStyleID.DrinkLiquid;
            item.noUseGraphic = false;
            item.UseSound = SoundID.Item3;
            item.shoot = 0;
            ItemID.Sets.DrinkParticleColors[item.type] = drinkParticleColor;
        }

        /// <summary>
        /// Gets the projectile object corresponding to the type of potion.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Thrown potion projectile ID.</returns>
        public int GetProjectile(Item item)
        {
            switch (item.type)
            {
                case 28:
                    return ModContent.ProjectileType<Projectiles.ThrownHealing>();

                case 110:
                    return ModContent.ProjectileType<Projectiles.ThrownMana>();

                case 2350:
                    return ModContent.ProjectileType<Projectiles.ThrownRecall>();

                case 305:
                    return ModContent.ProjectileType<Projectiles.ThrownGravity>();

                case 297:
                    return ModContent.ProjectileType<Projectiles.ThrownInvis>();

                default:
                    return 1;
            }
        }

        /// <summary>
        /// Emables all potionIDs to have functionality on a right mouse press.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="player"></param>
        /// <returns>true if item is a potion, false otherwise.</returns>
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (Array.Exists(potionIDs, element => element == item.type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// If a potion is in the play inventory, sets the properties of the potion based on play input. 
        /// A right mouse press sets it to a projectile; a left mouse press sets it to an item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="player"></param>
        public override void UpdateInventory(Item item, Player player)
        {
            if (Array.Exists(potionIDs, element => element == item.type)) 
            {
                if (Main.mouseRight)
                {
                    SetProjectileDefaults(item);
                }
                else if (Main.mouseLeft)
                {
                    SetItemDefaults(item);
                }     
            }
            else
            {
                base.UpdateInventory(item, player);
            }
        }  
    }
}