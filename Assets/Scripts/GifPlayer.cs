using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

enum GifPlayerType { Video, ImageSequence };

public class GifPlayer : MonoBehaviour {

    GifPlayerType playerType;

	// Use this for initialization
	void Start () {
        MediaPlayer videoPlayer = GetComponent<MediaPlayer>();

        if (videoPlayer)
        {
            playerType = GifPlayerType.Video;
        } else
        {
            playerType = GifPlayerType.ImageSequence;
        }

        Renderer renderer = GetComponent<Renderer>();

        if (renderer && renderer.isVisible)
        {
            OnBecameVisible();
        } else
        {
            OnBecameInvisible();
        }
	}

    void OnBecameVisible() {
        if (playerType == GifPlayerType.Video)
        {
            GetComponent<MediaPlayer>().Play();
        } else
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    void OnBecameInvisible() {
        if (playerType == GifPlayerType.Video)
        {
            GetComponent<MediaPlayer>().Pause();
        } else
        {
            GetComponent<Animator>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
