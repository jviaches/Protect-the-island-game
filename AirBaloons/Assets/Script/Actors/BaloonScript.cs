using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{
    private GameObject island;
    private float speed = 1f;
    private float step;

    // Use this for initialization
    void Start()
    {
        island = GameObject.Find("Island");
        step = speed * Time.deltaTime;
    }

    void OnMouseDown()
    {
        // generate 2 coins as reward
        Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.left, Quaternion.identity);
        Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.right, Quaternion.identity);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, island.transform.position, step);
    }
}
