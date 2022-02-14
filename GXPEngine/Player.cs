using System;
using System.Resources;
using GXPEngine.Abilities;
using GXPEngine.Core;
using GXPEngine.Entities;

namespace GXPEngine
{
    public class Player : Entity
    {
        private BurgerWoman burgerWoman;
        private PastaMan pastaMan;
        
        // public int health
        // {
        //     get { return (int) currentPlayable.health; }
        // }

        public Player() : base("hitboxes/player_feet.png")
        {



            
        }
        
        /// <summary>
        /// Switches between the playable characters
        /// </summary>
        public void SwapCharacters()
        {
            // if (currentPlayable == burgerWoman && pastaMan != null)
            // {
            //     currentPlayable = pastaMan;
            // }
            // else if (burgerWoman != null)
            // {
            //     currentPlayable = burgerWoman;
            // }
        }

        public void UseMainAbility()
        {
            
        }

        public void UseSpecialAbility()
        {
            
        }

        // void Update()
        // {
        //     SetXY(currentPlayable.x,currentPlayable.y);
        //     
        // }

        
      
        
    }
}