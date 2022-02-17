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
        private Sprite manual  = new Sprite ("manualPLACEHOLDER.png", addCollider: false);   //show the controls of the game: maybe just use a button to make sprite: with manual visible and turn it off when pressed again
        private Sprite credits = new Sprite ("creditsPLACEHOLDER.png", addCollider: false);


        private EasyDraw canvas;                //canvas for displaying Text, make text blink if wanted
        public bool destroyMe { get; set; }

        public Menu()
        {
            destroyMe = false;
            Sprite menuBackground = new Sprite("menu.png");

            manual.SetOrigin(manual.width/2, manual.height/2);
            manual.SetXY(game.width / 2, game.height / 2);
            credits.SetOrigin(credits.width / 2, credits.height / 2);
            credits.SetXY(game.width / 2, game.height / 2);

            manual.visible = false;
            credits.visible = false;

            AddChild(menuBackground);
            AddChild(manual);
            AddChild(credits);
            

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
            if (Input.GetKeyDown(Key.S))    //HIGH FIVE
            {
                DestroyMenu();
            }
            if(Input.GetKeyDown(Key.F))     //Base Attack (PINK)
            {
                if(!manual.visible || credits.visible)  
                {
                    manual.visible = true;
                    credits.visible = false;
                }
                else if (manual.visible)   //close manual when pressing basic attack again
                {
                    manual.visible = false;
                    credits.visible=false;
                }
            }
            if (Input.GetKeyDown(Key.G))
            {
                if (manual.visible || !credits.visible)
                {
                    manual.visible = false;
                    credits.visible = true;
                }
                else if (credits.visible)
                {
                    manual.visible = false;
                    credits.visible = false;
                }
            }

        }
    }


}
