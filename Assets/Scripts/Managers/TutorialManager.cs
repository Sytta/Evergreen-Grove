using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    bool tutorial_on;

    public void On_Toggle()
    {
        GameManager.instance.isTutorialMode = true;
    }
}
    