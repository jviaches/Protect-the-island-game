using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandScript : MonoBehaviour {

    public int Health { get; set; }

    // Use this for initialization
    void Start () {
        Health = 10;// GameSettings.BaseIslandHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HealthUpdate(int healthAmount)
    {
        Health += healthAmount;
        GameObject.Find("bar_health_text").GetComponent<Text>().text = Health.ToString();
    }
}
