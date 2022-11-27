using UnityEngine;

public class SlowZoomer : MonoBehaviour {
    public float scaleFactor;
    public float speed = 5f;

    private Vector2 targetScale;

    // Start is called before the first frame update
    void Start() {
        Vector2 startScale = transform.localScale;
        targetScale = startScale * scaleFactor;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale = Vector2.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
    }
}
