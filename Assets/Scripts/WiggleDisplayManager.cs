using UnityEngine;
using System.Collections;

public class WiggleDisplayManager : MonoBehaviour {

	public Texture[] textures;
	public float waitTime;
	IEnumerator coroutine;
	Material picMaterial;
	int textureIndex;
	int textureCount;
	bool toPause = false;


	void Start () {
		textureIndex = 0;
		textureCount = textures.Length;
		picMaterial = GetComponent<Renderer> ().material;

		coroutine = ShiftTexture ();
		StartCoroutine (coroutine);
	}
	
	public IEnumerator ShiftTexture () {
		do {
			yield return new WaitForSeconds (waitTime);
			//
			picMaterial.SetTexture ("_MainTex", textures [textureIndex % textureCount]);
			textureIndex++;
//			print ("ShiftArtTexture at " + Time.time);

		} while (!toPause);
	}
}
