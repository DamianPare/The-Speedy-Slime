using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class GameManager : MonoBehaviour
{
    public Text timerText;
    public float startTime;
    private float timer;
    public GameObject UI;
    public GameObject gameOverUI;
    public GameObject gameFinishedUI;

    public GameObject mainMenu;
    private bool gameStarted;
    private bool gameOver;
    private bool gameFinished;

    public KeyCode inputKey;

    public AudioSource audioSource;
    public AudioClip fishKillClip;
    public AudioClip waterKillClip;



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            audioSource.PlayOneShot(waterKillClip);
            gameOver = true;
        }

        if (collision.gameObject.layer == 7)
        {
            audioSource.PlayOneShot(fishKillClip);
            gameOver = true;
        }

        if (collision.gameObject.layer == 8)
        {
            gameFinished = true;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        timer = startTime;
        PauseGame();
        gameOver = false;
        gameFinished = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay();

        if (Input.GetKeyDown(inputKey) && gameStarted == false)
        {
            StartGame();
        }

        if (gameOver == true)
        {
            GameOver();
        }

        if (gameFinished == true)
        {
            GameFinished();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void PauseGame()
    {
        mainMenu.SetActive(true);
        UI.SetActive(false);
    }
    public void StartGame()
    {
        Movement.instance.StartMoving();
        mainMenu.SetActive(false);
        UI.SetActive(true);
        PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppVolume.enabled = !ppVolume.enabled;
        ResetTimer();
        gameStarted = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        if(Input.GetKeyDown(inputKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameFinished()
    {
        Time.timeScale = 0f;
        gameFinishedUI.SetActive(true);

        if (Input.GetKeyDown(inputKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
