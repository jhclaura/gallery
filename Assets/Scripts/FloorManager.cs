using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FloorManager : MonoBehaviour {

    // json source holder
    public TextAsset floorDataFile;
    // SimpleJSON parse holder
    JSONObject floorData;

    public List<GalleryFloor> galleryFloors = new List<GalleryFloor>();

    void Awake() {
        JSONObject floorRawData = new JSONObject(floorDataFile.text);
        floorData = floorRawData["floors"];
        //Debug.Log(floorData);

        for (int i = 0; i < floorData.list.Count; i++)
        {
            // level number
            int key = int.Parse(floorData.keys[i]);
           
            JSONObject fd = (JSONObject)floorData.list[i];

            // floor name
            string f_name = fd["name"].str;

            // arrrts
            JSONObject arts = (JSONObject)fd["object"];
            JSONObject art_des = (JSONObject)fd["description"];
            List<JSONObject> allTheArts = new List<JSONObject>();

            for (int j=0; j< arts.list.Count; j++)
            {
                // art name
                string keyy = (string)arts.keys[j];
                // artist name
                string artistName = arts[keyy].str;
                // art description
                string artDes = art_des[keyy].str;

                JSONObject a = new JSONObject(JSONObject.Type.OBJECT);
                a.AddField("name", keyy);
                a.AddField("artist", artistName);
                a.AddField("description", artDes);

                allTheArts.Add( a );
            }

            GalleryFloor g_floor = new GalleryFloor( f_name, key, allTheArts );
            galleryFloors.Add(g_floor);
        }
    }
	void Start () {
        //for (int i=0; i< floors.Length; i++) {
        //    GalleryFloor gf = new GalleryFloor(floors[i]);
        //    galleryFloors.Add(gf);
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
