using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public float currency_increment_rate = 10f;
    private float currency = 20f;
    private float time_multiplier = 0.1f;
    private WaitForSeconds currency_increment_time = new WaitForSeconds(2f);

    public float powerup_spawn_rate = 10f;
    public GameObject[] powerup_prefabs;
    private WaitForSeconds powerup_spawn_time;

    public int max_meteor_count = 40;
    public float meteor_price = 5f;
    public GameObject[] meteor_prefabs;
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
            powerup_spawn_time = new WaitForSeconds(powerup_spawn_rate);
            StartCoroutine(CheckPowerUps());
        }

        StartCoroutine(PayDirector());
    }

    void Update()
    {
        CheckCurrency();
    }

    //currency methods
    private void CheckCurrency()
    {
        if (meteor_prefabs.Length != 0 && currency >= meteor_price)
        {
            CheckMeteors();
        }
    }

    private IEnumerator PayDirector()
    {
        while (true)
        {
            yield return currency_increment_time;

            currency += currency_increment_rate * (Time.time * time_multiplier);
        }
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

        GameObject meteor = Instantiate(meteor_prefabs[random_number], random_location, meteor_prefabs[random_number].transform.rotation);

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
