using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraTeleport : MonoBehaviour {

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
    public float cameraScale = 2f;

    public FloorDataManager floorDataManager;
    public Text flyerContents;
    public Text flyerContentDeets;

    public GameObject[] floors;
    SkyColorManager skyColorManager;
    LightManager lightManager;
    ToolManager toolManager;
    RoomManager roomManager;

    private bool initialized = false;

    public Animator eyeMaskAnimator;
    bool eyeMaskOn = false;

    IEnumerator Start ()
    {
        skyColorManager = GetComponent<SkyColorManager>();
        lightManager = GetComponent<LightManager>();
        toolManager = GetComponent<ToolManager>();
        roomManager = GetComponent<RoomManager>();

        cameraScale = transform.localScale.x;

        yield return StartCoroutine(roomManager.LoadAll());

        initialized = true;
    }
	
	void Update ()
    {
        if (initialized)
        {
            DetectFloorAndFall();
        }
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
        float eyeCamHeight = eyeCamera.transform.localPosition.y * cameraScale;
        fallDistance = eyeCamToFloorDist - eyeCamHeight;

        // Debug.Log(rayCollidedWith.transform.gameObject.name);

        if (rayHit)
        {
            if (CurrentFloorChanged(rayCollidedWith)) {
                currentFloor = rayCollidedWith.transform.gameObject;
                Debug.Log("Current Floor Changed to: " + currentFloor.name);

                string f_name = currentFloor.name;
                string[] splitString = f_name.Split('_');
                
                if (splitString.Length > 0)
                {
                    // if hit the floor
                    if (splitString[0] == "Floor")
                    {
                        /// FLYER ////////////////////////////////////////////////////////////////////////////////////
                        // update flyer(front)
                        int floorNum = int.Parse(splitString[1]);
                        GalleryFloor currentGF = floorDataManager.galleryFloors[floorNum]; //floorNum - 1

                        string newText = "Room - " + currentGF.name + "\n\n"
                                       + "Floor - " + currentGF.level + "\n\n"
                                       + "Objects" + "\n\n";

                        for (int i = 0; i < currentGF.arts.Count; i++)
                        {
                            newText += ("- " + currentGF.arts[i]["name"].str + ", by " + currentGF.arts[i]["artist"].str + "\n");
                        }
                        flyerContents.text = newText;

                        // update flyer(back)
                        string newTextBack = "/// Deets ///\n\n";
                        for (int i = 0; i < currentGF.arts.Count; i++)
                        {
                            newTextBack += ("- " + currentGF.arts[i]["name"].str + "\n" + currentGF.arts[i]["description"].str + "\n\n");
                        }
                        flyerContentDeets.text = newTextBack;

                        /// LevelManaging /////////////////////////////////////////////////////////////////////////////
                        // turn on floor(+1) if not already(eg.on floor_2, turn on floor_3)
                        int floorToTurnOn = floorNum+1;
                        if (floorNum == 6) floorToTurnOn = 0;

                        roomManager.ActivateRoom(floorToTurnOn);

                        // v.1
                        //// turn off floor (-3) if not already (eg. on floor_3, turn off floor_1, which is floors[0])
                        //if (floorNum == 1)
                        //{
                        //    // turn off floor 3~6
                        //    for (var i = 2; i < 6; i++)
                        //    {
                        //        roomManager.DeactivateRoom(i);
                        //    }

                        //    Debug.Log("turn off floors[3~6]");
                        //}
                        //else if (floorNum != 2) // don't need to turn off anything if on floor_2
                        //{
                        //    int floorToTurnOff = floorNum - 3;

                        //    roomManager.DeactivateRoom(floorToTurnOff);
                        //}

                        // v.2
                        // turn off floors
                        if (floorNum == 0)
                        {
                            // turn off floor 2~7 if on floor_0
                            for (var i = 2; i < 7; i++)
                            {
                                roomManager.DeactivateRoom(i);
                            }

                            Debug.Log("turn off floors[2~6]");
                        }
                        else if (floorNum != 1) // don't need to turn off anything if on floor_1
                        {
                            //  turn off floor (-2) if not already (eg. on floor_3, turn off floor_1)
                            int floorToTurnOff = floorNum - 2;

                            roomManager.DeactivateRoom(floorToTurnOff);
                        }

                        /// SKYBOX ////////////////////////////////////////////////////////////////////////////////////
                        skyColorManager.SetFloor(floorNum);
                        //RenderSettings.ambientLight = skyColorManager.

                        /// LIGHTING ////////////////////////////////////////////////////////////////////////////////////
                        //lightManager.SwitchLightOfFloor(floorNum - 1);

                        /// TOOL ////////////////////////////////////////////////////////////////////////////////////////
                        toolManager.SwitchToolOfFloor(floorNum);
                    }
                }

                // if inside rabbit hole
                // prepare to restart
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
                /// EyeMasking --------------------------------------------------------
                if (fallDistance < 5f && !eyeMaskOn)
                {
                    // trigger eye mask fade in
                    eyeMaskOn = true;
                    eyeMaskAnimator.SetTrigger("FadeIn");
                    Debug.Log("fade in eye mask");
                }

                // Falling ------------------------------------------------------------
                if (fallDistance > 0.1f)
                {
                    shouldFall = true;
                }
                else
                {
                    // shouldFall = false;

                    // restart into intro room's fake rabbit hole
                    // transform.position = restartPoint.transform.position;

                    // trigger eye mask fade out
                    if(eyeMaskOn)
                        Invoke("EyeMaskFadeOut", 2f);
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

    void EyeMaskFadeOut()
    {
        // trigger eye mask fade out
        transform.position = restartPoint.transform.position;
        eyeMaskAnimator.SetTrigger("FadeOut");

        Debug.Log("fade out eye mask");
        eyeMaskOn = false;
    }
}
