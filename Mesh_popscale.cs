using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_popscale : MonoBehaviour
{
    public Vector3 ScalePopAdd = Vector3.zero;
    private Vector3 OriginalScale = Vector3.one;
    private float lerpvalue = 0;
    public float popspeed = 4;
    private void Awake()
    {
        OriginalScale = transform.localScale;
    }


    public void Popscale()
    {
        print("popscale");
        lerpvalue = 1.1f;
    }
    private void Update()
    {
        if (lerpvalue > 0)
        {
            transform.localScale = Vector3.Lerp(OriginalScale, ScalePopAdd, lerpvalue);
            lerpvalue -= popspeed * Time.deltaTime;
        }
        else if (transform.localScale != OriginalScale ) { transform.localScale = OriginalScale; }
    }
}
