using UnityEngine;
using System.Collections;

public class PointLightMovement : MonoBehaviour {

	float scale;
	float xPos, yPos;
	public float zPos;
	float time;
	Transform transform;
	public float speed;
	public float distance;

	void Start () {
		transform = gameObject.transform;
		zPos = transform.localPosition.z;
	}
	
	void Update () {
		time = Time.time * speed;
		scale = 2f / (3f - Mathf.Cos(2f*time)) * distance;

		xPos = scale * Mathf.Cos(time);
		yPos = scale * Mathf.Sin(2f*time) / 2f;
		transform.localPosition = new Vector3 (xPos, yPos, zPos);
	}
}
