using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VideoPlayer : MonoBehaviour {

    public GameObject[] videos;
    List<MovieTexture> movTextures = new List<MovieTexture>();

    void Start () {
        //GetComponent<Renderer>().material.mainTexture = movTexture;
        //movTexture.Play();

        for (int i = 0; i < videos.Length; i++) {
            MovieTexture movT = (MovieTexture)videos[i].GetComponent<Renderer>().material.mainTexture;
            movT.loop = true;
            movT.Play();
            movTextures.Add(movT);
        }
    }
    
}
