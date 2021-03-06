﻿using UnityEngine;
using System.Collections;

public class InGameUI : MonoBehaviour {
    public PauseMenu pauseMenu;
    public EquilibriumGauge gauge;
    public GameObject congratsText;
    public GameObject loseText;
    public GameObject wireFrame;
    public GameObject seedPanel;
    public GameObject diseasePanel;
    public GameObject introTutorialPanel;
    bool waitingToQuit = false;
    bool diseaseTutorialPlayed = false;
    bool seedTutorialPlayed = false;
    bool waitingToUnpause = false;
    bool seedTutorialPlaying=false;
    bool showingIntroTutorial = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(waitingToQuit && Input.anyKey)
        {
            GameInstance.instance.ToMainMenu();
        }
        if(waitingToUnpause && (Input.GetButton("Player1Confirm") || Input.GetButton("Player2Confirm")))
        {
            GameManager.instance.UnPauseGame();
            if(seedTutorialPlaying)
            {
                seedPanel.gameObject.SetActive(false);
                seedTutorialPlaying = false;
            }
            else
            {
                diseasePanel.gameObject.SetActive(false);
                seedTutorialPlaying = true;
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
            if (showingIntroTutorial && (Input.GetButton("Player1PickupSeed") || Input.GetButton("Player2Action")))
            {
                showingIntroTutorial = false;
                introTutorialPanel.SetActive(false);
                GameManager.instance.PlayStartIntro();
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
    public void ShowIntroTutorial()
    {
        showingIntroTutorial = true;
        introTutorialPanel.SetActive(true);
    }

}
