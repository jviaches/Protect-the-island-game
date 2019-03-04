using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenWaveLoadingAbility : MonoBehaviour {

    private const  float baseAbilityLoadingDurationTimer = 40f;
    private float abilityLoadingDurationTimer = baseAbilityLoadingDurationTimer;

    private bool isReadyForActivation = true;

    private Image abilityLoadingProgress;
    private Button abilityButton;

    void Start () {

        abilityLoadingProgress = GameObject.Find("golden-wave-progress-image").GetComponent<Image>();
        abilityButton = GameObject.Find("GoldenWaveAbilityIcon").GetComponent<Button>();

        abilityButton.onClick.AddListener(() =>
        {
            if (isReadyForActivation)
            {
                GameObject island = GameObject.Find("Island");
                Instantiate((GameObject)Resources.Load("Prefabs/Abilities/GoldenWave"), island.transform.position, Quaternion.identity);
                isReadyForActivation = false;
            }
        });
    }
	
	void Update () {
        
        if (abilityLoadingDurationTimer <= 0.00001f)
            abilityLoadingDurationTimer = baseAbilityLoadingDurationTimer;

        abilityLoadingDurationTimer -= Time.deltaTime;

        if (!isReadyForActivation)
        {
            float percentLoading = 1 - (abilityLoadingDurationTimer / baseAbilityLoadingDurationTimer);
            abilityLoadingProgress.fillAmount = percentLoading;

            if (percentLoading >= 1)
                isReadyForActivation = true;
        }
    }
}
