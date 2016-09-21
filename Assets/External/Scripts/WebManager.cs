using UnityEngine;
using System.Collections;

public class WebManager : MonoBehaviour {

	public GameObject panel;
	Color[] myNormalMapColor;

	void Start () {
		string url = "https://lh3.googleusercontent.com/pcMrs1Q4o7dTNzyYd7fx9gb8R_GEM_S_bjrthUVSPHQg-SE1i7JT74BpHiqqEbKW05gx0WgYYebodVZ-MM-POYHi726dgHzjd6fpXZtGDgmGdDWXKzxyCDs6xjJDXrNhiRwhkgg5eW_ndowPishSoLLghHd5vKo0JZhBeVhiXzAZx1St7Mzdem8kEzoUqtCEP_wYuVT-BJiS89288_NR_MUDtCL8nyNtrnvXFJqQzm1B9zfIarxuowuMxDAQF7maqItDFCqDCC9V9Z0hnyfdTNzQnOwJ7xlNyzAF-Y5mwYVwj1aTu92vJRRT9LwGtatr6RGU5Ml7G6Dyqz81_b20atWSFOc1Vw3kYzIELxPoTidq6zDi4IBt2YpqVnWEq0By41EUvPNWhgYAqS_V8LM2dMATtjk_6Pawl6NWuSpbS9_JlAK6i-_nWj7V57Tmpp8HI186PcrAbJ0cxPBNqqvXlUtgCtNkrnf0ts0vzjRN9Wwd0FXNJu4kNL045jg_zex8ZI3jky7UM8tur6JSr9IvBnGIb4j-rnwgVTXZuogxmIIP9SiX7j0aeyjfBuoipfhM3FSXRpQAj_WXx6S_g_2bvq1cuTxheMxwDWhH3zgIbCOoaY_Mng=w1206-h1398-no";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www, "_MainTex"));

		url = "https://lh3.googleusercontent.com/cIS3JE-rXKRuEI7wKbKorvleUtQVHIUgN-o7ivLTIs5FWet3epM3grko1mWeYeMalE3z6hiYuKLjZnSRTlzlax4Rz2xwJSSZXrUwer2mP_VeMn3JRjos1ya7cK9ElspPbeiP4MrbW3nU3fTGCb3ampYLDJsL1e5XdnqK1v1GL6KYBuTB1duhixokCtkke_I0Pb_SXYMOqL0WtSQXKv1fWexeGZbJvy0WqJigyKST5uGY9H0UQf32bwbF9rLbx2N7ixkMMe70e36LjpCOG6-zMfp0D8knbXEWgyF6cJlafRr1M3CQEOGawk6A3PrNKrW-sx9inl_7aybwdruMagXbwyrjrLpRguhmeE0OlshAgax_NOskNhOkt33EvJiR33hNU5rZ15NSNtHVR_Lah2GkFSo8IXg5EQkOC0JItYOs6fDoToy9K60J-cDMX1NUT2cNu87xfcKqQJ9elhUnOxYq0QUzcjuxSMDSDNqaMrs8kUz-xoM6hUYvVx6W_5ypsatw-f-_yK2PI8rr-gvBlnyj34SRU9jkaqJicJVjg2HSWGzQWN4fSlSVqUUMUKVwz_YhEFWMMSej9r0h8U-IJGu9oEr32ea2SyPuGSJGHC6HaNv0E4WSVA=w1206-h1398-no";

		WWW www2 = new WWW (url);
		StartCoroutine(WaitForRequest(www2, "_BumpMap"));
	}

	IEnumerator WaitForRequest(WWW www, string texName)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + texName + www.data);
			Renderer renderer = panel.GetComponent<Renderer>();
			if (texName != "_BumpMap") {
				renderer.material.SetTexture (texName, www.texture);
			} else {
				Texture2D normalMap = new Texture2D (www.texture.width, www.texture.height, TextureFormat.ARGB32, false);
				myNormalMapColor = www.texture.GetPixels ();
				for(int i=0; i<myNormalMapColor.Length; i++){					
					myNormalMapColor [i].a = myNormalMapColor [i].r;
					myNormalMapColor [i].r = 0;
					myNormalMapColor [i].b = 0;
				}
				normalMap.SetPixels (myNormalMapColor);
				normalMap.Apply ();
				renderer.material.SetTexture (texName, normalMap);
			}

			float scaleNumber = (float)www.texture.height / www.texture.width;
			panel.transform.localScale = new Vector3(panel.transform.localScale.x,
				panel.transform.localScale.y,
				panel.transform.localScale.x * scaleNumber);
				
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
