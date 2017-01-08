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

        if (terrainManager != null)
        {
            if (Input.GetButtonDown(characterName + "AddTree") || Input.GetButtonDown(characterName + "AddTreeKeyboard"))
            {
                terrainManager.WispAction(gameObject.transform.position, "AddTree");
            }

            if (Input.GetButtonDown(characterName + "PickupSeed") || Input.GetButtonDown(characterName + "PickupSeedKeyboard"))
            {
                terrainManager.WispAction(gameObject.transform.position, "PickupSeed");
            }
        }   
    }

    //Plant a tree/Pick up a seed at the player's current position
    public override void ExecuteAction(){
        
    }
}
