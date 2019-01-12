using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    void OnMouseDown()
    {
        PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();

        player.Coins += 1;
        
        Destroy(gameObject);
    }
}
