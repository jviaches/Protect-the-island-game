using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSlownessBuff : MonoBehaviour {

    private float activationTimer = 20; // stay active for 10 sec

    void OnMouseDown()
    {
        GameSettings.IsSpeedSlownessBuffOn = true;
        gameObject.transform.position = GameObject.Find("ItemsCollectionObject").transform.position;

        updateBaloonSpeed();
    }

    void Update()
    {
        updateBaloonSpeed();

        if (GameSettings.IsSpeedSlownessBuffOn)
        {
            activationTimer -= Time.deltaTime;
            if (activationTimer <= 0.01f)
            {
                GameSettings.IsSpeedSlownessBuffOn = false;                
                Destroy(gameObject);
            }
        }
    }

    private void updateBaloonSpeed()
    {
        // update spped of all existing planes
        BaloonScript[] baloons = GameObject.FindObjectsOfType<BaloonScript>();
        for (int i = 0; i < baloons.Length; i++)
        {
            baloons[i].Speed = GameSettings.BaloonsSpeed;
        }
    }
}
