using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SomeService : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private Service2 _service;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        
    }
}
