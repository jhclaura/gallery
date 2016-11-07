using UnityEngine;
using System.Collections;

public class ToolManager : MonoBehaviour {

    public GameObject[] tools = new GameObject[6];
	public GameObject emptyObject;
	GameObject toolWheel;
	int currentFloor;

	float origianlHeight;
	public float liftHeight;
	bool toolUpThere = false;
	int[] floorToolIndex = {1,2,3,4,0,0,0};

	#if UNITY_STANDALONE_WIN
	public SteamVR_TrackedObject controllerRight;
	#endif

	AudioGuideManager audioGuideManager;

	void Start()
	{
		toolWheel = tools [0].transform.parent.gameObject;
		origianlHeight = toolWheel.transform.localPosition.y;

		audioGuideManager = tools [6].GetComponent<AudioGuideManager> ();
	}

    public void SwitchToolOfFloor(int currentFloorIndex)
    {
		currentFloor = currentFloorIndex;
		tools[currentFloorIndex].SetActive(true);

		// if there's no tool then lift up the tool_wheel
		if (tools [currentFloorIndex] == emptyObject) {
			if (!toolUpThere) {
				LeanTween.moveLocalY(toolWheel, liftHeight, 1f).setOnComplete( SwitchOffTool );
				toolUpThere = true;
			}
		}
		// if there's tool
		else
		{
			// if is lifted, bring it down
			if (toolUpThere) {
				LeanTween.moveLocalY(toolWheel, origianlHeight, 1f);
				toolUpThere = false;
			}

			// rotate the tool wheel
			float rotY = floorToolIndex[currentFloorIndex] * 72f;
			LeanTween.rotateLocal(toolWheel, new Vector3(0f,180f,rotY), 1f).setOnComplete( SwitchOffTool );
		}


//        for (var i = 0; i < tools.Length; i++)
//        {
//             if (tools[i].activeSelf) tools[i].SetActive(false);
//        }
//        tools[currentFloorIndex].SetActive(true);

        // right controller vibrates!
        // SteamVR_Controller.Input((int)controllerRight.index).TriggerHapticPulse(2000);
        // StartCoroutine(LongVibration(1f, 200));
    }

	void SwitchOffTool(){
		for (var i = 0; i < tools.Length; i++)
		{
			if (tools[i].activeSelf && i!=currentFloor)
				tools[i].SetActive(false);
		}
	}

	#if UNITY_STANDALONE_WIN
    IEnumerator HapticPulse(float duration, int hapticPulseStrength, float pulseInterval)
    {
        if (pulseInterval <= 0)
        {
            yield break;
        }

        while (duration > 0)
        {
            SteamVR_Controller.Input((int)controllerRight.index).TriggerHapticPulse((ushort)hapticPulseStrength);
            // device.TriggerHapticPulse((ushort)hapticPulseStrength);
            yield return new WaitForSeconds(pulseInterval);
            duration -= pulseInterval;
        }

    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)controllerRight.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
	    }
	}
	#endif

	public void AudioGuideVolumeDown(){
		audioGuideManager.AudioVolumeDown ();
	}

	public void AudioGuideVolumeReset(){
		audioGuideManager.AudioVolumeReset ();
	}
}
