using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TodoItem : MonoBehaviour {
    private TextMeshProUGUI label;
    private Toggle toggle;

    // Start is called before the first frame update
    void Start() {
        label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((bool _) => {
            RefreshLabelFontStyle();
        });

        RefreshLabelFontStyle();
    }

    // Update is called once per frame
    void Update() {

    }

    private void RefreshLabelFontStyle() {
        label.fontStyle = toggle.isOn ? FontStyles.Strikethrough : FontStyles.Normal;
    }
}
