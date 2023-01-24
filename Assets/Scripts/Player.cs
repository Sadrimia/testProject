using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("AnimationSettings")]
    [SerializeField] private Animator _anim;
    [Header("PickUp Settings")]
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _pickupTarget;
    [SerializeField] private Transform _startingPointOfRay;
    [SerializeField] private Transform _drop;
    [SerializeField] private Transform _basketParent;
    private Rigidbody _currentObject;
    private bool _isGrabbed = false;

    private void Update() {
        RaycastHit hit;
        Debug.DrawRay(_startingPointOfRay.position, Vector3.forward * 10f, Color.yellow);
            if (Physics.Raycast(_startingPointOfRay.position, Vector3.forward, out hit, 10f, _targetLayer))
            {
                if (!_isGrabbed && hit.rigidbody.GetComponent<Product>()._clicked){
                    StartCoroutine(Grab());
                    _isGrabbed = true;
                }
        }
    }

    private void FixedUpdate() {
        if(_currentObject){
            Vector3 DirectionToPoint = _pickupTarget.position - _currentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            _currentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }

    IEnumerator Grab(){
        RaycastHit hit;
        if (Physics.Raycast(_startingPointOfRay.position, Vector3.forward, out hit, 10f, _targetLayer))
        {
                _anim.SetTrigger("Grab");
                yield return new WaitForSeconds(0.6f);
                _currentObject = hit.rigidbody;
                _currentObject.transform.localScale = new Vector3(.8f, .8f, .8f);
                yield return new WaitForSeconds(0.65f);
                Debug.Log("Dropped");
                _isGrabbed = false;
                _currentObject.transform.parent = _basketParent;
                Vector3 DirectionToPoint = _pickupTarget.position - _drop.position;
                float DistanceToPoint = DirectionToPoint.magnitude;

                //_currentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
                _currentObject.MovePosition(_currentObject.position + DirectionToPoint * 12f * DistanceToPoint);
                _currentObject.freezeRotation = false;
                _currentObject = null;
        }
    }
}
