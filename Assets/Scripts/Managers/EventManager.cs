using UnityEngine;
using System.Collections;
public enum EM_Goal { Break_Equilibrium, Create_Disease, Create_Seed }

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public float frequency = 6;     //how often an event can occurs
    public float magnitude = 2;     //how large the event can be
    public float spread = 0.75f;    //the ratio of seed to disease or vice versa
    public float severity = 1;      //how much it is affected by the nature level
    public float timeLimit = 20;    //how long an event last before EXTREME MODE
    public bool wait = false;       //indicates if event manager is currently doing nothing
    public bool disease = true;     //indicates direction of nature level the event manager wants to go towards

    float eventTimer = 0;
    float stateTimer = 0;
    EM_Goal goal;
    bool firstTimeEquilibrium = true;   //indicates if it is the first time entering the equilibirum break loop
    bool isEventTimerStarted = false;   //indicates if the extreme mode event timer started

    //Functions to be used
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
        eventTimer = frequency; //set the event timer
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

    void Create_Seed(float magnitude)
    {
        eventTimer = frequency / magnitude; //set the event timer, which is now smaller due to higher magnitude
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
        eventTimer = frequency; //set the event timer
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

    void Create_Disease(float magnitude)
    {
        eventTimer = frequency / magnitude; //set the event timer
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

    // Use this for initializations
    void Awake()
    {
        instance = this;
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
                    goal = EM_Goal.Create_Seed;
                    Create_Seed();
                    //if you're not in high danger zone, ignore timer
                    if (GameManager.instance.GetNatureState() == GM_Nature_State.HighNatureLevel)
                    {
                        isEventTimerStarted = false;
                    }
                    //if in high danger zone, start timer
                    else if (!isEventTimerStarted && GameManager.instance.GetNatureState() == GM_Nature_State.VeryHighNatureLevel)
                    {
                        eventTimer = timeLimit;
                        isEventTimerStarted = true;
                    }
                }

                //Low nature
                else
                {
                    goal = EM_Goal.Create_Disease;
                    Create_Disease();
                    //if you're not in high danger zone, ignore timer
                    if (GameManager.instance.GetNatureState() == GM_Nature_State.LowNatureLevel)
                    {
                        isEventTimerStarted = false;
                    }
                    //if in high danger zone, start timer
                    else if (!isEventTimerStarted && GameManager.instance.GetNatureState() == GM_Nature_State.VeryLowNatureLevel)
                    {
                        eventTimer = timeLimit;
                        isEventTimerStarted = true;
                    }
                }
            }

            //if danger timer is started, time is at 0 or less, and in very high nature level, go to EXTREME DISEASE MODE!
            if (isEventTimerStarted && eventTimer <= 0 && GameManager.instance.GetNatureState() == GM_Nature_State.VeryHighNatureLevel)
            {
                Create_Disease(magnitude);
            }
            //if danger timer is started, time is at 0 or less, and in very low nature level, go to EXTREME SEED MODE!
            if (isEventTimerStarted && eventTimer <= 0 && GameManager.instance.GetNatureState() == GM_Nature_State.VeryLowNatureLevel)
            {
                Create_Seed(magnitude);
            }

            eventTimer -= Time.deltaTime;
            stateTimer -= Time.deltaTime;
            wait = true;
        }
    }

}