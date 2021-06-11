using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public Transform player;

    public float currency_increment_rate = 5f;
    private int spawn_count;
    private float currency = 24f;
    private const float time_multiplier = 0.1f;
    private WaitForSeconds currency_check_time = new WaitForSeconds(3f);

    public float powerup_spawn_cooldown = 6f;
    public GameObject[] powerup_prefabs;
    private WaitForSeconds powerup_spawn_time;

    public float[] enemy_prices;
    public GameObject[] enemy_prefabs;
    public Transform[] enemy_parents;
    private int[] max_enemy_count = { 3, 2, 1 };
    private int[] max_enemy_spawn_count = { 2, 1, 1 };

    public float meteor_price = 6f;
    public GameObject[] meteor_prefabs;
    public Transform meteor_parent;
    private int max_meteor_count = 30;
    private int max_meteor_spawn_count = 8;
    private int last_used_meteor_indx = -1;
    private List<GameObject> meteors = new List<GameObject>();

    private Vector2 random_location;
    private float[] director_area = new float[4];
    private BoxCollider2D coll;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        SetDirectorArea();

        if (powerup_prefabs.Length != 0)
        {
            powerup_spawn_time = new WaitForSeconds(powerup_spawn_cooldown);
            StartCoroutine(CheckPowerUps());
        }

        StartCoroutine(CheckCurrency());
    }

    void Update()
    {
        PayDirector();
    }

    //currency methods
    private IEnumerator CheckCurrency()
    {
        while (true)
        {
            for (int i = enemy_prefabs.Length - 1; i >= 0; i--)
            {
                if (currency >= enemy_prices[i])
                {
                    CheckEnemySpawnCount(i);
                }
            }

            if (meteor_prefabs.Length != 0 && currency >= meteor_price)
            {
                CheckMeteorSpawnCount();
            }

            yield return currency_check_time;
        }
    }

    private void PayDirector()
    {
        currency += (currency_increment_rate + CalculatePaymentIncreaseAmount()) * Time.deltaTime;
    }

    private float CalculatePaymentIncreaseAmount()
    {
        return (currency_increment_rate * Time.timeSinceLevelLoad * time_multiplier);
    }

    //powerup methods
    private void SpawnPowerUp()
    {
        int random_number = (int)Random.Range(0, powerup_prefabs.Length - 1);
        SetRandomLocation();

        Instantiate(powerup_prefabs[random_number], random_location, powerup_prefabs[random_number].transform.rotation);
    }

    private IEnumerator CheckPowerUps()
    {
        while (true)
        {
            yield return powerup_spawn_time;

            SpawnPowerUp();
        }
    }

    //enemy methods
    private void SpawnEnemy(int indx)
    {
        SetRandomLocation();

        GameObject enemy = Instantiate(enemy_prefabs[indx], random_location, enemy_prefabs[indx].transform.rotation, enemy_parents[indx]);

        enemy.GetComponent<Enemy>().player = player;
    }

    private void CheckEnemy(int indx)
    {
        if (enemy_parents[indx].childCount < max_enemy_count[indx])
        {
            currency -= enemy_prices[indx];
            SpawnEnemy(indx);
        }
    }

    private void CheckEnemySpawnCount(int indx)
    {
        spawn_count = (int)(currency / enemy_prices[indx]);
        if (spawn_count > max_enemy_spawn_count[indx])
            spawn_count = max_enemy_spawn_count[indx];

        for (int spawn_cnt = 0; spawn_cnt < spawn_count; spawn_cnt++)
        {
            CheckEnemy(indx);
        }
    }

    //meteor methods
    private void RelocateMeteor(GameObject meteor)
    {
        SetRandomLocation();
        meteor.SetActive(true);

        meteor.transform.position = random_location;
    }

    private void SpawnMeteor()
    {
        int random_number = (int)Random.Range(0, meteor_prefabs.Length - 1);
        SetRandomLocation();

        GameObject meteor = Instantiate(meteor_prefabs[random_number], random_location, meteor_prefabs[random_number].transform.rotation, meteor_parent);

        meteors.Add(meteor);
    }

    private void CheckMeteors()
    {
        if (meteors.Count != max_meteor_count)
        {
            currency -= meteor_price;
            SpawnMeteor();
        }
        else if (!meteors[(last_used_meteor_indx + 1) % max_meteor_count].activeSelf)
        {
            currency -= meteor_price;
            RelocateMeteor(meteors[++last_used_meteor_indx % max_meteor_count]);
        }
    }

    private void CheckMeteorSpawnCount()
    {
        spawn_count = (int)(currency / meteor_price);
        if (spawn_count > max_meteor_spawn_count)
            spawn_count = max_meteor_spawn_count;

        for (int spawn_cnt = 0; spawn_cnt < spawn_count; spawn_cnt++)
        {
            CheckMeteors();
        }
    }

    //director area methods
    private void SetRandomLocation()
    {
        random_location = new Vector3(Random.Range(director_area[0], director_area[1]), Random.Range(director_area[2], director_area[3]), meteor_prefabs[0].transform.position.z);
    }

    private void SetDirectorArea()
    {
        director_area[0] = transform.position.x - (coll.size.x / 2);
        director_area[1] = transform.position.x + (coll.size.x / 2);
        director_area[2] = transform.position.y - (coll.size.y / 2);
        director_area[3] = transform.position.y + (coll.size.y / 2);
    }
}
