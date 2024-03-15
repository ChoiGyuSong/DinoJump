using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround : MonoBehaviour
{
    public float speed;
    public Transform[] grounds;
    public GameObject[] cloudPref;
    CloudManager cloud;
    CactusManager cactus;

    float leftPosX = 0.0f;
    public float rightPosX = 0.0f;
    int groundCount = 0;
    float creatCactus;

    private void Awake()
    {
        leftPosX = -13.0f;
        rightPosX = grounds.Length * 3;
        cloud = FindObjectOfType<CloudManager>();
        cactus = FindObjectOfType<CactusManager>();
    }

    void Update()
    {
        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            if (grounds[i].position.x < leftPosX)
            {
                // ��� �̵�
                Vector3 nextPos = grounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                grounds[i].position = nextPos;

                if(this.gameObject.tag == "Sky")
                {
                    // ���� �� ��ũ��Ʈ�� ����ִ� ������Ʈ�� �ϴ��̶��
                    cloud.CreatCloud();
                }
                if(this.gameObject.tag == "Ground")
                {
                    // ���� �� ��ũ��Ʈ�� ����ִ� ������Ʈ�� ���̶��
                    groundCount++;
                    if(groundCount > 1)
                    {
                        // ��� ��ĭ�� ������ ����
                        creatCactus = Random.value;
                        if(creatCactus < 0.7f)
                        {
                            cactus.CreatCactus();
                            groundCount = 0;
                        }
                    }
                }
            }
        }
    }

}
