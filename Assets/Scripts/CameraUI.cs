﻿using UnityEngine;
using System.Collections;

public class CameraUI : MonoBehaviour {

	public void StartEndMovement()
    {
        GetComponent<Animation>().Play("CameraEndAnim");
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }
}
