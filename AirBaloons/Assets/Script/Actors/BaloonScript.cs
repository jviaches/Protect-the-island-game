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
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, island.transform.position, step);
    }
}
