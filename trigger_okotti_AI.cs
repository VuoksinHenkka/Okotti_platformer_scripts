using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_okotti_AI : MonoBehaviour
{

    public ai_okotti ourAI;


    public void FoundHome()
    {
        this.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ourAI.currentstate == ai_okotti.State.Safe) return;

        if (other.CompareTag("Player"))
        {
            if (ourAI.currentstate == ai_okotti.State.Waiting)
            {
                ourAI.currentstate = ai_okotti.State.Following;
            }
        }

    }
}
