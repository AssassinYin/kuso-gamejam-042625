using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _btnGroup;
    [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private int _btnNumber = 3;

    private List<GameObject> _buttons => new List<GameObject>();

    private void Start()
    {
        // Dynamic generating buttons
        for (int i = 0; i < _btnNumber; i++)
        {
            GameObject btn = Instantiate(_btnPrefab, _btnGroup.transform);
            _buttons.Add(btn);
            SetValueToButton();
        }
    }

    private void SetValueToButton()
    {
        // TODO: Input value to ButtonCallback.cs
    }
}
