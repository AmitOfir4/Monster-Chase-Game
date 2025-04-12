using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Slider shieldBar;

    private float shieldTime;
    private float _shieldDuration;
    private bool isShieldActive = false;

    public void ActivateShield(float duration)
    {
      _shieldDuration = duration;
      shieldTime = duration;
      isShieldActive = true;
      shieldBar.maxValue = duration;
      shieldBar.value = duration;
      shieldBar.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShieldActive)
        {
            return;
        }

        shieldTime -= Time.deltaTime;
        shieldBar.value = shieldTime;

        if (shieldTime <= 0f)
        {
            isShieldActive = false;
            shieldBar.gameObject.SetActive(false);
        }
    }
}
