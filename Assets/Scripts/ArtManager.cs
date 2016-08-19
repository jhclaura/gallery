using UnityEngine;
using System.Collections;
using System;

public class ArtManager : MonoBehaviour {

	public GvrViewer gvrViewer;
	public Camera mainCamera;
	public GameObject gallery;
	public GameObject art;
	public GameObject artPanel;
	public GameObject fakeShadow;
	public float shiftArtTime = 15f;
	public Texture[] artMainTextures;
	public Texture[] artNRMTextures;

	public Color fadeColor = Color.black;
	public float fadeTime = 5f;
	public float fadeDelayTime = 1f;

	public Light pointLight;
	PointLightMovement pLightMovScript;

	Material artMaterial;
	int textureIndex;
	int textureCount;
	bool toPause = false;
	AdjustSizeFromTexture adjustScaleScript;

	IEnumerator coroutine;

	bool toZoomIn = true;

	//Action toScale;
	Action change_M_T;

	public Material skyboxMat;

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
		ChangeSkyboxColor(artMainTextures[textureIndex%textureCount]);

		coroutine = ShiftArtTexture (shiftArtTime);
		StartCoroutine (coroutine);

		//toScale += ChangeArtScale;
		change_M_T += ChangeMaterialAndTexture;

		pLightMovScript = pointLight.GetComponent<PointLightMovement> ();
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

		// change skybox color
		ChangeSkyboxColor(artMainTextures [textureIndex % textureCount]);
	}

	void ChangeArtScale(){
		Vector3 newScale, newScalePanel, newScaleShadow;

		if (toZoomIn) {
			newScale = art.transform.localScale * 3.0F;
			newScalePanel = artPanel.transform.localScale * 3.0F;
			newScaleShadow = fakeShadow.transform.localScale * 3.0f;
			pLightMovScript.distance *= 2f;
			pLightMovScript.zPos -= 1f;
		} else {
			newScale = art.transform.localScale / 3.0F;
			newScalePanel = artPanel.transform.localScale / 3.0F;
			newScaleShadow = fakeShadow.transform.localScale / 3.0f;
			pLightMovScript.distance /= 2f;
			pLightMovScript.zPos += 1f;
		}
		art.transform.localScale = newScale;
		artPanel.transform.localScale = newScalePanel;
		fakeShadow.transform.localScale = newScaleShadow;
		toZoomIn = !toZoomIn;
	}

//	Texture2D t;
//	Texture2D texCopy;
	void ChangeSkyboxColor( Texture _texture ) {
		// sample colors from texture
		Texture2D t = _texture as Texture2D;

//		Texture2D texCopy = new Texture2D(t.width, t.height, t.format, t.mipmapCount > 1);
//		texCopy.LoadRawTextureData(t.GetRawTextureData());
//		texCopy.Apply ();

		Color32[] pix = t.GetPixels32();	// texCopy
//		Debug.Log (pix.Length);
		int firstPixToSample = Mathf.FloorToInt(pix.Length / 4);

		Color32 topColor = AverageColor (pix,3,1);
		Color32 midColor = AverageColor (pix,3,2);
		Color32 bottomColor = AverageColor (pix,3,3);

		skyboxMat.SetColor ("_Color1", topColor);
		skyboxMat.SetColor ("_Color2", midColor);
		skyboxMat.SetColor ("_Color3", bottomColor);
	}

	public Color32 AverageColor( Color32[] _pix, int division, int divisionIndex ) {
		int total = _pix.Length;
		int pixScopeToSample = Mathf.FloorToInt(total/division);
//		Debug.Log ("pixScopeToSample: " + pixScopeToSample);
		float r = 0;
		float g = 0;
		float b = 0;

		int startingIndex = (divisionIndex - 1) * pixScopeToSample;
		for(int i=startingIndex ; i<(startingIndex+pixScopeToSample/100); i++)
		{
			r += _pix[i].r;
			g += _pix[i].g;
			b += _pix[i].b;
		}

		return new Color32((byte)(r / (pixScopeToSample/100)) , (byte)(g / (pixScopeToSample/100)) , (byte)(b / (pixScopeToSample/100)) , 0);
	}
}
