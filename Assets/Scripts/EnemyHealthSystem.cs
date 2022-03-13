using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    private Image enemyHealthBar;
    public float enemyCurrentHealth;
    private float maxHealth = 100f;
    EnemyController enemy;

    // Start is called before the first frame update
    private void Start()
    {
        enemyHealthBar = GetComponent<Image>();
        enemy = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    private void Update()
    {
        enemyCurrentHealth = enemy.health;
        enemyHealthBar.fillAmount = enemyCurrentHealth / maxHealth;
    }
}
