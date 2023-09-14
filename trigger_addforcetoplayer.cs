using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_addforcetoplayer : MonoBehaviour
{

    public Vector3 MoveForce = Vector3.zero;
    public bool isActive = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;
            Manager_Game.Instance.ref_playermovement.MoveDirection_ExternalForce = Vector3.zero;
        }
    }

    private void Update()
    {
        if (!isActive) return;
        Manager_Game.Instance.ref_playermovement.MoveDirection_ExternalForce = MoveForce;
    }
}
