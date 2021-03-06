﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum GM_InGame_State { Initialising ,Starting, Playing, Paused ,Ending}
public enum GM_Nature_State { Equilibrium, LowNatureLevel, HighNatureLevel,VeryHighNatureLevel,VeryLowNatureLevel }
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float gameTimer = 0;
    public float equilibriumRange = .01f;
    public TerrainManager terrainManager;
    public InGameUI ui;
    public GM_InGame_State state;
    public bool isTutorialMode =false;
    //public List<PlayerCharacter> players;
    public const float EQUILIBRIUM_LEVEL=0.5F;
    private float deltaNatureLevel;
    private GM_Nature_State deltaNatureState=GM_Nature_State.Equilibrium;
    // Use this for initialization
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Initialise();
        


    }
    void Start()
    {
        isTutorialMode = GameInstance.instance.isTutorialMode;
        if(isTutorialMode)
        {
            FindObjectOfType<InGameUI>().ShowIntroTutorial();
        }
        else
        {
            PlayStartIntro();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(GetNatureLevel() <= 0.15 || GetNatureLevel() >= 0.85)
        {
            //if(GetNatureLevel() >= 0.85)
            //{
            //    StartCoroutine("KillAllTrees");
            //}
            EndGame(false);
        }
        if (state == GM_InGame_State.Playing)
        {
            //Was there a change in the nature level
            if(deltaNatureLevel != GetNatureLevel())
            {
                deltaNatureLevel = GetNatureLevel();
                if (GetNatureState()== GM_Nature_State.HighNatureLevel || GetNatureState()== GM_Nature_State.VeryHighNatureLevel)//Trees are sickly, update them
                {
                    StartCoroutine("TurnAllTreesSickly");
                }
                else if(deltaNatureState == GM_Nature_State.HighNatureLevel && GetNatureState()!= GM_Nature_State.HighNatureLevel && GetNatureState() !=  GM_Nature_State.VeryHighNatureLevel)//Trees were sickly but now arent, update them
                {
                    StartCoroutine("TurnAllTreesHealthy");
                }
            }
            gameTimer += Time.deltaTime;
        }
        
    }
    IEnumerator KillAllTrees()
    {
        foreach (TreeComponent tree in FindObjectsOfType<TreeComponent>())
        {
            tree.CutDown();
        }
        return null;
    }
    IEnumerator TurnAllTreesHealthy()
    {
        foreach (TreeComponent tree in FindObjectsOfType<TreeComponent>())
        {
            if (!tree.isDiseased)
                tree.TurnHealthy();
        }
        return null;
    }
    IEnumerator TurnAllTreesSickly()
    {
        foreach (TreeComponent tree in FindObjectsOfType<TreeComponent>())
        {
            if(!tree.isDiseased)
                tree.TurnSickly();
        }
        return null;
    }
    public float GetNatureLevel()
    {
        return terrainManager.GetNatureLevel();
    }
    public GM_Nature_State GetNatureState()
    {
        if (GetNatureLevel()<= EQUILIBRIUM_LEVEL-(equilibriumRange/2))
        {
            if(GetNatureLevel()<=0.25)
                return GM_Nature_State.VeryLowNatureLevel;
            return GM_Nature_State.LowNatureLevel;
        }
        if(GetNatureLevel() >= EQUILIBRIUM_LEVEL + (equilibriumRange / 2))
        {
            if (GetNatureLevel() >= 0.75)
                return GM_Nature_State.VeryHighNatureLevel;
            return GM_Nature_State.HighNatureLevel;
        }
        return GM_Nature_State.Equilibrium;
    }
    public void StartGame()
    {
        state = GM_InGame_State.Playing;
    }
    public void EndGame(bool win)
    {
        state = GM_InGame_State.Ending;
        if(win)
            ui.congratsText.gameObject.SetActive(true);
        else
            ui.loseText.gameObject.SetActive(true);

    }
    public void PlayStartIntro()
    {
        Camera.main.GetComponent<Animation>().Play("CameraIntroAnim");
    }
    public void PauseGame()
    {
        state = GM_InGame_State.Paused;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        state = GM_InGame_State.Playing;
        Time.timeScale = 1;
    }
    public void Initialise()
    {
        state = GM_InGame_State.Initialising;

        terrainManager = FindObjectOfType<TerrainManager>();

        terrainManager.Initialise();
        terrainManager.GenerateGrid();

       //StartCoroutine(callThisABitLater());
    }

    // Test code
    IEnumerator callThisABitLater()
    {

        yield return new WaitForSeconds(2);

        // This will eventually become a loop where either of those two are called once in a while
        // This loop will be placed in the EventManager.
        terrainManager.AddRandomSeed();
        //terrainManager.InfectRandomTree();
    }
}
