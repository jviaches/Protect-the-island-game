using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenWaveAbility : MonoBehaviour {

    private float abilityTimer = 3f;

	void Start () {

    }
	
	void Update () {

        if (abilityTimer <= 0.00001f)
            Destroy(gameObject);

        abilityTimer -= Time.deltaTime;
        gameObject.transform.localScale += new Vector3(0.8f, 0.8f, 0.8f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
            Destroy(other.gameObject);
    }
}
