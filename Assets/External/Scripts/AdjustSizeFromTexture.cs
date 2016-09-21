using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class AdjustSizeFromTexture : MonoBehaviour {

	Material mat;
	Texture tex;
	public GameObject artPanel;
	public GameObject fakeShadow;
	public GameObject gallery;
	DataManager dataManager;

	void Start () {
		mat = GetComponent<Renderer> ().material;

//		AdjustSize ();
		#if UNITY_ANDROID
			dataManager = gallery.GetComponent<DataManager>();
			GetSizeFromData();
		#else
			GetSize ();
		#endif
	}

	public void AdjustSize(int imgX, int imgY) {

		float scaleNumber = (float)imgY / imgX;
		//Debug.Log("scaleNumber: " + scaleNumber);

		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,
			gameObject.transform.localScale.y,
			gameObject.transform.localScale.x * scaleNumber);

		artPanel.transform.localScale = new Vector3 (artPanel.transform.localScale.x,
			artPanel.transform.localScale.x * scaleNumber,
			artPanel.transform.localScale.z);

		fakeShadow.transform.localScale = new Vector3 (fakeShadow.transform.localScale.x,
			gameObject.transform.localScale.y,
			fakeShadow.transform.localScale.x * scaleNumber);
	}

	public void GetSize() {
		tex = mat.GetTexture ("_MainTex");
		string relativePath = tex.name + ".jpg";
		string fullPath = Path.Combine( Application.streamingAssetsPath, relativePath );
		Vector2Int imgSize  = ImageHeader.GetDimensions(fullPath);
		// Debug.Log("art: " + tex.name + ", size.x =" + imgSize.x + ", size.y =" + imgSize.y);

		AdjustSize (imgSize.x, imgSize.y);
	}

	public void GetSizeFromData() {
		tex = mat.GetTexture ("_MainTex");
		string texName = tex.name;
		int imgX = dataManager.artDictionary [texName].width;
		int imgY = dataManager.artDictionary [texName].height;

		AdjustSize (imgX, imgY);
	}
}
