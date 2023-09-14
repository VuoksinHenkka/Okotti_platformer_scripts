using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_physics : MonoBehaviour
{
    private Rigidbody ourRigidbody;

    private void Awake()
    {
        ourRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() //laitoin fixedupdateen, toimii varmemmin (mut ei varmaan kannata sekoittaa heid‰n p‰it‰‰n viel‰)
    {
        ourRigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 2 * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
