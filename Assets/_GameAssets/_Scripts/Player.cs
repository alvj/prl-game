using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");

        transform.position += Vector3.right * hInput * speed * Time.deltaTime;
    }
}
