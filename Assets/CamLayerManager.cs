using UnityEngine;
using System.Collections;

public class CamLayerManager : MonoBehaviour {
	Transform leftEye;
	Transform rightEye;
	GvrEye leftEyeScript;
	GvrEye rightEyeScript;
	Camera leftEyeCam;
	Camera rightEyeCam;

	bool foundLeftEye = false;
	bool foundRightEye = false;

	public LayerMask rightEyeMask;
	public LayerMask leftEyeMask;

	void Start () {
		leftEye = transform.FindChild ("Main Camera Left");
		rightEye = transform.FindChild ("Main Camera Rigth");
	}

	void Update() {
		// LEFT
		if (!foundLeftEye) {
			leftEye = transform.FindChild ("Main Camera Left");

			if (leftEye == null) {
				Debug.LogWarning ("could not find leftEye");
			} else {
				Debug.Log("found leftEye!");
				//
				leftEyeScript = leftEye.gameObject.GetComponent<GvrEye>();
				leftEyeScript.toggleCullingMask = leftEyeMask;

				leftEyeCam = leftEye.gameObject.GetComponent<Camera>();
				leftEyeCam.cullingMask = ~leftEyeMask;
				//
				foundLeftEye = true;
			}
		}

		// RIGHT
		if (!foundRightEye) {
			rightEye = transform.FindChild ("Main Camera Right");

			if (rightEye == null) {
				Debug.LogWarning ("could not find rightEye");
			} else {
				Debug.Log("found rightEye!");
				//
				rightEyeScript = rightEye.gameObject.GetComponent<GvrEye>();
				rightEyeScript.toggleCullingMask = rightEyeMask;

				rightEyeCam = rightEye.gameObject.GetComponent<Camera>();
				rightEyeCam.cullingMask = ~rightEyeMask;
				//
				foundRightEye = true;
			}
		}
	}

}
