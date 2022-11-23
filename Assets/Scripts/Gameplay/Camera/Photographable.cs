using UnityEngine;

public class Photographable : MonoBehaviour {
    public string key;

    private Color defaultColor;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update() {
        if (GameState.inPhotographyMode && !GameState.hasTakenPictureOf[key]) {
            spriteRenderer.color = Color.Lerp(defaultColor, Color.green, Mathf.Sin(Time.time * 10f));
        } else {
            spriteRenderer.color = defaultColor;
        }
    }

    private void OnMouseDown() {
        if (GameState.inPhotographyMode && !GameState.hasTakenPictureOf[key]) {
            GameState.hasTakenPictureOf[key] = true;
            GameState.inPhotographyMode = false;

            GameObject phoneCanvas = GameObject.FindGameObjectWithTag("PhoneCanvas");
            phoneCanvas.GetComponent<PhoneCanvas>().ShowPhone();
            phoneCanvas.GetComponentInChildren<Phone>().StartApp("Pictures");
        }
    }
}
