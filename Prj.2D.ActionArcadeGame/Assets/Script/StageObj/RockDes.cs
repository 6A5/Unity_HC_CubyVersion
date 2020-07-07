using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDes : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        Invoke("DestroyRock",2);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed));
    }
    void DestroyRock()
    {
        Destroy(gameObject);
    }
}
