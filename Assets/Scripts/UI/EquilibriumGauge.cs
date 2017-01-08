using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EquilibriumGauge : MonoBehaviour {
    public Image TopBar;
    public Image BottomBar;
    public Image EquilibriumGlow1;
    public Image EquilibriumGlow2;
    public Animation anim;
    private GM_Nature_State lastState;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();

    }
	
	// Update is called once per frame
	void Update () {
        if(lastState== GM_Nature_State.Equilibrium && GameManager.instance.GetNatureState()!= GM_Nature_State.Equilibrium)
        {
            anim.Stop();
            EquilibriumGlow1.GetComponent<FadeUI>().alphaGoal = 0;
            EquilibriumGlow1.GetComponent<FadeUI>().StartFade();
            EquilibriumGlow2.GetComponent<FadeUI>().alphaGoal = 0;
            EquilibriumGlow2.GetComponent<FadeUI>().StartFade();
        }
        else if(lastState != GM_Nature_State.Equilibrium && GameManager.instance.GetNatureState() == GM_Nature_State.Equilibrium)
        {
            anim.Play();
            EquilibriumGlow2.GetComponent<FadeUI>().alphaGoal = 1;
            EquilibriumGlow2.GetComponent<FadeUI>().StartFade();
        }
        lastState = GameManager.instance.GetNatureState();

        switch (GameManager.instance.GetNatureState())
        {
            case GM_Nature_State.Equilibrium:
                BottomBar.fillAmount = 0;
                TopBar.fillAmount = 0;
                break;
            case GM_Nature_State.HighNatureLevel:
            case GM_Nature_State.VeryHighNatureLevel:
                BottomBar.fillAmount = 0;
                TopBar.fillAmount = Mathf.Lerp(TopBar.fillAmount,2 * (GameManager.instance.GetNatureLevel() - 0.5f),Time.deltaTime);
                break;
            case GM_Nature_State.LowNatureLevel:
            case GM_Nature_State.VeryLowNatureLevel:
                TopBar.fillAmount = 0;
                BottomBar.fillAmount = Mathf.Lerp(BottomBar.fillAmount, 2 * (0.5f-GameManager.instance.GetNatureLevel()), Time.deltaTime);
                break;
        }
	}
   
}
