using System;
using System.Drawing;
using TiledMapParser;
using GXPEngine.StageManagement;                                                                // still need to display score count
using GXPEngine.Core;

namespace GXPEngine
{
    class GameOver : GameObject
    {

        Hud hud;
        TiledLoader loader;
        Sprite background = new Sprite("background.png", addCollider: false);
        public bool destroyMe { get; private set; }
        private int score;                                                              //display the score in the canvas still needs to add it though

        EasyDraw canvas;
        private Font scoringFont;//canvas for showing the score



        public GameOver(Hud hud) : base()
        {
            destroyMe = false;
            //this.hud = hud;
            this.score = hud.scoreCount;
            scoringFont = Utils.LoadFont("fonts/Underground.ttf", 60);//canvas for showing the score
            AnimationSprite background = new AnimatedDecoration("tomato_death.png", 10, 1, 200, true);                     //REPLACE TO GAME OVER SCREEN: USE RED FRAME WITH TOMATO SEEDS
            AddChild(background);

            canvas = new EasyDraw(game.width, game.height);
            //canvas.SetXY(0,0);

            canvas.TextSize(60);
            canvas.TextFont(scoringFont);
            canvas.Fill(0);
            canvas.TextAlign(CenterMode.Max, CenterMode.Min);
            //locanvas.Text("Score: " + score, game.width / 2, game.height / 2);
            AddChild(canvas);

            loader = new TiledLoader("tiled/GameOver.tmx", addColliders: false);
            //loader.AddManualType("Score");

            loader.rootObject = this;
            loader.OnObjectCreated += OnWeirdObjectCreated;
            loader.LoadObjectGroups();
            loader.OnObjectCreated -= OnWeirdObjectCreated;

        }

        private void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {
            switch (obj.Type)
            {
                case "Score":
                    Console.WriteLine("display SCORE in GAMEOVER, x: {0}, y: {1}", obj.X, obj.Y);
                    canvas.TextAlign(CenterMode.Center, CenterMode.Min);
                    canvas.TextFont(scoringFont);

                    canvas.Text("" + score, obj.X + obj.Width / 2, obj.Y + 100);

                    break;

            }
        }

        public void DestroyGameOver()
        {
            myGame.AddChild(background);
            StageLoader.LoadStage(Stages.Test);
            myGame.hud = new Hud();
            myGame.AddChild(myGame.hud);
            destroyMe = true;           //actual destruction will be in myGame
            Console.WriteLine("GAMEOVER DESTROY ME TRUE");
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
