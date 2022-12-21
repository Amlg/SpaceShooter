using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _spawnTimer = 5f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRotuine());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator SpawnRotuine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(_spawnTimer);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
