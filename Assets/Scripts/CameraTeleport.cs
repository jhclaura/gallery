using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraTeleport : MonoBehaviour {

    GameObject currentFloor = null;
    public bool useGravity = false;
    public GameObject eyeCamera;
    float fallSpeed;
    public float fallSpeedNormal = 2f;
    public float fallSpeedFast = 4f;
    public float fallSpeedSlow = 0.5f;
    //public bool detectOnlyWhenFloorChanges = false;
    public GameObject theLastFloor;
    public GameObject restartPoint;

    public bool shouldFall = false;
    float fallDistance = 0f;
    bool toRestart = false;
    public float cameraScale = 2f;
    bool toFallIntoWater = false;

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
    public GameObject eyeMaskTop;
    public GameObject eyeMaskBottom;
    bool eyeMaskOn = false;

    AudioSource rabbitHole;
    WaterManager waterManager;
    GameObject water;
    bool cameraAboveWaterStatus = true;
    bool cameraAboveWaterPastStatus = true;
    public int currentFloorNum;

    IEnumerator Start ()
    {
        skyColorManager = GetComponent<SkyColorManager>();
        lightManager = GetComponent<LightManager>();
        toolManager = GetComponent<ToolManager>();
        roomManager = GetComponent<RoomManager>();

        cameraScale = transform.localScale.x;

        yield return StartCoroutine(roomManager.LoadAll());

        fallSpeed = fallSpeedNormal;
        rabbitHole = GetComponent<AudioSource>();
        rabbitHole.enabled = false;

        initialized = true;
    }
	
	void Update ()
    {
        if (initialized)
        {
            DetectFloorAndFall();
        }

        if (currentFloorNum == 3)
        {
            // if camera(head) lower than surface water, turn off surface water, turn on under water. vice versa
            if(eyeCamera.transform.position.y < water.transform.position.y)
            {
                cameraAboveWaterStatus = false;
                if(cameraAboveWaterStatus != cameraAboveWaterPastStatus)
                {
                    waterManager.TurnOnSurfaceWater(false);
                    cameraAboveWaterPastStatus = false;
                }
                
            }

            if (eyeCamera.transform.position.y >= water.transform.position.y)
            {
                cameraAboveWaterStatus = true;
                if (cameraAboveWaterStatus != cameraAboveWaterPastStatus)
                {
                    waterManager.TurnOnSurfaceWater(true);
                    cameraAboveWaterPastStatus = true;
                }
            }
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
                        int floorNum = int.Parse(splitString[1]);
                        currentFloorNum = floorNum;

                        /// LevelManaging /////////////////////////////////////////////////////////////////////////////
                        // turn on floor(+1) if not already(eg.on floor_2, turn on floor_3)
                        int floorToTurnOn = floorNum+1;
                        if (floorNum == 6) floorToTurnOn = 0;

                        roomManager.ActivateRoom(floorToTurnOn);

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

                        /// Audios /////////////////////////////////////////////////////////////////////////////////////
                        // turn on
                        // (eg.on floor_2, turn on floor_2)
                        int floorAudioToTurnOn = floorNum;
                        roomManager.ActivateAudio(floorAudioToTurnOn);
                        Debug.Log("turn on audio floor" + floorAudioToTurnOn);

                        // turn off
                        if (floorNum == 0)
                        {
                            // turn off floor 1~7 if on floor_0
                            for (var i = 1; i < 7; i++)
                            {
                                roomManager.DeactivateAudio(i);
                            }
                            Debug.Log("turn off audio floors[1~6]");
                        }
                        else
                        {
                            //  turn off floor (-1) (eg. on floor_3, turn off floor_2)
                            int floorToTurnOff = floorNum - 1;
                            roomManager.DeactivateAudio(floorToTurnOff);
                        }

                        /// Water //////////////////////////////////////////////////////////////////////////////////////
                        if (floorNum == 3)
                        {
                            if(waterManager==null)
                            {
                                waterManager = currentFloor.GetComponent<WaterManager>();
                                water = waterManager.surfaceWater;
                            }

                            // prepare to fall into water
                            toFallIntoWater = true;
                        }

                        /// SKYBOX ////////////////////////////////////////////////////////////////////////////////////
                        skyColorManager.SetFloor(floorNum);
                        //RenderSettings.ambientLight = skyColorManager.

                        /// TOOL ////////////////////////////////////////////////////////////////////////////////////////
                        toolManager.SwitchToolOfFloor(floorNum);
                    }
                }

                // if inside rabbit hole
                // prepare to restart
                if (currentFloor == theLastFloor)
                {
                    toRestart = true;

                    // AUDIO
                    rabbitHole.enabled = true;
                    fallSpeed = fallSpeedFast;

                    eyeMaskBottom.SetActive(true);
                    eyeMaskTop.SetActive(true);
                }
                else {
                    toRestart = false;
                }
            }

            /// FALLING ////////////////////////////////////////////////////////////////
            // NORMAL_FALL
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

                // WATER
                if (toFallIntoWater)
                {
                    if(fallDistance < 2.2f)
                    {
                        // decrease fall speed!
                        fallSpeed = fallSpeedSlow;
                        toFallIntoWater = false;

                        Invoke("ResetFallSpeed", 5f);
                    }
                }
            }
            // RABBIT_HOLE
            else
            {
                /// EyeMasking --------------------------------------------------------
                if (fallDistance < 5f && !eyeMaskOn)
                {
                    // trigger eye mask fade in
                    eyeMaskOn = true;
                    Debug.Log("eyeMaskOn = true");
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
                    if (eyeMaskOn && rabbitHole.isActiveAndEnabled)
                    {
                        Invoke("EyeMaskFadeOut", 2f);
                        // AUDIO
                        rabbitHole.enabled = false;
                        fallSpeed = fallSpeedNormal;

                    }
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

        eyeMaskBottom.SetActive(false);

        eyeMaskOn = false;
        Debug.Log("eyeMaskOn = false");

        Invoke("DisableEyeMask", 3.5f);

        Debug.Log("fade out eye mask");
    }

    void DisableEyeMask()
    {
        eyeMaskTop.SetActive(false);
    }

    void ResetFallSpeed()
    {
        fallSpeed = fallSpeedNormal;
    }
}
