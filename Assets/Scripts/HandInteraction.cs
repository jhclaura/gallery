using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour {

    [System.Serializable]
    public class LogoColorSet
    {
        public Color[] colors = new Color[2];
    }
    public LogoColorSet[] logoColorSets;
    Collider handCollider;

    Color[] oriColors;
    Material[] logoMaterial;
    int colorIndex = 0;
    int colorCount = 0;
    bool gotLogoMatYet = false;

    public GameObject[] logoObjects;

	#if UNITY_STANDALONE_WIN
    AudioSource electricalSound;
	#else
	GvrAudioSource electricalSound;
	#endif

    void Start()
    {
        handCollider = GetComponent<Collider>();

		#if UNITY_STANDALONE_WIN
        electricalSound = GetComponent<AudioSource>();
		#else
		electricalSound = GetComponent<GvrAudioSource>();
		#endif

        oriColors = new Color[2];
        colorCount = logoColorSets.Length;
    }

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Logo")
        {
            if (!gotLogoMatYet)
            {
                logoObjects = GameObject.FindGameObjectsWithTag("Logo");
                logoMaterial = new Material[logoObjects.Length];

                for (int i=0; i<logoObjects.Length; i++)
                {
                    logoMaterial[i] = logoObjects[i].GetComponent<Renderer>().material;
                }
                // logoMaterial = collision.gameObject.GetComponent<Renderer>().material;

                oriColors[0] = logoMaterial[0].GetColor("_Color1");
                oriColors[1] = logoMaterial[0].GetColor("_Color2");

                gotLogoMatYet = true;
            }

            if(gotLogoMatYet)
            {
                for (int i = 0; i < logoObjects.Length; i++)
                {
                    logoMaterial[i].SetColor("_Color1", logoColorSets[colorIndex % colorCount].colors[0]);
                    logoMaterial[i].SetColor("_Color2", logoColorSets[colorIndex % colorCount].colors[1]);
                }
                // logoMaterial.SetColor("_Color1", logoColorSets[colorIndex % colorCount].colors[0]);
                // logoMaterial.SetColor("_Color2", logoColorSets[colorIndex % colorCount].colors[1]);

                electricalSound.Play();

                colorIndex ++;
            }
            //Debug.Log("start touching logo!");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Logo")
        {
            if (gotLogoMatYet)
            {
                for (int i = 0; i < logoObjects.Length; i++)
                {
                    logoMaterial[i].SetColor("_Color1", oriColors[0]);
                    logoMaterial[i].SetColor("_Color2", oriColors[1]);
                }

                electricalSound.Stop();

                // logoMaterial.SetColor("_Color1", oriColors[0]);
                // logoMaterial.SetColor("_Color2", oriColors[1]);
            }
            //Debug.Log("stop touching logo!");
        }
    }
}
