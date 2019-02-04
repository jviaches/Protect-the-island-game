﻿using Assets.Script.Infra;
using Assets.Script.Levels;
using Assets.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    private PlayerScript player;

    private ILevel currLevel;
    private float levelTimer = LevelSettings.LevelTimer;

    private Text levelTimerText;

    private GameObject moneyBuffUI;
    private GameObject speedBuffUI;
    private GameObject levelCompletedDialog;
    private GameObject levelFailedDialog;

    private IslandScript islandScript;

    private Vector3 lastBornBalloonPosition = Vector3.zero;

    private bool isLevelSuccessfullyCompleted = false;
    private bool isLevelFailed = false;

    void Start () {

        player = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Player")).GetComponent<PlayerScript>(); // TODO: load from file
        currLevel = LevelSettings.GetCurrentLevel();

        islandScript = GameObject.Find("IslandShield").GetComponent<IslandScript>();
        GameObject.Find("bar_health_text").GetComponent<Text>().text = islandScript.Health.ToString();

        levelTimerText = GameObject.Find("bar_timer_text").GetComponent<Text>();

        moneyBuffUI = GameObject.Find("money-buff");
        GameObject.Find("money-buff").SetActive(false);

        speedBuffUI = GameObject.Find("speed-buff");
        GameObject.Find("speed-buff").SetActive(false);

        levelCompletedDialog = GameObject.Find("level_completed_Canvas");
        levelCompletedDialog.SetActive(false);

        levelFailedDialog = GameObject.Find("level_failed_Canvas");
        levelFailedDialog.SetActive(false);

        if (!GameSettings.isTutotrialOn)
        {
            Time.timeScale = 0.00001f;
            Invoke("hideLevelGoalNotification", 0.00002f);
        }
        else
        {
            hideLevelGoalNotification();
        }      
    }

    private void hideLevelGoalNotification()
    {
        GameObject.Find("level-goal-notification").SetActive(false);
        Time.timeScale = 1f;

        InvokeRepeating("generateBaloons", 0f, GameSettings.BallonsGenerationFrequensy + currLevel.BaloonGenerationFrequencyModifier);
        InvokeRepeating("generateBonusPlanes", GameSettings.PlanesGenerationFrequensy + currLevel.PlaneGenerationFrequencyModifier, 
                                               GameSettings.PlanesGenerationFrequensy + currLevel.PlaneGenerationFrequencyModifier);

        // create enemy after at certain amount of time
        //for (int i = 0; i < currLevel.TimeActivationDic.Count; i++)
        //{
        //    StartCoroutine(generateFloatableItems(currLevel.TimeActivationDic.ElementAt(i).Value, currLevel.TimeActivationDic.ElementAt(i).Key));
        //}
    }

    private void generateBaloons()
    {
        //var random = UnityEngine.Random.onUnitSphere * GameSettings.BaloonsBornRadius; //Returns a random point on the surface of a sphere with radius 40
        //GameObject balloon = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1,3)), random, Quaternion.identity);
        //balloon.transform.Rotate(-90, 0, 0);

        GameObject balloon = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3)));

        for (int i = 0; i < 10; i++)    // 10 attemtps to make sure, no previos location was generated
        {
            var random = UnityEngine.Random.Range(0, GameSettings.BalloonsBornPositions.Length - 1);
            if (GameSettings.BalloonsBornPositions[random].x != lastBornBalloonPosition.x &&
                GameSettings.BalloonsBornPositions[random].y != lastBornBalloonPosition.y &&
                GameSettings.BalloonsBornPositions[random].z != lastBornBalloonPosition.z)
            {
                balloon.transform.position = GameSettings.BalloonsBornPositions[random];
                lastBornBalloonPosition = GameSettings.BalloonsBornPositions[random];
                break;
            }
        }
    }

    private void generateBonusPlanes()
    {
        GameObject plane = Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Plane8_1"));
        plane.transform.position = GameSettings.PlanesBornPosition;
    }

    void Update()
    {

        if (isLevelSuccessfullyCompleted || isLevelFailed)
            return;
        
        levelTimer -= Time.deltaTime;

        if (islandScript.Health <= 0)
        {
            isLevelFailed = true;
            levelFailedDialog.SetActive(true);

            GameObject.Find("square_button_fail_levels").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LevelsScene"));
            GameObject.Find("square_button_fail_repeat").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("GameScene"));

            stopLevel();
        }
       
        if (levelTimer <= 0.01f)
        {
            isLevelSuccessfullyCompleted = true;
            levelCompletedDialog.SetActive(true);

            GameObject.Find("square_button_menu").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("LevelsScene"));
            GameObject.Find("square_button_repeat").GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("GameScene"));

            GameObject.Find("square_button_play").GetComponent<Button>().onClick.AddListener(() =>
            {
                LevelSettings.NextLevel(LevelSettings.GetCurrentLevel().LevelIndex);
                SceneManager.LoadScene("LevelsScene");
            });

            stopLevel();
        }      

        // ------ UI updates -------

        levelTimerText.text = (int)levelTimer + " sec";

        // handle money multiplyer buff
        if (GameSettings.IsMoneyIncreaseBuffOn)
            moneyBuffUI.SetActive(true);
        else if(false == GameSettings.IsMoneyIncreaseBuffOn)
            moneyBuffUI.SetActive(false);

        // handle speed decrease buff
        if (GameSettings.IsSpeedSlownessBuffOn)
            speedBuffUI.SetActive(true);
        else if (false == GameSettings.IsSpeedSlownessBuffOn)
            speedBuffUI.SetActive(false);
    }

    private void stopLevel()
    {
        CancelInvoke("generateBaloons");
        CancelInvoke("generateBonusPlanes");

        BaloonScript[] ballons = FindObjectsOfType<BaloonScript>();

        for (int i = 0; i < ballons.Length; i++)
            Destroy(ballons[i].gameObject);

        BonusPlane[] planes = FindObjectsOfType<BonusPlane>();

        for (int i = 0; i < planes.Length; i++)
            Destroy(planes[i].gameObject);

        Time.timeScale = 0;
    }

    private IEnumerator generateFloatableItems(FloatItem floatItem, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GameObject flingEnemy = Instantiate((GameObject)Resources.Load(floatItem.Prefab), floatItem.Location, Quaternion.LookRotation(islandScript.transform.position));
        flingEnemy.transform.Rotate(-90, 0, -180);
    }
}
