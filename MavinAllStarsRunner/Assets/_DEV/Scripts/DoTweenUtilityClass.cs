
using DG.Tweening;


using UnityEngine;


public static class DoTweenUtilityClass 
{
    public static void OpenPanelDT(Transform PanelToOpen)
    {
        PanelToOpen.gameObject.SetActive(true);
       
        PanelToOpen.GetComponent<CanvasGroup>().alpha = 0;
        PanelToOpen.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f,-1000f,0f);
        PanelToOpen.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 1f, false).SetEase(Ease.OutElastic);
        PanelToOpen.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    }
    
    public static void OpenPanel2DT(Transform PanelToOpen)
    {
        PanelToOpen.gameObject.SetActive(true);
       
        PanelToOpen.GetComponent<CanvasGroup>().alpha = 0;
        /*PanelToOpen.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f,-1000f,0f);
        PanelToOpen.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 1f, false).SetEase(Ease.OutElastic);*/
        PanelToOpen.GetComponent<CanvasGroup>().DOFade(1, 0.7f);
    }
    
    public static void ClosePanelDT(Transform PanelToClose)
    {
        PanelToClose.gameObject.SetActive(true);
       
        PanelToClose.GetComponent<CanvasGroup>().alpha = 1;
        PanelToClose.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f,0f,0f);
        PanelToClose.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 1f, false).SetEase(Ease.InOutQuint);
        PanelToClose.GetComponent<CanvasGroup>().DOFade(0, 0.7f).OnComplete((delegate
        {
            PanelToClose.gameObject.SetActive(false);
        }));
    }

    public static void ScreenBounce(float CameraFOV)
    {
        
    }

    
}
