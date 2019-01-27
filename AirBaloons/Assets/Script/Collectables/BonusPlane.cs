using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlane : MonoBehaviour
{
    private Vector3 destination;
    private GameObject explosion;
    public float speed = 3f;
    private float step;

    void Start()
    {
        destination = new Vector3(160, 7.2f, -87f); //TODO: set in Settings
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }


    void OnDestroy()
    {
        dropItems();
    }

    void OnMouseDown()
    {
        dropItems();
    }

    private void dropItems()
    { 
        // generate 10 coins as reward
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(3,8), Random.Range(3, 8), 0);
            Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + position, Quaternion.identity);
        }
        
        Object[] explosionsObjects = Resources.LoadAll("Prefabs/Explosions");
        int randomExplosionIndex = Random.Range(0, explosionsObjects.Length - 1);

        explosion = Instantiate((GameObject)explosionsObjects[randomExplosionIndex], gameObject.transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}