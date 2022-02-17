using GXPEngine.StageManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        Sprite background = new Sprite("background.png", addCollider: false);
        private Sprite manual;  //= new Sprite ("manual.png", addCollider: false);   //show the controls of the game: maybe just use a button to make sprite: with manual visible and turn it off when pressed again
        private Sprite credits; //= new Sprite ("credits.png", addCollider: false);


        private EasyDraw canvas;                //canvas for displaying Text, make text blink if wanted
        public bool destroyMe { get; set; }

        public Menu()
        {
            destroyMe = false;
            Sprite menuBackground = new Sprite("menu.png");
            AddChild(menuBackground);

            canvas = new EasyDraw(game.width, game.height);

        }

        private void DestroyMenu()
        {
            myGame.AddChild(background);
            StageLoader.LoadStage(Stages.Test);

            myGame.hud = new Hud();
            myGame.AddChild(myGame.hud);
            destroyMe = true;               //actual destruction in myGame in seperate 
        }
        private void Update()
        {
            if (Input.GetKeyDown(Key.S))
            {
                DestroyMenu();
            }
        }
    }


}
