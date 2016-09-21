using UnityEngine;
using System.Collections;

public class WaterSoundManager : MonoBehaviour {

    public GameObject cameraEye;
    AudioSource waterSound;

    Vector3 oldPos;
    Vector3 currPos;

    public float minDistance = 2f;
    public float maxDistance = 10f;

    void Start()
    {
        oldPos = cameraEye.transform.localPosition;
        waterSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        currPos = cameraEye.transform.localPosition;
        float dist = Vector3.Distance(oldPos, currPos);

        Debug.Log(dist);
        //dist = (maxDistance - minDistance)
        //float volume = Mathf.Clamp(dist, );

        oldPos = currPos;
    }
}
