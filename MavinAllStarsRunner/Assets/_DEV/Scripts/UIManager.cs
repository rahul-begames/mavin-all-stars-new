
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Character & World Selection Panel")]
    public Transform CharWorldPanel;
    public TextMeshProUGUI characterNameTxt;
    public TextMeshProUGUI characterAbilityTxt;
    public TextMeshProUGUI characterAbilityDetailsTxt;
    public GameObject[] characterSelectionGO;
    public Transform AbilityDetailsPanel_Transform;
    public Button[] LevelSelectedBtn;
    
    
    
    [Header("SettingsPanel")] 
    public Transform SettingsPanel_Transform;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public Button hapticToggle;
    public Image hapticToggleImage;
    public TextMeshProUGUI hapticTXT;
    public Sprite onSprite;
    public Sprite offSprite;
    public GameObject pauseMenuGamePanel;
    public GameObject closeBtn;

    [Header("BoomboxPanel")] 
    public TextMeshProUGUI musicTXT;
    public TextMeshProUGUI artistTXT;
    public TextMeshProUGUI artist_musicTXT;
    public string[] artistName;
    public string[] musicName;
    public Transform musicBoomboxPanel;
    
    
    [Header("OriginalConfig")] 
    public TextMeshProUGUI mavinTXT;
    public TextMeshProUGUI healthTXT;
    public TextMeshProUGUI coinTXT;

    [Header("DOTweenUI Animations")] 
    public Transform[] LHSMenu;
    public Transform[] RHSMenu;
    public Transform[] TOPMenu;
    public Transform LHSGame;
    public Transform RHSGame;
    public Transform CentreGame;
    
    [Header("InGameUIConfig")]
    public TextMeshProUGUI mavinGameTXT;
    public TextMeshProUGUI healthGameTXT;
    public TextMeshProUGUI coinGameTXT;
    public TextMeshProUGUI topRunTXT;
    public TextMeshProUGUI currentDistanceTXT;
    public TextMeshProUGUI countdownTXT;
    
    private Manager _manager;
    private AudioManager _audioManager;

    [Header("Misc")]
    public Transform TapToPlayImageTransform;
    public Transform fortuneWheelWindow;
    
    [Header("All UI Panels")]
    public Transform MenuUIPanel;
    public Transform GameUIPanel;
    public Transform MainGameUIPanel;
    public Transform ReviveUIPanel;
    public Transform TopBGPanel;
    public Transform ObjectiveUIPanel;

    [Header("Revive Panel Dets")] 
    public TextMeshProUGUI reviveMultipliertxt;
    public Image crashScreen;
    public Button noThanksBtns;
    public Button getUpWithHeartsBtn;

    [Header("Vehicle Mode Variables")] 
    public Button VehicleModeBtn;
    public Image VehicleModeTimerImage;
    

    private void Start()
    {
        _manager = GameObject.Find("Manager").GetComponent<Manager>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
        SetInitialMusic();
        SetInitialMenu();
        SetInitialGameUIConfig();
        
    }

    


    void SetInitialCharacter()
    {
        
        // Disable all character selection GameObjects first
        foreach (GameObject go in characterSelectionGO)
        {
            go.SetActive(false);
        }

        // Set the selected character and update the UI
        _manager.selectedCharacter = _manager.mavinCharacters[_manager.selectedCharacterNumber];

        if (_manager.selectedCharacter != null)
        {
            characterSelectionGO[_manager.selectedCharacterNumber].SetActive(true);
            characterNameTxt.text = _manager.selectedCharacter.name;
            characterAbilityTxt.text = $"Special Moves: {_manager.abilityName[_manager.selectedCharacterNumber]}";
            characterAbilityDetailsTxt.text = _manager.abilityDetails[_manager.selectedCharacterNumber];
            Debug.Log($"Selected Character: {_manager.selectedCharacter.name} (Index: {_manager.selectedCharacterNumber})");
        }
        else
        {
            Debug.LogError($"No character found at index {_manager.selectedCharacterNumber}.");
            return;
            
        }
    }

    void SetInitialMusic()
    {
        
        // Set the new music clip and update the UI text
        _audioManager.musicAudioSource.clip = _audioManager.artistMusic[_manager.selectedCharacterNumber];

        if (_audioManager.musicAudioSource.clip != null)
        {
            _audioManager.musicAudioSource.Play(); // Play the newly set clip
        }
        else
        {
            return;
        }

        // Update the UI text with the new track's name
        musicTXT.text = musicName[_manager.selectedCharacterNumber];
        artistTXT.text = artistName[_manager.selectedCharacterNumber];
        artist_musicTXT.text = _audioManager.musicAudioSource.clip.name;
    }

    void SetInitialGameUIConfig()
    {
        //TODO : setup values using player prefs
        mavinGameTXT.text = 0.ToString();
        healthGameTXT.text = 0.ToString();
        coinGameTXT.text = 0.ToString();
        topRunTXT.text = 0.ToString();
        currentDistanceTXT.text = 0.ToString();
        
    }
    
    //Unity Methods

    public void SwitchCharacter(int buttonDirection) 
    {
        // Define characterCount to get the total number of characters
        int characterCount = _manager.mavinCharacters.Length;

        // Adjust the current character index based on button direction
        if (buttonDirection == -1)
        {
            // Move to the previous character
            _manager.selectedCharacterNumber = (_manager.selectedCharacterNumber - 1 + characterCount) % characterCount;
        }
        else if (buttonDirection == 1)
        {
            // Move to the next character
            _manager.selectedCharacterNumber = (_manager.selectedCharacterNumber + 1) % characterCount;
        }
        else
        {
            Debug.LogWarning("Invalid buttonDirection. Expected -1 or 1.");
            return;
        }
        
        // Disable all character selection GameObjects first
        foreach (GameObject go in characterSelectionGO)
        {
            go.SetActive(false);
        }

        // Set the selected character and update the UI
        _manager.selectedCharacter = _manager.mavinCharacters[_manager.selectedCharacterNumber];

        if (_manager.selectedCharacter != null)
        {
            characterSelectionGO[_manager.selectedCharacterNumber].SetActive(true);
            characterNameTxt.text = _manager.selectedCharacter.name;
            characterAbilityTxt.text = $"Special Moves: {_manager.abilityName[_manager.selectedCharacterNumber]}";
            characterAbilityDetailsTxt.text = _manager.abilityDetails[_manager.selectedCharacterNumber];
            Debug.Log($"Selected Character: {_manager.selectedCharacter.name} (Index: {_manager.selectedCharacterNumber})");
        }
        else
        {
            Debug.LogError($"No character found at index {_manager.selectedCharacterNumber}.");
            return;
        }
    }
    
    public void SwitchMusic(int buttonDirection)
    {
        // Define trackCount to get the total number of tracks
        int trackCount = _audioManager.artistMusic.Length;
       
       
        // Adjust the temporary variable based on button direction
        if (buttonDirection == -1)
        {
            // Move to the previous track
            _manager.selectedCharacterNumber = ( _manager.selectedCharacterNumber - 1 + trackCount) % trackCount;
          
        }
        else if (buttonDirection == 1)
        {
            // Move to the next track
            _manager.selectedCharacterNumber = ( _manager.selectedCharacterNumber + 1) % trackCount;
          
        }
        else
        {
            Debug.LogWarning("Invalid buttonDirection. Expected -1 or 1.");
            return;
        }

        // Set the new music clip and update the UI text
        _audioManager.musicAudioSource.clip = _audioManager.artistMusic[_manager.selectedCharacterNumber];

        if (_audioManager.musicAudioSource.clip != null)
        {
            _audioManager.musicAudioSource.Play(); // Play the newly set clip
        }
        else
        {
            return;
        }

        // Update the UI text with the new track's name
        musicTXT.text = musicName[_manager.selectedCharacterNumber];
        artistTXT.text = artistName[_manager.selectedCharacterNumber];
        artist_musicTXT.text = _audioManager.musicAudioSource.clip.name;
       
    }
    
    public void ToggleAbilityDetailsPanel()
    {
        // Check if the panel is currently active and toggle accordingly
        bool isActive = AbilityDetailsPanel_Transform.gameObject.activeSelf;
        AbilityDetailsPanel_Transform.gameObject.SetActive(!isActive);
    }
    
    public void OpenCharacterPanel()
    {
        DoTweenUtilityClass.OpenPanelDT(CharWorldPanel);
        SetInitialCharacter();
        InitializeSelectedLevelUI();
        
    }

    public void CloseCharacterPanel()
    {
        DoTweenUtilityClass.ClosePanelDT(CharWorldPanel);
        //TODO : turn of character image and change character and levels in real scene
        
        // Disable all character selection ui GameObjects first
        _manager.SwitchCharacter();
        _manager.SwitchLevel();
        
        
    }

    public void OpenSettingsPanel()
    {
        DoTweenUtilityClass.OpenPanel2DT(SettingsPanel_Transform);
    }
    
    public void CloseSettingsPanel()
    {
        DoTweenUtilityClass.ClosePanelDT(SettingsPanel_Transform);
        
        PlayerPrefs.SetFloat("MusicValue",musicVolumeSlider.value);
        
        foreach (var sfxAs in  _audioManager.soundAudioSource)
        {
            PlayerPrefs.SetFloat("SFXValue",soundVolumeSlider.value);
        }
       
        PlayerPrefs.SetInt("IsHapticOn", hapticTXT.text == "ON" ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void OpenMusicPanel()
    {
        DoTweenUtilityClass.OpenPanel2DT(musicBoomboxPanel);
    }
    
    public void CloseMusicsPanel()
    {
        DoTweenUtilityClass.ClosePanelDT(musicBoomboxPanel);
    }

    public void ToggleHaptic()
    {
        // Toggle the haptic state
        int currentState = PlayerPrefs.GetInt("IsHapticOn", 1); // Default to 1 (ON) if not set
        int newState = currentState == 1 ? 0 : 1;

        // Save the new state to PlayerPrefs
        PlayerPrefs.SetInt("IsHapticOn", newState);
        PlayerPrefs.Save();

        // Update UI text based on the new state
        hapticTXT.text = newState == 1 ? "ON" : "OFF";

        // Update the toggle image based on the state
        hapticToggleImage.sprite = newState == 1 ? onSprite : offSprite;

        Debug.Log("Haptic toggled: " + (newState == 1 ? "ON" : "OFF"));
    }
    
    public void OpenFortuneWheel()
    {
        DoTweenUtilityClass.OpenPanel2DT(fortuneWheelWindow);
    }
    
    public void CloseFortuneWheel()
    {
        DoTweenUtilityClass.ClosePanelDT(fortuneWheelWindow);
        
    }
    
    void SetInitialMenu()
    {
        CharWorldPanel.gameObject.SetActive(false);
        pauseMenuGamePanel.SetActive(false);
        closeBtn.SetActive(true);
        GameUIPanel.gameObject.SetActive(false);
        MenuUIPanel.gameObject.SetActive(true);
        TopBGPanel.gameObject.SetActive(true);
        ObjectiveUIPanel.gameObject.SetActive(false);
        //menu ui initial setup to left
        foreach (Transform menu in LHSMenu)
        {
            menu.DOMoveX(-3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in RHSMenu)
        {
            menu.DOMoveX(3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in TOPMenu)
        {
            menu.DOMoveY(3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in LHSMenu)
        {
            
            menu.DOMoveX(3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in RHSMenu)
        {
            menu.DOMoveX(-3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in TOPMenu)
        {
            menu.DOMoveY(-3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        
        ObjectiveUIPanel.DOMoveX(-3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        
        
        
        crashScreen.DOFade(0f, 0f);
    }

    public void RemoveMenuUIAddGameUI()
    {
        
        LHSGame.DOMoveX(-3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        RHSGame.DOMoveX(3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        CentreGame.DOMoveY(3000, 0f).SetEase(Ease.InOutQuad).SetRelative(true);
        
        foreach (Transform menu in LHSMenu)
        {
            menu.DOMoveX(-3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in RHSMenu)
        {
            menu.DOMoveX(3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        }
        foreach (Transform menu in TOPMenu)
        {
            menu.DOMoveY(3000, 0.8f)
                .SetEase(Ease.InOutQuad)
                .SetRelative(true)
                .OnComplete(() => 
                {
                    MenuUIPanel.gameObject.SetActive(false);
                    TopBGPanel.gameObject.SetActive(false);
                });
        }
    }

    public void GameUIDTSetup()
    {
      
        ObjectiveUIPanel.DOMoveX(3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true).OnComplete(() => 
        {
            ObjectiveUIPanel.gameObject.SetActive(false);
            GameUIPanel.gameObject.SetActive(true);
            MainGameUIPanel.gameObject.SetActive(true);
            
            LHSGame.DOMoveX(3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
            RHSGame.DOMoveX(-3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
            CentreGame.DOMoveY(-3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        });
        
        
    }
    
    public void InitializeSelectedLevelUI()
    {
        //TODO : add haptic feedback when selected, lock icon setup, prices and everything write and setup, feedback when level unlocked
        // Loop through all the buttons and particle systems
        for (int i = 0; i < LevelSelectedBtn.Length; i++)
        {
            if (i == _manager.selectedLevelNumber)
            {
                LevelSelectedBtn[i].interactable = false; // Disable the initially selected button
                
            }
            else
            {
                LevelSelectedBtn[i].interactable = true; // Enable other buttons
                
            }
        }
        
    }
    
    // Call this method to activate a specific level
    public void SelectLevel(int levelIndex)
    {
        // Loop through all the buttons and particle systems
        for (int i = 0; i < LevelSelectedBtn.Length; i++)
        {
            if (i == levelIndex)
            {
                LevelSelectedBtn[i].interactable = false; // Disable the selected button
                
            }
            else
            {
                LevelSelectedBtn[i].interactable = true; // Enable other buttons
                
            }
        }

        // Store the selected level number
       _manager.selectedLevelNumber = levelIndex;
    }
    
    public void PauseGame()
    {
        
        _manager.bIsPlayPressed = false;
        SettingsPanel_Transform.gameObject.SetActive(true);
       
        SettingsPanel_Transform.GetComponent<CanvasGroup>().alpha = 0;
        SettingsPanel_Transform.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f,-1000f,0f);
        SettingsPanel_Transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 1f, false).
            SetEase(Ease.OutElastic).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
        SettingsPanel_Transform.GetComponent<CanvasGroup>().DOFade(1, 0.4f);

        /*if (_audioFiles.SpecialVehicleSFX.isPlaying)
            _audioFiles.SpecialVehicleSFX.volume = 0;*/
    }
    
    public void UnPauseGame()
    {
        
        StartCoroutine(StartUnPauseCountdown());
        
        
    }
    
    
    private IEnumerator StartUnPauseCountdown()
    {
        SettingsPanel_Transform.gameObject.SetActive(true);
       
        SettingsPanel_Transform.GetComponent<CanvasGroup>().alpha = 1;
        SettingsPanel_Transform.GetComponent<RectTransform>().transform.localPosition = new Vector3(0f,0f,0f);
        SettingsPanel_Transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 1f, false)
            .SetEase(Ease.InOutQuint).SetUpdate(true).OnComplete((delegate
            {
                SettingsPanel_Transform.gameObject.SetActive(false);
            }));;
        SettingsPanel_Transform.GetComponent<CanvasGroup>().DOFade(0, 0.7f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.2f);

        for (int i = 3; i > 0; i--)
        {
            countdownTXT.text = i.ToString();
            yield return new WaitForSecondsRealtime(0.5f);
        }

        countdownTXT.text = "";
        Time.timeScale = 1;
        _manager.bIsPlayPressed = true;
        
        /*if (_audioFiles.SpecialVehicleSFX.volume == 0)
            _audioFiles.SpecialVehicleSFX.volume = 0.4f;*/
        
        #region MusicRegion
        
        PlayerPrefs.SetFloat("MusicValue",musicVolumeSlider.value);
            
        foreach (var sfxAs in  _audioManager.soundAudioSource)
        {
            PlayerPrefs.SetFloat("SFXValue",soundVolumeSlider.value);
        }
           
        PlayerPrefs.SetInt("IsHapticOn", hapticTXT.text == "ON" ? 1 : 0);
        PlayerPrefs.Save();
            
        #endregion
    }

    
}
