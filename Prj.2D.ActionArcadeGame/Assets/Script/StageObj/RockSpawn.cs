using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public GameObject rock;
    public float time;
    public float speed;

    private void Start()
    {
        InvokeRepeating("SpawnRock",0,time);
    }

    void SpawnRock()
    {
        GameObject temp = Instantiate(rock, gameObject.transform.position, Quaternion.identity);

        temp.AddComponent<Rigidbody2D>();
        temp.GetComponent<Rigidbody2D>().velocity = speed * Vector2.down;

    }
}
