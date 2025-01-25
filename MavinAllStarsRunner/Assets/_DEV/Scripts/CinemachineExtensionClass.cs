
using Cinemachine;


public static class CinemachineExtensionClass 
{
    public static void SwitchCineCamera(CinemachineVirtualCamera cameraToDisable, CinemachineVirtualCamera cameraToEnable)
    {
        cameraToDisable.gameObject.SetActive(false);
        cameraToEnable.gameObject.SetActive(true);
    }
}
