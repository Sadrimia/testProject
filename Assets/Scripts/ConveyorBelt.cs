using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private List<GameObject> _onBelt;

    private void Update() {
        for(int i = 0; i <= _onBelt.Count -1; i++){
            _onBelt[i].GetComponent<Rigidbody>().velocity = _speed * _direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other) {
        _onBelt.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other) {
        _onBelt.Remove(other.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Product"){
            _onBelt.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
