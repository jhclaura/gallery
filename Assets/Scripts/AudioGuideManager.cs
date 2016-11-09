using UnityEngine;
using System.Collections;

public class AudioGuideManager : MonoBehaviour {

    public GameObject leftButton;
    public GameObject rightButton;

    Vector3 resetAngle = new Vector3();
    Vector3 leftDownAngle = new Vector3(0, 0, -20);
    Vector3 rightDownAngle = new Vector3(0, 0, 20);

    int currentAudioIndex = 0;

	public bool toAnimateButton = false;
	public bool autorun = true;

	int audioCount;

	IEnumerator coroutine;

	#if UNITY_STANDALONE_WIN
    public AudioSource[] audioGuides = new AudioSource[3];
	#else
	public GvrAudioSource[] audioGuides;
	#endif

	bool[] bePlayed = {false, false, false};

//	void Start()
//	{
//		Debug.Log ("Start()!");
//		for (int i = 0; i < audioGuides.Length; i++)
//		{
//			audioGuides[i].Play();
//			Debug.Log ("play audio guide " + i + " in Start");
//			//			audioGuides[i].Pause();
//		}
//	}

	void OnDisable()
	{
//		Debug.Log ("OnDisable()!");
		if (autorun)
		{
			StopCoroutine(coroutine);
		}

		for (int i = 0; i < audioGuides.Length; i++)
		{
			audioGuides[i].Pause();
			bePlayed [i] = false;
		}
	}

	void OnEnable()
	{
//		Debug.Log ("OnEnable()!");
//
//		for (int i = 0; i < audioGuides.Length; i++)
//		{
//			audioGuides[i].Play();
//			Debug.Log ("play audio guide " + i);
////			audioGuides[i].Pause();
//		}
//		Debug.Log ("stop all audio guide audios! amount: " + audioGuides.Length);

		if (autorun)
		{
			coroutine = Autorun ();
			StartCoroutine(coroutine);
		}
	}

	IEnumerator Autorun()
	{
//		for (int i = 0; i < audioGuides.Length; i++)
//		{
////			audioGuides[i].Play();
////			Debug.Log ("play audio guide " + i);
//			audioGuides[i].Pause();
//		}
		currentAudioIndex++;
		SwitchAudios(currentAudioIndex);
		yield return new WaitForSeconds(5f);
		yield return Autorun();
	}


    public void PressLeftButton()
    {
        leftButton.transform.localEulerAngles = leftDownAngle;       
    }

    public void PressRightButton()
    {
        rightButton.transform.localEulerAngles = rightDownAngle;
    }

    public void ReleaseLeftButton()
    {
		if(toAnimateButton)
	        leftButton.transform.localEulerAngles = resetAngle;

        currentAudioIndex--;
        SwitchAudios(currentAudioIndex);
    }

    public void ReleaseRightButton()
    {
		if(toAnimateButton)
  	      rightButton.transform.localEulerAngles = resetAngle;

        currentAudioIndex++;
        SwitchAudios(currentAudioIndex);
    }

    void SwitchAudios(int audioIndex)
    {
        int currentOnIndex = audioIndex % 3;
		Debug.Log ("switch audio guide: " + currentOnIndex);

        for (var i = 0; i < audioGuides.Length; i++)
        {
			if (i == currentOnIndex) {
				if(bePlayed[i]){
					audioGuides [i].UnPause ();
					Debug.Log ("UnPause audio guide: " + i);

				} else {
					audioGuides [i].Play ();
					bePlayed [i] = true;
					Debug.Log ("Play audio guide: " + i);

				}
			} else {
				audioGuides[i].Pause();
				Debug.Log ("Pause audio guide: " + i);

			}
                
        }
    }

	public void AudioVolumeDown (){
		foreach(GvrAudioSource audio in audioGuides){
			audio.gainDb = -18f;
		}
	}

	public void AudioVolumeReset (){
		foreach(GvrAudioSource audio in audioGuides){
			audio.gainDb = 0f;
		}
	}
}
