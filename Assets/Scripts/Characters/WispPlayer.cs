using UnityEngine;
using System.Collections;

public class WispPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

	// Use this for initialization
	void Start () {
        terrainManager = GameObject.FindGameObjectWithTag("TerrainManager").GetComponent<TerrainManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Plant a tree/Pick up a seed at the player's current position
    public override void ExecuteAction(){
        //play the animation
        Debug.Log("PlantTree");
        terrainManager.WispAction(gameObject.transform.position);
    }


}
