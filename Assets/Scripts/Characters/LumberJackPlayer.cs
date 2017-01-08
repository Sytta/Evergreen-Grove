using UnityEngine;
using System.Collections;

public class LumberJackPlayer : PlayerCharacter {

    private TerrainManager terrainManager;
    bool cuttingTree = false;

    private AudioSource audioSource;

	// Use this for initialization
	 protected override void Start () {
        base.Start();
        characterName = "Player2";
        GameObject tm = GameObject.FindGameObjectWithTag("TerrainManager");

        if (tm != null)
            terrainManager = tm.GetComponent<TerrainManager>();

        audioSource = transform.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if((Input.GetButton(characterName+"Action") || Input.GetButton(characterName + "Action")) && !cuttingTree)
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
        bool treeCut = terrainManager.CutTree(transform.position);
        /*//if (treeCut)
            audioSource.Play();*/
    }

    IEnumerator CutTree()
    {
        cuttingTree = true;
        Vector3 cutPosition = transform.position;
        if(terrainManager)
        {
            bool treeCut = terrainManager.CutTree(cutPosition);
            if (treeCut)
                audioSource.Play();
        }
        cuttingTree = false;
        yield return null;
    }
}
