using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class item_coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) PickUp();
    }


    public void PickUp()
    {
        Manager_Game.Instance.Coin_PickedUp();
        Manager_Game.Instance.ref_SpawnablesLibrary.Spawn_FX_CoinPickup(transform.position);
        Manager_Game.Instance.ref_audioengine.Play_Pickupcoin();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Manager_Game.Instance.ScoreLeft++;
    }
}
