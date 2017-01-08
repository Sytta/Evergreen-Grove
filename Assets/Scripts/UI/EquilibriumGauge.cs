using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EquilibriumGauge : MonoBehaviour {
    public Image TopBar;
    public Image BottomBar;
    public Image EquilibriumGlow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    switch(GameManager.instance.GetNatureState())
        {
            case GM_Nature_State.Equilibrium:
                BottomBar.fillAmount = 0;
                TopBar.fillAmount = 0;
                EquilibriumGlow.enabled = true;
                break;
            case GM_Nature_State.HighNatureLevel:
            case GM_Nature_State.VeryHighNatureLevel:
                BottomBar.fillAmount = 0;
                TopBar.fillAmount = Mathf.Lerp(TopBar.fillAmount,2 * (GameManager.instance.GetNatureLevel() - 0.5f),Time.deltaTime);
                EquilibriumGlow.enabled = false;
                break;
            case GM_Nature_State.LowNatureLevel:
            case GM_Nature_State.VeryLowNatureLevel:
                TopBar.fillAmount = 0;
                BottomBar.fillAmount = Mathf.Lerp(BottomBar.fillAmount, 2 * (0.5f-GameManager.instance.GetNatureLevel()), Time.deltaTime);
                EquilibriumGlow.enabled = false;
                break;
        }
	}
}
