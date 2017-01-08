using UnityEngine;
using System.Collections;

public class TimerSun : MonoBehaviour {
    public void StopTimer()
    {
        GameManager.instance.EndGame(true);
    }
}
