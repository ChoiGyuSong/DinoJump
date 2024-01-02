using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    float speed = 4.0f;

    void Update()
    {
        transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

        if (transform.position.x < -13.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
