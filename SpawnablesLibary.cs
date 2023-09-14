using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnablesLibary : MonoBehaviour
{
    public List<GameObject> FX_CoinPickup;
    public List<GameObject> FX_Liquid1_Decal;
    public List<GameObject> FX_OnEnemyDeath;

    private Vector3 previousSpawnedPos_FX_Liquid1_Decal;

    public void Spawn_FX_CoinPickup(Vector3 _position)
    {
        foreach (var item in FX_CoinPickup)
        {
            if (item.activeSelf == false) { item.transform.position = _position; item.SetActive(true); break; }
        }
    }

    public void Spawn_FX_OnEnemyDeath(Vector3 _position)
    {
        foreach (var item in FX_OnEnemyDeath)
        {
            if (item.activeSelf == false) { item.transform.position = _position; item.SetActive(true); break; }
        }
    }

    public void Spawn_FX_Liquid1_Decal(Vector3 _position)
    {

        bool noneActive = true;
        foreach(var item in FX_Liquid1_Decal)
        {
            if (item.activeInHierarchy) noneActive = false;
        }
        if(noneActive == false)
        {
            if (Vector3.Distance(previousSpawnedPos_FX_Liquid1_Decal, _position) < 1) return;
        }

        foreach (var item in FX_Liquid1_Decal)
        {
            if (item.activeSelf == false) 
            {
                Vector3 spawnPos = _position + Vector3.up * 1.5f;
                item.transform.LookAt(Vector3.Lerp(_position, (Manager_Game.Instance.ref_playermovement.transform.position+Vector3.up*5f), 0.15f));
                item.transform.position = spawnPos;
                previousSpawnedPos_FX_Liquid1_Decal = _position;
                item.SetActive(true); 
                break; 
            }
        }
    }
}
