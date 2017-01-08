using UnityEngine;
using System.Collections;

public class WispPlayer : PlayerCharacter {

    private TerrainManager terrainManager;
    /*float actionCoolDownTimer;
    float actionCoolDownTime;*/

	// Use this for initialization
	protected override void Start () {
        base.Start();

        // How much time needs to pass between the Wisp's actions
        /*actionCoolDownTime = 1;
        actionCoolDownTimer = 0;*/

        characterName = "Player1";

        terrainManager = FindObjectOfType<TerrainManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetButtonDown(characterName+"Action"))
        {
            ExecuteAction();
        }

        //actionCoolDownTimer += Time.deltaTime;
    }

    //Plant a tree/Pick up a seed at the player's current position
    public override void ExecuteAction(){
        //play the animation

        /*if (actionCoolDownTimer >= actionCoolDownTime)
        {
            actionCoolDownTimer = 0;*/
           if(terrainManager != null)
            terrainManager.WispAction(gameObject.transform.position);
        //}
    }
}
