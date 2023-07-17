using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PickUp Settings")]
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _pickupTarget;
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _drop;
    private Animator _anim;
    private Rigidbody _currentObject;
    private bool _isGrabbed = false;

    private void OnEnable()
    {
        Product.OnClicked += StartGrab;
    }
    private void OnDisable()
    {
        Product.OnClicked -= StartGrab;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(_currentObject){
            _leftHandTarget.position = _currentObject.position;
            _currentObject.transform.position = Vector3.MoveTowards(_currentObject.transform.position, _pickupTarget.position, 20 * Time.deltaTime);
        }
    }

    private void StartGrab(GameObject hit)
    {
        if (!_isGrabbed)
        {
            StartCoroutine(Grab(hit));
            _isGrabbed = true;
        }
    }

    IEnumerator Grab(GameObject hit)
    {
        if (hit.TryGetComponent(out Rigidbody rb))
        {
            _currentObject = rb;
        }
        _currentObject.transform.localScale = new Vector3(.8f, .8f, .8f);
        yield return new WaitForSeconds(0.4f);
        _anim.SetTrigger("Grab");
        _currentObject.transform.parent = _drop.parent;
        yield return new WaitForSeconds(1.5f);
        _currentObject.transform.localPosition = _drop.transform.position;
        _currentObject.freezeRotation = false;
        _currentObject = null;
        yield return new WaitForSeconds(2f);
        _isGrabbed = false;
    }
}
