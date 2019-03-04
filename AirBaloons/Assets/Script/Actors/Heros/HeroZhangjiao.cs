using Assets.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroZhangjiao : MonoBehaviour
{
    public GameObject attackBullet;
    public GameObject magicBullet;
    public GameObject magic2Bullet;
    public GameObject ultimateBullet;
    public GameObject rbullet;
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

        var zhaHero = gameSettings.UpgradeSettings.PlayerHerosList.FirstOrDefault(hero => hero.Hero == Hero.Zhangjiao);
        if (zhaHero != null)
        {
            GameObject.Find("hero-zha-image-lvl").GetComponent<Text>().text = zhaHero.Level.ToString();

            heroUIElement = GameObject.Find("hero_zha_UI");
            heroUIElement.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameSettings.SelectedHero = GameObject.Find("hero_zhangjiao");
            });
        }
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
        {

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
                        bullet.effectObj = damageEffect1;
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

                    StartCoroutine(delayBullet());
                    StartCoroutine(delayBullet1());
                    break;

                case AnimationName.Ultimate:
                    if (ultimateBullet != null)
                    {
                        GameObject obj = GameObject.Instantiate(ultimateBullet);
                        LightBullet bullet = obj.GetComponent<LightBullet>();
                        bullet.player = transform;
                        bullet.target = enemyTarget.transform;
                        bullet.effectObj = damageEffect3;
                        bullet.bulleting();
                    }

                    break;
            }
        }
    }

    IEnumerator delayBullet()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(rbullet);
            RotateBullet bullet = obj.GetComponent<RotateBullet>();
            bullet.player = transform;
            bullet.effectObj = damageEffect1;
            bullet.bulleting();
            bullet.y = 0.35f;

            yield return new WaitForSeconds(0.1f);

            if (i % 9 == 0)
            {
                AttackedController c = enemyTarget.GetComponent<AttackedController>();
                c.attacked();
                if (damageEffect2 != null)
                {
                    GameObject obj1 = Instantiate(damageEffect2);
                    ParticlesEffect effect = obj1.AddComponent<ParticlesEffect>();
                    Transform target = enemyTarget.transform;
                    effect.transform.position = MathUtil.findChild(target, "attackedPivot").position;
                    effect.play();
                }
            }
        }
    }

    IEnumerator delayBullet1()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = GameObject.Instantiate(rbullet);
            RotateBullet bullet = obj.GetComponent<RotateBullet>();
            bullet.player = transform;
            bullet.effectObj = damageEffect1;
            bullet.bulleting();
            bullet.y = 0.7f;
            bullet.flag = -1f;

            yield return new WaitForSeconds(0.1f);

            if (i % 9 == 0)
            {
                AttackedController c = enemyTarget.GetComponent<AttackedController>();
                c.attacked();
                if (damageEffect2 != null)
                {
                    GameObject obj1 = Instantiate(damageEffect2);
                    ParticlesEffect effect = obj1.AddComponent<ParticlesEffect>();
                    Transform target = enemyTarget.transform;
                    effect.transform.position = MathUtil.findChild(target, "attackedPivot").position;
                    effect.play();
                }
            }
        }
    }
}