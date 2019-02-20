using Assets.Script.Actors;
using Assets.Script.Settings;
using System.Collections;
using UnityEngine;

public class ZeppelinScript : MonoBehaviour, IEnemy
{
    private GameObject island;
    private GameObject explosion;
    private Vector3 originalScale;
    private float step;
    private bool? isClicked = null;

    private GameSettings gameSettings;

    public float Speed;

    public float DPS { get { return 20; } }

    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (Health <= 0)
                dropItems();
        }
    }

    public bool IsIslandEngaged = false;

    void Start ()
    {
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();

        island = GameObject.Find("Island");
        step = Speed * Time.deltaTime;
        Health = gameSettings.ZeppelinHealth;
        Speed = gameSettings.ZeppelinSpeed;

        originalScale = gameObject.transform.localScale;
        transform.rotation = Quaternion.Euler(Vector3.zero);        
    }
    void Update()
    {
        if (!IsIslandEngaged)
        {
            transform.position = Vector3.MoveTowards(transform.position, island.transform.position, step);
            transform.forward = (island.transform.position - transform.position).normalized;
        }

        if (isClicked != null)
        {
            if (isClicked == true)
                StartCoroutine(ScaleOverTime(0.1f, true));
            else
                StartCoroutine(ScaleOverTime(0.1f, false));
        }
    }

    private IEnumerator ScaleOverTime(float time, bool upscale)
    {
        Vector3 destinationScale;

        if (upscale)
            destinationScale = new Vector3(0.9f, 0.9f, 0.9f);
        else
            destinationScale = new Vector3(1, 1, 1f);

        float currentTime = 0.0f;

        do
        {
            gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;

            if (upscale)
                isClicked = false;
            else
                isClicked = null;

        } while (currentTime <= time);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "goldenWave")
            dropItems();

        if (other.tag == "islandShield")
        {
            IsIslandEngaged = true;

            InvokeRepeating("startDamage", 0, 1);
        }
    }

    private void startDamage()
    {
        island.GetComponent<IslandScript>().HealthUpdate(-DPS);

        Object[] explosionsObjects = Resources.LoadAll("Prefabs/Explosions");
        int randomExplosionIndex = Random.Range(0, explosionsObjects.Length - 1);

        explosion = Instantiate((GameObject)explosionsObjects[randomExplosionIndex], gameObject.transform.position + Vector3.up, Quaternion.identity);
    }

    void OnMouseDown()
    {
        isClicked = true;
        gameSettings.SelectedEnemy = gameObject;
    }

    private void dropItems()
    {
        // generate 2 coins as reward
        Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.left, Quaternion.identity);

        if (!gameSettings.IsMoneyIncreaseBuffOn)
        {
            float buffProbability = Random.Range(0f, 1f);

            if (buffProbability <= gameSettings.MoneyIncreaseBuffProbability)
                Instantiate((GameObject)Resources.Load("Prefabs/Buffs/MoneyIncreaseBuff"), gameObject.transform.position + Vector3.right, Quaternion.identity);
            else
                Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.right, Quaternion.identity);
        }

        if (!gameSettings.IsSpeedSlownessBuffOn)
        {
            float buffProbability = Random.Range(0f, 1f);

            if (buffProbability <= gameSettings.SpeedSlownessBuffProbability)
                Instantiate((GameObject)Resources.Load("Prefabs/Buffs/SpeeedSlownessBuff"), gameObject.transform.position + Vector3.right, Quaternion.identity);
            else
                Instantiate((GameObject)Resources.Load("Prefabs/Collectables/Coin"), gameObject.transform.position + Vector3.right, Quaternion.identity);
        }

        Object[] explosionsObjects = Resources.LoadAll("Prefabs/Explosions");
        int randomExplosionIndex = Random.Range(0, explosionsObjects.Length - 1);

        explosion = Instantiate((GameObject)explosionsObjects[randomExplosionIndex], gameObject.transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}
