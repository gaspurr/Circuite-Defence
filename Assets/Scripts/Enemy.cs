﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float enemyHealth;

    [SerializeField]
    private float movementSpeed;

    private int damage; // the amount of damage the enemy does when it reaches the end

    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        initializeEnemy();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;
        if(enemyHealth <= 0)
        {
            die();
        }
    }

    private void reachedEnd() {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
        Health.health -= 1;
    }

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
        Currency.currency += 10;
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if(targetTile != null && targetTile != MapGenerator.endTile)
        {
            float distance = Vector3.Distance(targetTile.transform.position, transform.position);

            if(distance < 0.001f)
            {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

            // Temporary fix
                try {
                    targetTile = MapGenerator.pathTiles[currentIndex + 1];
                }
                catch (System.ArgumentOutOfRangeException e) {
                    reachedEnd();
                    Debug.Log("Enemy reached the endtile");
                }
                
            
            }
        }
        else {
            Debug.Log("Test");
        }
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();    
    }
}
