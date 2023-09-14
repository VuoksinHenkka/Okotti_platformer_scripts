using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_transform : MonoBehaviour
{
    private float movespeed = 10;
    // Update is called once per frame


    void Update() //transform liikkuminen
    {
        transform.Translate((new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")))* movespeed * Time.deltaTime, Space.World);    
    }
}
