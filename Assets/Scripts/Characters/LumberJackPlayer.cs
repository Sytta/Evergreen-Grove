using UnityEngine;
using System.Collections;

public class LumberJackPlayer : PlayerCharacter {

    private TerrainManager terrainManager;
    bool cuttingTree = false;

	// Use this for initialization
	 protected override void Start () {
        base.Start();
        characterName = "Player2";
        GameObject tm = GameObject.FindGameObjectWithTag("TerrainManager");

        if (tm != null)
            terrainManager = tm.GetComponent<TerrainManager>();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if((Input.GetButton(characterName+"Action") || Input.GetButton(characterName + "ActionKeyboard")) && !cuttingTree)
        {
            anim.SetBool("Chopping", true);
            StartCoroutine(CutTree());
        }
        else
        {
            anim.SetBool("Chopping", false);
        }
    }

    //Cut down Tree
    public override void ExecuteAction()
    {
        Debug.Log("Cut Down Tree");
        terrainManager.CutTree(transform.position);
    }

    IEnumerator CutTree()
    {
        cuttingTree = true;
        Vector3 cutPosition = transform.position;
        if(terrainManager)
            terrainManager.CutTree(cutPosition);
        cuttingTree = false;
        yield return null;
    }
}
