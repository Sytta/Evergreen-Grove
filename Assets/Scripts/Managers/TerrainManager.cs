using UnityEngine;
using System.Collections;

public class TerrainManager : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        gridMinLength = new Vector2(30, 30);
        squareLength = 1;

        treePercentage = 0.5f;
        natureLevel = 0.5f;
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

                // If this tile doesn't collide with a mountain, water etc. it will be walkable
                if (!Physics.CheckSphere(worldPosition, squareLength/2, unwalkableMask))
                {
                    float probability = Random.Range(0f, 1f);

                    // In this case Random chose to spawn a tree here
                    if (probability < treePercentage)
                        newTileState = Tile.TileState.Tree;
                    else
                        newTileState = Tile.TileState.Empty;
                }

                grid[i, j] = new Tile(newTileState, gridPosition, worldPosition);
            }
        }
    }

    public void InfectRandomTree()
    {
        //Goes through the list of healthy trees and pick one at random
        //Call ReceiveDisease() on the tree

    }
    public void SpreadInfection(Vector2 gridPosition)
    {
        //Sets all adjacent tiles to infected if there is a healthy tree

    }
    public void SpreadSeed(Vector2 gridPosition)
    {
        //Sets all adjacent tiles to seeds if there is a healthy tree
    }
    public void AddRandomSeed()
    {
        //Add random seed to a tree (integer)
    }
    public void PlantTree(Vector2 gridPosition)
    {
        //Spawns a tree and add it to the healthy tree list, change the tile from seed to tree

        UpdateNatureLevel();
    }
    void InfectTree(Vector2 gridPosition)
    {
        //Check if the tile is a healthy tree, then infect it . Remove it from the healthy tree list
    }
    void PlantSeed(Vector2 gridPosition)
    {
        //Check if the tile is empty, then plant a seed on it
    }
    void UpdateNatureLevel()
    {

    }
    public void KillTree(Vector2 gridPosition)
    {
        // Sets the gameobject of the tile at this position to null
        // Sets the state of this tile to Empty
        // Remove the tree from the diseased trees list

        UpdateNatureLevel();
    }
    // For debugging purposes, showing the grid.
    void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.grid.GetLength(1); j++)
                {
                    // Unwalkable tiles will be rendered red, walkable tiles green.
                    if (this.grid[i, j].GetState() == Tile.TileState.UnWalkable)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.green;

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
}