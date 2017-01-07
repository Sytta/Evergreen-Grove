using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour
{
    public List<GameObject> treePrefabs;

    public GameObject seedPrefab;
    public float heightOfSeed;

    // The LayerMask where the players should not be able to walk
    public LayerMask unwalkableMask;

    // How wide and long we want the grid to be
    private Vector2 gridMinLength;
    // How big each square inside the grid will be
    private float squareLength;

    // How many trees we shall start with (percentage)
    float treePercentage;

    // How much percentage of trees is present and healthy
    // (percentage)
    private float natureLevel;

    // The final Grid
    private Tile[,] grid;

    // A list of healthy trees
    List<Tile> trees_healthy;

    // A list of healthy trees that have a seed that is about to burst
    List<Tile> trees_seed;

    // A list of diseased trees
    List<Tile> trees_disease;

    

    public void Initialise()
    {
        gridMinLength = new Vector2(30, 30);
        squareLength = 3f;

        treePercentage = 0.5f;
        natureLevel = 0.5f;

        heightOfSeed = 4.7f;

        trees_healthy = new List<Tile>();
        trees_seed    = new List<Tile>();
        trees_disease = new List<Tile>();
    }


    // Instantiates the internal grid variable
    public void GenerateGrid()
    {
        // Figure out how many x and y squares there will be in this grid.
        int xAmount = (int)Mathf.Ceil(this.gridMinLength.x / this.squareLength);
        int yAmount = (int)Mathf.Ceil(this.gridMinLength.x / this.squareLength);

        this.grid = new Tile[xAmount,yAmount];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                // The world position of this Tile. The pivot is in the center of the tile
                Vector3 worldPosition = transform.position + new Vector3(squareLength/2 + squareLength * i,
                                                                         0,
                                                                         squareLength/2 + squareLength * j);
                // Where this Tile is located on the grid
                Vector2 gridPosition = new Vector2(i, j);

                // Now we shall determine the state of this Tile
                Tile.TileState newTileState = Tile.TileState.UnWalkable;

                GameObject currentObject = null;

                // If this tile doesn't collide with a mountain, water etc. it will be walkable
                if (!Physics.CheckSphere(worldPosition, squareLength/2, unwalkableMask))
                {
                    float probability = Random.Range(0f, 1f);

                    // In this case Random chose to spawn a tree here
                    if (probability < treePercentage)
                    {
                        newTileState = Tile.TileState.Tree;
                        // Spawn the tree with a random rotation and keep a reference to it in the Tile
                        currentObject = Instantiate(treePrefabs[0],
                            new Vector3(worldPosition.x, 0, worldPosition.z),
                            Quaternion.Euler(0, (int)Random.Range(0f, 359f), 0)) as GameObject;
                    }
                    else
                        newTileState = Tile.TileState.Empty;
                }

                grid[i, j] = new Tile(newTileState, gridPosition, worldPosition, currentObject);

                // Add the Tile to the healthy tree list
                if (newTileState == Tile.TileState.Tree)
                    this.trees_healthy.Add(grid[i, j]);
            }
        }
    }

    // Places a disease on a random healthy tree
    public void InfectRandomTree()
    {
        if (trees_healthy.Count != 0)
        {
            // Pick a random healthy tree tile
            int you = Random.Range(0, this.trees_healthy.Count - 1);
            Tile newSeedTile = this.trees_healthy[you];
            // Remove this tile from the healthy list
            this.trees_healthy.Remove(newSeedTile);

            newSeedTile.SetState(Tile.TileState.Disease);
            newSeedTile.GetCurrentObject().GetComponent<TreeComponent>().ReceiveDisease();

            // Place this Tile in the disease list
            this.trees_disease.Add(newSeedTile);
        }
    }

    // Places a seed on a random healthy tree
    public void AddRandomSeed()
    {
        if (trees_healthy.Count != 0)
        {
            // Pick a random healthy tree tile
            int you = Random.Range(0, this.trees_healthy.Count - 1);
            Tile newSeedTile = this.trees_healthy[you];
            // Remove this tile from the healthy list
            this.trees_healthy.Remove(newSeedTile);

            Vector3 spawnPosition = newSeedTile.GetWorldPosition();
            spawnPosition.y = 0;

            // Instantiate a random seed to that tree's tile
            GameObject newSeed = Instantiate(seedPrefab,
                spawnPosition + new Vector3(0, this.heightOfSeed, 0),
                Quaternion.identity) as GameObject;

            newSeedTile.SetCurrentObject(newSeed);
            newSeedTile.SetState(Tile.TileState.Seed);

            // Place this Tile in the seed list
            this.trees_seed.Add(newSeedTile);
        }
    }

    // A TreeComponent script calls this function when the tree dies and spreads
    // its disease.
    public void SpreadInfection(Vector3 worldPosition)
    {
        //Sets all adjacent tiles to infected if there is a healthy tree

        
    }

    // A TreeComponent script calls this function when a seed on a tree
    // bursts.
    public void SpreadSeed(Vector3 worldPosition)
    {
        //Sets all adjacent tiles to seeds if there is a healthy tree
    }
    

    public void SpawnTree(Vector3 worldPosition)
    {
        //Spawns a tree and add it to the healthy tree list, change the tile from seed to tree

        UpdateNatureLevel();
    }

    void InfectTree(Vector3 worldPosition)
    {
        //Check if the tile is a healthy tree, then infect it . Remove it from the healthy tree list
    }

    void PlantSeed(Vector3 worldPosition)
    {
        //Check if the tile is empty, then plant a seed on it
    }

    void UpdateNatureLevel()
    {

    }


    //////////////
    // Lumberjack/Wisp call these three functions
    //////////////

    // Wisp picks up a seed at a worldPosition
    public void PickUpSeed(Vector3 worldPosition)
    {

    }

    // Wisp plants a tree
    public void PlantTree(Vector3 worldPosition)
    {

    }

    // The lumberjack calls this function when he is finished cutting the tree
    public void RemoveTree(Vector3 worldPosition)
    {

    }

    public void KillTree(Vector3 worldPosition)
    {
        // Sets the gameobject of the tile at this position to null
        // Sets the state of this tile to Empty
        // Remove the tree from the diseased trees list

        UpdateNatureLevel();
    }

    // For debugging purposes, showing the grid.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (grid != null)
        {
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.grid.GetLength(1); j++)
                {
                    // Only draw a wire cube if a tree can be spawned there
                    if (!(this.grid[i,j].GetState() == Tile.TileState.UnWalkable))
                        Gizmos.DrawWireCube(this.grid[i, j].GetWorldPosition(), Vector3.one*this.squareLength);
                }
            }
        }
    }

    // Returns a float between 0 and 1 which tells us where on the equilibrium bar we are now
    public float GetNatureLevel()
    {
        return this.natureLevel;
    }


    public Vector2 WorldPosToGridPos(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - transform.position;

        Vector2 gridPosition = new Vector2(0, 0);

        // Make sure our new gridPosition wont be out of bounds
        float xPosClamped = Mathf.Clamp(localPosition.x, 0, this.gridMinLength.x);
        float yPosClamped = Mathf.Clamp(localPosition.z, 0, this.gridMinLength.y);

        gridPosition.x = (int)(xPosClamped-1 / squareLength);
        gridPosition.y = (int)(yPosClamped-1 / squareLength);

        return gridPosition;
    }
    //void HighlightTiles()
    //{
    //    foreach (PlayerCharacter player in GameManager.instance.players)
    //    {
    //        Vector2 gridPos = WorldPosToGridPos(player.transform.position);
    //        grid[gridPos.x,gridPos.y].
    //    }
        
    //}
}