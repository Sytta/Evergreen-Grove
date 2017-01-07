using UnityEngine;
using System.Collections;
public enum GM_InGame_State { Playing,Starting,Paused}
public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public float gameTimer=0;
    public GM_InGame_State state;
    // Use this for initialization
    void Awake () {
	    if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        state = GM_InGame_State.Starting;

    }
	
	// Update is called once per frame
	void Update () {
        if(state== GM_InGame_State.Playing)
        {
            gameTimer += Time.deltaTime;
        }
	
	}
    public float GetNatureLevel()
    {
        return 0.5f;
    }
}
