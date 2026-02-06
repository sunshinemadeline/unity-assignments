using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject quitPopup;
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;

    public Toggle fullscreenToggle;
    public TextMeshProUGUI fullscreenText;

    public TMP_Dropdown resolutionDropdown;
    public TextMeshProUGUI resolutionText;

    void Start()
    {
        Debug.Log("MenuManager started");
        ShowMainMenu();
        OptionsUI();
    }

//Panels Logic
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        quitPopup.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        creditsMenu.SetActive(false);
        quitPopup.SetActive(false);
    }

    public void ShowCreditsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(true);
        quitPopup.SetActive(false);
    }

    public void ShowQuitPopup()
    {
        quitPopup.SetActive(true);
    }

    public void HideQuitPopup()
    {
        quitPopup.SetActive(false);
    }

//OPTIONS LOGIC
    void OptionsUI()
    {
        UpdateVolumeText(volumeSlider.value);
        UpdateFullscreenText(fullscreenToggle.isOn);
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new System.Collections.Generic.List<string>
        {
            "1280 x 720",
            "1920 x 1080",
            "2560 x 1440"
        });

        UpdateResolutionText(resolutionDropdown.options[resolutionDropdown.value].text);
    }

    public void OnVolumeChanged(float value)
    {
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        volumeText.text = "Volume: " + percent + "%";
    }

    public void OnFullscreenToggled(bool isFullscreen)
    {
        UpdateFullscreenText(isFullscreen);
    }

    void UpdateFullscreenText(bool isFullscreen)
    {
        fullscreenText.text = "Fullscreen: " + (isFullscreen ? "On" : "Off");
    }

    public void OnResolutionChanged(int index)
    {
        UpdateResolutionText(resolutionDropdown.options[index].text);
    }

    void UpdateResolutionText(string resolution)
    {
        resolutionText.text = "Resolution: " + resolution;
    }

//QUIT

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

