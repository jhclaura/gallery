using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;

public class Gif : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<GameObject> targetObjects = new List<GameObject>();

        MediaPlayer rootPlayer = GetComponent<MediaPlayer>();
        Animator rootAnimator = GetComponent<Animator>();

        if (rootPlayer || rootAnimator)
        {
            targetObjects.Add(transform.gameObject);
        }

        MediaPlayer[] childPlayers = GetComponentsInChildren<MediaPlayer>();
        Animator[] childAnimators = GetComponentsInChildren<Animator>();
        foreach (MediaPlayer player in childPlayers) {
            targetObjects.Add(player.transform.gameObject);
        }

        foreach (Animator animator in childAnimators) {
            targetObjects.Add(animator.transform.gameObject);
        }

        foreach (GameObject targetObject in targetObjects) {
            targetObject.AddComponent<GifPlayer>();
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
