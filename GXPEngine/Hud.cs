using System;
using GXPEngine.Core;
using TiledMapParser;

namespace GXPEngine
{
    public class Hud : GameObject
    {
        private readonly EasyDraw canvas;      //show and update data on canvas

        //enemies killed: kill count still needs to be displayed in HUD
        public int killCount { get; private set; } 
    
        private Vector2 scorePos;
        private Vector2 healthPos;            
        private Vector2 specialBarPos;        
   
        private Sprite[] hearts;              
    
        public Hud() : base(false)
        {
            canvas = new EasyDraw(game.width, game.height, false);
            TiledLoader loader = new TiledLoader("tiled/hud.tmx", canvas, addColliders: false); //load 

            hearts = new Sprite[(int)myGame.player.health]; //got the amount but each object will be instantiated in AddHeart()
            killCount = 0;

            //does not create the sprite from tiled
            loader.AddManualType("hearts");
            loader.AddManualType("scorePoints");

            loader.OnObjectCreated += OnWeirdObjectCreated;
            loader.LoadObjectGroups();
            loader.OnObjectCreated -= OnWeirdObjectCreated;

            AddChild(canvas);
            UpdateCanvas();   
        }

        private void OnWeirdObjectCreated(Sprite sprite, TiledObject obj) 
        {
            switch (obj.Type)
            {
                case "hearts":
                    healthPos.Set(obj.X, obj.Y);
                    for(int i= 0; i<hearts.Length; i++) 
                    {
                        AddHeart();
                    }
                    break;

                case "scorePoints":
                    scorePos.Set(obj.X, obj.Y);
                    break;
            }
        }

        /// <summary>
        /// instantiate every heart Object in hearts
        /// </summary>
        public void AddHeart()
        {
            for(int i = 0; i < hearts.Length; i++)
            {
                if (hearts[i] == null)
                {
                    hearts[i] = new Sprite("models/heart.png", addCollider: false);
                    hearts[i].SetXY(healthPos.x + (64 * i), healthPos.y - 64);     
                    canvas.AddChild(hearts[i]);
                    Console.WriteLine(hearts.Length);
                }
            }
        }

        //called in Entity.Damage(), only applies to Player
        public void RemoveHearts(int amount)
        {
            for (int t = 0; t < amount; t++)
            {
                if (hearts.Length != 0)
                {
                    for (int i = hearts.Length -1; i >= 0; i--) 
                    {
                        if (hearts[i] != null)
                        {
                            hearts[i].LateDestroy();
                            break;                          
                        }
                    }
                }
            }
            
            
            UpdateCanvas();
        }

        /// <summary>
        /// clear canvas of any text and fills but not sprites
        /// </summary>
        private void UpdateCanvas()
        {
            canvas.ClearTransparent();
        }

        //called in Entity.Damage, only when Enemy killed
        public void AddScore()
        {
            killCount++; //kill count still needs to be displayed in HUD
        }
    }
}


