using Assets.Script.Actors;
using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{
    private GameObject island;
    private GameObject explosion;
    public float speed = GameSettings.BaloonsSpeed;
    private float step;

    public int DPS { get  { return 10;  }  }

    public bool isIslandEngaged = false;

    void Start()
    {
        island = GameObject.Find("IslandShield");
        step = speed * Time.deltaTime;
    }

    void Update()
    {
        if(!isIslandEngaged)
            transform.position = Vector3.MoveTowards(transform.position, island.transform.position, step);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "islandShield")
        {
            print("Ballon detected in area");
            isIslandEngaged = true;

            InvokeRepeating("startDamage", 0, 1);
        }
    }

    private void startDamage()
    {
        island.GetComponent<IslandScript>().HealthUpdate(-DPS);
        print("Ballon doing damage: " + DPS + " Time: " + Time.timeSinceLevelLoad);
    }

    void OnMouseDown()
    {
        dropItems();
    }

    void OnDestroy()
    {
        dropItems();
    }

    private void dropItems()
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

        if (!GameSettings.IsSpeedSlownessBuffOn)
        {
            float buffProbability = Random.Range(0f, 1f);
            print("buffProbability=" + buffProbability);

            if (buffProbability > GameSettings.SpeedSlownessBuffProbability)
                Instantiate((GameObject)Resources.Load("Prefabs/Buffs/SpeeedSlownessBuff"), gameObject.transform.position + Vector3.right, Quaternion.identity);
            else
                Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.right, Quaternion.identity);
        }

        Object[] explosionsObjects = Resources.LoadAll("Prefabs/Explosions");
        int randomExplosionIndex = Random.Range(0, explosionsObjects.Length - 1);

        explosion = Instantiate((GameObject)explosionsObjects[randomExplosionIndex], gameObject.transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
