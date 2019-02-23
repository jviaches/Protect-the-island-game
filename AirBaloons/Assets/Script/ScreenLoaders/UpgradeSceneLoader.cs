using Assets.Script.Settings;
using System;
using System.Collections;
using System.Linq;
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

        setupHeroDetails();
    }

    private void setupHeroDetails()
    {
        var daoHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Daochan);
        if (daoHero != null)
            GameObject.Find("info_Hero Daochan").GetComponent<Text>().text = "<b>Level</b> " + daoHero.Level +"          <b>Damage:</b> " + daoHero.Damage;
        else
            GameObject.Find("info_Hero Daochan").GetComponent<Text>().text = "<b>Level</b> 0          <b>Damage:</b> ---";


        var zhoHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhouyu);
        if (zhoHero != null)
            GameObject.Find("info_HeroZhouyu").GetComponent<Text>().text = "<b>Level</b> " + zhoHero.Level + "          <b>Damage:</b> " + zhoHero.Damage;
        else
            GameObject.Find("info_HeroZhouyu").GetComponent<Text>().text = "<b>Level</b> 0          <b>Damage:</b> ---";


        var zhaHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhangjiao);
        if (zhaHero != null)
            GameObject.Find("info_Hero Zhangjiao").GetComponent<Text>().text = "<b>Level</b> " + zhaHero.Level + "          <b>Damage:</b> " + zhaHero.Damage;
        else
            GameObject.Find("info_Hero Zhangjiao").GetComponent<Text>().text = "<b>Level</b> 0          <b>Damage:</b> ---";


        var zhuHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhugeliang);
        if (zhuHero != null)
            GameObject.Find("info_Hero Zhugeliang").GetComponent<Text>().text = "<b>Level</b> " + zhuHero.Level + "          <b>Damage:</b> " + zhuHero.Damage;
        else
            GameObject.Find("info_Hero Zhugeliang").GetComponent<Text>().text = "<b>Level</b> 0          <b>Damage:</b> ---";
    }
}

