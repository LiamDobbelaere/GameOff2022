using UnityEngine;

public enum TransitionType {
    ZOOM_OUTER_TO_INNER,
    ZOOM_INNER_TO_OUTER
}

public class ScreenTransition : MonoBehaviour {
    public TransitionType type;
    private RectTransform rect;

    private const float TRANSITION_SPEED = 8f;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable() {
        switch (type) {
            case TransitionType.ZOOM_OUTER_TO_INNER:
                rect.localScale = Vector2.one * 4f;
                break;
            case TransitionType.ZOOM_INNER_TO_OUTER:
                rect.localScale = Vector2.zero;
                break;
        }
    }

    private void Update() {
        rect.localScale = Vector2.Lerp(rect.localScale, Vector2.one, TRANSITION_SPEED * Time.deltaTime);
    }
}
