using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Magnet : MonoBehaviour{
    private bool _activateMagnet;
    private MyPlayer _myPlayer;
    
    private void Update(){
        if (_activateMagnet){
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 4f);
            foreach (var col in collider){
                if (col.CompareTag("Coins")){
                    col.transform.DOMove(_myPlayer.transform.position, 2f).OnComplete(() => 
                        CollectablesSpawner.Instance.ReAddingCollectable(col.gameObject));
                }                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            gameObject.transform.SetParent(other.transform);
            gameObject.transform.localPosition = Vector3.zero;
            _activateMagnet = true;
            _myPlayer = other.GetComponent<MyPlayer>();
            GameManager.Instance.SetAttachedMagnet(gameObject);
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
