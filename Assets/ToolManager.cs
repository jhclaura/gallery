using UnityEngine;
using System.Collections;

public class ToolManager : MonoBehaviour {

    public GameObject[] tools = new GameObject[6];

    public void SwitchToolOfFloor(int currentFloorIndex)
    {
        for (var i = 0; i < tools.Length; i++)
        {
             if (tools[i].activeSelf) tools[i].SetActive(false);
        }

        tools[currentFloorIndex].SetActive(true);
    }
}
