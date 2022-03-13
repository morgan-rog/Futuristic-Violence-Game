using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 40.0f;
    private float rightBound = 20;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        if (transform.position.x > rightBound || transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
