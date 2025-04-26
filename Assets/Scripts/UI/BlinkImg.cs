using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImg : MonoBehaviour
{
    private Image _img;

    [SerializeField] private float blinkSpeed = 1f;

    private float _alphaVal;
    private bool _isFadeIn = false;

    private void Start()
    {
        _img = GetComponent<Image>();
        _alphaVal = _img.color.a;
    }

    private void Update()
    {
        if (!_isFadeIn) _alphaVal -= blinkSpeed * Time.deltaTime;
        else _alphaVal += blinkSpeed * Time.deltaTime;

        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _alphaVal);

        if (!_isFadeIn && _alphaVal <= 0) _isFadeIn = true;
        else if (_isFadeIn && _alphaVal >= 1) _isFadeIn = false;
    }
}
