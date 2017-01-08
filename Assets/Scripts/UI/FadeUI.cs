using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeUI : MonoBehaviour {

    public float timeToFade=1;
    public bool fadeOnStart=false;
    public float alphaGoal;
    public Image toFade;
    public bool isFading;
    private bool restartFade=false;
    public void Start()
    {
        if(!toFade)
            toFade = GetComponent<Image>();

    }
    

    public void StartFade()
    {
        if(!isFading)
            StartCoroutine("Fade");
        else
        {
            restartFade = true;
        }
    }
    IEnumerator Fade()
    {
        isFading = true;
        for (int i = 0; i < 50; i++)
        {
            if(restartFade)
            {
                i = -1;
                restartFade = false;
                continue;
            }
            Color newColor = toFade.color;
            newColor.a=Mathf.Lerp(newColor.a,alphaGoal,i/50.0f);
            toFade.color = newColor;
            yield return new WaitForSeconds(timeToFade / 50.0f);
        }
        isFading = false;
    }


}
