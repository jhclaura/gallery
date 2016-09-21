using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViveTeleport : MonoBehaviour {

    GameObject currentFloor = null;
    public bool useGravity = false;
    public GameObject eyeCamera;
    public float fallSpeed = 2f;
    //public bool detectOnlyWhenFloorChanges = false;
    public GameObject theLastFloor;
    public GameObject restartPoint;

    public bool shouldFall = false;
    float fallDistance = 0f;
    bool toRestart = false;

    public FloorManager floorManager;
    public Text flyerContents;

	// Use this for initialization
	void Start ()
    {
        //DetectFloorAndFall();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DetectFloorAndFall();
    }

    bool CurrentFloorChanged(RaycastHit collidedObj)
    {
        return (currentFloor != collidedObj.transform.gameObject);
    }

    void DetectFloorAndFall()
    {
        Ray ray = new Ray(eyeCamera.transform.position, -transform.up);
        RaycastHit rayCollidedWith;
        bool rayHit = Physics.Raycast(ray, out rayCollidedWith);
        float eyeCamToFloorDist = rayCollidedWith.distance;
        float eyeCamHeight = eyeCamera.transform.localPosition.y;
        fallDistance = eyeCamToFloorDist - eyeCamHeight;

        if (rayHit)
        {
            //Debug.Log("fallDistance: " + fallDistance);

            if (CurrentFloorChanged(rayCollidedWith)) {
                currentFloor = rayCollidedWith.transform.gameObject;


                //Debug.Log(currentFloor.name);
                string f_name = currentFloor.name;
                string[] splitString = f_name.Split('_');
                // update flyer
                if (splitString[0]=="Floor") { 
                    int floorNum = int.Parse(splitString[1]);
                    //Debug.Log( floorManager.galleryFloors[floorNum-1].name );
                    GalleryFloor currentGF = floorManager.galleryFloors[floorNum - 1];

                    string newText = "Room - " + currentGF.name + "\n\n"
                                        + "Floor - " + currentGF.level + "\n\n"
                                        + "Objects" + "\n\n";

                    for (int i = 0; i < currentGF.arts.Count; i++) {
                        newText += ("- " + currentGF.arts[i]["name"].str + ", by " + currentGF.arts[i]["artist"].str + "\n");  // + currentGF.arts[i]["description"].str
                    }

                    flyerContents.text = newText;
                }
                //
                if (currentFloor == theLastFloor)
                {
                    toRestart = true;
                }
                else {
                    toRestart = false;
                }
            }

            if (!toRestart)
            {
                // if the eyeCam to floor distance is bigger than eyeCam height
                if (fallDistance > 0.1f)
                {
                    // should fall until the distance is zero
                    shouldFall = true;
                }
                else
                {
                    shouldFall = false;
                }
            }
            else {
                if (fallDistance > 0.1f)
                {
                    shouldFall = true;
                }
                else
                {
                    shouldFall = false;
                    transform.position = restartPoint.transform.position;
                }
            }
            
        }
        else
        {
            Debug.Log("ray hits nothing");
        }
    }

    void FixedUpdate()
    {
        if (shouldFall) {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
        }
    }
}
