using UnityEngine;
using System.Collections;

public class WebManager : MonoBehaviour {

	public GameObject panel;
	Color[] myNormalMapColor;

	void Start () {
		string url = "https://lh3.googleusercontent.com/Kldo4P2YHsJDiEZ5w6StZA-flQwD61p_S_o64JkLE8ajqe-tJlfr_jOwZ1DD5QpmHUrHBNUfmGfs7v92d4le7HuPZoFmdz-Wn8y3NDwZQoJBpK4SIH7xqixpCsBc6tg7_c5HKt6nY5GqxKX7vFmo31BwSdZlQ80eIw9IfdzxDAbWz9NOiLQkQBICKRlF9LbLnsl2LIBHk7JOZJu9H566G8jvya40dtKb6JAFUhpbBFdVlmjIphFAX4pZL9aBUSJNP7SWy6mCVAmoTOpzYaRqpK6M46QntNOQu48G3QxEYDOeT_6o1Jf8B8C9WyzMCnY0mkBHpTYur0mg67PKnAOjyK_xLdkDUyazjMQoS3z32cV-1zf4ir7L1ZVyzePVIZsA0PC2VM9ykUT3sm1A2ybpUpT7m5tLaPfPi3DoUMF5AF-wlMn0iShr5MOr9Jt06Tla8fwgGTzgs4Ni0wykpiRXIPvV8U-E3PRGRfFXqcFnib_3_bQmu4ThDQ2GfXT28ROlDvpWJECzTqTcSkET6h_kbrfL6JDeLIQYf6-QGW-AYZNQ5WddT3N0UR_lPdeHeZ2KCYihdPQ1yk8ZX5hvUhFHlf6jOox3TcetWNX2IOa1Cj9xrw_QHQ=w992-h1486-no";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www, "_MainTex"));

		url = "https://lh3.googleusercontent.com/HXECsLoq-mvlUdrJh4aMmAIUz6l_UOlhXM8Tt4pArQCWjrYFmONGVBeeUbbQ6wPMXqmaXSaYZ0QbSG2_Z5VMfKsgphQd6BfEeFe-bolUPaoIEQjV__Xxw-EmHSRYZPbPjdJObDwvmeZq_sw7rbWHCP0x28ohR4WP0E4_yJC52eeIHIczLJ-yhPjJehpOejroeFp2sU35H0k1nTjGH-UBzUWmlwgydh48EhssltGb7dVVztM5o7EfCFzKkMnb69r6_1rPb4lHt0CA-gtzWmly7SaW3gW1ybQr0p6VqXsfezROJrp8ELzSJevhkh_fD2SjCdm0lHTD3j8y1mamc6RE7MH2R9uvHXm5i-nvevuraKWMSPflfe2Pg-KknJHIZk36XpePseFSTinq2Tsp13_YKPxhMll09ei_8PQdcS2gQTTtZigu5d4KVtsXj6H2eTkM_19we8vECv5hgBtr-c8w0mXrhbAlSkGh_aLcbV1z_sqNqSUZ_SIT6GTJlheCCyaLy3uC3drDBzk6vySAhff1E-LkQaY09EA4pqVMwKxqDHTmlnkJreVxYktO46gsQh4g4aV7T2RR0YtVMPUsh5InQynZJUxHbodoo7AcKy3VpdPFhk4hdw=w992-h1486-no";
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
