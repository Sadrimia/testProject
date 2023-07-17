using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _products;
    [SerializeField] private float _secondsToSpawnNext, _spawnTime;
    private bool _spawnDisabled;

    private void Awake() {
        SpawnProduct();
    }

    private void Update() {
        if(_spawnDisabled){
            _spawnTime += Time.deltaTime;
        }
        if(_spawnTime >= _secondsToSpawnNext){
            _spawnDisabled = false;
            SpawnProduct();
        }
    }

    private void SpawnProduct(){
        _spawnTime = 0;
        _spawnDisabled = true; 
        Instantiate(_products[Random.Range(0, _products.Length)], this.transform);
    }

}
