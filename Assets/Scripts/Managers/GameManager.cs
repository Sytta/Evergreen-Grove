using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum GM_InGame_State { Initialising , Playing, Paused ,Ending}
public enum GM_Nature_State { Equilibrium, LowNatureLevel, HighNatureLevel }
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float gameTimer = 0;
    public float equilibriumRange = .2f;
    public int seeds=0;
    public TerrainManager terrainManager;
    public GM_InGame_State state;
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

    // Update is called once per frame
    void Update()
    {
        if (state == GM_InGame_State.Playing)
        {
            //Was there a change in the nature level
            if(deltaNatureLevel != GetNatureLevel())
            {
                deltaNatureLevel = GetNatureLevel();
                if (GetNatureState()== GM_Nature_State.HighNatureLevel)//Trees are sickly, update them
                {
                    StartCoroutine("TurnAllTreesSickly");
                }
                else if(deltaNatureState == GM_Nature_State.HighNatureLevel)//Trees were sickly but now arent, update them
                {
                    StartCoroutine("TurnAllTreesHealthy");
                }
            }
            gameTimer += Time.deltaTime;
        }
        
    }
    IEnumerator TurnAllTreesHealthy()
    {
        foreach (TreeComponent tree in FindObjectsOfType<TreeComponent>())
        {
            tree.TurnHealthy();
        }
        return null;
    }
    IEnumerator TurnAllTreesSickly()
    {
        foreach (TreeComponent tree in FindObjectsOfType<TreeComponent>())
        {
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
            return GM_Nature_State.LowNatureLevel;
        }
        if(GetNatureLevel() >= EQUILIBRIUM_LEVEL + (equilibriumRange / 2))
        {
            return GM_Nature_State.HighNatureLevel;
        }
        return GM_Nature_State.Equilibrium;
    }
    public void StartGame()
    {
        state = GM_InGame_State.Playing;
    }    public void EndGame()
    {
        state = GM_InGame_State.Ending;
    }
    public void PauseGame()
    {
        state = GM_InGame_State.Paused;
    }
    public void Initialise()
    {
        state = GM_InGame_State.Initialising;

        StartCoroutine(callThisABitLater());
    }

    // Test code
    IEnumerator callThisABitLater()
    {
        if (terrainManager)
        {
            terrainManager = FindObjectOfType<TerrainManager>();

            terrainManager.Initialise();
            terrainManager.GenerateGrid();

            Debug.Log(terrainManager.WorldPosToGridPos(new Vector3(-11, 0, -12)));
            Debug.Log(terrainManager.WorldPosToGridPos(new Vector3(-2, 0, -14)));
            Debug.Log(terrainManager.WorldPosToGridPos(new Vector3(14.4f, 0, -6)));

            yield return new WaitForSeconds(2);

            // This will eventually become a loop where either of those two are called once in a while
            // This loop will be placed in the EventManager.
            //terrainManager.AddRandomSeed();
            //terrainManager.InfectRandomTree();
        }
    }
}
