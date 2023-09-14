using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;


    public void UseCamera(CinemachineVirtualCamera toUse)
    {
        if (cameras.Contains(toUse))
        {
            cameras.Remove(toUse);
        }
        cameras.Insert(0, toUse);
        
        for (int i = 0; i < cameras.Count; i++)
        {
            if (i == 0) cameras[i].Priority = 100;
            else cameras[i].Priority = 100 - i;
        }
    }
}
