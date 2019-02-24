using Assets.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroZhugeliang : MonoBehaviour
{
    public GameObject attackBullet;
    public GameObject magicBullet;
    public GameObject magic2Bullet;
    public GameObject ultimateBullet;
    public GameObject damageEffect1;
    public GameObject damageEffect2;
    public GameObject damageEffect3;

    private GameObject enemyTarget;
    private GameObject heroUIElement;
    private Animator animator;
    private GameSettings gameSettings;

    void Awake()
    {
        animator = GetComponent<Animator>();
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        heroUIElement = GameObject.Find("hero_zhu_UI");

        heroUIElement.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameSettings.SelectedHero = GameObject.Find("hero_zhugeliang");
        });
    }

    void OnDisable()
    {
        gameSettings.EnemySelected -= GameSettings_EnemySelected;
        gameSettings.HeroSelected -= GameSettings_HeroSelected;
    }

    void OnEnable()
    {
        gameSettings.EnemySelected += GameSettings_EnemySelected;
        gameSettings.HeroSelected += GameSettings_HeroSelected;
    }

    private void GameSettings_HeroSelected(object sender, EventArgs e)
    {
        if (gameSettings.SelectedHero == gameObject)
            heroUIElement.GetComponent<Image>().enabled = true;
        else
            heroUIElement.GetComponent<Image>().enabled = false;
    }

    private void GameSettings_EnemySelected(object sender, EventArgs e)
    {
        if (gameSettings.SelectedHero == gameObject)
            enemyTarget = gameSettings.SelectedEnemy;
    }

    void Update()
    {
        if (enemyTarget != null && enemyTarget.tag == "enemy")
        {
            animator.SetBool("setAttack", true);
        }
        else
        {
            enemyTarget = null;
            animator.SetBool("setAttack", false);
        }
    }

    void ActionStart() { }  //TODO: remove call from animation and deleted this function
    void ActionDone() { }   //TODO: remove call from animation and deleted this function

    void preAction(string actionName)
    {
        if (enemyTarget == null)
            return;

        AttackedController c = enemyTarget.GetComponent<AttackedController>();
        string[] arr = actionName.Split('|');
        string name = arr[0];
        switch (name)
        {
            case AnimationName.Attack:
                if (attackBullet != null)
                {
                    GameObject obj = Instantiate(attackBullet);
                    NormalBullet bullet = obj.GetComponent<NormalBullet>();
                    bullet.player = transform;
                    bullet.target = enemyTarget.transform;
                    bullet.effectObj = damageEffect1;
                    bullet.bulleting();
                }
                break;

            case AnimationName.Magic:
                if (magicBullet != null)
                {
                    GameObject obj = Instantiate(magicBullet);
                    NormalBullet bullet = obj.GetComponent<NormalBullet>();
                    bullet.player = transform;
                    bullet.target = enemyTarget.transform;
                    bullet.effectObj = damageEffect2;
                    bullet.bulleting();
                }

                StartCoroutine(delayBullet());
                StartCoroutine(delayBullet1());

                break;
            case AnimationName.Magic2:
                if (magic2Bullet != null)
                {
                    GameObject obj = Instantiate(magic2Bullet);
                    NormalBullet bullet = obj.GetComponent<NormalBullet>();
                    bullet.player = transform;
                    bullet.target = enemyTarget.transform;
                    bullet.effectObj = damageEffect2;
                    bullet.bulleting();
                }
                break;
            case AnimationName.Ultimate:
                if (damageEffect3 != null)
                {
                    GameObject obj1 = Instantiate(damageEffect3);
                    ParticlesEffect effect = obj1.AddComponent<ParticlesEffect>();
                    Transform target = enemyTarget.transform;
                    effect.transform.position = MathUtil.findChild(target, "attackedPivot").position;
                    effect.play();
                }
                c.attacked();
                break;
        }
    }

    IEnumerator delayBullet()
    {
        AttackedController c = enemyTarget.GetComponent<AttackedController>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(ultimateBullet);
            xuanzhuanBullet bullet = obj.GetComponent<xuanzhuanBullet>();
            bullet.player = transform;
            bullet.effectObj = damageEffect1;
            bullet.target = c.transform;
            bullet.bulleting();

            yield return new WaitForSeconds(0.1f);
            if (i % 9 == 0)
                c.attacked();
        }
    }

    IEnumerator delayBullet1()
    {
        AttackedController c = enemyTarget.GetComponent<AttackedController>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(ultimateBullet);
            xuanzhuanBullet bullet = obj.GetComponent<xuanzhuanBullet>();
            bullet.player = transform;
            bullet.effectObj = damageEffect1;
            bullet.target = c.transform;
            bullet.flag = -1f;
            bullet.bulleting();

            yield return new WaitForSeconds(0.1f);
            if (i % 9 == 0)
                c.attacked();
        }
    }
}