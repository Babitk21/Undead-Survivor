using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectablesSpawner : MonoBehaviour{
    public static CollectablesSpawner Instance{ get; private set; }
    
    [SerializeField] private GameObject bronzeExpPrefab, silverExpPrefab, goldExpPrefab;
    [SerializeField] private GameObject magnePrefab, hpPrefab;

    private List<GameObject> _bronze;
    private List<GameObject> _silver;
    private List<GameObject> _gold;
    private List<GameObject> _magnet;
    private List<GameObject> _hp;
    private float _bronzeSpawnTime = 2f, _silverSpawnTime = 4f, _goldSpawnTime = 6f, _magnetSpawnTime = 5f, _hpSpawnTime = 5f;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(Instance);
        }
    }

    private void Start(){
        _bronze = new List<GameObject>();
        _silver = new List<GameObject>();
        _gold = new List<GameObject>();
        _magnet = new List<GameObject>();
        _hp = new List<GameObject>();
        for (int i = 0; i < 10; i++){
            GameObject bronzeExp = Instantiate(bronzeExpPrefab);
            _bronze.Add(bronzeExp);
            bronzeExp.SetActive(false);
            GameObject silverExp = Instantiate(silverExpPrefab);
            _silver.Add(silverExp);
            silverExp.SetActive(false);
            GameObject goldExp = Instantiate(goldExpPrefab);
            _gold.Add(goldExp);
            goldExp.SetActive(false);
            GameObject magnet = Instantiate(magnePrefab);
            _magnet.Add(magnet);
            magnet.SetActive(false);
            GameObject hp = Instantiate(hpPrefab);
            _hp.Add(hp);
            hp.SetActive(false);
        }
        
    }

    private void Update(){
        _bronzeSpawnTime -= Time.deltaTime;
        if (_bronzeSpawnTime <= 0f){
            SpawnEnemy(_bronze);
            _bronzeSpawnTime = 10f;
        }
        _silverSpawnTime -= Time.deltaTime;
        if (_silverSpawnTime <= 0f){
            SpawnEnemy(_silver);
            _silverSpawnTime = 20f;
        }
        _goldSpawnTime -= Time.deltaTime;
        if (_goldSpawnTime <= 0f){
            SpawnEnemy(_gold);
            _goldSpawnTime = 30f;
        }
        _magnetSpawnTime -= Time.deltaTime;
        if (_magnetSpawnTime <= 0f){
            SpawnEnemy(_magnet);
            _magnetSpawnTime = 30f;
        }
        _hpSpawnTime -= Time.deltaTime;
        if (_hpSpawnTime <= 0f){
            SpawnEnemy(_hp);
            _hpSpawnTime = 35f;
        }
    }
    public void SpawnEnemy(List<GameObject> T)
    {
        GameObject spawnedObject = GetPooledObject(T);
        if (spawnedObject != null)
        {
            RandomPositionGenerator(out var x, out var y);
            spawnedObject.transform.position = new Vector3(x, y, 0);
            spawnedObject.SetActive(true);
        }
    }
    public GameObject GetPooledObject(List<GameObject> pooledObjects)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    private void RandomPositionGenerator(out float _x, out float _y)
    {
        _x = Random.Range(-25, 25);
        _y = Random.Range(-5.5f, 5.5f);
    }
    //For deactivating the GO and readding it to the list. 
    public void ReAddingCollectable(GameObject collectable){
        switch (collectable.name){
            case "Exp 0(Clone)":
                collectable.SetActive(false);
                _bronze.Remove(collectable);
                _bronze.Add(collectable);
                break;
            case "Exp 1(Clone)":
                collectable.SetActive(false);
                _silver.Remove(collectable);
                _silver.Add(collectable);
                break;
            case "Exp 2(Clone)":
                collectable.SetActive(false);
                _gold.Remove(collectable);
                _gold.Add(collectable);
                break;
            case "Mag(Clone)":
                collectable.SetActive(false);
                _gold.Remove(collectable);
                _gold.Add(collectable);
                break;
            case "Health(Clone)":
                collectable.SetActive(false);
                _gold.Remove(collectable);
                _gold.Add(collectable);
                break;
            default:
                Debug.Log("Unknown");
                break;
        }
    }
}
