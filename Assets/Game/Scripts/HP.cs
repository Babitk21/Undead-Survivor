using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            GameManager.Instance.SetHP(0.5f);
            CollectablesSpawner.Instance.ReAddingCollectable(gameObject);
        }
    }
}
