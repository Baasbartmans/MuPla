using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            transform.Rotate(Vector3.forward);
        }
        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(Vector3.back);
        }

    }
}
