using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MobileCameraMover : MonoBehaviour {

	CamTeleportManager c_t;
	MenuManger m_m;

	public Color noGazedColor = Color.red;
	public Color gazedAtColor = Color.green;
	
	// Use this for initialization
	void Start () {
		SetGazedAt (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetGazedAt(bool gazedAt) {
		GetComponent<Renderer> ().material.color = gazedAt ? gazedAtColor : noGazedColor;
	}

	public void OnGazeEnter() {
		SetGazedAt (true);
	}

	public void OnGazeExit() {
		SetGazedAt (false);
	}

	public void OnGazeTrigger(){
//		Debug.Log ("Be looked and clicked! Camera move to here.");
		Vector3 newLocation = new Vector3 (transform.position.x, Camera.main.transform.position.y, transform.position.z);
		LeanTween.move (Camera.main.gameObject, newLocation, 2f);

		if(c_t==null)
			c_t = Camera.main.gameObject.GetComponent<CamTeleportManager> ();
		if(m_m==null)
			m_m = c_t.menuManager;

		if (m_m.flyerManager.inInfoMode) {
			m_m.flyerManager.ToggleMode (false);
		}
	}

	public void OnGazeTriggerHopspot(){
		//		Debug.Log ("Be looked and clicked! Camera move to here.");
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
			Camera.main.transform.position.x - 1f,
			Camera.main.transform.position.y - c_t.camHeight,
			Camera.main.transform.position.z
		);
		m_m.UpdatePosition (newPos);
	}
		
}
