using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai_okotti : MonoBehaviour
{
    public enum State {Waiting, Following, Safe}
    public State currentstate = State.Waiting;
    public NavMeshAgent ourAgent;
    public Animator ourAnimator;
    private void Awake()
    {
        if (!ourAgent) ourAgent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
    }

    // Update is called once per frame
    void Update()
    {

        ourAnimator.SetFloat("Speed", ourAgent.velocity.magnitude);
        if (currentstate == State.Waiting)
        {
            return;
        }
        if (currentstate == State.Following)
        {
            ourAgent.SetDestination(Manager_Game.Instance.ref_playermovement.transform.position);
        }
        if (currentstate == State.Safe)
        {
            if(ourAgent.isStopped == false) ourAgent.Stop();
            return;
        }
    }

    public void FoundHome()
    {
        Manager_Game.Instance.ref_Level.Okot_Rescue();
        currentstate = State.Safe;
    }
}
