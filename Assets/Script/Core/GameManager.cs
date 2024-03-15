using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    PlayerInput inputAction;
    public GameObject pausePanel;
    Transform child;
    RankingManager rankManager;
    public bool rankDis;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        inputAction = new PlayerInput();

        SceneManager.sceneLoaded += OnSceneLoaded;
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

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (this == instance || instance == null) // ���� ������Ʈ�� �̱��� �ν��Ͻ��� �ƴ϶��
        {
            pausePanel = GameObject.FindWithTag("Pause");
            rankDis = false;
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
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

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Gamestart()
    {
        // ���ӽ��� ������ �̵�
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test_UI");
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// ���� ����� ��ư
    /// </summary>
    public void Restart()
    {
        rankManager = FindObjectOfType<RankingManager>();
        rankManager.sceneLoad++;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// �Ͻ� ����(ESCŰ)
    /// </summary>
    /// <param name="obj"></param>
    private void Pause(InputAction.CallbackContext obj)
    {
        if(!rankDis)
        {
            // ��ŷ â�� �����ִ� ���°� �ƴ϶��
            // Pause�ǳ� ������ ���� �Ͻ� ����
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// ���� �簳
    /// </summary>
    public void Resume()
    {
        // Pause�ǳ� false�� ���� ����
        pausePanel = GameObject.FindWithTag("Pause");
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// ���� ȭ������ �̵�
    /// </summary>
    public void Quit()
    {
        // ���� ������ �̵�
        SceneManager.LoadScene("Start");
    }
}
