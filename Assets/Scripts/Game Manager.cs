using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text recordText; 
    public float startTime;
    private float timeToDisplay;
    private string timerString;
    private string firstTime;
    private string secondTime;
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

    public int initialFreeze;

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
        gameStarted = true;
        StartCoroutine(StartCountdown());
        Time.timeScale = 1f;
        timeToDisplay = startTime;
        PauseGame();
        gameOver = false;
        gameFinished = false;
    }

    void Update()
    {
        timeToDisplay += Time.deltaTime;
        UpdateTimerDisplay();
        recordText.text = timerString;

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

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(initialFreeze);
        gameStarted = false;
        Debug.Log("You can move");
    }

    void UpdateTimerDisplay()
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay /60);
        float Seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float MilliSeconds = timeToDisplay % 1 * 100;

        timerString = string.Format("{0:0}:{1:00}:{2:00}", Minutes, Seconds, MilliSeconds);
        timerText.text = timerString;
    }

    public void ResetTimer()
    {
        timeToDisplay = 0f;
    }

    public void PauseGame()
    {
        mainMenu.SetActive(true);
        UI.SetActive(false);
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        UI.SetActive(true);
        PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppVolume.enabled = !ppVolume.enabled;
        ResetTimer();
        gameStarted = true;
        Movement.instance.StartMoving();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        UI.SetActive(false);
        if (Input.GetKeyDown(inputKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameFinished()
    {
        Time.timeScale = 0f;
        gameFinishedUI.SetActive(true);
        UI.SetActive(false);

        if (Input.GetKeyDown(inputKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
