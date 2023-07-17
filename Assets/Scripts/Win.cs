using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;   

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject _conveyor;
    [SerializeField] private GameObject _basket;
    [SerializeField] private Animator _anim;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _nextLevelButton;
    private Camera _mainCamera;

    private void Awake() {
        _mainCamera = Camera.main;
    }

    private void OnEnable() {
        try
        {
            Basket.onWin += WinningLevel;
        }
        catch
        {
            throw new System.Exception("Basket script don't found.");
        }
    }

    private void OnDisable() {
        try
        {
            Basket.onWin -= WinningLevel;
        }
        catch
        {
            throw new System.Exception("Basket script don't found.");
        }
    }

    private void WinningLevel(){
        Sequence _cameraTween = DOTween.Sequence();
        _text.text = "Level Passed";
        _text.color = new Color(149, 255, 145);
        _conveyor.SetActive(false);
        _basket.SetActive(false);
        _anim.SetTrigger("Dance");
        _cameraTween.Append(_nextLevelButton.DOAnchorPosY(120f, 3f));
        _cameraTween.Join(_mainCamera.transform.DOMove(new Vector3(0, 5.03f, 2.92f), 3f));
        _cameraTween.Join(_mainCamera.transform.DORotate(new Vector3(20.348f, 180, 0), 3f));
    }

}
