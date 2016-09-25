using UnityEngine;
using System.Collections;

public class HandMirror : MonoBehaviour {

    public GameObject mesh;
    public GameObject objects;

    private int currentObjectIndex = 0;

    void Start()
    {
        VisiblityTracker tracker = objects.AddComponent<VisiblityTracker>();
        tracker.BecameInvisibleCallback = ShowNextObject;
        SetActiveObject();
    }

    void OnEnable()
    {
        objects.SetActive(true);
    }

    void OnDisable()
    {
        objects.SetActive(false);
    }

    void ShowNextObject()
    {
        Debug.Log("Showing next object");
        currentObjectIndex += 1;

        if (currentObjectIndex >= objects.transform.childCount)
        {
            currentObjectIndex = 0;
        }

        SetActiveObject();
    }

    void SetActiveObject()
    {
        objects.transform.GetChild(currentObjectIndex).gameObject.SetActive(true);

        for (int i = 0; i < objects.transform.childCount; ++i)
        {
            Transform child = objects.transform.GetChild(i);

            if (i != currentObjectIndex)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
