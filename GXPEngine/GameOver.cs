using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;
using GXPEngine.StageManagement;
using TiledMapParser;                                                                   // still need to display score count

namespace GXPEngine
{
    class GameOver : GameObject
    {

        Hud hud;
        TiledLoader loader;
        Sprite background = new Sprite("background.png", addCollider: false);
        public bool destroyMe { get; private set; }
        private int score;                                                              //display the score in the canvas still needs to add it though


        public GameOver(Hud hud) : base()
        {
            destroyMe = false;
            this.hud = hud;
            Sprite background = new Sprite("gameOverPLACEHOLDER.png");                      //REPLACE TO GAME OVER SCREEN: USE RED FRAME WITH TOMATO SEEDS
            AddChild(background);

            loader = new TiledLoader("tiled/GameOver.tmx", addColliders: false);
            loader.rootObject = this;
            loader.LoadObjectGroups();

            //STILL NEED TO DO: Display the scores from hud into Game Over Screen
            //Displa

        }
        public void DestroyGameOver()
        {
            myGame.AddChild(background);
           
            destroyMe = true;           //actual destruction will be in myGame
        }
        private void Update()
        {
            if (Input.GetKeyDown(Key.S))            //if pushing HighFiveButton
            {
                DestroyGameOver();
            }
        }
    }
}
