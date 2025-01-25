using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;     // Assign your loading screen panel
    public TextMeshProUGUI progressText;            // Text to display loading progress
    public TextMeshProUGUI tipText;                  // Text to show tips
    public float fakeLoadingDuration = 3f; // Fake loading time for UX

    private string[] tips = 
    {
        "Tip: Use cover to avoid enemy fire!",
        "Tip: Collect coins to unlock new characters.",
        "Tip: Press 'Jump' twice to double jump.",
        "Tip: Different weapons have different range and power.",
        "Tip: Explore hidden paths for bonus rewards."
    };

    // Call this method to load a scene with enhancements
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<CanvasGroup>().alpha = 0;
        loadingScreen.GetComponent<CanvasGroup>().DOFade(1, 0.7f);

        // Set random tip text
        tipText.text = tips[Random.Range(0, tips.Length)];
        

        // Start loading scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevent immediate activation

        float elapsed = 0f;

        // Simulate fake loading delay
        while (!operation.isDone)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress (0-1)

            // Smoothly animate progress bar
            progressText.text = (progress * 100).ToString("F0") + "%";

            // Fake loading to enhance UX
            if (progress >= 1f && elapsed >= fakeLoadingDuration)
            {
                operation.allowSceneActivation = true; // Activate the scene
            }

            yield return null; // Wait for the next frame
        }
        
       
        loadingScreen.GetComponent<CanvasGroup>().DOFade(0, 0.7f).OnComplete((delegate
        {
            loadingScreen.gameObject.SetActive(false);
        }));
    }
}
