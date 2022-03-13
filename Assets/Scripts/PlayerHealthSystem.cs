using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour 
{
    private Image playerHealthBar;
    public float playerCurrentHealth;
    private float maxHealth = 100f;
    PlayerController player;
    
    private void Start()
    {
        playerHealthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        playerCurrentHealth = player.health;
        playerHealthBar.fillAmount = playerCurrentHealth / maxHealth;
        
    }
}
