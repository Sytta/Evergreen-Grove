using UnityEngine;
using System.Collections;

public class EquilibriumGauge : MonoBehaviour {
    public 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    switch(GameManager.instance.GetNatureState())
        {
            case GM_Nature_State.Equilibrium:
                break;
            case GM_Nature_State.HighNatureLevel:
                break;
            case GM_Nature_State.LowNatureLevel:
                break;
        }
	}
}
