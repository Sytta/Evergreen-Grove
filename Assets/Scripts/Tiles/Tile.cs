using UnityEngine;
using System.Collections;

public class Tile
{
    public enum TileState {UnWalkable, Empty, Tree, Disease, Seed};

    TileState currentState;
    Vector2 gridPosition;
    // The world position of this Tile. The pivot is in the center of the tile
    Vector3 worldPosition;
    //The gameobject tied to this tile
    //Either a Tree or null for now
    public GameObject currentObject;

    public Tile(TileState currentState, Vector2 gridPosition, Vector3 worldPosition, GameObject currentObject)
    {
        this.currentState = currentState;
        this.gridPosition = gridPosition;
        this.worldPosition = worldPosition;
        this.currentObject = currentObject;
    }

    public TileState GetState()
    {
        return this.currentState;
    }

    public void SetState(TileState newState)
    {
        this.currentState = newState;
    }

    public Vector2 GetGridPosition()
    {
        return this.gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return this.worldPosition;
    }

    public void SetCurrentObject(GameObject currentObject)
    {
        this.currentObject = currentObject;
    }

    public GameObject GetCurrentObject()
    {
        return this.currentObject;
    }
}
