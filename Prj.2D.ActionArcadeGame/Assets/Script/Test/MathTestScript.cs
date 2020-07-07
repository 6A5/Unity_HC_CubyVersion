using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathTestScript : MonoBehaviour
{
    public Text a;

    public GameObject fire;

    public float b;

    private void Start()
    {
        fire.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
    }

    private void Update()
    {
        b = 3 * Mathf.Sin(Time.time);

        float c;
        if (b >= 0)
        {
            c = b;
        }
        else
        {
            c = 0;
        }

        fire.GetComponent<SpriteRenderer>().size = new Vector2(1, c);
        fire.GetComponent<BoxCollider2D>().size = new Vector2(1, c);
        fire.GetComponent<BoxCollider2D>().offset = new Vector2(0, -c * 0.5f);

        a.text = b.ToString();
    }
}
