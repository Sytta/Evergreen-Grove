using UnityEngine;
using System.Collections;
public enum GM_InGame_State { Playing, Starting, Paused ,Ending}
public enum GM_Nature_State { Equilibrium, LowNatureLevel, HighNatureLevel }
public class GameManager : MonoBehaviour
{
    public float debugNatureLevel=0.5f;
    public static GameManager instance = null;
    public float gameTimer = 0;
    public float equilibriumRange = .2f;
    public GM_InGame_State state;
    public const float EQUILIBRIUM_LEVEL=0.5F;

    private float deltaNatureLevel;
    private GM_Nature_State deltaNatureState;
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
        state = GM_InGame_State.Starting;

        StartGame();

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
        return debugNatureLevel;
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
}
