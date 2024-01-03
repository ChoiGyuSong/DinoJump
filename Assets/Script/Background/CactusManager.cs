using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusManager : MonoBehaviour
{
    public GameObject[] cactusPref;
    float creatCloud;
    Vector3 genPosition;
    BackGround background;
    float randCactus;
    float randCactusGen;
    float genX;

    private void Awake()
    {
        background = FindObjectOfType<BackGround>();
    }
    public void CreatCactus()
    {
        genX = background.rightPosX * 0.5f;
        randCactus = Random.Range(1, 4);
        randCactusGen = Random.Range(genX -2, genX +4);
        genPosition = new Vector3(randCactusGen, -1.3f, 0);
        if (randCactus == 1)
        {
            Instantiate(cactusPref[0], genPosition, Quaternion.identity);
        }
        else if (randCactus == 2)
        {
            Instantiate(cactusPref[1], genPosition, Quaternion.identity);
        }
        else if (randCactus == 3)
        {
            Instantiate(cactusPref[2], genPosition, Quaternion.identity);
        }
    }
}
