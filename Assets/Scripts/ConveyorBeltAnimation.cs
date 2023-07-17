using UnityEngine;

public class ConveyorBeltAnimation : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _animSpeed;

    private void Update() {
        _meshRenderer.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * _animSpeed, 0f);
    }
}
