using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : MonoBehaviour
{
    int hp = 1;

    //被玩家踩到
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerStep")
        {
            hp -= 1;
            if (hp <= 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
        }
    }

    //碰到玩家
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("AAAAAAAAA");
        }
    }
}
