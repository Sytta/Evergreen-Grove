using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {
    public bool enabledMenu=false;
	// Use this for initialization
	void Start () {
        
	}
	public void EnableMenu()
    {
        enabledMenu = true;
        GameManager.instance.PauseGame();
        GetComponent<FadeUI>().alphaGoal = 0.6f;
        GetComponent<FadeUI>().StartFade();
        GetComponentInChildren<Text>().enabled = true;

    }
    public void DisableMenu()
    {
        enabledMenu = false;
        GetComponent<FadeUI>().alphaGoal = 0.0f;
        GetComponent<FadeUI>().StartFade();
        GetComponentInChildren<Text>().enabled = false;

    }
    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("PauseGame") && !enabledMenu)
        {
            EnableMenu();
        }
        else if (GameManager.instance.state == GM_InGame_State.Paused && Input.anyKeyDown && enabledMenu)
        {
            GameManager.instance.UnPauseGame();
            DisableMenu();
        }
	}
}
