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

    private GameObject hero_zho_UI;
    private GameObject hero_dao_UI;
    private GameObject hero_zha_UI;
    private GameObject hero_zhu_UI;

    private GameObject hero_zhouyu;
    private GameObject hero_daochan;
    private GameObject hero_zhugeliang;
    private GameObject hero_zhangjiao;

    private GameObject levelCompletedDialog;
    private GameObject levelFailedDialog;

    private IslandScript islandScript;

    private Vector3 lastBornBalloonPosition = Vector3.zero;

    private bool isLevelSuccessfullyCompleted = false;
    private bool isLevelFailed = false;

    private GameSettings gameSettings;

    void Start()
    {
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        levelTimer = gameSettings.LevelSettings.LevelTimer;

        player = GameObject.Find("Player").GetComponent<PlayerScript>(); // TODO: load from file
        currLevel = gameSettings.LevelSettings.SelectedLevel;
        currLevel.CollectedCoins = 0;

        if (hero_zhouyu == null)
        {
            hero_zhouyu = GameObject.Find("hero_zhouyu");
            hero_zho_UI = GameObject.Find("hero_zho_UI");
        }

        if (hero_daochan == null)
        {
            hero_daochan = GameObject.Find("hero_daochan");
            hero_dao_UI = GameObject.Find("hero_dao_UI");
        }

        if (hero_zhugeliang == null)
        {
            hero_zhugeliang = GameObject.Find("hero_zhugeliang");
            hero_zhu_UI = GameObject.Find("hero_zhu_UI");
        }

        if (hero_zhangjiao == null)
        {
            hero_zhangjiao = GameObject.Find("hero_zhangjiao");
            hero_zha_UI = GameObject.Find("hero_zha_UI");
        }

        foreach (var hero in gameSettings.UpgradeSettings.HerosList)
        {
            if (!gameSettings.UpgradeSettings.PlayerHerosList.Exists(hr => hr.Hero == hero.Hero))
            {
                if (hero.Hero == Hero.Daochan)
                {
                    hero_daochan.SetActive(false);
                    hero_dao_UI.SetActive(false);
                }

                if (hero.Hero == Hero.Zhangjiao)
                {
                    hero_zhangjiao.SetActive(false);
                    hero_zha_UI.SetActive(false);
                }

                if (hero.Hero == Hero.Zhouyu)
                {
                    hero_zhouyu.SetActive(false);
                    hero_zho_UI.SetActive(false);
                }

                if (hero.Hero == Hero.Zhugeliang)
                {
                    hero_zhugeliang.SetActive(false);
                    hero_zhu_UI.SetActive(false);
                }
            }
            else
            {
                if (hero.Hero == Hero.Daochan)
                {
                    hero_daochan.SetActive(true);
                    hero_dao_UI.SetActive(true);
                }

                if (hero.Hero == Hero.Zhangjiao)
                {
                    hero_zhangjiao.SetActive(true);
                    hero_zha_UI.SetActive(true);
                }

                if (hero.Hero == Hero.Zhouyu)
                {
                    hero_zhouyu.SetActive(true);
                    hero_zho_UI.SetActive(true);
                }

                if (hero.Hero == Hero.Zhugeliang)
                {
                    hero_zhugeliang.SetActive(true);
                    hero_zhu_UI.SetActive(true);
                }
            }
        }

        gameSettings.SelectedHero = GameObject.Find("hero_zhouyu");

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

        InvokeRepeating("generateBaloons", 0f, gameSettings.BallonsGenerationFrequency + currLevel.BaloonGenerationFrequencyModifier);
        InvokeRepeating("generateBonusPlanes", gameSettings.PlanesGenerationFrequency + currLevel.PlaneGenerationFrequencyModifier,
                                               gameSettings.PlanesGenerationFrequency + currLevel.PlaneGenerationFrequencyModifier);

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
        gameSettings.SelectedEnemy = null;

        CancelInvoke("generateBaloons");
        CancelInvoke("generateBonusPlanes");

        BaloonScript[] ballons = FindObjectsOfType<BaloonScript>();

        for (int i = 0; i < ballons.Length; i++)
            Destroy(ballons[i].gameObject);

        BonusPlane[] planes = FindObjectsOfType<BonusPlane>();

        for (int i = 0; i < planes.Length; i++)
            Destroy(planes[i].gameObject);


        hero_zhouyu.SetActive(true);
        hero_daochan.SetActive(true);
        hero_zhugeliang.SetActive(true);
        hero_zhangjiao.SetActive(true);

        Time.timeScale = 0;
    }

    private IEnumerator generateFloatableItems(FloatItem floatItem, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        GameObject flingEnemy = Instantiate((GameObject)Resources.Load(floatItem.Prefab), floatItem.Location, Quaternion.LookRotation(islandScript.transform.position));
        flingEnemy.transform.Rotate(-90, 0, -180);
    }
}
