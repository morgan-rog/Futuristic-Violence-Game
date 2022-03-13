using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyProjectile;
    private float spawnTime;
    private float currentTime = 0;
    public float minTime = 1f;
    public float maxTime = 2f;
    public float health = 100;
    private float damage = 5;
    private GameManager gameManager;
    public AudioClip damageSound;
    private AudioSource enemyAudio;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            CheckHealth();
            currentTime += Time.deltaTime;
            if (currentTime >= spawnTime)
            {
                currentTime = 0;
                ShootProjectile();
                SetRandomTime();
            }
        }
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            explosionParticle.Play();
            Destroy(this.gameObject);
            gameManager.PlayerWins();
        }
    }

    void ShootProjectile()
    {
        Instantiate(enemyProjectile, transform.position, enemyProjectile.transform.rotation);
    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerProjectile"))
        {
            enemyAudio.PlayOneShot(damageSound, 1.0f);
            health -= damage;
        }
    }
}
