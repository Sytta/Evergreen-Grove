using UnityEngine;
using System.Collections;

public class LumberJackPlayer : PlayerCharacter {

    private 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //
    public override void ExecuteAction()
    {
        Debug.Log("Cut Down Tree");
    }
}
