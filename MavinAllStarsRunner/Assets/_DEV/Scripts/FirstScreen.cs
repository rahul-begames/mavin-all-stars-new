using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstScreen : MonoBehaviour
{
     [SerializeField] private Slider loadingFillImage;
    [SerializeField] private TextMeshProUGUI loadingFillImageText;
    [SerializeField] private TextMeshProUGUI randomText_txt;
    private float previousValue;
    private AsyncOperation sceneLoadingOperation;
    private bool loadingComplete = false;

    // Array of random text options
    private readonly string[] randomTexts = { 
        "Loading records...", 
        "Gathering Fans...", 
        "Getting things ready...", 
        "Loading new music...", 
        "Hang tight, loading..." 
    };

    private void Start()
    {
        previousValue = loadingFillImage.value;

        // Start loading the scene in the background but don't activate it yet
        sceneLoadingOperation = SceneManager.LoadSceneAsync("GameScene");
        sceneLoadingOperation.allowSceneActivation = false;  // Prevent scene activation until we are ready

        // Simulate loading progress using DOTween
        DOTween.To(() => loadingFillImage.value, x => loadingFillImage.value = x, 100, 4f)
            .SetEase(Ease.InOutSine)
            .OnComplete(OnLoadingComplete);

        // Start the coroutine to change random text periodically
        StartCoroutine(ChangeRandomText());

        // Initial UI update
        UpdateLoadingText();
    }

    private void Update()
    {
        // Only update UI when the value changes to avoid unnecessary updates
        if (loadingFillImage.value != previousValue)
        {
            UpdateLoadingText();
            previousValue = loadingFillImage.value;
        }
    }

    private void UpdateLoadingText()
    {
        loadingFillImageText.text = Mathf.FloorToInt(loadingFillImage.value).ToString() + "%";
    }

    // Coroutine to change the random text periodically
    private IEnumerator ChangeRandomText()
    {
        while (!loadingComplete)
        {
            // Set a random text from the array
            randomText_txt.text = randomTexts[UnityEngine.Random.Range(0, randomTexts.Length)];

            // Wait for a random duration between 1 and 2 seconds before changing again
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        }
    }

    // Called when the loading progress reaches 100%
    private void OnLoadingComplete()
    {
        loadingComplete = true;  // Stop random text changes

        // Update text to show "Welcome!"
        randomText_txt.text = "Welcome to Mavin Records!";
        loadingFillImageText.text = "100%";

        // Wait for a short delay before activating the scene
        StartCoroutine(ActivateSceneAfterDelay());
    }

    private IEnumerator ActivateSceneAfterDelay()
    {
        // Optional: Add a short delay before activating the scene (e.g., 1 second)
        yield return new WaitForSeconds(1f);

        // Activate the scene
        sceneLoadingOperation.allowSceneActivation = true;
    }
}
