using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJump : MonoBehaviour
{
	[SerializeField] Transform feetPos;
	[SerializeField] float height,weight;
	[SerializeField] LayerMask whatIsGround, whatIsEnemy;
	[SerializeField] float jumpHeight;

    [SerializeField] PauseSystem pS;
    Rigidbody2D m_rg2d;
    bool canjumpNormal;

    private void Awake()
    {
        m_rg2d = GetComponent<Rigidbody2D>();
        pS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseSystem>();
    }

    void Update()
    {
        if (pS.gS == PauseSystem.GameState.Going) // 如果 PauseSystem 下的 GameState 狀態為 GameState.Going 就代表可操作
        {
            if (!GetComponent<PlayerControl>().dead)
            {
                weight = this.GetComponent<BoxCollider2D>().size.x; //取得碰撞寬度
                jumpHeight = GetComponent<PlayerControl>().jumpHeight; //抓角色控制的資料

                //踩到地板
                if (Physics2D.OverlapBox(feetPos.position, new Vector2(weight - .1f, height), 0f, whatIsGround))  //腳下碰撞
                {
                    if (m_rg2d.velocity.y <= 0)
                    {
                        canjumpNormal = true;
                    }
                }
                if (canjumpNormal == true)
                {
                    float v = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
                    m_rg2d.velocity = Vector2.up * v;   //V = Mathf.Sqrt(2 * H * Mathf.Abs(Gravity(default -9.8)) ) 跳躍公式
                    canjumpNormal = false;
                }
            }
        }
    }

    public void StepOnEnemy()   //踩到敵人
    {
        
        float v = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight * 0.75f);
        m_rg2d.velocity = Vector2.up * v;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(feetPos.position, new Vector2(this.GetComponent<BoxCollider2D>().size.x-.1f, height));
    }
}
