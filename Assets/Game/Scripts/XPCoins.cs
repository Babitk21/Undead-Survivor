using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPCoins : MonoBehaviour{
    [SerializeField] private int xpAmount;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            GameManager.Instance.xp += xpAmount;
            CollectablesSpawner.Instance.ReAddingCollectable(gameObject);
        }
    }
}
