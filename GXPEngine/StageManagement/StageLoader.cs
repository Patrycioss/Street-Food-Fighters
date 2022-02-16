using System;
using System.Collections.Generic;
using GXPEngine.Entities;

namespace GXPEngine.StageManagement
{
    /// <summary>
    /// Static class that handles accessing of the current stage and its children
    /// </summary>
    public static class StageLoader
    {
        public static Stage currentStage;
        public static Pivot stageContainer;

        private static MyGame myGame;
        
        /// <summary>
        /// Load in a new stage and get rid of the previous one if there is still one
        /// </summary>
        /// <param name="stage">A stage from the list of stages in Stages.cs</param>
        public static void LoadStage(Stages stage)
        {
            if (currentStage != null)
            {
                currentStage.Destroy();
            }

            currentStage = new Stage(stage);
        }

        /// <summary>
        /// Clears the current stage if there is one
        /// </summary>
        public static void ClearCurrentStage()
        {
            if (currentStage != null)
            {
                currentStage.Destroy();
            }
            else Console.WriteLine("There aren't any stages to get rid of I'm afraid");
        }

        /// <returns>A list of children of the current active stage</returns>
        public static List<GameObject> GetChildren()
        {
            return currentStage.GetChildren();
        }

        /// <summary>
        /// Adds a given GameObject to this stages hierarchy
        /// </summary>
        public static void AddObject(GameObject gameObject)
        {
            currentStage.AddChild(gameObject);
        }

        /// <summary>
        /// Adds a given GameObject to this stages hierarchy on a given index
        /// </summary>
        public static void AddObjectAt(GameObject gameObject, int index)
        {
            currentStage.AddChildAt(gameObject,index);
        }

        /// <returns>A list of enemies in the current stage</returns>
        public static List<Enemy> GetEnemies()
        {
            return currentStage.GetEnemies();
        }

     
        /// <returns>A list of enemies in the current stage</returns>
        public static List<Entity> GetEntities()
        {
            return currentStage.GetEntities();
        }
        
    }
    
}