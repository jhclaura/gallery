using UnityEngine;
using System.Collections;

public class ToolManager : MonoBehaviour {

    public GameObject[] tools = new GameObject[6];

    public SteamVR_TrackedObject controllerRight;

    void Awake()
    {
        //
    }

    public void SwitchToolOfFloor(int currentFloorIndex)
    {
        for (var i = 0; i < tools.Length; i++)
        {
             if (tools[i].activeSelf) tools[i].SetActive(false);
        }

        tools[currentFloorIndex].SetActive(true);

        // right controller vibrates!
        //var deviceIndex = controllerRight.index;
        //var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        //if (deviceIndex != -1)
            //SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(1000);
    }
}
