using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Color buttonDisabled;
    private Color buttonEnabled;

    private float Timer;
    private float toggleTimerAt;
    private float blinkInterval = 1;

    private bool player1Ready = false;
    private bool player2Ready = false;
    private bool bothPlayersAreReady = false;

    private int[] playerButtonUpDown;

    public Text player1Text;
    public Text player2Text;

    public Transform buttons;
    public Transform buttonPlay;
    public Transform buttonTutorial;

    public string buttonSelected;

    public Text readyToStart;

    // Use this for initialization
    void Start () {
        Timer = 0;

        player1Text = transform.FindChild("Player1Text").GetComponent<Text>();
        player2Text = transform.FindChild("Player2Text").GetComponent<Text>();

        readyToStart = transform.FindChild("ReadyToStart").GetComponent<Text>();
        readyToStart.enabled = false;

        buttonDisabled = Color.white;
        buttonEnabled = Color.green;//new Color(105, 158,45);
        buttonSelected = "Play";

        buttons = transform.FindChild("Buttons");

        ButtonsEnabled(false);

        playerButtonUpDown = new int[2];

        buttonPlay = buttons.FindChild("Play");
        buttonTutorial = buttons.FindChild("Tutorial");
    }
	
	// Update is called once per frame
	void Update () {
        CheckInitialInput();

        if (bothPlayersAreReady) {
            ButtonsEnabled(true);

            //////////////////////////
            // Menu up and down input
            //////////////////////////
            float vertical1 = -Input.GetAxis("Player1Horizontal");
            float vertical2 = -Input.GetAxis("Player2Horizontal");

            if (vertical1 < 0 || vertical2 < 0)
            {
                buttonPlay.GetComponent<Image>().color = buttonDisabled;
                buttonTutorial.GetComponent<Image>().color = buttonEnabled;
                buttonSelected = "Tutorial";
            }
            if (vertical1 > 0 || vertical2 > 0)
            {
                buttonPlay.GetComponent<Image>().color = buttonEnabled;
                buttonTutorial.GetComponent<Image>().color = buttonDisabled;
                buttonSelected = "Play";
            }

            /////////////////
            // Select option
            /////////////////
            if (Input.GetButton("Player1PickupSeed") || Input.GetButton("Player2Action"))
            {
                if (buttonSelected.Equals("Tutorial"))
                    GameInstance.instance.isTutorialMode = true;

                GameInstance.instance.ToMainGame();
            }
        }
    }

    //End the game
    void Quit() {
        Application.Quit();
    }

    void ButtonsEnabled(bool enabled)
    {
        buttons.gameObject.SetActive(enabled);
    }

    void CheckInitialInput()
    {
        if ((Input.GetButtonDown("Player1PickupSeed") || Input.GetButtonDown("Player1PickupSeedKeyboard")) && !player1Ready)
        {
            player1Text.text = "Player 1 ready!";
            player1Ready = true;
            playerButtonUpDown[0]++;
        }
        if (player1Ready && Input.GetButtonUp("Player1PickupSeed"))
            playerButtonUpDown[0]++;

        if (Input.GetButtonDown("Player2Action") && !player2Ready)
        {
            player2Text.text = "Player 2 ready!";
            player2Ready = true;
            playerButtonUpDown[1]++;
        }
        if (player2Ready && Input.GetButtonUp("Player2Action"))
            playerButtonUpDown[1]++;

        bothPlayersAreReady = playerButtonUpDown[0] >= 2 && playerButtonUpDown[1] >= 2;
    }
}
