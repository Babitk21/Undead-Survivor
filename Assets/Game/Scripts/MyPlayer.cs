using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class MyPlayer : MonoBehaviour{
    public static MyPlayer Instance{ get; private set; }
    
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private Transform rightSideWeaponPoint;
    [SerializeField] private Transform[] weaponPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private PlayerInputActions _playerInputActions;
    private Vector3 _scale;
    private bool _facingRight = true;
    private bool _hasRangedWeapon;
    public bool GetHasRangedWeapon(){
        return _hasRangedWeapon;
    }

    public void SetHasRangedWeapon(bool _hasRangedWeapon){
        this._hasRangedWeapon = _hasRangedWeapon;
    }
    private bool _canCollectMeleeObjects = true;

    public void SetCanCollectMeleeObjects(bool canCoolectMeleeObjects){
        _canCollectMeleeObjects = canCoolectMeleeObjects;
    }
    
    private const float WeaponParentRotationSpeed = 100f;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(Instance);
        }
    }

    private void Start(){
        _scale = transform.localScale;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Move.Enable();
    }

    private void Update(){
        Vector2 playerMove = _playerInputActions.Player.Move.ReadValue<Vector2>();
        HorizontalMove(playerMove);
        HorizontalAnim(playerMove);
        if (!_hasRangedWeapon){
            WeaponRotation();
        }
        else if (_hasRangedWeapon){
            weaponParent.transform.rotation = Quaternion.identity;
            rightSideWeaponPoint.GetChild(0).localPosition = Vector3.zero;
            rightSideWeaponPoint.GetChild(0).rotation = Quaternion.identity;
            
        }
    }

    private void WeaponRotation(){
        weaponParent.transform.Rotate(new Vector3(0,0, -WeaponParentRotationSpeed * Time.deltaTime));
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        _scale.x *= -1;
        transform.localScale = _scale;
    }
    private void HorizontalAnim(Vector3 playerMove){
        animator.SetFloat(Speed, playerMove.magnitude);
    }
    private void HorizontalMove(Vector3 playerMove){
        transform.Translate(new Vector3(playerMove.x, playerMove.y, 0) * (Time.deltaTime * moveSpeed));
        if (playerMove.x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (playerMove.x < 0 && _facingRight)
        {
            Flip();
        }
        transform.Translate(new Vector3(playerMove.x * moveSpeed * Time.deltaTime, playerMove.y * moveSpeed * Time.deltaTime, 0));
    }

    private void SetWeaponParent(GameObject weapon){
        for (int i = 0; i < weaponPoints.Length; i++){
            if (weaponPoints[i].childCount == 0){
                weapon.transform.SetParent(weaponPoints[i]);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.rotation = weaponPoints[i].transform.rotation;
            }
        }
    }

    private void SetRangedWeaponParent(GameObject rangedWeapon){
        if (rightSideWeaponPoint.childCount == 0){
            rangedWeapon.transform.SetParent(rightSideWeaponPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Weapon") && _canCollectMeleeObjects){
            SetWeaponParent(other.gameObject);
        }
        else if (other.CompareTag("RangedWeapon")){
            _hasRangedWeapon = true;
            _canCollectMeleeObjects = false;
            SetRangedWeaponParent(other.gameObject);
        }
    }
}
