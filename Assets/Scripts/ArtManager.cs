using UnityEngine;
using System.Collections;
using System;

public class ArtManager : MonoBehaviour {

	public GvrViewer gvrViewer;
	public Camera mainCamera;
	public GameObject gallery;
	public GameObject art;
	public float shiftArtTime = 15f;
	public Texture[] artMainTextures;
	public Texture[] artNRMTextures;

	public Color fadeColor = Color.black;
	public float fadeTime = 5f;
	public float fadeDelayTime = 1f;

	Material artMaterial;
	int textureIndex;
	int textureCount;
	bool toPause = false;
	AdjustSizeFromTexture adjustScaleScript;

	IEnumerator coroutine;

	bool toZoomIn = true;

	//Action toScale;
	Action change_M_T;

	// Use this for initialization
	void Start () {
		textureIndex = 0;
		textureCount = artMainTextures.Length;

		artMaterial = art.GetComponent<Renderer> ().material;
		adjustScaleScript = art.GetComponent<AdjustSizeFromTexture> ();

		ChangeMatText (artMaterial,
			artMainTextures[textureIndex%textureCount],
			artNRMTextures[textureIndex%textureCount]);
		//
		coroutine = ShiftArtTexture (shiftArtTime);
		StartCoroutine (coroutine);

		//toScale += ChangeArtScale;
		change_M_T += ChangeMaterialAndTexture;
	}

	public IEnumerator ShiftArtTexture(float waitTime) {
		do {
			yield return new WaitForSeconds (waitTime);
			//
//			ChangeMatText (artMaterial,
//				artMainTextures [textureIndex % textureCount],
//				artNRMTextures [textureIndex % textureCount]);
			//
			CameraFade.StartAlphaFadeInOut(fadeColor, fadeTime, fadeDelayTime, change_M_T);
			//
			textureIndex++;
			print ("ShiftArtTexture at " + Time.time);

			// time to change the scale!
			//adjustScaleScript.AdjustSize();	// move to be inside of ChangeMaterialAndTexture

		} while (!toPause);
	}

	void Update () {
		if(Input.GetKeyDown("space")){
//			StopCoroutine (coroutine);
			Debug.Log ("pause at " + Time.time);
			toPause = !toPause;
		}

		if(gvrViewer.Triggered){
			Debug.Log ("triggered! Zoom in/out art");
			ChangeArtScale ();
			//
//			CameraFade.StartAlphaFadeInOut(fadeColor, fadeTime, fadeDelayTime, toScale);
		}
	}

	void ChangeMatText(Material _mat, Texture _mainTex, Texture _bumpMap){
		_mat.SetTexture ("_MainTex", _mainTex);
		_mat.SetTexture ("_BumpMap", _bumpMap);
	}

	void ChangeMaterialAndTexture(){
		artMaterial.SetTexture ("_MainTex", artMainTextures [textureIndex % textureCount]);
		artMaterial.SetTexture ("_BumpMap", artNRMTextures [textureIndex % textureCount]);
		adjustScaleScript.AdjustSize();

		Vector3 rotFollowGaze = new Vector3 (0f,mainCamera.transform.eulerAngles.y,0f);
		gallery.transform.eulerAngles = rotFollowGaze;
	}

	void ChangeArtScale(){
		Vector3 newScale;

		if (toZoomIn)
			newScale = art.transform.localScale * 3.0F;
		else
			newScale = art.transform.localScale / 3.0F;

		art.transform.localScale = newScale;

		toZoomIn = !toZoomIn;
	}
}
