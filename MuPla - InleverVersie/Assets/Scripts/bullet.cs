using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(10, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
