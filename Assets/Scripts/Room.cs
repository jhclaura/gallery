﻿using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public string roomName;
    public Light[] lights;
    public AudioSource[] audios;

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

    public void Deactivate()
    {
        transform.gameObject.SetActive(false);
    }

    public void PlayAudios()
    {
        if (audios.Length > 0)
        {
            foreach (AudioSource audio in audios)
            {
                //audio.UnPause();
                audio.enabled = true;
            }
        }
        
    }

    public void PauseAudios()
    {
        if (audios.Length > 0)
        {
                foreach (AudioSource audio in audios)
            {
                //audio.Pause();
                audio.enabled = false;
            }
        }
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
