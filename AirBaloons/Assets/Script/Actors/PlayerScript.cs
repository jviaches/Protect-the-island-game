using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    private int coins;
    public int Coins { get { return coins; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CoinsUpdate(int coinsAmount)
    {
        coins += coinsAmount;
        GameObject.Find("bar_coins_text").GetComponent<Text>().text = Coins.ToString();
    }
}
