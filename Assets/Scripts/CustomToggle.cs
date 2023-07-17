using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    [SerializeField] private Sprite _enabled, _disabled;
    private Toggle _toggle;

    public static Action<bool> OnValueChanged;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(delegate { OnOff(_toggle.isOn); });
        _toggle.isOn = PlayerPrefs.GetInt("Music", 0) == 1 ? true : false;
        OnOff(_toggle.isOn);
    }

    private void OnOff(bool on)
    {
        _toggle.GetComponent<Image>().sprite = on ? _disabled : _enabled;
        PlayerPrefs.SetInt("Music", on ? 1 : 0);
        OnValueChanged?.Invoke(on);
    }
}
