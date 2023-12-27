using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeapon : MonoBehaviour{
    [SerializeField] private GameObject bulletPrefab;
    
    private int _bulletsRemaning = 10;
    private GameObject _bullet;
    private PlayerInputActions _playerInputActions;

    private void Start(){
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Fire.Enable();
    }

    private void Update(){
        _playerInputActions.Player.Fire.performed += Fired;
    }

    private void Fired(InputAction.CallbackContext callbackContext){
        if (MyPlayer.Instance.GetHasRangedWeapon()){
            _bulletsRemaning--;
            if (_bulletsRemaning == 0){
                MyPlayer.Instance.SetCanCollectMeleeObjects(true);
                Destroy(gameObject);
                MyPlayer.Instance.SetHasRangedWeapon(false);
            }
            GameManager.Instance.SetBulletsRemaining(_bulletsRemaning);
            GameManager.Instance.UpdateBulletsRemaining();
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 90));
        }
    }

    private void OnDestroy(){
        _playerInputActions.Player.Fire.performed -= Fired;
    }
}
