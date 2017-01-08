using UnityEngine;
using System.Collections;

public class WispPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

	// Use this for initialization
	void Awake () {
        GameObject tm = GameObject.FindGameObjectWithTag("TerrainManager");

        if (tm != null)
        {
            terrainManager = GetComponent<TerrainManager>();
        }
    }

    //Plant a tree/Pick up a seed at the player's current position
    public override void ExecuteAction(){
        //play the animation
        terrainManager.WispAction(gameObject.transform.position);
    }
}
