using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MobileCameraMover : MonoBehaviour {

	CamTeleportManager c_t;
	MenuManger m_m;
	
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
//		Debug.Log ("Be looked and clicked! Camera move to here.");
//		Debug.Log (Camera.main);
		Vector3 newLocation = new Vector3 (transform.position.x, Camera.main.transform.position.y, transform.position.z);
		LeanTween.move (Camera.main.gameObject, newLocation, 2f);

		if(c_t==null)
			c_t = Camera.main.gameObject.GetComponent<CamTeleportManager> ();
		if(m_m==null)
			m_m = c_t.menuManager;

//		Invoke ("UpdateMenuPosition", 2f);

		if (m_m.flyerManager.inInfoMode) {
			m_m.flyerManager.ToggleMode (false);
//			Invoke ("ToggleOnFlyer", 2f);
		}
	}

	public void OnGazeTriggerHopspot(){
		//		Debug.Log ("Be looked and clicked! Camera move to here.");
		//		Debug.Log (Camera.main);
		Vector3 newLocation = new Vector3 (transform.position.x, Camera.main.transform.position.y, transform.position.z);
		LeanTween.move (Camera.main.gameObject, newLocation, 2f);

		if(c_t==null)
			c_t = Camera.main.gameObject.GetComponent<CamTeleportManager> ();
		if(m_m==null)
			m_m = c_t.menuManager;

		Invoke ("UpdateMenuPosition", 2f);

		if (m_m.flyerManager.inInfoMode) {
			m_m.flyerManager.ToggleMode (false);
			Invoke ("ToggleOnFlyer", 2f);
		}
	}

	void ToggleOnFlyer() {
		m_m.flyerManager.ToggleMode (true);
	}

	void UpdateMenuPosition() {
		Vector3 newPos = new Vector3 (
			Camera.main.transform.position.x,
			Camera.main.transform.position.y - 2.5f,
			Camera.main.transform.position.z);
		m_m.UpdatePosition (newPos);
	}
		
}
