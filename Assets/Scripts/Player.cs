using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 direction;
    Vector3 velocity;
    Rigidbody body;
    [SerializeField]float speed;
    float horizontal;
    float vertical;
    [SerializeField]float jump;
    bool grounded = false;
    float horizontalMouse;

    [SerializeField]GameObject camera;


    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

	void Update ()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.velocity = new Vector3(body.velocity.x, jump, body.velocity.z);
            grounded = false;
        }
        direction = new Vector3(horizontal * speed, body.velocity.y, vertical * speed);
        direction = camera.transform.TransformDirection(direction);
        transform.localEulerAngles = new Vector3(0, camera.transform.eulerAngles.y, 0);

        direction.y = 0.0f;
    }

    void FixedUpdate()
    {
       body.velocity = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }
}
