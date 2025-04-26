using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private ValueController valueController;
    [SerializeField] private DateUpdater dateUpdater;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject btnPrefab;

    private List<GameObject> _buttons = new List<GameObject>();

    private void BtnInit()
    {
        foreach (var btn in _buttons)
        {
            UnbindClickEvents(btn);
            Destroy(btn);
        }
        _buttons.Clear();
    }

    public void GenerateBtn(int btnNumber, List<ChoiceData> choiceDatas)
    {
        BtnInit();

        // Dynamic generating buttons
        for (int i = 0; i < btnNumber; i++)
        {
            GameObject btn = Instantiate(btnPrefab, btnGroup.transform);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choiceDatas[i].text;
            btn.AddComponent<ButtonCallback>();
            BindClickEvents(btn);
            _buttons.Add(btn);
        }
    }

    private void BindClickEvents(GameObject obj)
    {
        obj.GetComponent<ButtonCallback>().OnClick += valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick += dateUpdater.AddDate;
    }

    private void UnbindClickEvents(GameObject obj)
    {
        obj.GetComponent<ButtonCallback>().OnClick -= valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick -= dateUpdater.AddDate;
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
