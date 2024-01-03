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
        randY = Random.Range(0.8f, 5.0f);
        genPosition = new Vector3(background.rightPosX * 0.5f, randY, 0);
        creatCloud = Random.value;
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
