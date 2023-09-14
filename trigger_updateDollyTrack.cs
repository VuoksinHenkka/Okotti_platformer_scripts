using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class trigger_updateDollyTrack : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public List<BoxCollider> ourColliders;

    void OnDrawGizmosSelected()
    {
        if (ourColliders.Count == 0) return;
        foreach(BoxCollider foundCollider in ourColliders)
        {
            // Draw a semitransparent red cube at the transforms position
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position + foundCollider.center, foundCollider.size);
        }

    }
    private void Awake()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Manager_Game.Instance.ref_VirtualCameraManager.UseCamera(virtualCamera);
        }
    }
}
