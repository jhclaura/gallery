using UnityEngine;
using System.Collections;

public class ToolManager : MonoBehaviour {

    public GameObject[] tools = new GameObject[6];

	#if UNITY_STANDALONE_WIN
	public SteamVR_TrackedObject controllerRight;
	#endif

    public void SwitchToolOfFloor(int currentFloorIndex)
    {
        for (var i = 0; i < tools.Length; i++)
        {
             if (tools[i].activeSelf) tools[i].SetActive(false);
        }

        tools[currentFloorIndex].SetActive(true);

        // right controller vibrates!
        // SteamVR_Controller.Input((int)controllerRight.index).TriggerHapticPulse(2000);
        // StartCoroutine(LongVibration(1f, 200));
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
}
