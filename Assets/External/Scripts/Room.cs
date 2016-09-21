using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public string roomName;
    public Light[] lights;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate()
    {
        transform.gameObject.SetActive(true);
    }

    public void Deactivate() {
        transform.gameObject.SetActive(false);
    }

    void ActivateLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
    }

    void DeactivateLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }
}
