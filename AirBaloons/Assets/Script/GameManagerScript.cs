using Assets.Script.Levels;
using Assets.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    private PlayerScript player;

    private ILevel currLevel;
    private float levelTimer = LevelSettings.LevelTimer;
    private Text levelTimerText;

    void Start () {

        Time.timeScale = 0.00001f;
        Invoke("showLevelGoalNotification", 0.00002f);
        
        player = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Player")).GetComponent<PlayerScript>(); // TODO: load from file
        currLevel = LevelSettings.GetCurrentLevel();

        levelTimerText = GameObject.Find("bar_timer_text").GetComponent<Text>();

        InvokeRepeating("generateBaloons", 0f, GameSettings.BallonsGenerationFrequensy * currLevel.BaloonGenerationFrequencyModifier);
        InvokeRepeating("generateBonusPlanes", 0f, GameSettings.PlanesGenerationFrequensy * currLevel.PlaneGenerationFrequencyModifier);
    }

    private void showLevelGoalNotification()
    {
        GameObject.Find("level-goal-notification").SetActive(false);
        Time.timeScale = 1f;
    }

    private void generateBaloons()
    {
        var random = UnityEngine.Random.onUnitSphere * GameSettings.BornRadius; //Returns a random point on the surface of a sphere with radius 40
        Instantiate((GameObject)Resources.Load("Prefabs/Actors/Baloon" + UnityEngine.Random.Range(1,3)), random, Quaternion.identity);
    }

    private void generateBonusPlanes()
    {
        GameObject plane = Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Plane8_1"));
        plane.transform.position = GameSettings.PlanesBornPosition;
    }

    void Update()
    {
        levelTimer -= Time.deltaTime;
        if (levelTimer <= 0.01f )
            SceneManager.LoadScene("LevelsScene");

        levelTimerText.text = (int)levelTimer + " sec";
    }
}
