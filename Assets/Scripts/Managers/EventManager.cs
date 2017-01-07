using UnityEngine;
using System.Collections;
public enum EM_Goal { Break_Equilibrium, Create_Disease, Create_Seed }

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public float frequency = 1; //how often an event can occur
    public float magnitude = 1; //how large the event can be
    public float spread = 1;    //how many places can get affected simultaneously
    public float severity = 1;  //how much it is affected by the nature level

    // Use this for initializations
    void Awake()
    {
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GM_InGame_State.Playing)
        {

        }

    }



}