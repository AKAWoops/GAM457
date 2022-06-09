using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private string name;
    [SerializeField]
    private float speed;

    private Rigidbody body;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputControls();
    }

    //if
    //(condition with a true/false answer)
    //{ what happens }

    //if (health > 100) // true
    //{
    //   Debug.Log("Health is more than 100");
    //}
    //else //false
    //{
    //   Debug.Log("Health is *NOT* more than 100");
    //}

    void InputControls()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.left * -speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float force = jumpForce();
            Jump(force);
        }
    }


    void Jump(float jumpforce)
    {
        body.AddForce(Vector3.up * jumpforce);
    }

    float jumpForce()
    {
        float force = body.velocity.magnitude + 100;
        return force;
    }

}