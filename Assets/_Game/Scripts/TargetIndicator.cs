using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : GameUnit
{
    [SerializeField] RectTransform rect;
    [SerializeField] RectTransform dirict;
    [SerializeField] Image dirictImg;
    [SerializeField] Image scoreImg;
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;

    [SerializeField] CanvasGroup canvasGroup;

    Transform target;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    Vector3 viewPoint;

    Vector2 viewPointX = new Vector2(0.1f, 0.9f);
    Vector2 viewPointY = new Vector2(0.05f, 0.95f);

    Vector2 viewPointInCameraX = new Vector2(0.1f, 0.9f);
    Vector2 viewPointInCameraY = new Vector2(0.1f, 0.9f);

    Camera Camera => CameraFollow.Ins.Camera;

    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(target.position);
        nameText.gameObject.SetActive(IsInCamera);
        dirictImg.gameObject.SetActive(!IsInCamera);

        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

        Vector3 targetPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerPoint = Camera.WorldToScreenPoint(LevelManager.Ins.player.TF.position) - screenHalf;

        rect.anchoredPosition = targetPoint;

        dirict.up = (targetPoint - playerPoint).normalized;

    }

    public void OnInit(Transform target)
    {
        SetScore(0);
        SetTarget(target);
        SetAlpha(1);
        SetColor(new Color(Random.value, Random.value, Random.value, 1));
        SetAlpha(GameManager.Ins.IsState(GameState.GAMEPLAY) ? 1 : 0);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetColor(Color color)
    {
        scoreImg.color = color;
        nameText.color = color;
    }

    public void SetAlpha(int alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
