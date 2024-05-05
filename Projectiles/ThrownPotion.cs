using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;

namespace ThrowablePotions.Projectiles
{
    /// <summary>
    /// Defines the standard behaviour for a thrown-potion projectile. 
    /// </summary>
    public abstract class ThrownPotion : ModProjectile
    {
        // The color of the liquid particles produced by the thrown-potion.
        public Color color;
        // The status effect corresponding to the thrown-potion.
        public int buff;
        // The duration the status effect will last for.
        public int duration;
        // The time the thrown potion has been active for.
        public int time = 0;
        // The factor the thrown-potion rotation is slowed.
        public float rotationSlow = 1;
        // The factor the thrown-potion is slowed by water.
        public static float waterViscosity = (float)Math.Pow(0.5, 1f/60f);
        // The factor the thrown-potion is slowed by lava.
        public static float lavaViscosity = (float)Math.Pow(0.5, 1f/45f);
        // The factor the thrown-potion is slowed by honey.
        public static float honeyViscosity = (float)Math.Pow(0.5, 1f/30f);


        /// <summary>
        /// Sets the properites of the thrown-potion projectile.
        /// </summary>
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.scale = 0.9f;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.ignoreWater = false;
            Projectile.timeLeft = 1200;
            Projectile.light = 0.2f;
        }

        /// <summary>
        /// Controles the behaviour of the thrown-potion in flight.
        /// </summary>
        public override void AI()
        {  
            // Slows the thrown-potion based on the 'viscosity' of the liquid it is in.
            if (Projectile.wet)
            {
                Projectile.velocity *= waterViscosity;
            }
            else if (Projectile.lavaWet)
            {
                Projectile.velocity *= lavaViscosity;
            }
            else if (Projectile.honeyWet)
            {
                Projectile.velocity *= honeyViscosity;
            }
            
            // Net force applied on the thrown-potion by the environment.
            if (Projectile.wet || Projectile.lavaWet || Projectile.honeyWet)
            {
                Projectile.velocity.Y -= 0.08f;
                rotationSlow += 0.01f;
            }
            else if (Projectile.shimmerWet)
            {
                Projectile.velocity.Y -= 0.15f;
                if (rotationSlow > 1) rotationSlow -= 0.005f;
            }
            else
            {
                Projectile.velocity.Y += 0.15f;
                if (rotationSlow > 1) rotationSlow -= 0.005f;
            }
            
            Projectile.rotation += (float)(Math.Pow(Math.Abs(Projectile.velocity.Y) * 0.015f, 0.35) / rotationSlow);

            GenerateDustTrail();
            CheckCollide(false);
            time++;
        }

        /// <summary>
        /// Checks if the player or an enemy has collided with the thrown-potion.
        /// If it is colliding for the first time, it is shattered; If it is colliding after, is applies the corresponding status effect.
        /// </summary>
        /// <param name="explode">Whether projectile has shattered and splashed outward.</param>
        public void CheckCollide(bool explode)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (Projectile.Hitbox.Intersects(npc.Hitbox))
                { 
                    if (explode)
                    {
                        npc.AddBuff(buff, duration, true);
                    }
                    else 
                    {
                        OnKill(0);
                        break;
                    }
                }
            }
            if (!explode)
            {
               Player player = Main.player[0];
                if (time > 10 && Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    OnKill(0);
                }
            }
        }

        /// <summary>
        /// Controls the behaviour of the thrown-potion on initial collision.
        /// </summary>
        /// <param name="timeLeft"></param>
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            GenerateDust();
            GenerateGore();
            GenerateAOE();
            Projectile.active = false;
        }

        /// <summary>
        /// Generates a particle trail that emminates from the thrown-potion.
        /// </summary>              
        public void GenerateDustTrail()
        { 
            Random random = new Random();
            int randomNum = random.Next(1,16);
            if (randomNum == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(Projectile.position, 1, 1, 4, 0f, 0f, 180, color, 0.6f);
                }
            }
        }

        /// <summary>
        /// Generates the splash particles that are released from the thrown-potion on collision.
        /// </summary>              
        public void GenerateDust()
        {
            for (int i = 0; i < 40; i++)
            {
                double dustAngle = Math.PI * i/39;
                float xVelocity = (float)(Math.Cos(dustAngle) * 2.25 + Projectile.velocity.X * 0.4);
                float yVelocity = -(float)(Math.Sin(dustAngle) * 2.25 + Projectile.velocity.Y * 0.4);
                Dust.NewDust(Projectile.position, 1, 1, 4, xVelocity, yVelocity, 180, color, 0.6f);
            }
        }

        /// <summary>
        /// Generates the glass particles that are released from the thrown-potion on collision.
        /// </summary>
        public void GenerateGore()
        {
            for (int i = 0; i < 6; i++)
            {
                double goreAngle = Math.PI * i/5;
                float xVelocity = (float)(Math.Cos(goreAngle) * 0.3 + Projectile.velocity.X * 0.25);
                float yVelocity = -(float)(Math.Sin(goreAngle) * 0.3 + Projectile.velocity.Y * 0.25);
                Vector2 goreVelocity = new Vector2(xVelocity, yVelocity);
                int goreIndex = Gore.NewGore(Entity.GetSource_Death(), Projectile.position, goreVelocity, 704, 0.75f);
                Main.gore[goreIndex].alpha = 100;
                Main.gore[goreIndex].timeLeft = 10;
            }
        }

        /// <summary>
        /// Creates an 120px by 120px area-of-effect at the thrown-potion on collision.
        /// </summary>
        public void GenerateAOE()
        {
            Projectile.velocity = new Vector2(0f, 0f);
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.Resize(120, 120);

            CheckCollide(true);
        }
    }
}