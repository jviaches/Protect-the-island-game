﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroZhouyu : MonoBehaviour
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

    void ActionStart() { }  //TODO: remove call from animation and deleted this function
    void ActionDone() { }   //TODO: remove call from animation and deleted this function


    private void detectCloseEnemy()
    {
        Collider[] colliders  = Physics.OverlapSphere(transform.position, enemyRadiusDetection);
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
                    GameObject obj = Instantiate(ultimateBullet);
                    LightBullet bullet = obj.GetComponent<LightBullet>();
                    bullet.player = transform;
                    bullet.target = enemyTarget.transform;
                    bullet.effectObj = damageEffect3;
                    bullet.bulleting();
                }
                if (damageEffect3 != null)
                {
                    GameObject obj = Instantiate(damageEffect3);
                    ParticlesEffect effect = obj.AddComponent<ParticlesEffect>();
                    effect.transform.position = enemyTarget.transform.position;
                    effect.play();
                    StartCoroutine(delayAttacked());
                }
                break;
        }
    }

    IEnumerator delayAttacked()
    {
        yield return new WaitForSeconds(1.5f);
        if (enemyTarget != null)
        {
            AttackedController c = enemyTarget.transform.GetComponent<AttackedController>();
            c.attacked();
        }
    }
}