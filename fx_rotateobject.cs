using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_rotateobject : MonoBehaviour
{
    public Vector3 Rotate_Direction = Vector3.left;
    public float Speed = 1f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Rotate_Direction, Speed*Time.deltaTime);
    }
}
