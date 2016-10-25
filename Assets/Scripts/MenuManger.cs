using UnityEngine;
using System.Collections;

public class MenuManger : MonoBehaviour {

	public Material infoModeMaterial;
	Material pepperoniMaterial;
	Renderer pepperoni;

	public Color pointerHitColor = new Color(0f, 0.5f, 0f, 1f);
	public Color pointerMissColor = new Color(0.8f, 0f, 0f, 1f);
	Material[] materials;
	public bool inInfoMode = false;
	public FlyerManagerMobile flyerManager;

	void Start () {
		materials = GetComponent<Renderer> ().materials;
		pepperoniMaterial = materials [0];
		pepperoni = GetComponent<Renderer> ();
		materials [2].color = pointerMissColor;
	}

	void SetGazedAt(bool gazedAt) {
		materials[2].color = gazedAt ? pointerHitColor : pointerMissColor;
	}

	public void OnGazeEnter() {
		SetGazedAt (true);
	}

	public void OnGazeExit() {
		SetGazedAt (false);
	}

	public void OnGazeTrigger(){
		inInfoMode = !inInfoMode;
		Debug.Log ("Be looked and clicked! Toggle menu mode: " + inInfoMode);

		GetComponent<Renderer> ().material = inInfoMode ? infoModeMaterial : pepperoniMaterial;

		flyerManager.ToggleMode (inInfoMode);
	}
}
