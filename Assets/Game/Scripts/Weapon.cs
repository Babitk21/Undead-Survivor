using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    private float _destroyTimer = 5f;
    private void Update(){
        _destroyTimer -= Time.deltaTime;
        if (_destroyTimer <= 0 && gameObject.transform.parent == null){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy")){
            other.GetComponent<EnemyMovement>().numOfHitTaken--;
            if (other.GetComponent<EnemyMovement>().numOfHitTaken <= 0){
                MyEnemySpawner.Instance.RemoveGameObjectFromPooledObject(other.gameObject);
                GameManager.Instance.IncreaseKills();
                Destroy(gameObject);
            }
        }    
    }
}
