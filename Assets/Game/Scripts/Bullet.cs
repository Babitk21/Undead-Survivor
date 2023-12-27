using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private float speed;

    private float _yValue;

    private void Start(){
        _yValue = -MyPlayer.Instance.transform.localScale.x;
    }

    private void Update(){
        transform.Translate(new Vector3(0, _yValue,0) * (Time.deltaTime * speed));
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy")){
            MyEnemySpawner.Instance.RemoveGameObjectFromPooledObject(other.gameObject);
            GameManager.Instance.IncreaseKills();
            Destroy(gameObject);
        }
    }
}
