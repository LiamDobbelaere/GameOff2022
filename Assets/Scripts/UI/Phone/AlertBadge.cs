using UnityEngine;
using UnityEngine.UI;

public class AlertBadge : MonoBehaviour {
    private Image image;

    private const float MAX_UPDATE_TIME = 0.3f;
    private float updateTimer;

    // Start is called before the first frame update
    void Start() {
        image = GetComponent<Image>();
    }

    private void OnEnable() {
        image.enabled = GameState.hasUnreadNotifications;
    }

    // Update is called once per frame
    void Update() {
        updateTimer += Time.deltaTime;
        if (updateTimer > MAX_UPDATE_TIME) {
            image.enabled = GameState.hasUnreadNotifications;
            updateTimer = 0f;
        }
    }
}
