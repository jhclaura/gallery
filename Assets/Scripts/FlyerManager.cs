using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRTK;

public class FlyerManager : MonoBehaviour {

    private Flyer flyer;

	// Use this for initialization
	void Start () {
        VRTK_SimplePointer pointer = GameObject.FindObjectOfType<VRTK_SimplePointer>();
        pointer.DestinationMarkerEnter += new DestinationMarkerEventHandler(OnObjectHover);

        flyer = GameObject.FindObjectOfType<Flyer>();
	}

    void OnObjectHover(object sender, DestinationMarkerEventArgs e)
    {
        GameObject target = e.target.gameObject;
        DescriptionTag tag = target.GetComponentInParent<DescriptionTag>();

        if (tag)
        {
            string description = tag.description;
            flyer.SetText(description);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
