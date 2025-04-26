using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueController : MonoBehaviour
{
    public static ValueController instance;

    [SerializeField] private float _authority;
    [SerializeField] private float _follower;
    [SerializeField] private float _money;
    [SerializeField] private float _publicity;

    private void Start()
    {
        if (instance == null) instance = this;   
    }
}
