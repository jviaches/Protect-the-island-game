using Assets.Script.Settings;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class UpgradeSceneLoader : MonoBehaviour
{
    private Button exitButton;
    
    private Button upgradeZhoHeroButton;
    private Button upgradeDaoHeroButton;
    private Button upgradeZhuHeroButton;
    private Button upgradeZhaHeroButton;

    private string ActiveHeroItem = "Graphics/universal_slot";
    private string InActiveHeroItem = "Graphics/button_simple_inactive";

    private string activeButton = "Graphics/button_medium_o";
    private string inActiveButton = "Graphics/button_medium_inactive";

    private GameSettings gameSettings;
    private UpgradeSettings upgradeSettings;

    void Start()
    {
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
        upgradeSettings = GameObject.Find("Settings").GetComponent<UpgradeSettings>();

        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("LevelsScene"));

        upgradeZhoHeroButton = GameObject.Find("medium_button_zho").GetComponent<Button>();

        // disable upgrade in current version
        if (gameSettings.UpgradeSettings.PlayerHerosList.Find(hero => hero.Hero == Hero.Zhouyu) == null)
        {
            upgradeZhoHeroButton.onClick.AddListener(() =>
            {
                gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhouyu);
                setupHeroDetails();
                gameSettings.SaveData();
            });
        }
        else
        {
            upgradeZhoHeroButton.enabled = false;
            upgradeZhoHeroButton.interactable = false;
            GameObject.Find("medium_button_zho").GetComponent<Image>().sprite = Resources.Load<Sprite>(inActiveButton);  
        }

        upgradeDaoHeroButton = GameObject.Find("medium_button_dao").GetComponent<Button>();

        if (gameSettings.UpgradeSettings.PlayerHerosList.Find(hero => hero.Hero == Hero.Daochan) == null)
        {
            upgradeDaoHeroButton.onClick.AddListener(() =>
            {
                gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Daochan);
                setupHeroDetails();
                gameSettings.SaveData();

                GameObject.Find("medium_button_dao").GetComponent<Image>().sprite = Resources.Load<Sprite>(inActiveButton);
                upgradeDaoHeroButton.enabled = false;
                upgradeDaoHeroButton.interactable = false;                
            });

            upgradeDaoHeroButton.enabled = true;
            upgradeDaoHeroButton.interactable = true;
            GameObject.Find("medium_button_dao").GetComponent<Image>().sprite = Resources.Load<Sprite>(activeButton);
        }
        else
        {
            upgradeDaoHeroButton.enabled = false;
            upgradeDaoHeroButton.interactable = false;
            GameObject.Find("medium_button_dao").GetComponent<Image>().sprite = Resources.Load<Sprite>(inActiveButton);
        }

        upgradeZhuHeroButton = GameObject.Find("medium_button_zhu").GetComponent<Button>();

        if (gameSettings.UpgradeSettings.PlayerHerosList.Find(hero => hero.Hero == Hero.Zhugeliang) == null)
        {
            upgradeZhuHeroButton.onClick.AddListener(() =>
            {
                gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhugeliang);
                setupHeroDetails();
                gameSettings.SaveData();
            });

            upgradeZhuHeroButton.enabled = true;
            upgradeZhuHeroButton.interactable = true;
            GameObject.Find("medium_button_zhu").GetComponent<Image>().sprite = Resources.Load<Sprite>(activeButton);
        }
        else
        {
            upgradeZhuHeroButton.enabled = false;
            upgradeZhuHeroButton.interactable = false;
            GameObject.Find("medium_button_zhu").GetComponent<Image>().sprite = Resources.Load<Sprite>(inActiveButton);
        }

        upgradeZhaHeroButton = GameObject.Find("medium_button_zha").GetComponent<Button>();

        if (gameSettings.UpgradeSettings.PlayerHerosList.Find(hero => hero.Hero == Hero.Zhangjiao) == null)
        {
            upgradeZhaHeroButton.onClick.AddListener(() =>
            {
                gameSettings.UpgradeSettings.UpgradeHeroNextLevel(Hero.Zhangjiao);
                setupHeroDetails();
                gameSettings.SaveData();
            });

            upgradeZhaHeroButton.enabled = true;
            upgradeZhaHeroButton.interactable = true;
            GameObject.Find("medium_button_zha").GetComponent<Image>().sprite = Resources.Load<Sprite>(activeButton);
        }
        else
        {
            upgradeZhaHeroButton.enabled = false;
            upgradeZhaHeroButton.interactable = false;
            GameObject.Find("medium_button_zha").GetComponent<Image>().sprite = Resources.Load<Sprite>(inActiveButton);
        }

        gameSettings.LoadData();
        setupHeroDetails();
    }

    private void setupHeroDetails()
    {
        var daoHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Daochan);
        if (daoHero != null)
        {
            GameObject.Find("info_Hero Daochan").GetComponent<Text>().text = "<b>Level</b> " + daoHero.Level + "          <b>Damage:</b> " + daoHero.Damage;
            GameObject.Find("descr_HeroDaochan").GetComponent<Text>().text = daoHero.Description;
            GameObject.Find("item_Hero Daochan").GetComponent<Image>().sprite = Resources.Load<Sprite>(ActiveHeroItem);
        }
        else
        {
            GameObject.Find("info_Hero Daochan").GetComponent<Text>().text = "<b>Level</b> 1          <b>Damage:</b> " + gameSettings.UpgradeSettings.HerosList.First(hero => hero.Hero == Hero.Daochan).Damage;
            GameObject.Find("descr_HeroDaochan").GetComponent<Text>().text = "Unlock to use";
            GameObject.Find("item_Hero Daochan").GetComponent<Image>().sprite = Resources.Load<Sprite>(InActiveHeroItem);
        }

        var zhoHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhouyu);
        if (zhoHero != null)
        {
            GameObject.Find("info_HeroZhouyu").GetComponent<Text>().text = "<b>Level</b> " + zhoHero.Level + "          <b>Damage:</b> " + zhoHero.Damage;
            GameObject.Find("descr_HeroZhouyu").GetComponent<Text>().text = zhoHero.Description;
            GameObject.Find("item_HeroZhouyu").GetComponent<Image>().sprite = Resources.Load<Sprite>(ActiveHeroItem);
        }
        else
        {
            GameObject.Find("info_HeroZhouyu").GetComponent<Text>().text = "<b>Level</b> 1          <b>Damage:</b> " + gameSettings.UpgradeSettings.HerosList.First(hero => hero.Hero == Hero.Zhouyu).Damage; 
            GameObject.Find("descr_HeroZhouyu").GetComponent<Text>().text = "Unlock to use";
            GameObject.Find("item_HeroZhouyu").GetComponent<Image>().sprite = Resources.Load<Sprite>(InActiveHeroItem);
            
        }

        var zhaHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhangjiao);
        if (zhaHero != null)
        {
            GameObject.Find("info_Hero Zhangjiao").GetComponent<Text>().text = "<b>Level</b> " + zhaHero.Level + "          <b>Damage:</b> " + zhaHero.Damage;
            GameObject.Find("descr_HeroZhangjiao").GetComponent<Text>().text = zhaHero.Description;
            GameObject.Find("item_Hero Zhangjiao").GetComponent<Image>().sprite = Resources.Load<Sprite>(ActiveHeroItem);
        }
        else
        {
            GameObject.Find("info_Hero Zhangjiao").GetComponent<Text>().text = "<b>Level</b> 1          <b>Damage:</b> " + gameSettings.UpgradeSettings.HerosList.First(hero => hero.Hero == Hero.Zhangjiao).Damage;
            GameObject.Find("descr_HeroZhangjiao").GetComponent<Text>().text = "Unlock to use";
            GameObject.Find("item_Hero Zhangjiao").GetComponent<Image>().sprite = Resources.Load<Sprite>(InActiveHeroItem);
        }

        var zhuHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhugeliang);
        if (zhuHero != null)
        {
            GameObject.Find("info_Hero Zhugeliang").GetComponent<Text>().text = "<b>Level</b> " + zhuHero.Level + "          <b>Damage:</b> " + zhuHero.Damage;
            GameObject.Find("descr_HeroZhugeliang").GetComponent<Text>().text = zhuHero.Description;
            GameObject.Find("item_Hero Zhugeliang").GetComponent<Image>().sprite = Resources.Load<Sprite>(ActiveHeroItem);
        }
        else
        {
            GameObject.Find("info_Hero Zhugeliang").GetComponent<Text>().text = "<b>Level</b> 1          <b>Damage:</b> " + gameSettings.UpgradeSettings.HerosList.First(hero => hero.Hero == Hero.Zhugeliang).Damage;
            GameObject.Find("descr_HeroZhugeliang").GetComponent<Text>().text = "Unlock to use";
            GameObject.Find("item_Hero Zhugeliang").GetComponent<Image>().sprite = Resources.Load<Sprite>(InActiveHeroItem);
        }
    }
}

