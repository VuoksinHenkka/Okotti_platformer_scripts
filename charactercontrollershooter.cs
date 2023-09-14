using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontrollershooter : MonoBehaviour
{
    [SerializeField] public ParticleSystem ourLiquidParticles;
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) ourLiquidParticles.Play();
        if (Input.GetButtonUp("Fire2")) ourLiquidParticles.Stop();
    }
}
