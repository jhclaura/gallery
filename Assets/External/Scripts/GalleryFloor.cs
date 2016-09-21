using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalleryFloor {

    public string name;
    public int level;
    public List<JSONObject> arts; // name, artist, description

    public GalleryFloor(string _name, int number, List<JSONObject> _arts) {
        name = _name;
        level = number;
        arts = _arts;
    }
}
