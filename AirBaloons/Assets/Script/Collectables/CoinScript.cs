using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();

        PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
        player.CoinsUpdate(1);

        Invoke("destroyCoin", 0.07f);
        
    }

    private void destroyCoin()
    {
        Destroy(gameObject);
    }
}
