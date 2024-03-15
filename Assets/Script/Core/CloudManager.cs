using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject[] cloudPref;
    float creatCloud;
    Vector3 genPosition;
    BackGround background;
    float randY;

    private void Awake()
    {
        background = FindObjectOfType<BackGround>();
    }
    public void CreatCloud()
    {
        randY = Random.Range(0.8f, 5.0f);   // 구름 생성 위치 랜덤
        genPosition = new Vector3(background.rightPosX * 0.5f, randY, 0);   // 구름 생성
        creatCloud = Random.value;  // 구름 두종류중 랜덤으로 생성
        if (creatCloud < 0.25f)
        {
            Instantiate(cloudPref[0], genPosition, Quaternion.identity);
        }
        else if (creatCloud < 0.5f)
        {
            Instantiate(cloudPref[1], genPosition, Quaternion.identity);
        }
    }
}
