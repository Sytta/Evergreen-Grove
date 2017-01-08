using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private float Timer;
    private float toggleTimerAt;
    private float blinkInterval = 1;

    private bool player1Ready = false;
    private bool player2Ready = false;
    private bool bothPlayersAreReady = false;

    private int[] playerButtonUpDown;

    public Text player1Text;
    public Text player2Text;

    public Text readyToStart;

    public Text text;

    // Use this for initialization
    void Start () {
        Timer = 0;

        player1Text = transform.FindChild("Player1Text").GetComponent<Text>();
        player2Text = transform.FindChild("Player2Text").GetComponent<Text>();

        readyToStart = transform.FindChild("ReadyToStart").GetComponent<Text>();
        readyToStart.enabled = false;

        playerButtonUpDown = new int[2];
    }
	
	// Update is called once per frame
	void Update () {
        CheckInitialInput();

        if (bothPlayersAreReady) {
            BlinkReadyText();

            if (Input.anyKeyDown)
                GameInstance.instance.ToMainGame();
        }
    }

    void BlinkReadyText()
    {
        Timer += Time.deltaTime;

        if (Timer >= toggleTimerAt)
        {
            readyToStart.enabled = !readyToStart.enabled;
            toggleTimerAt += blinkInterval;
        }            
    }

    //End the game
    void Quit() {
        Application.Quit();
    }

    void CheckInitialInput()
    {
        if (Input.GetButtonDown("Player1Action") && !player1Ready)
        {
            player1Text.text = "Player 1 ready!";
            player1Ready = true;
            playerButtonUpDown[0]++;
        }
        if (player1Ready && Input.GetButtonUp("Player1Action"))
            playerButtonUpDown[0]++;

        if (Input.GetButtonDown("Player2Action") && !player2Ready)
        {
            player2Text.text = "Player 2 ready!";
            player2Ready = true;
            playerButtonUpDown[1]++;
        }
        if (player2Ready && Input.GetButtonUp("Player2Action"))
            playerButtonUpDown[1]++;

        bothPlayersAreReady = playerButtonUpDown[0] == 2 && playerButtonUpDown[1] == 2;
    }
}
