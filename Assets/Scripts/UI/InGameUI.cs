using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
    public PauseMenu pauseMenu;
    public EquilibriumGauge gauge;
    public GameObject congratsText;
    public GameObject loseText;
    bool waitingToQuit = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(waitingToQuit && Input.anyKey)
        {
            GameInstance.instance.ToMainMenu();
        }

	
	}
    public void WaitToQuit()
    {
        waitingToQuit = true;
    }

}
