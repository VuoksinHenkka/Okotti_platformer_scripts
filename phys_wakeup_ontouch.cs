using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phys_wakeup_ontouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
