using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool inMainMenu = true;

    public Animator animator;

    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject controlsMenuUI;
    public GameObject creditsMenu;
    public GameObject confirmationScreen;
    public GameObject backButton1;
    public GameObject backButton2;
    public GameObject backButton3;
    public GameObject backButton4;
    public GameObject MenuCanvas;



    public bool controlsInverted = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        animator.SetTrigger("Fade_Out");
        Cursor.visible = false;
        inMainMenu = false;
        mainMenuUI.SetActive(false);
        Invoke("LoadGame", 1);
    }

    void Update()
    {
        if (inMainMenu == false)
        {
            if (Input.GetButtonDown("PauseGame"))
            {
                if (GameIsPaused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }
        }

        if (controlsInverted == true)
        {
            FindObjectOfType<InputManager>().playerControlsInverted = true;
        }
        else if (controlsInverted == false)
        {
            FindObjectOfType<InputManager>().playerControlsInverted = false;
        }

    }

    public void Resume()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        confirmationScreen.SetActive(false);
        backButton1.SetActive(false);
        backButton2.SetActive(false);
        backButton3.SetActive(false);
        backButton4.SetActive(false);

    }

    void Pause()
    {
        Cursor.visible = true;
        Debug.Log("Paused");
        Time.timeScale = 0f;
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadGame()
    {
        creditsMenu.SetActive(true);
        SceneManager.LoadScene("Main_GameScene");
        
    }
    public void QuitToMenu()
    {
        Cursor.visible = true;
        animator.SetTrigger("Fade_Out");
        inMainMenu = true;
        Invoke("LoadMainMenu", 1);
        Resume();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu_2");
        mainMenuUI.SetActive(true);
    }
}
