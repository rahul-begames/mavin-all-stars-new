﻿
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR

#endif

public class Manager : MonoBehaviour {
    
    
    public float timeFromStartToPlay = 1.54f;

    [Header("Pursuer Settings")]
    public string pursuerGameObjName;
    public float pursuitTime;
    [HideInInspector]
    public float pursuitTimeLoc;
    [HideInInspector]
    public GameObject pursuer;
    

    [Header("Game Config")] 
    public float currentDistanceCovered;

    [Header("Bool Variable")] 
    public bool bIsPlayPressed;
    public bool bIsVehicleModeActive;
    public bool bIsVehicleModeRunning;

    [Header("ParticleSystems")] 
    public ParticleSystem VehicleModeTransitionPS;
    public ParticleSystem OtherTransitionPS;

    [Header("PlayerAnimation")] 
    public Animator charAnimator;
    public Avatar[] charAvatars;
    

    [Header("LevelDets")]
    public GameObject[] mavinCharacters;
    public string[] abilityName;
    public string[] abilityDetails;
    public GameObject selectedCharacter;
    [HideInInspector]public int selectedCharacterNumber;
    public int selectedLevelNumber;
    public GameObject[] levelGo;
    public GameObject[] menuBGlevelGo;
    public List<GameObject> levels = new List<GameObject>();
    public GameObject menuLevel;
    
    public Player player;
    

    protected Vector3 cameraPos;
    protected Quaternion cameraRot;
    

    [HideInInspector]
    public bool cameraLerp;

    [HideInInspector]
    public float startPlayerSpeed;

    [Header("CarModeVariables")] 
    public GameObject[] allVehicles;
    public int selectedVehicle;

    [Header("CinemachineCameras")] 
    public CinemachineVirtualCamera DefaultCam_CM;
    public CinemachineVirtualCamera DeathCam_CM;
    public CinemachineVirtualCamera SlideCam_CM;
    public CinemachineVirtualCamera PreRunCam_CM;
    public CinemachineVirtualCamera CarModeCam_CM;

    public UIManager uiManager;
    public AudioManager audioManager;
    public LoadingScreenManager loadingScreenManager;
    /// <summary>
	/// Deactivate all active levels on scene
	/// </summary>
    protected virtual void Awake()
    {
        
#if UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = 45;
#endif

        if (!uiManager) return;
        
        Level[] levelsLoc = GameObject.FindObjectsOfType<Level>();

        for (int i = 0; i < levelsLoc.Length; i++)
        {
            levelsLoc[i].transform.parent.gameObject.SetActive(false);
        }
    }

    

    protected virtual void Start ()
    {
        if (audioManager.musicAudioSource != null)
        {
            audioManager.musicAudioSource.ignoreListenerVolume = true;
            audioManager.musicAudioSource.Play();
        }
        
        //TODO : change thse back
        PlayerPrefs.SetInt("MavinValue", 100);
        PlayerPrefs.SetInt("CoinValue", 100);
        PlayerPrefs.SetInt("HealthValue", 100);

        TapToPlayDT(uiManager.TapToPlayImageTransform);
        
        
        SetInitialUIValues();
        
        
        SetCharacter();
        SetLevel();

        CreateMenuScene();

        SetInitialCarMode();


    }
    
    

    private void DelayedRotationCall()
    {
        TapToPlayDT(uiManager.TapToPlayImageTransform);
    }
    

    void SetInitialUIValues()
    {
        #region AudioValuesSetup

            if (PlayerPrefs.HasKey("MusicValue"))
            {
                audioManager.musicAudioSource.volume = PlayerPrefs.GetFloat("MusicValue");
                uiManager.musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicValue");   
            }
            if (PlayerPrefs.HasKey("SFXValue"))
            {
                foreach (var sfxAs in audioManager.soundAudioSource)
                {
                    sfxAs.volume = PlayerPrefs.GetFloat("SFXValue");
                }
                uiManager.soundVolumeSlider.value = PlayerPrefs.GetFloat("SFXValue");            
            }
            if (PlayerPrefs.HasKey("IsHapticOn"))
            {
                uiManager.hapticTXT.text = PlayerPrefs.GetInt("IsHapticOn") == 1 ? "ON" : "OFF";
                uiManager.hapticToggleImage.sprite = PlayerPrefs.GetInt("IsHapticOn") == 1 ? uiManager.onSprite : uiManager.offSprite;
            }
            
            
        #endregion

        #region RewardValuesSetup

            if (PlayerPrefs.HasKey("MavinValue"))
            {
                uiManager.mavinTXT.text = PlayerPrefs.GetInt("MavinValue").ToString();
                              
            }
            if (PlayerPrefs.HasKey("CoinValue"))
            {
                uiManager.coinTXT.text = PlayerPrefs.GetInt("CoinValue").ToString();
                              
            }
            if (PlayerPrefs.HasKey("HealthValue"))
            {
                uiManager.healthTXT.text = PlayerPrefs.GetInt("HealthValue").ToString();
                              
            }

        #endregion
        
    }
    
    
    

    public void SetCharacter()
    {
        
        //disable all characters
        for (int i = 0; i < mavinCharacters.Length; i++)
        {
            mavinCharacters[i].SetActive(false);
        }

        //setup player prefs
        
        selectedCharacterNumber = PlayerPrefs.GetInt("CurrentStar");
        selectedCharacter = mavinCharacters[selectedCharacterNumber];
        selectedCharacter.SetActive(true);
        
        //set character avatar
        charAnimator.avatar = charAvatars[selectedCharacterNumber];

    }

    void SetLevel()
    {
        selectedLevelNumber = PlayerPrefs.GetInt("CurrentLevel");
        
        levels.Clear(); 
        
        

        // Get all child objects of the selected level parent
        Transform selectedLevelParent = levelGo[selectedLevelNumber].transform;

        foreach (Transform child in selectedLevelParent)
        {
            levels.Add(child.gameObject); // Add each child GameObject to the list
        }
        
    }


    void SetInitialCarMode()
    {
        foreach (var vehicle in allVehicles)
        {
            vehicle.SetActive(false);
        }
    }
   

    public virtual void Play()
    {

        //Stop Music
        audioManager.musicAudioSource.Stop();
        // PLay Cassette AS
        audioManager.soundAudioSource[1].Play();
        
        uiManager.ObjectiveUIPanel.gameObject.SetActive(true);
        uiManager.ObjectiveUIPanel.DOMoveX(3000, 0.8f).SetEase(Ease.InOutQuad).SetRelative(true);
        
        uiManager.pauseMenuGamePanel.SetActive(true);
        uiManager.closeBtn.SetActive(false);

        
        StartCoroutine(DelayPlayGame());
        
        
        uiManager.RemoveMenuUIAddGameUI();
        
       // GamePanels.SetActive(true);
       
       
    }

    IEnumerator DelayPlayGame()
    {
        yield return new WaitForSeconds(4f);
        
        //Play Music
        //change music
        audioManager.musicAudioSource.clip = audioManager.artistMusic[selectedCharacterNumber];
        audioManager.musicAudioSource.Play();
        
        uiManager.GameUIDTSetup();
        
        pursuitTimeLoc = pursuitTime;
        bIsPlayPressed = true;

        StartCoroutine(DistanceCounterSystem());
        
        
        Invoke("StopCameraLerp", 0.05f);
        cameraLerp = true;
    }
    
    private IEnumerator DistanceCounterSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (bIsPlayPressed)
            {
                currentDistanceCovered++;
                uiManager.currentDistanceTXT.text = currentDistanceCovered.ToString() + " m ";


                if (currentDistanceCovered > PlayerPrefs.GetFloat("TopRun", 0))
                {
                    uiManager.topRunTXT.text = currentDistanceCovered.ToString() + " m ";
                    PlayerPrefs.SetFloat("TopRun", currentDistanceCovered);
                }
            }
        }
    }

    private void Update()
    {
        DataUpdate();
        
    }
    

    protected virtual void StopCameraLerp()
    {
        cameraLerp = false;
        
        //Switch To Default Camera
        if (DefaultCam_CM != null)
            if (PreRunCam_CM != null)
                CinemachineExtensionClass.SwitchCineCamera(PreRunCam_CM,DefaultCam_CM);
        //cameraTransform.rotation = Quaternion.Euler(player.cameraRotation);
    }

    public virtual void Stop()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        
    }
    

    protected virtual void CreateMenuScene()
    {
        
        SwitchMenuBGLevel();
        
        //Switch To PreRun Camera
        if (DefaultCam_CM != null)
                if (PreRunCam_CM != null)
                    CinemachineExtensionClass.SwitchCineCamera(DefaultCam_CM,PreRunCam_CM);

        //player = Instantiate(characterSkins[PlayerPrefs.GetInt("CharacterSkin")], menuLvl.transform.Find("StartPosition").position, Quaternion.identity).GetComponent<Player>();
        startPlayerSpeed = player.speed;
        /*pursuer = menuLvl.transform.Find(pursuerGameObjName).gameObject;
        pursuer.transform.SetParent(transform.parent);
        pursuerTransform = pursuer.transform;*/

        //cameraTransform.position = cameraPos;
        //cameraTransform.rotation = cameraRot;
    }

   

    public void DataUpdate()
    {
        audioManager.musicAudioSource.volume = uiManager.musicVolumeSlider.value;
        foreach (var sfxAs in  audioManager.soundAudioSource)
        {
            sfxAs.volume = uiManager.soundVolumeSlider.value;
        }
        uiManager.hapticTXT.text = PlayerPrefs.GetInt("IsHapticOn") == 1 ? "ON" : "OFF";
        
        
        #region RewardValuesSetup

        if (PlayerPrefs.HasKey("MavinValue"))
        {
            uiManager.mavinTXT.text = PlayerPrefs.GetInt("MavinValue").ToString();
                              
        }
        if (PlayerPrefs.HasKey("CoinValue"))
        {
            uiManager.coinTXT.text = PlayerPrefs.GetInt("CoinValue").ToString();
                              
        }
        if (PlayerPrefs.HasKey("HealthValue"))
        {
            uiManager.healthTXT.text = PlayerPrefs.GetInt("HealthValue").ToString();
                              
        }

        #endregion
        
        PlayerPrefs.Save();
        
    }

    public void SwitchLevel()
    {
        levels.Clear(); 

        // Get all child objects of the selected level parent
        Transform selectedLevelParent = levelGo[selectedLevelNumber].transform;

        foreach (Transform child in selectedLevelParent)
        {
            levels.Add(child.gameObject); // Add each child GameObject to the list
        }
        
        PlayerPrefs.SetInt("CurrentLevel",selectedLevelNumber);
    }

    public void SwitchMenuBGLevel()
    {
        menuLevel.SetActive(true);
        // Disable all menu backgrounds
        foreach (GameObject bg in menuBGlevelGo)
        {
            bg.SetActive(false);
        }
        // Enable the selected level background
        menuBGlevelGo[selectedLevelNumber].SetActive(true);
    }

    public void SwitchCharacter()
    {
        foreach (GameObject go in uiManager.characterSelectionGO)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in mavinCharacters)
        {
            go.SetActive(false);
        }
        selectedCharacter.SetActive(true);
        
        //change character number player prefs
        PlayerPrefs.SetInt("CurrentStar", selectedCharacterNumber);
        
        //change animator avatar
        charAnimator.avatar = charAvatars[selectedCharacterNumber];
        
        

        //change speech character
    }
    
    public void TapToPlayDT(Transform targetImage)
    {
        // Rotate the image from -30 to 30 on Z-axis and back, looping with a delay
        targetImage.GetComponent<RectTransform>()
            .DORotate(new Vector3(0, 0, 7f), 0.3f)
            .SetEase(Ease.Linear)
            .SetLoops(1, LoopType.Yoyo)
            .SetRelative(false)
            .OnComplete(() =>
            {
                targetImage.GetComponent<RectTransform>()
                    .DORotate(new Vector3(0, 0, -7f), 0.3f)
                    .SetEase(Ease.OutBounce)
                    .SetLoops(1, LoopType.Yoyo)
                    .SetRelative(false).OnComplete(() =>
                    {
                        targetImage.GetComponent<RectTransform>()
                            .DORotate(new Vector3(0, 0, 0f), 0.3f)
                            .SetEase(Ease.Linear)
                            .SetLoops(1, LoopType.Yoyo)
                            .SetRelative(false);
                    });
            });

        // Use Invoke with a string method reference and delay
        Invoke(nameof(DelayedRotationCall), 5f);
    }

    public void ResumeGame()
    {
        
    }

    public void RestartGame()
    {
        if(loadingScreenManager)
            loadingScreenManager.LoadScene("GameScene");
    }

    public void ReturnToMenu()
    {
        
    }

    public void Crashed()
    {
        //default values
        uiManager.noThanksBtns.GetComponent<CanvasGroup>().DOFade(0, 0f);
        uiManager.noThanksBtns.interactable = false;
        
        //TODO : haptic feedback
        //GameManager.Instance.VibrateOnce(0.2f,0.4f);

        
        //TODO : Crash SFX
       
        
        //pause music
        if (audioManager.musicAudioSource.isPlaying)
        {
            audioManager.musicAudioSource.Pause();
        }

        
        if (DefaultCam_CM != null)
            if (PreRunCam_CM != null)
                CinemachineExtensionClass.SwitchCineCamera(DefaultCam_CM,DeathCam_CM);
        
        
        
        //condition for busted and revive + in detail for both
        uiManager.ReviveUIPanel.gameObject.SetActive(true);
       
        uiManager.ReviveUIPanel.GetComponent<CanvasGroup>().alpha = 0;
        uiManager.ReviveUIPanel.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(1f);
        
        uiManager.reviveMultipliertxt.text = PlayerPrefs.GetInt("HealthValue")+1.ToString();
        

       Invoke(nameof(ReviveTimerAfterCrashed),10f);
       
       
       if (PlayerPrefs.GetInt("HealthValue") >= 1)
        {
            uiManager.getUpWithHeartsBtn.gameObject.SetActive(true);
        }
        else
        {
            uiManager.getUpWithHeartsBtn.gameObject.SetActive(false);
        }
            
        
        
        //TODO : screen shake

        //TODO : red screen
        uiManager.crashScreen.DOFade(1f, 1f);
        
        
        Invoke(nameof(DisableFromCrashMethod), 1f);
        
        
    }

    void ReviveTimerAfterCrashed()
    {
        uiManager.noThanksBtns.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        uiManager.noThanksBtns.interactable = true;
    }

    void DisableFromCrashMethod()
    {
        //TODO : screen shake off
        
        
        //TODO : red screen
        uiManager.crashScreen.DOFade(0f, 3f).SetDelay(1f);
        
    }

    #region RevivePanel Buttons

    public void ContinuePostCrash(bool withAds)
    {
        if (withAds)
        {
            //TODO : Ad setup and then revive
        }
        else
        {
            //TODO : revive and reduce one heart
            if (PlayerPrefs.GetInt("AllLives", 3) > 0)
                PlayerPrefs.SetInt("AllLives", PlayerPrefs.GetInt("AllLives", 3) - 1);

            Time.timeScale = 1;
            uiManager.ReviveUIPanel.gameObject.SetActive(false);

            /*yield return StartCoroutine(ReviveCountdown());
            yield return new WaitForSeconds(0.5f);

            levelParticleEffects.highSmoke.Play();
            levelParticleEffects.immunityEffect.Play();

            _endlessRunnerCameras._CineMachines.deathCam.gameObject.SetActive(false);
            _endlessRunnerCameras._CineMachines.mainCam.gameObject.SetActive(true);

            currentCollider.SetActive(false);
            _uiManager._ingameUIParentGO.SetActive(true);

            yield return new WaitForSeconds(0.6f);

            isGameOver = false;
            isGameStarted = true;

            if (isNormal)
                selectedPlayer.GetComponent<Animator>().SetInteger("moveValue", 1);

            _playerManager.transform.GetComponent<Rigidbody>().isKinematic = false;
            _audioFiles.MusicAS.Play();
            _playerManager.GetComponent<Rigidbody>().constraints =
                ~RigidbodyConstraints.FreezeAll | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;

            yield return new WaitForSeconds(2f);

            currentCollider.SetActive(true);
            levelParticleEffects.immunityEffect.Stop();

            isGetUpPressed = false;
            immunityON = false;*/
        }
    }


    public void NoThanksButton()
    {
        //TODO : exit to menu but save all dets
    }

    #endregion

    public void CarModeActive()
    {
        //TODO : check if ability counter is above a certain level

        StartCoroutine(ActivateCarMode());
    }

    IEnumerator ActivateCarMode()
    {
        //seperate swipe mode activate for it or phone gesture
        bIsVehicleModeRunning = true;
        
        //play particle system transition one
        VehicleModeTransitionPS.Play();
        
        //activate vehicle 
        allVehicles[selectedVehicle].SetActive(true);
        
        if (selectedVehicle == 0)
        {
            //vehicle collider
            //selectedPlayer.GetComponent<CapsuleCollider>().radius = 0.52f;
            
            //speed increase based on vehicle
            player.speed += 5;
            
            //camera switch
            if (DefaultCam_CM != null)
                if (CarModeCam_CM != null)
                    CinemachineExtensionClass.SwitchCineCamera(DefaultCam_CM,CarModeCam_CM);
            
            //change player animation respective to the vehicle
            if(selectedVehicle == 0)
                selectedCharacter.SetActive(false);
        }




        #region GameFeel

        //vigneete effect
        /*if (postProcessVolume.profile.TryGetSettings<Vignette>(out vignette))
        {
            // Set initial intensity (optional)
            vignette.intensity.Override(0f);

            // Smoothly increase the intensity to 0.595f over 1 second
            DOTween.To(() => vignette.intensity.value,
                    x => vignette.intensity.Override(x),
                    0.595f, 1f)
                .SetEase(Ease.InOutQuad);
        }*/
        
        //car sound effects and other haptics like vibration
        
        //fireworks
        /*levelParticleEffects.firework1.Play();
        levelParticleEffects.firework2.Play();*/
        
        //flying ps
        /*levelParticleEffects.flyingeffect_2.Play();
        var emission = levelParticleEffects.flyingeffect_2.emission;
        emission.rateOverTime = 12;*/
        
        /*DOTween.To(() => polyverseSkies.timeOfDay, x => polyverseSkies.timeOfDay = x, 1, 80f).OnComplete(() =>
       {
           DOTween.To(() => polyverseSkies.timeOfDay, x => polyverseSkies.timeOfDay = x, 0, 80f);
       });*/

        #endregion

        #region UI

        //gyro tutorial
        //UIManager.Instance.GyroReminder_Tutorial.SetActive(true);
        
        

        #endregion




        #region General

        //disable obstacles all over the level

        
        //add coins only
        
        // remove ability pickups also
        
        //disable ability and car btn
        uiManager.VehicleModeBtn.interactable = false;
        
        //for showing timer do it on the button as a clockwise image thing
        #endregion

        //uiManager.VehicleModeTimerImage.GetComponent<UIShiny>().enabled = true;
        uiManager.VehicleModeTimerImage.DOFillAmount(0, 10f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                
            });


        yield return new WaitForSeconds(10f);
        
        /* Exit Car Mode */
        


        

    
    }

    #region AbilityRegion

    public void AbilityTriggered()
    {
        //check if ability is already not full(3 counter)
        
        //play sound
        
        //increase value ui 
        
        //play ps on the player
    }

    #endregion

    #region MavinModeRegion

    public void MavinTriggered()
    {
        //check if ability is already not full(3 counter)
        
        //play sound
        
        //increase value ui 
        
        //play ps on the player
    }

    #endregion

    public void CoinTriggered()
    {
        //play sound
        
        //increase value ui 
        
        //play ps on the player
    }

    
    
}
