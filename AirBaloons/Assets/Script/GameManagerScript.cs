﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    private PlayerScript player;

    void Start () {
        player = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Player")).GetComponent<PlayerScript>(); // TODO: load from file
        InvokeRepeating("generateBaloons", 0f, 2.5f);
	}
	
    private void generateBaloons()
    {
        var random = Random.onUnitSphere * 40; //Returns a random point on the surface of a sphere with radius 1
        Instantiate((GameObject)Resources.Load("Prefabs/Actors/Baloon" + Random.Range(1,3)), random, Quaternion.identity);
    }
}