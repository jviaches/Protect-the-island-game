using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Animator animator;

    private float enemyRadiusDetection = 45f;   // TODO: pull from GameSettings
    private GameObject enemyTarget;
    private bool isLockedOnEnemy = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLockedOnEnemy || enemyTarget == null)
            detectCloseEnemy();
    }

    void ActionStart() { }  //TODO: remove call from animation and deleted this function
    void ActionDone() { }   //TODO: remove call from animation and deleted this function

    private void detectCloseEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyRadiusDetection);
        if (colliders.Length > 0 && colliders[0].tag == "enemy")
        {
            isLockedOnEnemy = true;
            enemyTarget = colliders[0].gameObject;
            animator.SetBool("setAttack", true);
        }
        else
        {
            isLockedOnEnemy = false;
            enemyTarget = null;
            animator.SetBool("setAttack", false);
        }
    }

    void preAction(string actionName)
    {
        if (enemyTarget == null || isLockedOnEnemy == false)
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