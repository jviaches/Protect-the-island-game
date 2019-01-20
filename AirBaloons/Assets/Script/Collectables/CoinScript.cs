using Assets.Script.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private const float speed = 20f;
    private bool allowedToBeCollected;
    private bool isCointAlreadyCounted;
    private ILevel currLevel;

    private GameObject itemsCollectedObject;

    void Start()
    {
        itemsCollectedObject = GameObject.Find("ItemsCollectionObject");
        currLevel = LevelSettings.GetCurrentLevel();
    }

    void Update()
    {
        if (allowedToBeCollected)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, itemsCollectedObject.transform.position, step);
        }
        else
        {
            Invoke("destroyCoin", 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "ItemsCollectionObject")
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (!isCointAlreadyCounted)
            coinCounted(1);
    }

    private void coinCounted(int bonus = 0)
    {
        GetComponent<AudioSource>().Play();

        //GetComponent<Animator>().SetBool("Pulse", true);
        //GetComponent<Animator>().PlayInFixedTime("Pulsation", 0, 1f);
        int coinsCalculated = currLevel.MoneyGenerationModifier + bonus;


        GameObject.Find("Player").GetComponent<PlayerScript>().CoinsUpdate(coinsCalculated);

        isCointAlreadyCounted = true;
        allowedToBeCollected = true;
    }

    private void destroyCoin()
    {
        if (!isCointAlreadyCounted)
            coinCounted();
    }
}
