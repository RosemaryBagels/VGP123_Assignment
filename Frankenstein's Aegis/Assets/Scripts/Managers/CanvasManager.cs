using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button restartButton;
    public Button settingsButton;
    public Button quitButton;
    public Button quitButton2;
    public Button quitButton3;
    public Button backToMainButton;
    public Button backToPauseButton;
    public Button reloadMainButton;
    public Button reloadMainButton2;
    public Button reloadMainButton3;
    public Button resumeButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject gameOver;
    public GameObject victoryScreen;

    [Header("Text")]
    public Text masterVSliderText;
    public Text musicVSliderText;
    public Text sfxVSliderText;
    public Text smallVictoryText;

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
        { 
            playButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));
            gameIsPaused = false;
        }

        if (restartButton)
        {
            restartButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(1));
            gameIsPaused = false;
        }

        if (reloadMainButton)
        {
            gameIsPaused = false;
            reloadMainButton.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));
        }

        if (reloadMainButton2)
        {
            gameIsPaused = false;
            reloadMainButton2.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));
        }

        if (reloadMainButton3)
        {
            gameIsPaused = false;
            reloadMainButton3.onClick.AddListener(() => GameManager.Instance.ChangeScene(0));
        }

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

        if (quitButton2)
            quitButton2.onClick.AddListener(Quit);

        if (quitButton3)
            quitButton3.onClick.AddListener(Quit);

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
        GameManager.Instance.OnDeath.AddListener(ShowGameOver);
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

    void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void OnWin()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        victoryScreen.SetActive(true);
        smallVictoryText.text = ("You got a total of " + GameManager.Instance.count.ToString() + " runes! Congratulations :D");
        MusicBox.Instance.PlayVictoryMusic();
    }

}
