using UnityEngine;
using System.Collections;

public class LumberJackPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

	// Use this for initialization
	void Start () {
        terrainManager = GameObject.FindGameObjectWithTag("TerrainManager").GetComponent<TerrainManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //
    public override void ExecuteAction()
    {
        Debug.Log("Cut Down Tree");
        terrainManager.RemoveTree(transform.position);
    }
}
