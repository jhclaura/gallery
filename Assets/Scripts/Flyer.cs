using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flyer : MonoBehaviour {

    public Text text;

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
