﻿using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandScript : MonoBehaviour {

    public float Health { get; set; }

    void Start () {
        Health = GameSettings.BaseIslandHealth;
    }
	
    public void HealthUpdate(int healthAmount)
    {
        Health += healthAmount;
        GameObject.Find("bar_health_text").GetComponent<Text>().text = Health.ToString();
        GameObject.Find("health_fill").GetComponent<Image>().fillAmount = Health / GameSettings.BaseIslandHealth;
    }
}
