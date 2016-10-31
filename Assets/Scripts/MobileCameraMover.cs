using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MobileCameraMover : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		SetGazedAt (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer> ().material.color = gazedAt ? Color.green : Color.red;
	}

	public void OnGazeEnter() {
		SetGazedAt (true);
	}

	public void OnGazeExit() {
		SetGazedAt (false);
	}

	public void OnGazeTrigger(){
		Debug.Log ("Be looked and clicked! Camera move to here.");
		Debug.Log (Camera.main);
		Vector3 newLocation = new Vector3 (transform.position.x, Camera.main.transform.position.y, transform.position.z);
		LeanTween.move (Camera.main.gameObject, newLocation, 2f);

		CamTeleportManager c_t = Camera.main.gameObject.GetComponent<CamTeleportManager> ();
		MenuManger m_m = c_t.menuManager;
		m_m.flyerManager.ToggleMode (false);
	}
}
