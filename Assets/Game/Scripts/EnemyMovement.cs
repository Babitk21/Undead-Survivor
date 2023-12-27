using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    public int numOfHitTaken;
    
    private bool _isFacingRight;
    private GameObject _player;
    private void Start(){
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update(){
        Movement();

    }

    private void Movement(){
        Vector3 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * (Time.deltaTime * speed));
        if (direction.x > 0 && !_isFacingRight || direction.x < 0 && _isFacingRight) {
            Flip();
        }
    }
    
    private void Flip() {
        _isFacingRight = !_isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.transform.CompareTag("Player")){
            GameManager.Instance.DecreaseHP(0.1f);
        }
    }
}
