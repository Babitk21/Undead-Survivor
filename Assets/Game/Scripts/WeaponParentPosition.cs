using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParentPosition : MonoBehaviour{
    [SerializeField] private Transform player;
    void Update(){
        transform.position = player.position;
    }
}
