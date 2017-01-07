using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score;
    public int multiplier;
    public Text scoreText;
    public Text multiplierText;

    private int IntScore;

    // Use this for initialization
    void Start()
    {
        score = 0;
        multiplier = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetNatureState() == GM_Nature_State.Equilibrium)
            multiplier = 4;

        else if (GameManager.instance.GetNatureState() == GM_Nature_State.LowNatureLevel || GameManager.instance.GetNatureState() == GM_Nature_State.HighNatureLevel)
            multiplier = 2;

        else if (GameManager.instance.GetNatureState() == GM_Nature_State.VeryLowNatureLevel || GameManager.instance.GetNatureState() == GM_Nature_State.VeryHighNatureLevel)
            multiplier = 1;

        score += multiplier * Time.deltaTime;

        IntScore = (int)score;
        //Update the score every frame
        scoreText.text = "Score: " + IntScore.ToString();
        multiplierText.text = "Multiplier: " + multiplier.ToString();
    }
}
