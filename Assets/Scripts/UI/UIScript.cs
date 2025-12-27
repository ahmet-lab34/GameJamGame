using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject GameMap1;
    [SerializeField] private GameObject ESC_Menu;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button MainPlayButton;
    [SerializeField] private Button MainOptionsButton;
    [SerializeField] private Button MainExitButton;
    [SerializeField] private Button OptionsBackButton;
    [SerializeField] private PlayerScript playerScript;

    private bool open = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("SkipMainMenu", 0) == 1)
        {
            MainMenu.SetActive(false);
            GameMap1.SetActive(true);
            GameUI.SetActive(true);
            Time.timeScale = 1f;
            PlayerPrefs.SetInt("SkipMainMenu", 0);
        }
        else
        {
            MainMenu.SetActive(true);
        }
        RestartButton.onClick.AddListener(() => OnButtonPressed(RestartButton));
        MainMenuButton.onClick.AddListener(() => OnButtonPressed(MainMenuButton));


        MainPlayButton.onClick.AddListener(() => OnButtonPressed(MainPlayButton));
        MainOptionsButton.onClick.AddListener(() => OnButtonPressed(MainOptionsButton));
        OptionsBackButton.onClick.AddListener(() => OnButtonPressed(OptionsBackButton));
        MainExitButton.onClick.AddListener(() => OnButtonPressed(MainExitButton));
    }
    public void RestartScene()
    {
        PlayerPrefs.SetInt("SkipMainMenu", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UIDie()
    {
        DeathPanel.SetActive(true);
        PlayerObject.SetActive(false);
        Time.timeScale = 0f;
    }
    void OnButtonPressed(Button button)
    {
        if (button == RestartButton)
        {
            Debug.Log("Just the Restart Button Is Pressed !");
            RestartScene();
        }
        if (button == MainMenuButton)
        {
            /*Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);*/
            ESC_Menu.SetActive(false);
            MainMenu.SetActive(true);
            GameMap1.SetActive(false);


        }
        if (button == MainPlayButton)
        {
            StartGameFromMenu();
        }
        if (button == MainOptionsButton)
        {
            Settings.SetActive(true);
            MainMenu.SetActive(false);
        }
        if (button == OptionsBackButton)
        {
            MainMenu.SetActive(true);
            Settings.SetActive(false);
        }
        if (button == MainExitButton)
        {
            Application.Quit();
        }
    }
    private void StartGameFromMenu()
    {
        GameMap1.SetActive(true);
        GameUI.SetActive(true);
        MainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void repeatLevel()
    {
        playerScript.playerNumbers.playerSouls += 1;
        GameMap1.SetActive(true);
    }
    public void ESC_Button()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && open)
        {
            ESC_Menu.SetActive(true);
            open = !open;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !open)
        {
            ESC_Menu.SetActive(false);
            open = !open;
        }
    }
}
