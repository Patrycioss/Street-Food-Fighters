using System;
using GXPEngine.Core;
using TiledMapParser;
using System.Drawing;

namespace GXPEngine
{
    public class Hud : GameObject
    {
        private readonly EasyDraw canvas;      //show and update data on canvas

        public int killCount;
        public int scoreCount { get; private set; }                //display killCount x 100


        private Vector2 scorePos;
        private Vector2 healthPos;


        private const int BURGER = 0;
        private const int PASTA = 1;

        private Sprite[] characterSmall;
        private Sprite[] characterBig;

        private Sprite burgerWoman;
        private Sprite pastaMan;
        private Sprite specialBarFrame;
        private Sprite specialBar;
        private Sprite[] hearts;

        private Font scoringTextFont = Utils.LoadFont("fonts/HaarlemDeco.ttf", 30);
        private Font scoringNumberFont = Utils.LoadFont("fonts/Underground.ttf", 30);


        public Hud() : base(false)
        {
            canvas = new EasyDraw(game.width, game.height, false);
            TiledLoader loader = new TiledLoader("tiled/hud.tmx", canvas, addColliders: false); //load 

            characterSmall = new Sprite[2];
            characterBig = new Sprite[2];

            hearts = new Sprite[(int)myGame.player.health]; //instatiate amount, each indivual object will be instantiated in AddHeart()
            killCount = 0;
            scoreCount = 0;

            //does not create the sprite from tiled
            loader.AddManualType("hearts");
            loader.AddManualType("scorePoints");
            loader.AddManualType("specialBar");
            loader.AddManualType("characterSmall");
            loader.AddManualType("characterBig");

            loader.OnObjectCreated += OnWeirdObjectCreated;
            loader.LoadObjectGroups();
            loader.OnObjectCreated -= OnWeirdObjectCreated;


            canvas.TextSize(20);
            canvas.TextFont(scoringNumberFont);
            AddChild(canvas);
            UpdateCanvas();
        }

        private void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {
            switch (obj.Type)
            {
                case "hearts":
                    healthPos.Set(obj.X, obj.Y);
                    for (int i = 0; i < hearts.Length; i++)
                    {
                        AddHeart();
                    }
                    break;

                case "scorePoints":
                    scorePos.Set(obj.X, obj.Y);
                    break;

                case "specialBar":
                    //bar, coloured
                    specialBar = new Sprite("placeholders/specialbarFULL.png", addCollider: false);
                    specialBar.SetXY(obj.X + 1, obj.Y - obj.Height);                                 //plus one to make it look nice
                    canvas.AddChild(specialBar);

                    //specialbar frame on top of coloured bar
                    specialBarFrame = new Sprite("placeholders/specialbarframeTHIC.png", addCollider: false);
                    specialBarFrame.SetXY(obj.X, obj.Y - obj.Height - 1);
                    AddChild(specialBarFrame);
                    Console.WriteLine("added Specialbar, x {0}, y {1}", specialBar.x, specialBar.y);
                    break;



                //problem with this approach: characters cannot be selected from the start. fixed character model
                case "characterSmall":

                    characterSmall[0] = new Sprite("models/burger_woman_idle.png", addCollider: false);
                    characterSmall[1] = new Sprite("models/pasta_man_1.png", addCollider: false);
                    foreach (Sprite smallChar in characterSmall)
                    {
                        smallChar.SetXY(obj.X, obj.Y - obj.Height);
                        smallChar.scale = 0.5f;
                        smallChar.visible = false;
                    }
                    AddChild(characterSmall[BURGER]);
                    AddChild(characterSmall[PASTA]);

                    Console.WriteLine("characterSMOL added, x {0}, y {1}", obj.X, obj.Y);
                    break;

                case "characterBig":
                    characterBig[0] = new Sprite("models/burger_woman_idle.png", addCollider: false);
                    characterBig[1] = new Sprite("models/pasta_man_1.png", addCollider: false);
                    foreach (Sprite bigChar in characterBig)
                    {
                        bigChar.SetXY(obj.X, obj.Y - obj.Height);
                        bigChar.visible = false;
                    }
                    AddChild(characterBig[BURGER]);
                    AddChild(characterBig[PASTA]);

                    Console.WriteLine("characterBIG added, x {0}, y {1}", obj.X, obj.Y);
                    break;
            }
        }


        /// <summary>
        /// checks which character is currently being played and changes the hud respectively
        /// </summary>
        public void CheckCharacterSprites()
        {
            if (myGame.player.currentCharacter == burgerWoman)
            {
                characterSmall[BURGER].visible = false;
                characterSmall[PASTA].visible = true;

                characterBig[BURGER].visible = true;
                characterBig[PASTA].visible = false;
                Console.WriteLine("PASTA SMALL");
            }
            else
            // (myGame.player.currentCharacter == pastaMan)
            {
                characterSmall[BURGER].visible = true;
                characterSmall[PASTA].visible = false;

                characterBig[BURGER].visible = false;
                characterBig[PASTA].visible = true;
                Console.WriteLine("PASTA BIG");
            }
            // else                                                        //currently currentCharacter is always NULL
            // {
            //     characterSmall[BURGER].visible = false;
            //     characterSmall[PASTA].visible = false;
            //
            //     characterBig[BURGER].visible = false;
            //     characterBig[PASTA].visible = false;
            //     Console.WriteLine("currentCharacter is NULL");
            // }

            UpdateCanvas();
        }


        public void BurgerBig()
        {
            characterSmall[BURGER].visible = false;
            characterSmall[PASTA].visible = true;

            characterBig[BURGER].visible = true;
            characterBig[PASTA].visible = false;
        }

        public void PastaBig()
        {
            characterSmall[BURGER].visible = true;
            characterSmall[PASTA].visible = false;

            characterBig[BURGER].visible = false;
            characterBig[PASTA].visible = true;
        }

        //called in Entity.Damage, only when Enemy killed
        public void AddScore()
        {
            killCount++; //kill count still needs to be displayed in HUD
            scoreCount = killCount * 100;
            Console.WriteLine("killCount" + killCount);
            UpdateCanvas();
        }

        /// <summary>
        /// instantiate every heart Object in hearts
        /// </summary>
        public void AddHeart()
        {
            for (int i = 0; i < hearts.Length; i++)
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
        public void RemoveHeart()
        {
            for (int i = hearts.Length - 1; i >= 0; i--)
            {
                if (hearts[i] != null)
                {
                    hearts[i].LateDestroy();
                    hearts[i] = null;
                    break;
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
            canvas.TextFont(scoringTextFont);
            canvas.Text("Score : ", scorePos.x, scorePos.y);
            canvas.TextFont(scoringNumberFont);
            canvas.Text("        " + scoreCount.ToString(), scorePos.x + 10, scorePos.y + 12);

            if (myGame.player.chargedAmount < 1)
            {
                specialBar.visible = false;     //only show 
            }
            else
            {
                specialBar.visible = true;
                if (myGame.player.chargedAmount <= myGame.player.necessaryCharge)
                {
                    specialBar.scaleX = (myGame.player.chargedAmount);
                }

            }

            //Console.WriteLine("player charged amount: " + myGame.player.chargedAmount);


        }

        /// <summary>
        /// CanvasUpdate but from other classes
        /// </summary>
        public void ExternalCanvasUpdate()
        {
            UpdateCanvas();
        }
    }
}
