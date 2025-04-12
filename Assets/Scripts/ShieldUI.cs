using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Slider shieldBar;
    [SerializeField] private string anchorObjectName = "ShieldBarAnchor"; // The child GameObject name on Player
    [SerializeField] private Vector3 screenOffset = new Vector3(0, 50, 0);

    private Transform followTarget;
    private float shieldTime;
    private float shieldDuration;
    private bool isShieldActive = false;


    private void Start()
    {
        // Initialize the shield bar
        shieldBar.gameObject.SetActive(false);
        shieldBar.maxValue = shieldDuration;
        shieldBar.value = 0f;

        // Try to find the followTarget if it's not already assigned
        if (followTarget == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                var anchor = playerObj.transform.Find(anchorObjectName);
                if (anchor != null)
                {
                    followTarget = anchor;
                }
            }
        }
    }

    private void Update()
    {
        // Try to find the followTarget if it's not already assigned
        if (followTarget == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                var anchor = playerObj.transform.Find(anchorObjectName);
                if (anchor != null)
                {
                    followTarget = anchor;
                }
            }
        }

        // Position the UI
        if (followTarget != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(followTarget.position);
            if (screenPos.z > 0)
            {
                shieldBar.transform.position = screenPos + screenOffset;
            }
        }

        // Update bar value
        if (!isShieldActive) return;

        shieldTime -= Time.deltaTime;
        shieldBar.value = shieldTime;

        if (shieldTime <= 0f)
        {
            isShieldActive = false;
            shieldBar.gameObject.SetActive(false);
        }
    }

    public void ActivateShield(float duration)
    {
        shieldDuration = duration;
        shieldTime = duration;
        isShieldActive = true;
        shieldBar.maxValue = duration;
        shieldBar.value = duration;
        shieldBar.gameObject.SetActive(true);
    }
}
