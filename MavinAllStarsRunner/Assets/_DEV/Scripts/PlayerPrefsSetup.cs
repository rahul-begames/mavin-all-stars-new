using UnityEngine;

public class PlayerPrefsSetup : MonoBehaviour
{
    private void Start()
    {
        // Check if each PlayerPrefs key exists before setting a default value
        if (!PlayerPrefs.HasKey("MusicValue"))
            PlayerPrefs.SetFloat("MusicValue", 1);

        if (!PlayerPrefs.HasKey("SFXValue"))
            PlayerPrefs.SetFloat("SFXValue", 1);

        if (!PlayerPrefs.HasKey("IsHapticOn"))
            PlayerPrefs.SetInt("IsHapticOn", 1);
        

        if (!PlayerPrefs.HasKey("MavinValue"))
            PlayerPrefs.SetInt("MavinValue", 0);

        if (!PlayerPrefs.HasKey("CoinValue"))
            PlayerPrefs.SetInt("CoinValue", 0);

        if (!PlayerPrefs.HasKey("HealthValue"))
            PlayerPrefs.SetInt("HealthValue", 3);
        
        if (!PlayerPrefs.HasKey("CurrentStar"))
            PlayerPrefs.SetInt("CurrentStar", 0);
        

        // Save PlayerPrefs changes immediately
        PlayerPrefs.Save();
        

        // Check if a key exists
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Debug.Log("High Score exists!");
        }
    }

    // Function to reset all PlayerPrefs data
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs data has been deleted.");
    }

    
}