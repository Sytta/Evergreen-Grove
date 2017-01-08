using UnityEngine;
using System.Collections;

public class CongratsScript : MonoBehaviour {

	public void StartCamera()
    {
        Camera.main.GetComponent<CameraUI>().StartEndMovement();
        FindObjectOfType<InGameUI>().GetComponent<Animation>().Play("UIEndAnimMoveOut");
    }
}
