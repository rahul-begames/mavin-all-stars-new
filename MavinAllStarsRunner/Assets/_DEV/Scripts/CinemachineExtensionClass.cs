using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public static class CinemachineExtensionClass 
{
    public static void SwitchCineCamera(CinemachineVirtualCamera cameraToDisable, CinemachineVirtualCamera cameraToEnable)
    {
        cameraToDisable.gameObject.SetActive(false);
        cameraToEnable.gameObject.SetActive(true);
    }
}
