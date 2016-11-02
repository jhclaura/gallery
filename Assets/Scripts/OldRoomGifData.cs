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
	GvrAudioSource[] radioAudios;
	#endif
    int audioCount;

	IEnumerator coroutine;

    // Use this for initialization
    void Start () {
		Room room = oldRoom.GetComponent<Room>();
		radioAudios = room.audios;
		audioCount = radioAudios.Length;
    }

    void OnDisable()
    {
		Debug.Log ("OnDisable()!");
		if (autorun)
		{
			StopCoroutine(coroutine);
		}
			
        ResetMaterial();

		for (int i = 0; i < audioCount; i++)
		{
			radioAudios[i].Stop();
		}
    }

    void OnEnable()
    {
		Debug.Log ("OnEnable()!");

		for (int i = 0; i < audioCount; i++)
		{
			radioAudios[i].UnPause();
			radioAudios[i].Pause();
		}
		Debug.Log ("pause all audios! total: " + audioCount);

        if (autorun)
        {
			coroutine = Autorun ();
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
		Debug.Log ("show next gif");

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
		Debug.Log ("Shuffle Audio, audio count: " + audioCount);
        for (int i = 0; i < audioCount; i++)
        {
			if (i == gifIndex) {
				radioAudios[i].UnPause();
			} else {
				radioAudios[i].Pause();
			}
                
        }
    }
}
