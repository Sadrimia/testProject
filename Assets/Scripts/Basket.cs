using System;
using Random=UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Basket : MonoBehaviour
{
    [SerializeField] private string[] _productsList;
    [SerializeField] private ParticleSystem _starParticle;
    [Header("Sounds")]
    [SerializeField] private AudioClip _incorrectProductSound;
    [SerializeField] private AudioClip _correctProductSound;
    [Header("Text +1 Settings")]
    [SerializeField] private Text _textForSpawn;
    [SerializeField] private Transform _canvas;
    private AudioSource _basketAudioSource;
    private List<Text> _plusOneScore = new List<Text>();
    private string _currentProduct;
    private int _numberOfFruits;

    public static Action onWin;
    public static Action<int, string> onNumberOfProductsChanged;
    
    private void Awake() {
        _numberOfFruits = Random.Range(1, 5);
        _currentProduct = _productsList[Random.Range(1, _productsList.Length)];
        onNumberOfProductsChanged?.Invoke(_numberOfFruits, _currentProduct);
        _basketAudioSource = GetComponent<AudioSource>();
    }

    private void plusOneTween(int element){
        Sequence _plus = DOTween.Sequence();
        _plus.Append(_plusOneScore[element].transform.DOScale(2.5f, 5f));
        _plus.Join(_plusOneScore[element].DOFade(1f, 0.5f));
        _plus.Join(_plusOneScore[element].rectTransform.DOAnchorPosY(475f, 5f));
        _plus.AppendInterval(0.5f);
        _plus.Join(_plusOneScore[element].DOFade(0f, 0.5f));
        _plus.OnComplete(()=>{Destroy(_plusOneScore[0].gameObject); _plusOneScore.RemoveAt(0);});
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == _currentProduct+"(Clone)" && other.TryGetComponent(out Product product))
        {
            if(product.isClicked)
            {
                _basketAudioSource.clip = _correctProductSound;
                _plusOneScore.Add(Instantiate(_textForSpawn, new Vector3(500, -517, 0), Quaternion.identity, _canvas));
                plusOneTween(_plusOneScore.Count - 1);
                _numberOfFruits -= 1;
                onNumberOfProductsChanged?.Invoke(_numberOfFruits, _currentProduct);
                _basketAudioSource.Play();
                _starParticle.Play();
                Destroy(product);
                if(_numberOfFruits <= 0){
                    onWin?.Invoke();
                }
            }
        }
        else if(other.name != _currentProduct + "(Clone)" && other.TryGetComponent(out Product incorrectProduct))
        {
            if(incorrectProduct.isClicked)
            {
                Destroy(incorrectProduct);
                _basketAudioSource.clip = _incorrectProductSound;
                _basketAudioSource.Play();
            }
        }
    }
}
