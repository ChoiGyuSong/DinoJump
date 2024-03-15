using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    PlayerInput inputActions;
    public float jumpPower = 7.0f;
    Rigidbody2D rigid;
    public bool isJump;
    public bool doubleJump;
    RankingManager rankManager;
    Timer timer;
    TextMeshProUGUI nowScore;
    public GameObject rank;
    float moveSpeed = 3.5f;
    Vector2 inputMove;
    GameManager gameManager;

    void Awake()
    {
        inputActions = new PlayerInput();
        rigid = GetComponent<Rigidbody2D>();

        gameManager = FindObjectOfType<GameManager>();
        rankManager = FindObjectOfType<RankingManager>();
        timer = FindAnyObjectByType<Timer>();

        GameObject game = GameObject.FindWithTag("Score");
        nowScore = game.GetComponent<TextMeshProUGUI>();

        rank = GameObject.FindWithTag("Rank");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputMove = obj.ReadValue<Vector2>();
    }

    private void OnMoveCancel(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputMove = new Vector2(0,0);
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + new Vector3(inputMove.x, 0, 0) * moveSpeed * Time.fixedDeltaTime;
        transform.position = newPosition;
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("점프 키는 실행");
        if (!isJump)
        {
            Debug.Log("점프실행, 점프중 아님");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
        // 점핑 중일때
        else if(!doubleJump)
        {
            Debug.Log("더블 점프실행");
            rigid.AddForce(Vector2.up * 6.0f, ForceMode2D.Impulse);
            doubleJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cactus")
        {
            Die();
        }
    }

    void Die()
    {
        gameManager.rankDis = true;
        rank.gameObject.SetActive(true);
        nowScore.text = string.Format("{0:N2}", timer.time);
        rankManager.CheckRanking(timer.time);
        Time.timeScale = 0.0f;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMoveCancel;
        inputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Move.performed -= OnMoveCancel;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }
}
