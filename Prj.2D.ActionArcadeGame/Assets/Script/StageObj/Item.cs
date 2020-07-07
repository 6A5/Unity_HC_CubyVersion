using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType {medic, coin, diamond, lifeUp, low, normal, high};
    public ItemType type;

    [SerializeField] float heightJump;
    [SerializeField] float lowJump;
    [SerializeField] Text scoreText;
    [SerializeField] int bonusItemScore;

    [SerializeField] PlayerControl pC;
    Collider2D m_col;
    AudioSource m_aS;
    SpriteRenderer m_spr;

    void Start()
    {
        m_col = GetComponent<Collider2D>();
        m_aS = GetComponent<AudioSource>();
        m_spr = GetComponent<SpriteRenderer>();
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerControl PC = other.gameObject.GetComponent<PlayerControl>();
            switch (type)
            {
                case ItemType.medic:
                    if (PC.cur_health < PC.maxHealth)
                    {
                        PC.cur_health++;
                    }
                    break;

                case ItemType.coin:
                    pC.score += bonusItemScore;
                    break;

                case ItemType.diamond:
                    pC.score += bonusItemScore;
                    break;

                case ItemType.lifeUp:
                    PC.life++;
                    break;

                case ItemType.low:
                    PC.jumpHeight = PC.defaultJumpHeight;
                    PC.jumpHeight *= lowJump;
                    break;

                case ItemType.normal:
                    PC.jumpHeight = PC.defaultJumpHeight;
                    break;

                case ItemType.high:
                    PC.jumpHeight = PC.defaultJumpHeight;
                    PC.jumpHeight *= heightJump;
                    break;
            }
            m_aS.Play();

            Destroy(m_col);
            Destroy(m_spr);
        }
    }
}
