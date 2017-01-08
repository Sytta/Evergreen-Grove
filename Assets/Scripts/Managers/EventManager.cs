using UnityEngine;
using System.Collections;
public enum EM_Goal { Break_Equilibrium, Create_Disease, Create_Seed }

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public float frequency = 6;     //how often an event can occurs
    public float initialSpread = 0.75f;    //the ratio of seed to disease or vice versa
    public float severity = 1;      //The higher the severity, the more frequent the events at further nature levels
    bool wait = false;       //indicates if event manager is currently doing nothing
    bool disease = true;     //indicates direction of nature level the event manager wants to go towards

    public float eventTimer = 0;
    float spread = 0;
    EM_Goal goal;
    bool firstTimeEquilibrium = true;   //indicates if it is the first time entering the equilibirum break loop


    // Use this for initializations
    void Awake()
    {
        instance = this;
        spread = initialSpread;
    }

    // Update is called once per frame
    void Update()
    {
        //check if game is started
        if (GameManager.instance.state == GM_InGame_State.Playing)
        {
            //eventTimer float resets to zero every "frequency" seconds. This creates the frequency at which events occur
            if (eventTimer <= 0)
            {
                wait = false;
                //checks game state and does the appropriate decision ( breaks equilibirum, adds trees, adds diseases)

                //equilibrium breaker
                if (GameManager.instance.GetNatureState() == GM_Nature_State.Equilibrium)
                {
                    spread = initialSpread;
                    if (goal != EM_Goal.Break_Equilibrium)
                    {
                        firstTimeEquilibrium = true;
                    }
                    goal = EM_Goal.Break_Equilibrium;
                    Break_Equilibrium(firstTimeEquilibrium);
                    firstTimeEquilibrium = false;
                }

                //High Nature
                else if (GameManager.instance.GetNatureState() == GM_Nature_State.HighNatureLevel || GameManager.instance.GetNatureState() == GM_Nature_State.VeryHighNatureLevel)
                {
                    spread = initialSpread;
                    goal = EM_Goal.Create_Seed;
                    Create_Seed();
                }

                //Low nature
                else
                {
                    goal = EM_Goal.Create_Disease;
                    spread = 1;
                    Create_Disease();
                }
            }

            eventTimer -= Time.deltaTime;
            wait = true;
        }
    }
    
    //Functions to be used

    //Severity calculator
    void SeverityCalculator()
    {
        //increases severity with nature level
        if (GameManager.instance.GetNatureState() == GM_Nature_State.HighNatureLevel || GameManager.instance.GetNatureState() == GM_Nature_State.VeryHighNatureLevel)
        {
            eventTimer = frequency / (GameManager.instance.GetNatureLevel() * severity); //set the event timer
        }
        else if (GameManager.instance.GetNatureState() == GM_Nature_State.LowNatureLevel || GameManager.instance.GetNatureState() == GM_Nature_State.VeryLowNatureLevel)
        {
            eventTimer = frequency / ((1 - GameManager.instance.GetNatureLevel()) * severity); //set the event timer
        }
        else
            eventTimer = frequency;
    }   
    void Break_Equilibrium(bool firstTime)
    {
        if (firstTime)
        {
            //chooses direction for nature level. Disease means "spread" disease and "1-spread" seeds, and vice versa )
            disease = (Random.value > 0.5f);
            firstTime = false;
        }

        if (disease)
        {
            Create_Disease();
        }
        else
        {
            Create_Seed();
        }
    }

    void Create_Seed()
    {
        //increases severity with nature level
        SeverityCalculator();
      
        //determines ratio of seed to disease
        if (Random.value <= spread)
        {
            //creates the majority type
            GameManager.instance.terrainManager.AddRandomSeed();
        }
        else
        {
            GameManager.instance.terrainManager.InfectRandomTree();
        }
    }


    void Create_Disease()
    {
        SeverityCalculator();
        //determines ratio of seed to disease
        if (Random.value <= spread)
        {
            //creates the majority type
            GameManager.instance.terrainManager.InfectRandomTree();

        }
        else
        {
            GameManager.instance.terrainManager.AddRandomSeed();
        }
    }

}