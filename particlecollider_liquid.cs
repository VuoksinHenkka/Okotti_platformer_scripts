using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlecollider_liquid : MonoBehaviour
{
    public ParticleSystem ourParticlesystem;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Awake()
    {
        if (!ourParticlesystem) GetComponent(typeof(ParticleSystem));
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy")) other.BroadcastMessage("Receive", Receivables.receivetype.liquid, SendMessageOptions.DontRequireReceiver);

        int numCollisionEvents = ourParticlesystem.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            if (i % 5 == 0) SpawnDecal(collisionEvents[i].intersection);
            i++;
        }
    }

    public void SpawnDecal(Vector3 worldPosition)
    {
        Manager_Game.Instance.ref_SpawnablesLibrary.Spawn_FX_Liquid1_Decal(worldPosition);
    }
}
