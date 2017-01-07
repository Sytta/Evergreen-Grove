using UnityEngine;
using System.Collections;
public enum InGame_State { Playing,Starting,Paused}
public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public float gameTimer=0;
    public InGame_State state;
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
        state = InGame_State.Starting;

    }
	
	// Update is called once per frame
	void Update () {
        if(state==InGame_State.Playing)
        {
            gameTimer += Time.deltaTime;
        }
	
	}
    public float GetNatureLevel()
    {
        return 0.5f;
    }
}
