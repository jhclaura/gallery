using UnityEngine;
using System.Collections;

public class AudioGuideManager : MonoBehaviour {

	float scale;
	float xPos,yPos,zPos;
	float time;
	Transform transform;

	public float speed;
	public float moveRadius;
	public float shadowDistance;

	void Start () {
		transform = gameObject.transform;
		xPos = transform.localPosition.x;
		yPos = transform.localPosition.y;
		zPos = transform.localPosition.z;
	}

	void Update () {
		time = Time.time * speed;
		xPos = moveRadius * Mathf.Sin(time);
		yPos = moveRadius * Mathf.Sin(time);
		zPos = moveRadius * Mathf.Cos(time/2);
		transform.localPosition = new Vector3 (xPos, yPos, zPos);
	}
}
