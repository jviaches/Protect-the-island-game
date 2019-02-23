using Assets.Script.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSceneLoader : MonoBehaviour
{
    private Button exitButton;
    
    private Button upgradeZhoHeroButton;
    private Button upgradeDaoHeroButton;
    private Button upgradeZhuHeroButton;
    private Button upgradeZhaHeroButton;

    private GameSettings gameSettings;
    private UpgradeSettings upgradeSettings;

    void Start()
    {
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
        upgradeSettings = GameObject.Find("Settings").GetComponent<UpgradeSettings>();

        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("LevelsScene"));

        upgradeZhoHeroButton = GameObject.Find("medium_button_zho").GetComponent<Button>();
        upgradeZhoHeroButton.onClick.AddListener(() => gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhouyu));

        upgradeDaoHeroButton = GameObject.Find("medium_button_dao").GetComponent<Button>();
        upgradeDaoHeroButton.onClick.AddListener(() => gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Daochan));

        upgradeZhuHeroButton = GameObject.Find("medium_button_zhu").GetComponent<Button>();
        upgradeZhuHeroButton.onClick.AddListener(() => gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhugeliang));

        upgradeZhaHeroButton = GameObject.Find("medium_button_zha").GetComponent<Button>();
        upgradeZhaHeroButton.onClick.AddListener(() => gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhangjiao));
    }
}
