using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

enum GifPlayerType { Video, ImageSequence, Model };

public class GifPlayer : MonoBehaviour {

    GifPlayerType playerType;

	MediaPlayer videoPlayer;
	Animator animator;

	// Use this for initialization
	void Start () {
        videoPlayer = GetComponent<MediaPlayer>();

        if (videoPlayer)
        {
            playerType = GifPlayerType.Video;
        } else
        {
            playerType = GifPlayerType.ImageSequence;
			animator = GetComponent<Animator>();
        }

        Renderer renderer = GetComponent<Renderer>();
		if (renderer == null)
		{
			playerType = GifPlayerType.Model;
		}


        if (renderer && renderer.isVisible)
        {
            OnBecameVisible();
        } else
        {
            OnBecameInvisible();
        }

		Debug.Log (playerType);
	}

    void OnBecameVisible() {
        if (playerType == GifPlayerType.Video)
        {
            GetComponent<MediaPlayer>().Play();
        } else
        {
            GetComponent<Animator>().enabled = true;
        }
//		Debug.Log ("On Became Visible");
    }

    void OnBecameInvisible() {
        if (playerType == GifPlayerType.Video)
        {
			GetComponent<MediaPlayer> ().Pause ();
        } else
        {
            GetComponent<Animator>().enabled = false;
        }
//		Debug.Log ("On Became Invisible");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
