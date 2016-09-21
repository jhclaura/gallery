using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    public Light[] allLights = new Light[6];

	public void SwitchLightOfFloor(int currentFloorIndex)
    {
        for (var i=0; i<allLights.Length; i++) {
            if (i != currentFloorIndex)
            {
                if (allLights[i].isActiveAndEnabled) allLights[i].enabled = false;
            }
            else
            {
                if (!allLights[i].isActiveAndEnabled) allLights[i].enabled = true;
            }
        }
    }
}
