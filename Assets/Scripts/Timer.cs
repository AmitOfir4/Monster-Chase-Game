using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float _elapsedTime;
    public bool isRunning = true;
    private Player _player;
    
    private void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player)
        {
            StopTimer();
            return;
        }
        
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
