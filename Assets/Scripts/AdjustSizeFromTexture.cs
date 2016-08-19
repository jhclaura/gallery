using UnityEngine;
using System.Collections;
using System.IO;

public class AdjustSizeFromTexture : MonoBehaviour {

	Material mat;
	Texture tex;
	public GameObject artPanel;
	public GameObject fakeShadow;

	void Start () {
		mat = GetComponent<Renderer> ().material;

//		Debug.Log ( Pathing.AppContentDataUri );

		AdjustSize ();

//		string myStreamingAssetsPath = Pathing.AppContentDataUri+"";
	}

	public void AdjustSize() {
		tex = mat.GetTexture ("_MainTex");
//		Debug.Log ("art: " + tex.name);
		string relativePath = tex.name + ".jpg";
		string fullPath = Path.Combine( Application.streamingAssetsPath, relativePath );

		// Vector2Int imgSize  = ImageHeader.GetDimensions(Application.persistentDataPath+"/Images/Amedeo_Modigliani_Christina.jpg");
//		Vector2Int imgSize  = ImageHeader.GetDimensions(Application.dataPath+"/Images/" + tex.name + ".jpg");
		Vector2Int imgSize  = ImageHeader.GetDimensions(fullPath);
		Debug.Log("art: " + tex.name + ", size.x =" + imgSize.x + ", size.y =" + imgSize.y);

		int imgX = imgSize.x;
		int imgY = imgSize.y;
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
}
