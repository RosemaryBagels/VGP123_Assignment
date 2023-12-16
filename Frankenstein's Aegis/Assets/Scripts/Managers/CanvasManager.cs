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

    //public AudioMixer audioMixer;
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
