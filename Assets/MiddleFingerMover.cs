using UnityEngine;
using System.Collections;

public class MiddleFingerMover : MonoBehaviour {

	Camera mainCamera;
	ToolManager toolManager;
	GameObject tool_finger;
	GameObject middleFinger;

	Vector3 middleFingerOriPosition;
	Vector3 middleFingerOriRotation;

	bool setupDone = false;
	bool fingerOut = false;

	// Use this for initialization
	void Start () {
		// find things
		if(!setupDone){
			mainCamera = Camera.main;
			toolManager = mainCamera.gameObject.GetComponent<ToolManager> ();
			tool_finger = toolManager.tools [0].gameObject;
			middleFinger = tool_finger.transform.GetChild (0).gameObject;

			setupDone = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnGazeTrigger(){
		if (fingerOut)
			return;
		
		middleFinger.transform.parent = null;
		middleFingerOriPosition = middleFinger.transform.position;
//		middleFingerOriRotation = middleFinger.transform.eulerAngles;

		LeanTween.move (middleFinger, transform.position, 1f);
		fingerOut = true;
//		Debug.Log ("move finger out!");

		Invoke ("MoveBackFinger", 2.5f);
	}

	void MoveBackFinger(){
		LeanTween.move (middleFinger, tool_finger.transform.position, 1f).setOnComplete( ResetFinger );
//		Debug.Log ("move finger back!");
	}

	void ResetFinger(){
		middleFinger.transform.parent = tool_finger.transform;
		middleFinger.transform.localPosition = new Vector3();
		middleFinger.transform.localEulerAngles = new Vector3();

		fingerOut = false;
//		Debug.Log ("re-attach finger!");
	}
}
