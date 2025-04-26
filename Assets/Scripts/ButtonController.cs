using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _btnGroup;
    [SerializeField] private GameObject _btnPrefab;
    [SerializeField] private int _btnNumber = 3;
    [SerializeField] private Vector2 _pos;

    private List<GameObject> _buttons => new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _btnNumber; i++)
        {
            _buttons.Add(Instantiate(_btnPrefab, _btnGroup.transform));
            //_buttons[i].GetComponent<RectTransform>().localPosition
            //    += new Vector3(i * (_buttons[i].GetComponent<RectTransform>().sizeDelta.x + _pos.x),
            //                   i * (_buttons[i].GetComponent<RectTransform>().sizeDelta.y + _pos.y), 0);
        }
    }
}
