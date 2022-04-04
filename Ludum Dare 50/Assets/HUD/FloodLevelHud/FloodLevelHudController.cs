using Assets.GameManagement;
using Assets.Tower;
using UnityEngine;
using UnityEngine.UI;

public class FloodLevelHudController : MonoBehaviour
{
    [SerializeField] RectTransform mask;
    [SerializeField] RectTransform towerRect;
    [SerializeField] RectTransform waterRect;
    [SerializeField] RectTransform playerRect;
    [SerializeField] Image exclamationMarkImage;

    [SerializeField] float minHeight = 4;

    private GameObject player;
    private IFloodLevel floodLevel;
    private ITowerHeightDetector towerHeightDetector;
    private bool isCritical;
    private float isCriticalBlinkTime;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        floodLevel = GameManager.Instance.FloodLevel;
        towerHeightDetector = GameManager.Instance.TowerHeightDetector;
    }

    private void FixedUpdate()
    {
        var height = GetMaxGameHeight();
        UpdateTowerRectUIHeight(height);
        UpdateWaterRectUIHeight(height);
        UpdatePlayerUIHeight(height);
        UpdateIsCritical(height);
    }

    private void Update()
    {
        if (isCritical)
        {
            isCriticalBlinkTime += Time.deltaTime;
            float colorAlpha = (Mathf.Sin(isCriticalBlinkTime * Mathf.PI * 2) + 1) * 0.5f;

            var c = exclamationMarkImage.color;
            exclamationMarkImage.color = new Color(c.r, c.g, c.b, colorAlpha);
        }
    }

    private void UpdateTowerRectUIHeight(float height)
    {
        var towerOffsetMax = towerRect.offsetMax;
        // here it's in pixed due to anchor
        var heightRatio = 1 - (towerHeightDetector.TowerHeight / height);
        var uiHeight = mask.rect.height;

        towerOffsetMax.y = -heightRatio * uiHeight;
        towerRect.offsetMax = towerOffsetMax;
    }

    private void UpdateWaterRectUIHeight(float height)
    {
        var waterOffsetMax = waterRect.offsetMax;
        // here it's in pixed due to anchor
        var heightRatio = 1 - (floodLevel.FloodHeight / height);
        var uiHeight = mask.rect.height;

        waterOffsetMax.y = -heightRatio * uiHeight;
        waterRect.offsetMax = waterOffsetMax;
    }

    private void UpdatePlayerUIHeight(float height)
    {
        var playerAnchoredPosition = playerRect.anchoredPosition;
        // here it's in pixed due to anchor
        var heightRatio = player.transform.position.y / height;
        var uiHeight = mask.rect.height;

        playerAnchoredPosition.y = heightRatio * uiHeight;
        playerRect.anchoredPosition = playerAnchoredPosition;
    }

    private void UpdateIsCritical(float height)
    {
        var percent = floodLevel.FloodHeight / towerHeightDetector.TowerHeight;
        if (percent > 0.6)
        {
            isCritical = true;
        }
        else
        {
            isCritical = false;
            isCriticalBlinkTime = 0;
            var c = exclamationMarkImage.color;
            exclamationMarkImage.color = new Color(c.r, c.g, c.b, 0);
        }
    }

    private float GetMaxGameHeight()
    {
        var targetHeight = towerHeightDetector.TowerHeight + 3;
        return Mathf.Max(targetHeight, minHeight);
    }
}
