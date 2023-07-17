using System;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool isClicked { get; private set; } = false;
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;

    public static Action<GameObject> OnClicked;

    private void Awake() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(_ray, out _hit, 1000f)){
                if(_hit.transform == transform){
                    isClicked = true;
                    OnClicked?.Invoke(this.gameObject);
                }
            }
        }
    }
}
