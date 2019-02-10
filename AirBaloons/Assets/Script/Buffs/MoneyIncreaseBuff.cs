using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIncreaseBuff : MonoBehaviour {

    private float activationTimer = 20; // stay active for 10 sec

    private GameSettings gameSettings;

    void Awake()
    {
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
    }

    void OnMouseDown()
    {
        gameSettings.IsMoneyIncreaseBuffOn = true;
        gameObject.transform.position = GameObject.Find("ItemsCollectionObject").transform.position;
    }

    void Update()
    {
        if (gameSettings.IsMoneyIncreaseBuffOn)
        {
            activationTimer -= Time.deltaTime;
            if (activationTimer <= 0.01f)
            {
                gameSettings.IsMoneyIncreaseBuffOn = false;
                Destroy(gameObject);
            }
        }
    }
}
