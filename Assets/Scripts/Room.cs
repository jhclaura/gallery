using UnityEngine;
using System.Collections;
using UnityStandardAssets.Water;

public class Room : MonoBehaviour {

    public string roomName;
    public Light[] lights;

	#if UNITY_STANDALONE_WIN
    public AudioSource[] audios;
	#else
	public GvrAudioSource[] audios;
	#endif

	public OldRoomGifData gifData;

	public Water water;
	public GameObject dummyWater;

	public Animator[] animators;

	public GameObject[] arts;

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
			#if UNITY_STANDALONE_WIN
            foreach (AudioSource audio in audios)
			#else
			foreach (GvrAudioSource audio in audios)
			#endif
            {
                //audio.UnPause();
                audio.enabled = true;
				audio.Play();
            }
        }

        if (gifData != null)
        {
            gifData.ShuffleAudio();
        }
    }

    public void PauseAudios()
    {
        if (audios.Length > 0)
        {
			#if UNITY_STANDALONE_WIN
			foreach (AudioSource audio in audios)
			#else
			foreach (GvrAudioSource audio in audios)
			#endif
            {
                //audio.Pause();
                audio.enabled = false;
            }
        }
    }

	public void ActivateLights()
    {
        foreach (Light light in lights)
        {
			if(!light.isActiveAndEnabled)
	            light.enabled = true;
        }
    }

	public void DeactivateLights()
    {
        foreach (Light light in lights)
        {
			if(light.isActiveAndEnabled)
	            light.enabled = false;
        }
    }

	public void ActivateWater()
	{
		if (water && !water.isActiveAndEnabled) {
			dummyWater.SetActive (false);
			water.enabled = true;
		}
	}

	public void DeactivateWater()
	{
		if (water && water.isActiveAndEnabled) {
			dummyWater.SetActive (true);
			water.enabled = false;
		}
	}

	public void ActivateAnimators()
	{
		foreach (Animator ani in animators)
		{
			if(ani && !ani.isActiveAndEnabled)
				ani.enabled = true;
		}
	}

	public void DeactivateAnimators()
	{
		foreach (Animator ani in animators)
		{
			if(ani && ani.isActiveAndEnabled)
				ani.enabled = false;
		}
	}

	public void ActivateArts()
	{
		foreach (GameObject art in arts)
		{
			if(art && !art.activeSelf)
				art.SetActive(true);
		}
	}

	public void DeactivateArts()
	{
		foreach (GameObject art in arts)
		{
			if(art && art.activeSelf)
				art.SetActive(false);
		}
	}

}
