using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    PlayerInput inputAction;
    GameObject pausePanel;
    Transform child;
    RankingManager rankManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        inputAction = new PlayerInput();
        Canvas canvas = FindObjectOfType<Canvas>();
        child = canvas.transform.GetChild(2);
        pausePanel = child.gameObject;
        pausePanel.SetActive(false);
    }
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        inputAction.Game.Enable();
        inputAction.Game.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        inputAction.Game.Pause.performed -= Pause;
        inputAction.Game.Disable();
    }

    public void Gamestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test_UI");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        rankManager = FindObjectOfType<RankingManager>();
        rankManager.sceneLoad++;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Start");
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausePanel = GameObject.FindWithTag("Pause");
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
