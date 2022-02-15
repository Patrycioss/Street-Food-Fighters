using System;
using GXPEngine.Core;
using GXPEngine.Entities;
using GXPEngine.StageManagement;

namespace GXPEngine.Abilities
{
    public class BasicMelee : Ability
    {
        
        /// <summary>
        /// Basic melee ability involving a hitbox in which other types of entities get damaged
        /// </summary>
        protected BasicMelee(string path, int cols, int rows) : base(path,cols,rows)
        {
            visible = false;
        }

        public new void Update()
        {
            //Usage cooldown
            base.Update();
            
            
            //The punching itself
            if (attacking)
            {
                foreach (Entity entity in StageLoader.GetEntities())
                {
                    Entity parentInfo = (Entity) this.parent;
                    
                    //Makes sure that enemies can't attack enemies and players can't attack players
                    if (entity.entityType != parentInfo.entityType)
                    {
                        if (!entitiesHit.Contains(entity))
                        {
                            if (HitTest(entity.bodyHitbox))
                            {
                                entity.Damage(damage);
                                entitiesHit.Add(entity);
                            }
                        }
                    }
                }
            }
        }
    }
    
    
}   