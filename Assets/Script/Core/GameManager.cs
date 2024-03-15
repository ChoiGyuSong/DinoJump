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
        if (this == instance || instance == null) // 현재 오브젝트가 싱글톤 인스턴스가 아니라면
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
    /// 게임 시작
    /// </summary>
    public void Gamestart()
    {
        // 게임시작 씬으로 이동
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test_UI");
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 게임 재시작 버튼
    /// </summary>
    public void Restart()
    {
        rankManager = FindObjectOfType<RankingManager>();
        rankManager.sceneLoad++;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 일시 정지(ESC키)
    /// </summary>
    /// <param name="obj"></param>
    private void Pause(InputAction.CallbackContext obj)
    {
        if(!rankDis)
        {
            // 랭킹 창이 열려있는 상태가 아니라면
            // Pause판넬 생성후 게임 일시 정지
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// 게임 재개
    /// </summary>
    public void Resume()
    {
        // Pause판넬 false후 게임 시작
        pausePanel = GameObject.FindWithTag("Pause");
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// 메인 화면으로 이동
    /// </summary>
    public void Quit()
    {
        // 시작 씬으로 이동
        SceneManager.LoadScene("Start");
    }
}
