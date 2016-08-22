using UnityEngine;
using System.Collections;

public class DirectionalLightMovement : MonoBehaviour {

	float scale;
	float xRot,yRot,zRot;
	float time;
	Transform transform;
	public float speed;
	public float angle;
	public float shadowDistance;

	public GameObject fakeShadow;
	float xPos_shadow, yPos_shadow, zPos_shadow;

	void Start () {
		transform = gameObject.transform;
		xRot = transform.localEulerAngles.x;
		zRot = transform.localEulerAngles.z;

		zPos_shadow = fakeShadow.transform.localPosition.z;
		yPos_shadow = fakeShadow.transform.localPosition.y;
	}

	void Update () {
		time = Time.time * speed;
		yRot = angle * Mathf.Sin(time);
		transform.localEulerAngles = new Vector3 (xRot, yRot, zRot);

		// when Mathf.Sin(time) ==  1, fs xPos =  0.5
		// when Mathf.Sin(time) == -1, fs xPos = -0.5
		// when Mathf.Sin(time) == 0, fs yPos = 1
		// when Mathf.Sin(time) == 1 & -1, fs yPos = 0.7

		xPos_shadow = shadowDistance * Mathf.Sin(time);
//		yPos_shadow = 1f - (shadowDistance * Mathf.Abs(Mathf.Sin(time)));
		fakeShadow.transform.localPosition = new Vector3 (xPos_shadow, yPos_shadow, zPos_shadow);
	}
}
