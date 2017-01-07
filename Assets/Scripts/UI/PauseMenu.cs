using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	void OnEnable()
    {
        GameManager.instance.PauseGame();
    }
	// Update is called once per frame
	void Update () {
	    if(Input.anyKey)
        {
            GameManager.instance.UnPauseGame();
            gameObject.SetActive(false);
        }
	}
}
