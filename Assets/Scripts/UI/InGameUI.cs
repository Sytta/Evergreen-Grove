using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
    public PauseMenu pauseMenu;
    public EquilibriumGauge gauge;
    public GameObject congratsText;
    public GameObject loseText;
    public GameObject wireFrame;
    public GameObject seedPanel;
    public GameObject diseasePanel;
    bool waitingToQuit = false;
    bool diseaseTutorialPlayed = false;
    bool seedTutorialPlayed = false;
    bool waitingToUnpause = false;
    bool seedTutorialPlaying=false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(waitingToQuit && Input.anyKey)
        {
            GameInstance.instance.ToMainMenu();
        }
        if(waitingToUnpause && (Input.GetButton("Player1PickupSeed") || Input.GetButton("Player2Action")))
        {
            GameManager.instance.UnPauseGame();
            if(seedTutorialPlaying)
            {
                seedPanel.gameObject.SetActive(false);
            }
            else
            {
                diseasePanel.gameObject.SetActive(false);
            }
        }
        if(GameManager.instance.isTutorialMode)
        {
            if(EventManager.instance.firstDisease && !diseaseTutorialPlayed)
            {
                diseaseTutorialPlayed = true;
                GetComponent<Animation>().Play("DiseaseTut");
            }
            else if(EventManager.instance.firstSeed && !seedTutorialPlayed)
            {
                seedTutorialPlayed = true;
                GetComponent<Animation>().Play("SeedTut");
                seedTutorialPlaying = true;
            }
        }
	
	}
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
        waitingToUnpause = true;
    }
    public void WaitToQuit()
    {
        waitingToQuit = true;
    }

}
