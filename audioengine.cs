using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;
using FMOD;
using FMOD.Studio;

public class audioengine : MonoBehaviour
{
    private float pickup_pitch = 1.0f;
    public EventReference Sound_Pickup;

    public EventInstance Music;
    public EventReference Song1;


    private void Start()
    {
        Music = RuntimeManager.CreateInstance(Song1);

        Music.start();
    }

    private void OnDestroy()
    {
        Music.release();
    }

    public void Play_Pickupcoin()
    {
        RuntimeManager.PlayOneShot(Sound_Pickup);
        pickup_pitch += 0.25f; 
    }

    private void Update() 
    {
        if (Manager_Game.Instance.currentGameState != Manager_Game.State.Action) Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (pickup_pitch != 1.0f) { pickup_pitch = Mathf.Clamp(pickup_pitch -= 1 * Time.deltaTime, 1, 3); }
    }
}
