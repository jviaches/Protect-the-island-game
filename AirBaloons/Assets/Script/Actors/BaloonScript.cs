using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{
    private GameObject island;
    private GameObject explosion;
    private float speed = 1f;
    private float step;

    void Start()
    {
        island = GameObject.Find("Island");
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, island.transform.position, step);
    }

    void OnMouseDown()
    {
        // generate 2 coins as reward
        Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.left, Quaternion.identity);

        if (!GameSettings.IsMoneyIncreaseBuffOn)
        {
            float buffProbability = Random.Range(0f, 1f);
            print("buffProbability=" + buffProbability);

            if (buffProbability > GameSettings.MoneyIncreaseBuffProbability)
                Instantiate((GameObject)Resources.Load("Prefabs/Buffs/MoneyIncreaseBuff"), gameObject.transform.position + Vector3.right, Quaternion.identity);
            else
                Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.right, Quaternion.identity);
        }

        Object[] explosionsObjects = Resources.LoadAll("Prefabs/Explosions");
        int randomExplosionIndex = Random.Range(0, explosionsObjects.Length - 1);

        explosion = Instantiate((GameObject)explosionsObjects[randomExplosionIndex], gameObject.transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
