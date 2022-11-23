using TMPro;
using UnityEngine;

public class HintsCanvas : MonoBehaviour {
    private TextMeshProUGUI text;

    private Color textTargetColor;
    private Color defaultColor = Color.white;
    private Color transparent = new Color(1f, 1f, 1f, 0f);

    private float hintDisplayTimer = 0f;

    // Start is called before the first frame update
    void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = transparent;
        textTargetColor = text.color;
    }

    // Update is called once per frame
    void Update() {
        if (hintDisplayTimer > 0f) {
            hintDisplayTimer -= Time.deltaTime;

            if (hintDisplayTimer <= 0f) {
                textTargetColor = transparent;
            }
        }

        text.color = Color.Lerp(text.color, textTargetColor, Time.deltaTime * 5f);
    }

    public void ShowHint(string message) {
        text.text = message;
        textTargetColor = defaultColor;
        hintDisplayTimer = Mathf.Max(2f, (message.Length / 8f));
    }
}
