using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

public class OldRoomGifData : MonoBehaviour {

    public bool autorun;

    int gifIndex = 0;
    int gifObjectCount;
    int gifSourceCount;

    private MediaPlayer workingMediaPlayer;
    private Material workingMaterial;
    private Color originalMaterialColor;
    private bool originalMaterialUnlit;

    bool roomTextureNormal;

    public GameObject oldRoom;
    AudioSource[] radioAudios;
    int audioCount;

    // Use this for initialization
    void Start () {
        SetGifState();

        if (autorun)
        {
            StartCoroutine(Autorun());
        }

        // AUDIO
        Room room = oldRoom.GetComponent<Room>();
        radioAudios = room.audios;
        audioCount = radioAudios.Length;

        ShuffleAudio();
    }

    void OnDisable()
    {
        ResetMaterial();
    }

    void OnEnable()
    {
        ShuffleAudio();
    }

    IEnumerator Autorun()
    {
        yield return new WaitForSeconds(3f);
        ShowNextGif();
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
        if (workingMediaPlayer)
        {
            workingMediaPlayer.Events.RemoveAllListeners();
        }

        ResetMaterial();

        for (var i = 0; i < transform.childCount; ++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(i == gifIndex);
        }

        GameObject activeChild = transform.GetChild(gifIndex).gameObject;
        ApplyToMaterial animationApplier = activeChild.GetComponent<ApplyToMaterial>();
        
        if (animationApplier)
        {
            // We've got a video gif
            workingMediaPlayer = activeChild.GetComponent<MediaPlayer>();
            workingMediaPlayer.Events.AddListener((MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode e) => {
                if (et == MediaPlayerEvent.EventType.Started)
                {
                    mp.Events.RemoveAllListeners();

                    Material material = animationApplier._material;
                    workingMaterial = material;
                    originalMaterialColor = material.color;
                    originalMaterialUnlit = material.IsKeywordEnabled("S_UNLIT");

                    material.color = Color.white;
                    material.EnableKeyword("S_UNLIT");
                }
            });
        }
        else
        {
            // We've got a sprite gif
        }
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
        for (int i = 0; i < audioCount; i++)
        {
            if (i == (gifIndex % audioCount))
                radioAudios[i].UnPause();
            else
                radioAudios[i].Pause();
        }
    }
}
