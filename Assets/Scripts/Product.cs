using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool _clicked = false;

    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;
    
    private void Awake() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(_ray, out _hit, 1000f)){
                if(_hit.transform == transform){
                    Debug.Log(_hit.transform.name);
                    Debug.Log("Clicked");
                    _clicked = true;
                }
            }
        }
    }
}
