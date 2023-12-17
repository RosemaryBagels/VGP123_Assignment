using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backToMainButton;
    public Button backToPauseButton;
    public Button reloadMainButton;
    public Button resumeButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    //public Text livesText;
    public Text masterVSliderText;
    public Text musicVSliderText;
    public Text sfxVSliderText;

    [Header("Slider")]
    public Slider masterVSlider;
    public Slider musicVSlider;
    public Slider sfxVSlider;

    public AudioMixer audioMixer;
    public static bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        if (playButton)
            playButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));

        if (reloadMainButton)
            reloadMainButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));

        if (resumeButton)
            resumeButton.onClick.AddListener(UnpauseGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (backToMainButton)
            backToMainButton.onClick.AddListener(ShowMainMenu);

        if (backToPauseButton)
            backToPauseButton.onClick.AddListener(ShowPauseMenu);

        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (masterVSlider)
        {
            masterVSlider.onValueChanged.AddListener((value) => OnMasterSliderValueChanged(value));
            float newValue;
            audioMixer.GetFloat("MasterVol", out newValue);
            masterVSlider.value = newValue + 80;
            if (masterVSliderText)
                masterVSliderText.text = (Mathf.Ceil(newValue + 80).ToString());
        }

        if (musicVSlider)
        {
            musicVSlider.onValueChanged.AddListener((value) => OnMusicSliderValueChanged(value));
            float newValue;
            audioMixer.GetFloat("MusicVol", out newValue);
            musicVSlider.value = newValue + 80;
            if (musicVSliderText)
                musicVSliderText.text = (Mathf.Ceil(newValue + 80).ToString());
        }

        if (sfxVSlider)
        {
            sfxVSlider.onValueChanged.AddListener((value) => OnSFXSliderValueChanged(value));
            float newValue;
            audioMixer.GetFloat("SFXVol", out newValue);
            sfxVSlider.value = newValue + 80;
            if (sfxVSliderText)
                sfxVSliderText.text = (Mathf.Ceil(newValue + 80).ToString());
        }
    }
    void OnMasterSliderValueChanged(float value)
    {
        masterVSliderText.text = value.ToString();
        audioMixer.SetFloat("MasterVol", value - 80);
    }

    void OnMusicSliderValueChanged(float value)
    {
        musicVSliderText.text = value.ToString();
        audioMixer.SetFloat("MusicVol", value - 80);
    }

    void OnSFXSliderValueChanged(float value)
    {
        sfxVSliderText.text = value.ToString();
        audioMixer.SetFloat("SFXVol", value - 80);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                gameIsPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                gameIsPaused = false;
            }
        }
    }

    private void Quit() //version specific code
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    void ShowSettingsMenu()
    {
        if (mainMenu)
            mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

}
