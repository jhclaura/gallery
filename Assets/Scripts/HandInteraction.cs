using UnityEngine;
using System.Collections;

public class HandInteraction : MonoBehaviour {

    Collider handCollider;

    void Start()
    {
        handCollider = GetComponent<Collider>();
    }
	void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject == logo)
        //{
            Debug.Log("start touching logo!");
        //}
    }

    void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject == logo)
        //{
            Debug.Log("stop touching logo!");
        //}
    }
}
