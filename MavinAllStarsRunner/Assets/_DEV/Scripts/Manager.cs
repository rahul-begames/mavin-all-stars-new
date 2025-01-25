using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Manager : MonoBehaviour {

    [Header("Scene Settings")]

    public List<GameObject> levels = new List<GameObject>();
    public GameObject menuLevel;

    public float timeFromStartToPlay = 1.54f;

    [Header("Pursuer Settings")]

    public string pursuerGameObjName;

    public float pursuitTime;
    [HideInInspector]
    public float pursuitTimeLoc;
    

    [Header("Game Config")] 
    public float distanceCovered;
    
    [Header("Variables")]
    

    public Text timeScore;
    public Text coinsScore;
    public Text maxTimeScore;

    [Header("PlayerAnimation")] 
    public Animator charAnimator;
    public Avatar[] charAvatars;
    
   

    public Text gameOverScoreTxt;
    public Text congratulationsTxt;

    [Header("LevelDets")]
    public GameObject[] mavinCharacters;
    public string[] abilityName;
    public string[] abilityDetails;
    public GameObject selectedCharacter;
    [HideInInspector]public int selectedCharacterNumber;
    [HideInInspector]public int selectedWorldNumber; 
    
    
    
    public Player player;
    [HideInInspector]
    public GameObject pursuer;
    [HideInInspector]
    public bool play;

    protected Vector3 cameraPos;
    protected Quaternion cameraRot;

    [HideInInspector]
    public Transform pursuerTransform;

    [HideInInspector]
    public bool cameraLerp;

    [HideInInspector]
    public float startPlayerSpeed;

    protected int t;
    
    

    [Header("CinemachineCameras")] 
    public CinemachineVirtualCamera DefaultCam_CM;
    public CinemachineVirtualCamera SlideCam_CM;
    public CinemachineVirtualCamera PreRunCam_CM;

    public UIManager uiManager;
    public AudioManager audioManager;
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

        CinematicPartOne();
        
        SetInitialUIValues();

        
        SetCharacter();

        CreateMenuScene();
        
      
    }
    
    

    private void DelayedRotationCall()
    {
        TapToPlayDT(uiManager.TapToPlayImageTransform);
    }

    void CinematicPartOne()
    {
        //TODO : with dotween animate and show objective -> reach concert with maximum followers
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
    

    public virtual void Menu()
    {
        play = false;
        cameraLerp = false;

        player.SaveRecords();
        

        ClearScene();
        
        CreateMenuScene();

        if(IsInvoking("StopCameraLerp"))
            CancelInvoke("StopCameraLerp");
    }

    public virtual void SetLayer(Transform trans, int layer) 
    {
        trans.gameObject.layer = layer;
        foreach(Transform child in trans)
            SetLayer(child, layer);
    }

    public void SetCharacter()
    {
        
        
        //disable all characters
        for (int i = 0; i < mavinCharacters.Length; i++)
        {
            mavinCharacters[i].SetActive(false);
        }

        //TODO:setup player prefs
        
        selectedCharacterNumber = PlayerPrefs.GetInt("CurrentStar");
        selectedCharacter = mavinCharacters[selectedCharacterNumber];
        selectedCharacter.SetActive(true);
        
        //TODO : set character avatar
        charAnimator.avatar = charAvatars[selectedCharacterNumber];

    }
    
   

    public virtual void Play()
    {

        //Stop Music
        audioManager.musicAudioSource.Stop();
        // PLay Cassette AS
        audioManager.soundAudioSource[1].Play();
        
        uiManager.ObjectiveUIPanel.gameObject.SetActive(true);
        uiManager.ObjectiveUIPanel.DOMoveX(3000, 1.3f).SetEase(Ease.InQuart).SetRelative(true);
        
        uiManager.pauseMenuGamePanel.SetActive(true);
        
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
        play = true;
        
        
        Invoke("StopCameraLerp", 0.05f);
        cameraLerp = true;
    }

    private void Update()
    {
        DataUpdate();
        
    }

    protected virtual void FixedUpdate()
    {
        if(cameraLerp)
        {
            //cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.Euler(player.cameraRotation), 0.15f);           
        }
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

    protected virtual void ClearScene()
    {
        timeScore.text = "0";
        coinsScore.text = "0";

        Level[] levelsLoc = GameObject.FindObjectsOfType<Level>();

        for (int i = 0; i < levelsLoc.Length; i++)
        {
            Destroy(levelsLoc[i].transform.parent.gameObject);
        }

        Destroy(player.gameObject);
        Destroy(pursuer.gameObject);
    }

    protected virtual void CreateMenuScene()
    {
        GameObject menuLvl = Instantiate(menuLevel);
        menuLvl.SetActive(true);
        
        //Switch To PreRun Camera
        if (DefaultCam_CM != null)
                if (PreRunCam_CM != null)
                    CinemachineExtensionClass.SwitchCineCamera(DefaultCam_CM,PreRunCam_CM);

        //player = Instantiate(characterSkins[PlayerPrefs.GetInt("CharacterSkin")], menuLvl.transform.Find("StartPosition").position, Quaternion.identity).GetComponent<Player>();
        startPlayerSpeed = player.speed;
        pursuer = menuLvl.transform.Find(pursuerGameObjName).gameObject;
        pursuer.transform.SetParent(transform.parent);
        pursuerTransform = pursuer.transform;

        //cameraTransform.position = cameraPos;
        //cameraTransform.rotation = cameraRot;
    }

    public virtual void PlayAgain()
    {
      
        ClearScene();

        CreateMenuScene();

        Play();
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

    public void NewCharacterLevel()
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
}
