using UnityEngine;
using System.Collections;

public class RabbitHoleManager : MonoBehaviour {

    public GameObject objetScatter;

    void OnEnable()
    {
        objetScatter.SetActive(true);
    }

    void OnDisable()
    {
        objetScatter.SetActive(false);
    }
}
