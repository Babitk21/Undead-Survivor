using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WeaponSpawner : MonoBehaviour{
    [FormerlySerializedAs("weapons")] [SerializeField] private GameObject[] meleeWeapons;
    [SerializeField] private GameObject[] gunWeapons;

    private float _meleeWeaponSpawnTimer = 0f;
    private float _gunWeaponSpawnTimer = 10f;
    private void Update(){
        _meleeWeaponSpawnTimer -= Time.deltaTime;
        _gunWeaponSpawnTimer -= Time.deltaTime;
        if (_meleeWeaponSpawnTimer <= 0f){
            SpawningRandomMeleeWeapon();
            _meleeWeaponSpawnTimer = 3f;
        }

        if (_gunWeaponSpawnTimer <= 0f){
            SpawningRandomGunWeapon();
            _gunWeaponSpawnTimer = 15f;
        }
    }

    private void SpawningRandomGunWeapon(){
        int randomIndex = Random.Range(0, meleeWeapons.Length);
        float x = Random.Range(-15, 15);
        float y = Random.Range(-5.5f, 5.5f);
        Vector3 randomPos = new Vector3(x, y, 0);
        Instantiate(gunWeapons[randomIndex], randomPos, quaternion.identity);

    }
    private void SpawningRandomMeleeWeapon(){
        int randomIndex = Random.Range(0, meleeWeapons.Length);
        float x = Random.Range(-15, 15);
        float y = Random.Range(-5.5f, 5.5f);
        Vector3 randomPos = new Vector3(x, y, 0);
        Instantiate(meleeWeapons[randomIndex], randomPos, quaternion.identity);
    }
}
