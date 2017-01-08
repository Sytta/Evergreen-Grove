using UnityEngine;
using System.Collections;

public class WispPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        terrainManager = GameObject.FindGameObjectWithTag("TerrainManager").GetComponent<TerrainManager>();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
    }

    //Plant a tree/Pick up a seed at the player's current position
    public override void ExecuteAction(){
        
        Debug.Log("PlantTree");
        terrainManager.WispAction(gameObject.transform.position);
    }


}
