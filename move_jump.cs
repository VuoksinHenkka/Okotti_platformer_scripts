using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_jump : MonoBehaviour
{
   public Rigidbody body;
    public bool isGrounded = false;
    public float distanceToGround = 0;
    public float jumpforce = 2;
    private void Awake()
    {
        if (body == null) body = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y; //jos keskipiste keskellä hahmoa
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) jump();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }

    private void jump()
    {
        if (!isGrounded) return;
        body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

    }
}
