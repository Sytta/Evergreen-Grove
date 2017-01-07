using UnityEngine;
using System.Collections;

public class WispPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Plant a tree at the player's current position
    public override void ExecuteAction(){
        //play the animation
        Debug.Log("PlantTree");
        //terrainManager.PlantTree(gameObject.transform.position);
    }

    //Pick up a seed at the player's current position
    void PickUpSeed() {
        //play animation
        Debug.Log("PickUpSeed");
        //terrainManager.PickUpSeed(gameObject.transform.position);

    }
}
