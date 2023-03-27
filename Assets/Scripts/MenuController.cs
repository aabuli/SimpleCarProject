using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject InGameUI;
    public GameObject HowToUI;
    public GameObject HowToUIMenu;
    public GameObject MenuUI;
    public GameObject SelectCarUI;
    private CarController car;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        HowToUI.SetActive(false);
        HowToUIMenu.SetActive(false);
        InGameUI.SetActive(false);
        SelectCarUI.SetActive(false);

        car = FindObjectOfType<CarController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HowToUI.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        InGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void HowToPlay()
    {
        pauseMenuUI.SetActive(false);
        MenuUI.SetActive(false);
        HowToUI.SetActive(true);
    }

    public void HowToPlayMenu()
    {
        MenuUI.SetActive(false);
        HowToUIMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        HowToUI.SetActive(false);
        HowToUIMenu.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void SelectCar()
    {
        MenuUI.SetActive(false);
        SelectCarUI.SetActive(true);
    }

    public void BlueCar()
    {
        car.SelectBlueCar();
        SelectCarUI.SetActive(false);
        InGameUI.SetActive(true) ;
        car.SetReadyToPlay(true);
    }

    public void BlackCar()
    {
        car.SelectBlackCar();
        SelectCarUI.SetActive(false);
        InGameUI.SetActive(true);
        car.SetReadyToPlay(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
