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
        if (GameStateMono.instance.inPhotographyMode && !GameStateMono.instance.hasTakenPictureOf[key]) {
            spriteRenderer.color = Color.Lerp(defaultColor, Color.green, Mathf.Sin(Time.time * 10f));
        } else {
            spriteRenderer.color = defaultColor;
        }
    }

    private void OnMouseDown() {
        if (GameStateMono.instance.inPhotographyMode && !GameStateMono.instance.hasTakenPictureOf[key]) {
            GameStateMono.instance.hasTakenPictureOf[key] = true;
            GameStateMono.instance.inPhotographyMode = false;

            GameObject phoneCanvas = GameObject.FindGameObjectWithTag("PhoneCanvas");
            phoneCanvas.GetComponent<PhoneCanvas>().ShowPhone();
            phoneCanvas.GetComponentInChildren<Phone>().StartApp("Pictures");
        }
    }
}
