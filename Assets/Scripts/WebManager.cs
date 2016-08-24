using UnityEngine;
using System.Collections;

public class WebManager : MonoBehaviour {

	public GameObject panel;

	void Start () {
		string url = "https://lh3.googleusercontent.com/Fto3M8gXlz1rQEn4lhmd0SrK4rS8tvBwmTnqCWxpdiwTRmApRwd92t4HAJ2zeDh7gXVn-k-f65GXfhLMFgmXcObefRAr06XcraoCbo8XLBmGr34WWdiq0LkVl_T3Tvi18l-L2VlIDXyh0BCpXbztoNzbt1cpO3qgKjyygOHpRF-jTOcgjSFFxYV-jxJoe_5kl5pCvH1AGg9Cxba5Cvt8EapsQcrjNLFVPQ3E9KX1exJvEDD3CeKdk51vDv-aHQYbwqIPz-Jn2cO6v28fcC2yIi-EWnHLeg3Atk-3xaQ4YWuZ7Uk0AYnx5qm07Drn3KRTp57Dl7zlsOeDOKBa5Y4H-TKroRhNwPo2gyFS4f5n8YrRJGpmDqRVbsDgKfbj7Y7EV4d1ecGwaUbHDE78srUOMhqwBCBr2N9QYWFIe0NfGcmE516_liMF8Cu7xrpAgbYfn9qGsmet5qOlnSmKA-va6x_5QdT8_ROkHrrwye9DfIrh3UO4nqqdJvG9Q1QxxOq1g7zGaIO64-qTyvCWk_IFvHq1SsO7B-98jsYDAcWH4TVUN3yLD4LTXwI=s1334-w1334-h750-no";
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
			renderer.material.SetTexture(texName, www.texture);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
