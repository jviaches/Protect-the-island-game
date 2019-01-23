using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIncreaseBuff : MonoBehaviour {

    private float activationTimer = 20; // stay active for 10 sec

    void OnMouseDown()
    {
        GameSettings.IsMoneyIncreaseBuffOn = true;
        gameObject.transform.position = GameObject.Find("ItemsCollectionObject").transform.position;
    }

    void Update()
    {
        if (GameSettings.IsMoneyIncreaseBuffOn)
        {
            activationTimer -= Time.deltaTime;
            if (activationTimer <= 0.01f)
            {
                GameSettings.IsMoneyIncreaseBuffOn = false;
                Destroy(gameObject);
            }
        }
    }
}
