using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlyerManagerMobile : MonoBehaviour {

	public GameObject flyerCanvas;
	public GameObject pointer;
	Flyer flyer;
	SimplePointer simplePointer;

	public bool inInfoMode = false;

	// Use this for initialization
	void Start () {
//        VRTK_SimplePointer pointer = GameObject.FindObjectOfType<VRTK_SimplePointer>();
//        pointer.DestinationMarkerEnter += new DestinationMarkerEventHandler(OnObjectHover);

		flyer = flyerCanvas.GetComponent<Flyer>();
		simplePointer = pointer.GetComponent<SimplePointer> ();
		simplePointer.TogglePointer ( inInfoMode );
		flyerCanvas.SetActive( inInfoMode );
	}

	public void OnObjectHover(RaycastHit collidedObj)
    {
		DescriptionTag tag = collidedObj.transform.gameObject.GetComponentInParent<DescriptionTag>();

        if (tag)
        {
            string description = tag.description;
            flyer.SetText(description);
		} else {
			flyer.SetText("...");
		}
    }
	
	public void ToggleMode(bool _inInfoMode){
		inInfoMode = _inInfoMode;

		simplePointer.TogglePointer ( inInfoMode );
		flyerCanvas.SetActive( inInfoMode );
	}
}
