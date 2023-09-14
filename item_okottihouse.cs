using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_okottihouse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Okotti"))
        {
            other.transform.parent.BroadcastMessage("FoundHome");
        }
    }
}
