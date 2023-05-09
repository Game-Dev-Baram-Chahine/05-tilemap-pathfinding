using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    [Tooltip("player object of the game, to determinate if the cave is playable")]
    [SerializeField] GameObject playerObject;
    
    [Tooltip("number of times that it will create the cave")]
    [SerializeField] int numOfTries = 10;

    [Tooltip("number of tiles that it will need to see to use the cave")]
    [SerializeField] int numOfTilesToSee = 100;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    private CaveGenerator caveGenerator;

    void Start()  {
        //To get the same random numbers each time we run the script
        Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();
                
        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());
            
        //Start the simulation
        StartCoroutine(SimulateCavePattern());
        
        if (canNotSeeMoreThanXTiles(numOfTilesToSee))
            if (numOfTries-- > 0)
                Start();
            else
            {
                Debug.Log("cannot create map, change the 'Random Fill Precent' ");
                //restart the scene
                SceneManager.LoadScene(0);
            }
                
    }



    //determinates if the generated map is playable with more than X tiles to see
    bool canNotSeeMoreThanXTiles(int x)
    {
        /*
         * run BFS
         * count how many tiles it can see
         * when it reaches X it will return true
         * if it ended it will return false
         */
        TilemapGraph tilemapGraph = null;
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        Vector3Int startNode = tilemap.WorldToCell(playerObject.transform.position);
        int bfs = BFS.FindReachableTiles(tilemapGraph, startNode, maxIterations);
        Debug.Log(bfs);
        if (bfs >= x)
            return false;
        else return true;
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    private IEnumerator SimulateCavePattern()  {
        for (int i = 0; i < simulationSteps; i++)   {
            yield return new WaitForSeconds(pauseTime);

            //Calculate the new values
            caveGenerator.SmoothMap();

            //Generate texture and display it on the plane
            GenerateAndDisplayTexture(caveGenerator.GetMap());
        }
        Debug.Log("Simulation completed!");
    }



    //Generate a black or white texture depending on if the pixel is cave or wall
    //Display the texture on a plane
    private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile: floorTile;
                tilemap.SetTile(position, tile);
            }
        }
    }
}
