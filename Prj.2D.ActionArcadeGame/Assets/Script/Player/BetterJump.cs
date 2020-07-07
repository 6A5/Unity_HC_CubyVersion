using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] float fallMultiplier = 3.5f; //數值越大 跳到頂的降落速度越快 跳得越矮
    [SerializeField] float lowJumpMultiplier = 2.5f; //數值越大 沒跳到頂的降落速度越快 可以小跳

    [SerializeField] PauseSystem pS;
	Rigidbody2D rb;
	
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseSystem>(); // 暫停系統
    }

    void Update()
    {
        if (pS.gS == PauseSystem.GameState.Going) // 如果 PauseSystem 下的 GameState 狀態為 GameState.Going 就代表可操作
        {
            if (rb.velocity.y < 0)
		    {
			    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
		    }
		    else if (rb.velocity.y > 0)
		    {
			    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
		    }
        }
    }
}
