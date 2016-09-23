using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

public class DataManager : MonoBehaviour {

	// json source holder
	public TextAsset artData;

	// SimpleJSON parse holder
	SimpleJSON.JSONNode art;

	List<JSONNode> artList;
	List<JSONNode> datalist = new List<JSONNode>();
	public Dictionary<string, ArtPainting> artDictionary = new Dictionary<string, ArtPainting>();

	public bool dataAllLoaded = false;

	void Awake () {
		// painting name
		art = JSON.Parse (artData.text);
		List<JSONNode> artkeylist = art.Childs.ToList ();
		artList = artkeylist[0].Childs.ToList ();

		foreach(var v in artList){
			ArtPainting ap = new ArtPainting( v["name"], int.Parse(v["width"]), int.Parse(v["height"]) );		// create MacCountry with country name
			artDictionary.Add(ap.name, ap);
		}

		dataAllLoaded = true;
	}


	public Vector3 getVector3(string rString){
		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		Vector3 rValue = new Vector3(x,y,z);
		return rValue;
	}
}
