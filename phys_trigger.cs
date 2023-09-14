using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phys_trigger : MonoBehaviour
{

   [SerializeField] public List<Rigidbody> rigidbodies;
    [SerializeField]  public ParticleSystem ourparticlesystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sphere")
        {
            foreach(var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
                rigidbody.AddForce(((rigidbody.transform.position - other.transform.position).normalized + Vector3.forward*5f) * Random.Range(5,15), ForceMode.Impulse);
            }
            ourparticlesystem.Play();
        }
    }
}
