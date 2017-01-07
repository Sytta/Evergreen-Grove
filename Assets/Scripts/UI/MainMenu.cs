using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private float Timer;
    public Text text; 

    // Use this for initialization
    void Start () {
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Blinking Effect
        Timer += Time.deltaTime;

        if (Timer >= 0.4)
            text.enabled = true;

        if (Timer >= 1) {
            text.enabled = false;
            Timer = 0;
        }

        if (Input.anyKey)
            GameInstance.instance.ToMainGame();
    }

    //End the game
    void Quit() {
        Application.Quit();
    }

}
