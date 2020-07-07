using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    Vector2 move;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.acceleration.x;
        move.y = 0;

        print(move);

        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }

        GetComponent<Rigidbody2D>().velocity = move * 10;
    }
}
