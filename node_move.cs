using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_move : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "icon_path.png", true);
    }
}
