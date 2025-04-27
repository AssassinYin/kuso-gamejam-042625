using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReporterController : MonoBehaviour
{
    [SerializeField] private Image centerImg;
    [SerializeField] private GameObject reporterL;
    [SerializeField] private GameObject reporterR;
    [SerializeField] private GameObject reporterLPrefab;
    [SerializeField] private GameObject reporterRPrefab;
    [SerializeField] private float intervalRatio; // 0~1
    private float lastRatio = 0;

    private List<GameObject> reportersL_list = new List<GameObject>();
    private List<GameObject> reportersR_list = new List<GameObject>();
    private bool isLeft = true;
    private int count = 0;

    private void Awake()
    {
        //for (int i = 0; i < reporterL.transform.childCount; i++)
        //{
        //    reportersL_list.Add(reporterL.transform.GetChild(i).gameObject);
        //}

        //for (int i = 0; i < reporterR.transform.childCount; i++)
        //{
        //    reportersR_list.Add(reporterR.transform.GetChild(i).gameObject);
        //}
    }

    public void UpdateReporterUI(float ratio)
    {
        //HideAllReporters();
        if (ratio == lastRatio) return;

        if (ratio > lastRatio)
        {
            for (float r = lastRatio; r < ratio; r += intervalRatio)
            {
                if (isLeft)
                {
                    GameObject reporter = Instantiate(reporterLPrefab, reporterL.transform);
                    Vector3 pos = GetPositionRange(reporter);
                    reporter.GetComponent<RectTransform>().localPosition = pos;
                    reportersL_list.Add(reporter);
                }
                else
                {
                    GameObject reporter = Instantiate(reporterRPrefab, reporterR.transform);
                    Vector3 pos = GetPositionRange(reporter);
                    reporter.GetComponent<RectTransform>().localPosition = pos;
                    reportersR_list.Add(reporter);
                }
                isLeft = !isLeft;
            }
        }
        else
        {
            for (float r = lastRatio; r >= ratio; r -= intervalRatio)
            {
                if (isLeft)
                {
                    if (reportersL_list.Count > 0)
                    {
                        Destroy(reportersL_list[reportersL_list.Count - 1]);
                        reportersL_list.RemoveAt(reportersL_list.Count - 1);
                    }
                }
                else
                {
                    if (reportersR_list.Count > 0)
                    {
                        Destroy(reportersR_list[reportersR_list.Count - 1]);
                        reportersR_list.RemoveAt(reportersR_list.Count - 1);
                    }
                }
                isLeft = !isLeft;
            }
        }
        lastRatio = ratio;

        //int count = 0;
        //float totalRatio = 0;
        //while (totalRatio < 1)
        //{
        //    if (ratio > totalRatio)
        //    {
        //        //reportersL_list[count].SetActive(true);
        //        //reportersR_list[count].SetActive(true);
        //        //count++;
        //    }
        //    else
        //    {
        //        break;
        //    }
        //    totalRatio += intervalRatio;
        //}
    }

    private Vector3 GetPositionRange(GameObject obj)
    {

        float rndX = Random.Range(-reporterL.GetComponent<RectTransform>().sizeDelta.x / 2
                                    //+ obj.GetComponent<RectTransform>().sizeDelta.x / 2
                                    ,
                                  reporterL.GetComponent<RectTransform>().sizeDelta.x / 2
                                    //- obj.GetComponent<RectTransform>().sizeDelta.x / 2
                                    );
        float rndY = Random.Range(-reporterL.GetComponent<RectTransform>().sizeDelta.y / 2
                                    //+ obj.GetComponent<RectTransform>().sizeDelta.y / 2
                                    ,
                                  reporterL.GetComponent<RectTransform>().sizeDelta.y / 2
                                    //- obj.GetComponent<RectTransform>().sizeDelta.y / 2
                                    );
        return new Vector3(rndX, rndY, count++);
    }

    private void HideAllReporters()
    {
        for (int i = 0; i < reporterL.transform.childCount; i++)
        {
            reportersL_list[i].SetActive(false);
        }

        for (int i = 0; i < reporterR.transform.childCount; i++)
        {
            reportersR_list[i].SetActive(false);
        }
    }
}
