
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyEnemySpawner : MonoBehaviour{
    public static MyEnemySpawner Instance{ get; private set; }
    
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;
    [SerializeField] private float spawnTimer;
    
    private float _spawnTime = 4f;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(Instance);
        }
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    private void Update(){
        _spawnTime -= Time.deltaTime;
        if (_spawnTime <= 0){
            SpawnEnemy();
            _spawnTime = spawnTimer;
        }
    }
    public GameObject GetPooledObject()
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

    public void RemoveGameObjectFromPooledObject(GameObject hitObject){
        hitObject.SetActive(false);
        pooledObjects.Remove(hitObject);
        pooledObjects.Add(hitObject);
    }
    
    public void SpawnEnemy()
    {
        GameObject _enemy = GetPooledObject();
        if (_enemy != null)
        {
            RandomPositionGenerator(out var x, out var y);
            _enemy.transform.position = new Vector3(x, y, 0);
            _enemy.SetActive(true);
        }
    }

    private void RandomPositionGenerator(out float _x, out float _y)
    {
        _x = Random.Range(-15, 15);
        _y = Random.Range(-5.5f, 5.5f);
    }
}
