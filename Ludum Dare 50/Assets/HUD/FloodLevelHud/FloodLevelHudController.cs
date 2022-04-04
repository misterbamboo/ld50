using Assets.GameManagement;
using Assets.Tower;
using UnityEngine;

public class FloodLevelHudController : MonoBehaviour
{
    [SerializeField] RectTransform mask;
    [SerializeField] RectTransform towerRect;
    [SerializeField] RectTransform waterRect;
    [SerializeField] RectTransform playerRect;

    [SerializeField] float minHeight = 4;

    private GameObject player;
    private IFloodLevel floodLevel;
    private ITowerHeightDetector towerHeightDetector;

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
        UpdateWaterRectUIHeight2(height);
        UpdatePlayerUIHeight(height);
    }

    private void UpdateTowerRectUIHeight(float height)
    {
        var towerScale = towerRect.localScale;
        towerScale.y = towerHeightDetector.TowerHeight / height;
        towerRect.localScale = towerScale;
    }

    private void UpdateWaterRectUIHeight(float height)
    {
        var waterScale = waterRect.localScale;
        waterScale.y = floodLevel.FloodHeight / height;
        waterRect.localScale = waterScale;
    }

    private void UpdateWaterRectUIHeight2(float height)
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

    private float GetMaxGameHeight()
    {
        var targetHeight = towerHeightDetector.TowerHeight + 3;
        return Mathf.Max(targetHeight, minHeight);
    }
}
