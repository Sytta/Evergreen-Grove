using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameInstance : MonoBehaviour {
    public static GameInstance instance = null;
    public bool isTutorialMode=false;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        // Use this for initialization
        void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void ToMainGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void ToEndScreen()
    {

    }
}
