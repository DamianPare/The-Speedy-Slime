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

    public GameObject mainMenu;
    private bool gameStarted;

    public KeyCode inputKey;

    public GameObject Collider1;
    public GameObject Collider2;
    public Transform Start2;
    public Transform Start3;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Start()
    {
        timer = startTime;
        PauseGame();
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay();

        if (Input.GetKeyDown(inputKey) && gameStarted == false)
        {
            StartGame();
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
}
