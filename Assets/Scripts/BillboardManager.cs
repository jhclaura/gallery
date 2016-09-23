using UnityEngine;
using System.Collections;

public class BillboardManager : MonoBehaviour
{
	public Camera m_Camera;
	public bool autoInit =false;
	public bool followUpDown =false;
	bool isActive =false;
	GameObject myContainer;	

	void Awake(){
		if (autoInit == true) {
			m_Camera = Camera.main;
			isActive = true;
		}

		if (m_Camera != null) {
			isActive = true;
		}

		myContainer = new GameObject();
		myContainer.name = "BillboardGroup_"+transform.gameObject.name;
		myContainer.transform.position = transform.position;
		transform.parent = myContainer.transform;
	}


	void Update(){
		if(isActive==true){
			if (!followUpDown) {
				Vector3 targetPostition = new Vector3 (m_Camera.transform.position.x, 
					                          this.transform.position.y, 
					                          m_Camera.transform.position.z);
				myContainer.transform.LookAt (targetPostition, Vector3.up);
			} else {
				myContainer.transform.LookAt (m_Camera.transform, Vector3.up);
			}
		}
	}
}