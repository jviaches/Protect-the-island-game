using Assets.Script.Levels;
using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private const float speed = 20f;
    private bool allowedToBeCollected;
    private bool isCointAlreadyCounted;
    private ILevel currLevel;

    public int Value = 1;   // by default value is 1 coin. Can be changed by buff or level money modifier.

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
        {
            if (GameSettings.IsMoneyIncreaseBuffOn)
                coinCounted(Value * 4);
            else
                coinCounted(Value);
        }
    }

    private void coinCounted(int bonus = 0)
    {
        GetComponent<AudioSource>().Play();

        //GetComponent<Animator>().SetBool("Pulse", true);
        //GetComponent<Animator>().PlayInFixedTime("Pulsation", 0, 1f);

        int coinsCalculated = (currLevel.MoneyGenerationModifier * Value) + bonus;
        print("Coins collected: "+ coinsCalculated);

        GameObject.Find("Player").GetComponent<PlayerScript>().CoinsUpdate(coinsCalculated);

        isCointAlreadyCounted = true;
        allowedToBeCollected = true;
    }

    private void destroyCoin()
    {
        if (!isCointAlreadyCounted)
        {
            if (GameSettings.IsMoneyIncreaseBuffOn)
                coinCounted(Value * GameSettings.MoneyIncreaseBuffMultiplayer);
            else
                coinCounted(Value);
        }
    }
}
