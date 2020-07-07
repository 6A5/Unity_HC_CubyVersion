using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAtk : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool stepInvincible; // 踩到無敵 1 Frame
    public AudioClip damageSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDamage" && !Player.GetComponent<PlayerControl>().dead)
        {
            Player.GetComponent<AutoJump>().StepOnEnemy();
            GetComponent<AudioSource>().Play();
            stepInvincible = true;
        }
    }

    private void FixedUpdate()
    {
        stepInvincible = false;
    }
}
