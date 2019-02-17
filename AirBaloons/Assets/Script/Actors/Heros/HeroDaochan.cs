using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDaochan : MonoBehaviour
{
    public GameObject attackBullet;
    public GameObject magicBullet;
    public GameObject magic2Bullet;
    public GameObject ultimateBullet;
    public GameObject damageEffect1;
    public GameObject damageEffect2;
    public GameObject damageEffect3;

    private Animator animator;

    private float enemyRadiusDetection = 45f;   // TODO: pull from GameSettings
    public GameObject enemyTarget;
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
                    StartCoroutine(delayBullet());
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
                if (ultimateBullet != null)
                {
                    GameObject obj = GameObject.Instantiate(ultimateBullet);
                    LightBullet bullet = obj.GetComponent<LightBullet>();
                    bullet.player = transform;
                    bullet.target = enemyTarget.transform;
                    bullet.effectObj = damageEffect3;
                    bullet.bulleting();
                }
                if (damageEffect3 != null)
                {
                    GameObject obj1 = Instantiate(damageEffect3);
                    ParticlesEffect effect = obj1.AddComponent<ParticlesEffect>();
                    Transform target = enemyTarget.transform;
                    effect.transform.position = MathUtil.findChild(target, "attackedPivot").position;
                    effect.play();
                }
                break;
        }
    }

    IEnumerator delayBullet()
    {
        if (enemyTarget != null)
        {
            int count = 20;
            float angle = -count / 2f * 5f;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(magicBullet);

                PosBullet bullet = obj.GetComponent<PosBullet>();
                bullet.player = transform;
                bullet.tarPos = MathUtil.calcTargetPosByRotation(transform, angle + i * 5f, 10f);
                bullet.effectObj = damageEffect1;
                bullet.bulleting();

                yield return new WaitForSeconds(0.01f);

                if (i % 9 == 0)
                {
                    AttackedController c = enemyTarget.GetComponent<AttackedController>();
                    c.attacked();
                    if (damageEffect2 != null)
                    {
                        GameObject obj1 = GameObject.Instantiate(damageEffect2);
                        ParticlesEffect effect = obj1.AddComponent<ParticlesEffect>();
                        Transform target = enemyTarget.transform;
                        effect.transform.position = MathUtil.findChild(target, "attackedPivot").position;
                        effect.play();
                    }
                }
            }
        }
    }

    void ActionStart() { }  //TODO: remove call from animation and deleted this function
    void ActionDone() { }   //TODO: remove call from animation and deleted this function

}
