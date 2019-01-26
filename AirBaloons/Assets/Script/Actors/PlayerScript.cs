using Assets.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public int Coins { get; set; }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CoinsUpdate(int coinsAmount)
    {
        Coins += coinsAmount;
        GameObject.Find("bar_coins_text").GetComponent<Text>().text = Coins.ToString();
    }
}
