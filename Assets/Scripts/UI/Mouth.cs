using UnityEngine;
using UnityEngine.UI;

public class Mouth : MonoBehaviour {
    private RectTransform rect;
    private Image portrait;

    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        portrait = transform.parent.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (portrait.overrideSprite == null) {
            return;
        }

        Sprite sprite = portrait.overrideSprite;
        Bounds bounds = sprite.bounds;
        var pivotX = -bounds.center.x / bounds.extents.x / 2 + 0.5f;
        var pivotY = -bounds.center.y / bounds.extents.y / 2 + 0.5f;


        float flipXFloat = portrait.overrideSprite.name.Contains("$MF") ? -1f : 1f;

        transform.position = new Vector2(Screen.width, Screen.height) * new Vector2(pivotX, pivotY);
        transform.localScale = new Vector3(.8f * flipXFloat, .8f, 1f);
    }
}
