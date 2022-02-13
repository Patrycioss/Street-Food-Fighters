using System;
using System.Collections.Generic;
using TiledMapParser;

namespace GXPEngine
{
    public class Stage : GameObject
    {
        private Map stageData;
        private int tileSize;
        public int stageWidth;
        public int stageHeight;

        public readonly Stages stage;

        /// <summary>
        /// Object that holds all information about the currentstage including objects
        /// </summary>
        /// <param name="givenStage">A stage from the Stages.cs list</param>
        /// <exception cref="Exception">When the stage from Stages.cs doesn't have the same name as the file</exception>
        public Stage(Stages givenStage)
        {
            myGame.AddChild(this);
            
            stage = givenStage;
            string stagePath = "tiled/stages/" + stage.ToString() + ".tmx";
            stageData = MapParser.ReadMap(stagePath);
            
            //TileSize is the same as width and width is the same as height
            tileSize = stageData.TileWidth;

            if (stageData.Layers == null || stageData.Layers.Length <= 0)
            {
                throw new Exception("Tile file " + stagePath + " does not contain a layer!");
            }

            LoadStage();

        }

        void Update()
        {
            SortDisplayHierarchy();
        }

        /// <summary>
        /// Loads in the stage from tiled
        /// </summary>
        private void LoadStage()
        {
            stageWidth = stageData.Width * stageData.TileWidth;
            stageHeight = stageData.Height * stageData.TileHeight;
            
            Layer mainLayer = stageData.Layers[0];

            short[,] tileNumbers;

            tileNumbers = mainLayer.GetTileArray();
            for (int col = 0; col < mainLayer.Width; col++)
            for (int row = 0; row < mainLayer.Height; row++)
            {
                int x = col * tileSize;
                int y = row * tileSize;
                
                switch (tileNumbers[col, row])
                {
                        
                    //Barriers
                    case 1:
                        Barrier barrier = new Barrier();
                        barrier.SetXY(x,y);
                        AddChild(barrier);
                        break;
                    
                    case 35:
                        myGame.player = new Player();
                        myGame.player.SetXY(x,y);
                        AddChild(myGame.player);
                        myGame.player.SetWeapon(new BurgerPunch());
                        break;
                    
                    case 25:
                        Enemy enemy = new PizzaZombie();
                        enemy.SetXY(x,y);
                        enemy.SetTarget(myGame.player);
                        AddChild(enemy);
                        break;
                }
            }

            //Adds a barrier to the left side of the stage
            Barrier leftBorder = new Barrier();
            leftBorder.SetXY(-1,0);
            leftBorder.width = 1;
            leftBorder.height = stageHeight;
            AddChild(leftBorder);

            //Adds a floor to the game
            Barrier ground = new Barrier();
            ground.SetXY(0,stageHeight);
            ground.width = stageWidth;
            ground.height = 1;
            AddChild(ground);
        }
        
        /// <summary>
        /// Sorts the displayhierarchy to make sure objects are layered correctly.
        /// </summary>
        private void SortDisplayHierarchy()
        {
            List<GameObject> children = GetChildren();

            for (int i = children.Count-1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (!children[i].Equals(children[j]))
                    {
                        if (children[i].y < children[j].y)
                        {
                            SetChildIndex(children[j],i);
                        }
                    }
                }
            }
        }
        
        /// <returns>A list of all entities in this stage</returns>
        public List<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();
            foreach (GameObject gameObject in GetChildren())
            {
                if (gameObject is Entity)
                {
                    entities.Add((Entity)gameObject);
                }    
            }
            return entities;
        }

        /// <returns>A list of all enemies in this stage</returns>
        public List<Enemy> GetEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (GameObject gameObject in GetEntities())
            {
                if (gameObject is Enemy)
                {
                    enemies.Add((Enemy)gameObject);
                }
            }
            return enemies;
        }
    }
}