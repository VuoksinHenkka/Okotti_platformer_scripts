//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using Unity.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Decal_Liquid : MonoBehaviour
{

    public DecalProjector projector;
    public float lifetime = 0;

    private void OnEnable()
    {
        lifetime = Random.Range(1,4);
    }
    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled == false) return;
        if (lifetime > 0 ) { lifetime -= 1 * Time.deltaTime; }
        else gameObject.SetActive(false);
    }
}
