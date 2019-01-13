using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CharacterController : MonoBehaviour {

    public IInput input;
    public float jumpForce;
    private Rigidbody2D rigidbody;
    private bool isGrounded;

	// Use this for initialization
	void Start () {
        if (!ServiceManager.GetServiceManager.GetService(out input))
        {
            throw new System.Exception("No service found");
        }

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        if (input.GetButton("Fire"))
        {
            Debug.Log("Shot");
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }
}
