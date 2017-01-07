using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
    public PauseMenu pauseMenu;
    public EquilibriumGauge gauge;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("PauseGame") && !pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(true);
        }
	
	}
}
