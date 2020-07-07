using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShoot : MonoBehaviour
{
    public GameObject fire;

    SpriteRenderer f_Spr;
    BoxCollider2D f_Col;

    public float timeSpeed = 1;

    private void Start()
    {
        f_Spr = fire.GetComponent<SpriteRenderer>();
        f_Col = fire.GetComponent<BoxCollider2D>();

        f_Spr.drawMode = SpriteDrawMode.Tiled;
    }

    private void Update()
    {
        float a = 3 * Mathf.Sin(Time.time * timeSpeed);     // sin循環
        float b;                                            // 取值用

        if (a >= 0)
            b = a;
        else
            b = 0;

        f_Spr.size = new Vector2(1, b);

        f_Col.size = new Vector2(1, b);
        f_Col.offset = new Vector2(0, - b * 0.5f);
    }
}
