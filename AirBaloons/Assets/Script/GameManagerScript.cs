using Assets.Script.Infra;
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
    private float levelTimer;

    private Text levelTimerText;

    private GameObject moneyBuffUI;
    private GameObject speedBuffUI;
    private GameObject levelCompletedDialog;
    private GameObject levelFailedDialog;

    private IslandScript islandScript;

    private Vector3 lastBornBalloonPosition = Vector3.zero;

    private bool isLevelSuccessfullyCompleted = false;
    private bool isLevelFailed = false;

    private GameSettings gameSettings;

    void Start () {

        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
        levelTimer = gameSettings.LevelSettings.LevelTimer;

        player = GameObject.Find("Player").GetComponent<PlayerScript>(); // TODO: load from file
        currLevel = gameSettings.LevelSettings.SelectedLevel;
        currLevel.CollectedCoins = 0;

        islandScript = GameObject.Find("Island").GetComponent<IslandScript>();
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

        Time.timeScale = 1;

        if (gameSettings.IsLevelGuidenceOn)
        {
            GameObject.Find("level-goal-notification").SetActive(true);
            Time.timeScale = 0.00001f;
            Invoke("hideLevelGoalNotification", 2f);
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

        InvokeRepeating("generateBaloons", 0f, gameSettings.BallonsGenerationFrequensy + currLevel.BaloonGenerationFrequencyModifier);
        InvokeRepeating("generateBonusPlanes", gameSettings.PlanesGenerationFrequensy + currLevel.PlaneGenerationFrequencyModifier,
                                               gameSettings.PlanesGenerationFrequensy + currLevel.PlaneGenerationFrequencyModifier);

        // create enemy after at certain amount of time
        for (int i = 0; i < currLevel.TimeActivationDic.Count; i++)
        {
            StartCoroutine(generateFloatableItems(currLevel.TimeActivationDic.ElementAt(0).Value, currLevel.TimeActivationDic.ElementAt(0).Key));
        }
    }

    private void generateBaloons()
    {
        //var random = UnityEngine.Random.onUnitSphere * GameSettings.BaloonsBornRadius; //Returns a random point on the surface of a sphere with radius 40
        //GameObject balloon = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1,3)), random, Quaternion.identity);

        GameObject balloon = Instantiate((GameObject)Resources.Load("Prefabs/Actors/Balloon" + UnityEngine.Random.Range(1, 3)));

        for (int i = 0; i < 10; i++)    // 10 attemtps to make sure, no previos location was generated
        {
            var random = UnityEngine.Random.Range(0, gameSettings.BornPoints.Count - 1);
            if (gameSettings.BornPoints.ElementAt(random).Value.x != lastBornBalloonPosition.x &&
                gameSettings.BornPoints.ElementAt(random).Value.y != lastBornBalloonPosition.y &&
                gameSettings.BornPoints.ElementAt(random).Value.z != lastBornBalloonPosition.z)
            {
                balloon.transform.position = gameSettings.BornPoints.ElementAt(random).Value;
                lastBornBalloonPosition = gameSettings.BornPoints.ElementAt(random).Value;
                break;
            }
        }
    }

    private void generateBonusPlanes()
    {
        GameObject plane = Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Plane8_1"));
        plane.transform.position = gameSettings.PlanesBornPosition;
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

            updatePlayerStats();
            GameObject.Find("fail_score_money").GetComponent<Text>().text = currLevel.CollectedCoins.ToString();

            stopLevel();
            return;
        }

        if (levelTimer <= 0.01f)
        {
            isLevelSuccessfullyCompleted = true;
            levelCompletedDialog.SetActive(true);

            gameSettings.SaveData();

            GameObject.Find("square_button_menu").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("LevelsScene");
            });

            GameObject.Find("square_button_repeat").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });

            GameObject.Find("square_button_play").GetComponent<Button>().onClick.AddListener(() =>
            {
                gameSettings.LevelSettings.PrepareNextLevel(currLevel.LevelIndex + 1);
                SceneManager.LoadScene("GameScene");
            });
            
            updatePlayerStats();
            GameObject.Find("succ_score_money").GetComponent<Text>().text = currLevel.CollectedCoins.ToString();

            stopLevel();
            return;
        }      

        // ------ UI updates -------
        levelTimerText.text = (int)levelTimer + " sec";

        // handle money multiplyer buff
        if (gameSettings.IsMoneyIncreaseBuffOn)
            moneyBuffUI.SetActive(true);
        else if(false == gameSettings.IsMoneyIncreaseBuffOn)
            moneyBuffUI.SetActive(false);

        // handle speed decrease buff
        if (gameSettings.IsSpeedSlownessBuffOn)
            speedBuffUI.SetActive(true);
        else if (false == gameSettings.IsSpeedSlownessBuffOn)
            speedBuffUI.SetActive(false);
    }

    private void updatePlayerStats()
    {
        //gameSettings.LevelSettings.Episode1Levels[currLevel] = player.Coins;

        //print("[Player Coins] = " + player.Coins + " [Level coins] =  " + gameSettings.LevelSettings.Episode1Levels[currLevel]);
        //player.Coins = 0;
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
