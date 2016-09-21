using UnityEngine;
using System.Collections;
using System;

public class GalleryManager_Photo : MonoBehaviour {

	public GameObject painting;
	public GameObject artPanel;

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
	Vector3 closePos = new Vector3 (0f,0f,5f);
	Vector3 backPos = new Vector3 (0f,0f,0f);

	//Action toScale;
	Action change_M_T;

    
	void Start () {
		textureIndex = 0;
		textureCount = artMainTextures.Length;

		artMaterial = painting.GetComponent<Renderer> ().material;
		adjustScaleScript = painting.GetComponent<AdjustSizeFromTexture> ();

		ChangeMatText (artMaterial,
			artMainTextures[textureIndex%textureCount],
			artNRMTextures[textureIndex%textureCount]);
		//
		//ChangeSkyboxColor(artMainTextures[textureIndex%textureCount]);

		coroutine = ShiftArtTexture (shiftArtTime);
		StartCoroutine (coroutine);

		//toScale += ChangeArtScale;
		change_M_T += ChangeMaterialAndTexture;

		//pLightMovScript = pointLight.GetComponent<PointLightMovement> ();
		//dLightMovScript = directionalLight.GetComponent<DirectionalLightMovement> ();
	}

	public IEnumerator ShiftArtTexture(float waitTime) {
		do {
			yield return new WaitForSeconds (waitTime);

            ChangeMaterialAndTexture();
            textureIndex++;

		} while (!toPause);
	}

	float exp1, exp2, time;
	void Update () {
		if(Input.GetKeyDown("space")){
//			StopCoroutine (coroutine);
			Debug.Log ("pause at " + Time.time);
			toPause = !toPause;
		}
	}

	void ChangeMatText(Material _mat, Texture _mainTex, Texture _bumpMap){
		_mat.SetTexture ("_MainTex", _mainTex);
		_mat.SetTexture ("_BumpMap", _bumpMap);
	}

	void ChangeMaterialAndTexture(){
		artMaterial.SetTexture ("_MainTex", artMainTextures [textureIndex % textureCount]);
		artMaterial.SetTexture ("_BumpMap", artNRMTextures [textureIndex % textureCount]);
//		artMaterial.SetFloat("_BumpScale", 6f);

//		adjustScaleScript.AdjustSize();
		#if UNITY_ANDROID
			adjustScaleScript.GetSizeFromData();
		#else
			adjustScaleScript.GetSize ();
		#endif

		// update Gallery rotation
//		Vector3 rotFollowGaze = new Vector3 (0f,mainCamera.transform.eulerAngles.y,0f);
//		gallery.transform.eulerAngles = rotFollowGaze;

		// change skybox color
		// ChangeSkyboxColor(artMainTextures [textureIndex % textureCount]);
	}

	void ChangeArtScale(){
		Vector3 newScale, newScalePanel, newScaleShadow;

		if (toZoomIn) {
			newScale = painting.transform.localScale * 3.0F;
			newScalePanel = artPanel.transform.localScale * 3.0F;
			
		} else {
			newScale = painting.transform.localScale / 3.0F;
			newScalePanel = artPanel.transform.localScale / 3.0F;
			
		}
		painting.transform.localScale = newScale;
		artPanel.transform.localScale = newScalePanel;
		
		toZoomIn = !toZoomIn;
	}

}
