using UnityEngine;
using System.Collections;

public class LumberJackPlayer : PlayerCharacter {

    private TerrainManager terrainManager;

	// Use this for initialization
	 protected override void Start () {
        base.Start();
        terrainManager = GameObject.FindGameObjectWithTag("TerrainManager").GetComponent<TerrainManager>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if(Input.GetButton("CutDownTree"))
        {
            anim.SetBool("Chopping",true);
        }
        else
        {
            anim.SetBool("Chopping", false);
        }
    }

    //
    public override void ExecuteAction()
    {
        Debug.Log("Cut Down Tree");
        terrainManager.RemoveTree(transform.position);
    }
}
