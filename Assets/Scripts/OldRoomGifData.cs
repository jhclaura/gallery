using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

public class OldRoomGifData : MonoBehaviour {

    public bool autorun;

    int gifIndex = -1;
    int gifObjectCount;
    int gifSourceCount;

    private MediaPlayer workingMediaPlayer;
    private Material workingMaterial;
    private Color originalMaterialColor;
    private bool originalMaterialUnlit;

    bool roomTextureNormal;

    public GameObject oldRoom;
	#if UNITY_STANDALONE_WIN
    AudioSource[] radioAudios;
	#else
	public GvrAudioSource[] radioAudios;
	#endif
    int audioCount;

	IEnumerator coroutine;
	bool inCoroutin;

    // Use this for initialization
    void Awake () {
		Room room = oldRoom.GetComponent<Room>();
		//radioAudios = room.audios;
		audioCount = radioAudios.Length;
		inCoroutin = false;
    }

	public void StopAudios()
    {
//		Debug.Log ("StopAudios()!");
		if (autorun)
		{
			if (inCoroutin) {
				StopCoroutine(coroutine);
				inCoroutin = false;
			}				
		}
			
        //ResetMaterial();

		for (int i = 0; i < radioAudios.Length; i++)
		{
			radioAudios[i].Pause();
		}
//		Debug.Log ("pause all oldroom audios! total: " + radioAudios.Length);
    }

	public void StartAudios()
    {
//		Debug.Log ("StartAudios()!");

		for (int i = 0; i < radioAudios.Length; i++)
		{
			radioAudios[i].Pause();
		}
//		Debug.Log ("pause all oldroom audios! total: " + radioAudios.Length);

        if (autorun)
        {
			coroutine = Autorun ();
			inCoroutin = true;
			StartCoroutine(coroutine);
        }
    }
		
    IEnumerator Autorun()
    {
        ShowNextGif();
		yield return new WaitForSeconds(5f);
        yield return Autorun();
    }

    public void ShowPreviousGif()
    {
        --gifIndex;

        if (gifIndex < 0)
        {
            gifIndex = transform.childCount - 1;
        }

        SetGifState();

		ShuffleAudio();
    }
	
	public void ShowNextGif()
    {
//		Debug.Log ("show next gif");

        gifIndex++;

        if (gifIndex > transform.childCount - 1)
        {
            gifIndex = 0;
        }

        SetGifState();

		ShuffleAudio();
    }

    private void SetGifState()
    {
//        if (workingMediaPlayer)
//        {
//            workingMediaPlayer.Events.RemoveAllListeners();
//        }

//        ResetMaterial();

        for (var i = 0; i < transform.childCount; ++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(i == gifIndex);
        }

//        GameObject activeChild = transform.GetChild(gifIndex).gameObject;
//        ApplyToMaterial animationApplier = activeChild.GetComponent<ApplyToMaterial>();
//        
//        if (animationApplier)
//        {
//            // We've got a video gif
//            workingMediaPlayer = activeChild.GetComponent<MediaPlayer>();
//            workingMediaPlayer.Events.AddListener((MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode e) => {
//                if (et == MediaPlayerEvent.EventType.Started)
//                {
//                    mp.Events.RemoveAllListeners();
//
//                    Material material = animationApplier._material;
//                    workingMaterial = material;
//                    originalMaterialColor = material.color;
//                    originalMaterialUnlit = material.IsKeywordEnabled("S_UNLIT");
//
//                    material.color = Color.white;
//                    material.EnableKeyword("S_UNLIT");
//                }
//            });
//        }
//        else
//        {
//            // We've got a sprite gif
//        }
    }

    private void ResetMaterial()
    {
        if (workingMaterial)
        {
            workingMaterial.color = originalMaterialColor;

            if (!originalMaterialUnlit)
            {
                workingMaterial.DisableKeyword("S_UNLIT");
            }
        }
    }

	public void ShuffleAudio()
    {
		for (int i = 0; i < radioAudios.Length; i++)
        {
			if (i == gifIndex) {
				radioAudios[i].UnPause();
//				Debug.Log ("Shuffle Audio, turn on audio: " + gifIndex);
			} else {
				radioAudios[i].Pause();
			}
                
        }
    }

	public void PauseAllAudios()
	{
		for (int i = 0; i < radioAudios.Length; i++)
		{
			radioAudios[i].Pause();
		}
	}
}
