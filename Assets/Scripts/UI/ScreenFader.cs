using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {
    public bool fadeComplete = false;
    public Action onFadeComplete;
    private static bool lastFadedOut = true;
    private Color targetColor;

    private Color fadeOutColor = new Color(0f, 0f, 0f, 1f);
    private Color fadeInColor = new Color(0f, 0f, 0f, 0f);

    private Image image;

    private const float FADE_SPEED = 5f;

    private bool fadeInProgress = false;

    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        image.color = fadeOutColor;
        targetColor = fadeInColor;
    }

    private void SceneManager_activeSceneChanged(Scene previous, Scene next) {
        fadeInProgress = false;
        StartFade();
    }

    public void StartFade() {
        if (fadeInProgress) {
            return;
        }

        fadeComplete = false;
        fadeInProgress = true;

        if (lastFadedOut) {
            targetColor = fadeInColor;
        } else {
            targetColor = fadeOutColor;
        }

        lastFadedOut = !lastFadedOut;
    }

    // Update is called once per frame
    void Update() {
        image.color = Color.Lerp(image.color, targetColor, FADE_SPEED * Time.deltaTime);

        if (fadeInProgress && DistanceToTargetColor() < 0.001f) {
            fadeComplete = true;
            fadeInProgress = false;
            if (onFadeComplete != null) onFadeComplete.Invoke();
        }
    }

    private float DistanceToTargetColor() {
        return (Mathf.Abs(image.color.r - targetColor.r) +
        Mathf.Abs(image.color.g - targetColor.g) +
        Mathf.Abs(image.color.b - targetColor.b) +
        Mathf.Abs(image.color.a - targetColor.a)) / 4f;
    }
}
