using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamTeleportManager : MonoBehaviour {

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

    public GameObject[] floors;
    SkyColorManager skyColorManager;
    LightManager lightManager;
    ToolManager toolManager;
//	RoomManager roomManager;
    MobileRoomManager roomManager;

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

	bool amIVive = false;
	public float camHeight = 2;
	public LayerMask layersToIgnore;
	public LayerMask floorLayers;
	int floorMask;
	public GameObject menuObject;
	bool menuObjectIsHidden = false;
	public MenuManger menuManager;

	Vector3 downVec = new Vector3(0,-1,0);

	public Color fadeColor = Color.black;
	public float fadeTime = 2f;

	void Awake()
	{	
		//v.2
		roomManager = GetComponent<MobileRoomManager>();
		roomManager.PairRoomByType();

		// target frame rate to be 60, both in edior & build
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 60;
	}

	// IEnumerator
	void Start ()
    {
		#if UNITY_STANDALONE_WIN
		amIVive = true;
		#endif

        skyColorManager = GetComponent<SkyColorManager>();
        lightManager = GetComponent<LightManager>();
        toolManager = GetComponent<ToolManager>();

        cameraScale = transform.localScale.x;

		// v.1 ( with IEnumerator Start() )
//			roomManager = GetComponent<RoomManager>();
//	        yield return StartCoroutine(roomManager.LoadAll());

        fallSpeed = fallSpeedNormal;
        rabbitHole = GetComponent<AudioSource>();
        rabbitHole.enabled = false;

		menuManager = menuObject.GetComponent<MenuManger> ();
		menuObject.SetActive (false);

		floorMask = floorLayers;

        initialized = true;
		Debug.Log("fully initialized!");

//		Invoke ("ToInitialize", 2f);
    }
	
	void Update ()
    {
        if (initialized)
        {
            DetectFloorAndFall();
        }

		#if UNITY_STANDALONE_WIN
		// Vive feature: able to be up or down in the water
		if (amIVive && currentFloorNum == 3)
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
		#endif
    }

    bool CurrentFloorChanged(RaycastHit collidedObj)
    {
        return (currentFloor != collidedObj.transform.gameObject);
    }

    void DetectFloorAndFall()
    {
		#if UNITY_STANDALONE_WIN
        Ray ray = new Ray(eyeCamera.transform.position, -transform.up);
		#else
		Ray ray = new Ray(eyeCamera.transform.position, downVec);
		#endif

        RaycastHit rayCollidedWith;
		bool rayHit = Physics.Raycast(ray, out rayCollidedWith, Mathf.Infinity, floorMask);
        float eyeCamToFloorDist = rayCollidedWith.distance;

		// Camera to floor distance
		#if UNITY_STANDALONE_WIN
        float eyeCamHeight = eyeCamera.transform.localPosition.y * cameraScale;
		#else
		float eyeCamHeight = camHeight;
		#endif
        fallDistance = eyeCamToFloorDist - eyeCamHeight;

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
                        // turn on floor(+1)
						// if not already(eg.on floor_2, turn on floor_3)
                        int floorToTurnOn = floorNum+1;
                        if (floorNum == 6) floorToTurnOn = 0;

                        roomManager.ActivateRoom(floorToTurnOn);

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

                        /// Audios & Lights /////////////////////////////////////////////////////////////////////////////////////
						// turn on floor(+0)
						// (eg.on floor_2, turn on floor_2)
                        int floorAudioToTurnOn = floorNum;
						roomManager.ActivateArt(floorAudioToTurnOn);
                        roomManager.ActivateAudio(floorAudioToTurnOn);
						roomManager.ActivateLight(floorAudioToTurnOn);
                        Debug.Log("turn on audio+light floor" + floorAudioToTurnOn);

                        // turn off
                        if (floorNum == 0)
                        {
                            // turn off floor 1~7 if on floor_0
                            for (var i = 1; i < 7; i++)
                            {
                                roomManager.DeactivateAudio(i);
								roomManager.DeactivateLight(i);
								roomManager.DeactivateArt(i);
                            }
							Debug.Log("turn off audio+light floors[1~6]");
                        }
                        else
                        {
                            //  turn off floor (-1) (eg. on floor_3, turn off floor_2)
                            int floorToTurnOff = floorNum - 1;
                            roomManager.DeactivateAudio(floorToTurnOff);
							roomManager.DeactivateLight(floorToTurnOff);
							roomManager.DeactivateArt(floorToTurnOff);
                        }

                        /// Water //////////////////////////////////////////////////////////////////////////////////////
						if (floorNum == 3) {
							roomManager.ActivateWater (3);

							if (waterManager == null) {
								waterManager = currentFloor.GetComponent<WaterManager> ();
								water = waterManager.surfaceWater;
							}

							// prepare to fall into water
							toFallIntoWater = true;
						} else {
							roomManager.DeactivateWater (3);
						}
							

                        /// SKYBOX ////////////////////////////////////////////////////////////////////////////////////
                        skyColorManager.SetFloor(floorNum);

                        /// TOOL ////////////////////////////////////////////////////////////////////////////////////////
                        toolManager.SwitchToolOfFloor(floorNum);

						/// MENU_OBJECT ///
						menuObject.SetActive(false);
						menuObjectIsHidden = true;
						menuManager.flyerManager.ToggleMode (false);
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

					#if UNITY_STANDALONE_WIN
                    eyeMaskBottom.SetActive(true);
                    eyeMaskTop.SetActive(true);
					#endif

					roomManager.ActivateAnimator (6);
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

					if (menuObjectIsHidden) {
						// update menu_object position, and show it
						menuObject.transform.position = new Vector3(
							transform.position.x - 1f,
							transform.position.y - camHeight,
							transform.position.z);
						menuObject.SetActive(true);
						menuObjectIsHidden = false;

						if(menuManager.inInfoMode)
							menuManager.flyerManager.ToggleMode (true);
					}
                }

                // WATER
                if (toFallIntoWater)
                {
					if(fallDistance < (camHeight+.5f) )
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

					#if UNITY_STANDALONE_WIN
                    eyeMaskAnimator.SetTrigger("FadeIn");
                    Debug.Log("fade in eye mask");

					#else
					CameraFade.StartAlphaFadeInOut(fadeColor, fadeTime, 2f, null);

					#endif

					roomManager.DeactivateAnimator (6);
                }

                // Falling ------------------------------------------------------------
                if (fallDistance > 0.1f)
                {
                    shouldFall = true;
                }
                else
                {
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
            //Debug.Log("ray hits nothing");
        }
    }

	void ToInitialize()
	{
		initialized = true;
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
		eyeMaskOn = false;
		Debug.Log("eyeMaskOn = false");

		#if UNITY_STANDALONE_WIN
		eyeMaskAnimator.SetTrigger("FadeOut");
        eyeMaskBottom.SetActive(false);

        Invoke("DisableEyeMask", 3.5f);

        Debug.Log("fade out eye mask");
		#endif
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
