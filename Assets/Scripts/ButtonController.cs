using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _btnGroup;
    [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private int _btnNumber = 3;
    [SerializeField] private Vector2 _pos => new Vector2(0, 10);

    private List<GameObject> _buttons => new List<GameObject>();

    private void Start()
    {
        // Dynamic generating buttons
        for (int i = 0; i < _btnNumber; i++)
        {
            GameObject btn = Instantiate(_btnPrefab, _btnGroup.transform);
            _buttons.Add(btn);
            btn.GetComponent<RectTransform>().localPosition 
                += new Vector3(_pos.x != 0 ? i * (btn.GetComponent<RectTransform>().sizeDelta.x + _pos.x) : 0,
                               _pos.y != 0 ? i * (btn.GetComponent<RectTransform>().sizeDelta.y + _pos.y) : 0,
                               0);
            SetValueToButton();
        }
    }

    private void SetValueToButton()
    {
        // TODO: Input value to ButtonCallback.cs
    }
}
