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

    private void Update()
    {
        if (!isShieldActive)
        {
            shieldBar.gameObject.SetActive(false);
            return;
        };

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

        if (followTarget != null)
        {
            Vector3 worldPos = followTarget.position;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

            Canvas canvas = shieldBar.GetComponentInParent<Canvas>();
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            RectTransform shieldRect = shieldBar.GetComponent<RectTransform>();

            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, Camera.main, out localPoint))
            {
                shieldRect.anchoredPosition = localPoint + (Vector2)screenOffset;
            }
        }

        

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
