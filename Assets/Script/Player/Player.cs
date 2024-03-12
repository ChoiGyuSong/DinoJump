using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    Canvas canvas;
    Transform panel;

    void Awake()
    {
        inputActions = new PlayerInput();
        rigid = GetComponent<Rigidbody2D>();

        rankManager = FindObjectOfType<RankingManager>();
        timer = FindAnyObjectByType<Timer>();

        GameObject game = GameObject.FindWithTag("Score");
        nowScore = game.GetComponent<TextMeshProUGUI>();

        canvas = FindObjectOfType<Canvas>();
        panel = canvas.transform.GetChild(1);
        panel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Disable();
    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("���� Ű�� ����");
        if (!isJump)
        {
            Debug.Log("��������, ������ �ƴ�");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
        // ���� ���϶�
        else if(!doubleJump)
        {
            Debug.Log("���� ��������");
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
        panel.gameObject.SetActive(true);
        nowScore.text = string.Format("{0:N2}", timer.time);
        rankManager.CheckRanking(timer.time);
        Time.timeScale = 0.0f;
    }
}
