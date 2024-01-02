using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float speed = 0.5f;

    void Update()
    {
        transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

        if (transform.position.x < -13.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
