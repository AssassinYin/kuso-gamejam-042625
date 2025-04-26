using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private ValueController valueController;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject btnPrefab;

    private List<GameObject> _buttons;

    private void btnInit()
    {
        _buttons = new List<GameObject>();
        foreach (var btn in _buttons)
        {
            btn.GetComponent<ButtonCallback>().OnClick -= valueController.SetEffectData;
            Destroy(btn);
        }
        _buttons.Clear();
    }

    public void GenerateBtn(int btnNumber, List<ChoiceData> choiceDatas)
    {
        btnInit();

        // Dynamic generating buttons
        for (int i = 0; i < btnNumber; i++)
        {
            GameObject btn = Instantiate(btnPrefab, btnGroup.transform);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choiceDatas[i].text;
            btn.AddComponent<ButtonCallback>().OnClick += valueController.SetEffectData;
            _buttons.Add(btn);
        }
    }

    public List<ButtonCallback> GetBtnCallbacks()
    {
        List<ButtonCallback> btnCB = new List<ButtonCallback>();
        
        foreach (var btn in _buttons)
        {
            btnCB.Add(btn.GetComponent<ButtonCallback>());
        }
        return btnCB;
    }
}
