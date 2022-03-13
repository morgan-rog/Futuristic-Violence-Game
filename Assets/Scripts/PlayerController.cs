using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float jumpForce = 2.0f;
    public float moveSpeed = 10.0f;
    private int rightRange = -2;
    private int leftRange = -13;
    private int powerUpleftRange = -12;
    private int powerUprightRange = -2;
    public bool isGrounded = true;
    public float gravityModifier;
    public float health = 100;
    public float damage = 5;
    private float maxHealth = 100;
    private float spawnTime = 8;
    private float clickDelay = 0.3f;
    private float shootTimeTrack;
    private float powerUpTimeTrack;
    private bool hasPowerUp;
    private Rigidbody rb;
    public GameObject projectilePrefab;
    public GameObject powerUp;
    private GameManager gameManager;
    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip lootPickupSound;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        hasPowerUp = true;
        shootTimeTrack = 0;
        powerUpTimeTrack = 0;
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimeTrack += Time.deltaTime;
        if (gameManager.isGameActive)
        {
            MovePlayer();
            ConstrainPlayerMovement();
            ShootProjectile();
            CheckHealth();
            CheckPowerUp();
        }
        
        
    }

    void CheckHealth()
    {
        if(health <= 0)
        {
            explosionParticle.Play();
            Destroy(this.gameObject);
            gameManager.GameOver();
        }
    }
    
    void ShootProjectile()
    {
        if (Input.GetMouseButtonDown(0) && shootTimeTrack >= clickDelay)
        {
            playerAudio.PlayOneShot(shootSound, 1.0f);
            Instantiate(projectilePrefab, transform.position + new Vector3(0, 1, 0), projectilePrefab.transform.rotation);
            shootTimeTrack = 0;
        }
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Keep player in bounds
    void ConstrainPlayerMovement()
    {
        if (transform.position.x <= leftRange)
        {
            transform.position = new Vector3(leftRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightRange)
        {
            transform.position = new Vector3(rightRange, transform.position.y, transform.position.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(lootPickupSound, 1.0f);
            health = maxHealth;
            Destroy(collider.gameObject);
            //StartCoroutine(PowerupCountdownRoutine());
            hasPowerUp = true;
        }
        if (collider.gameObject.CompareTag("EnemyProjectile"))
        {
            playerAudio.PlayOneShot(damageSound, 1.0f);
            Destroy(collider.gameObject);
            health -= damage;
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(spawnTime);
        int spawnRange = Random.Range(powerUpleftRange, powerUprightRange);
        Instantiate(powerUp, new Vector3(spawnRange, powerUp.transform.position.y, powerUp.transform.position.z), powerUp.transform.rotation);
    }

    void CheckPowerUp()
    {
        if (hasPowerUp)
        {
            powerUpTimeTrack += Time.deltaTime;
        }
        if (powerUpTimeTrack >= spawnTime)
        {
            SpawnPowerUp();
            powerUpTimeTrack = 0;
            hasPowerUp = false;
        }
    }

    void SpawnPowerUp()
    {
        int spawnRange = Random.Range(powerUpleftRange, powerUprightRange);
        Instantiate(powerUp, new Vector3(spawnRange, powerUp.transform.position.y, powerUp.transform.position.z), powerUp.transform.rotation);
    }
}
