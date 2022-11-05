using UnityEngine;

public class DragAndDropObject : MonoBehaviour {
    Vector2 distanceFromOrigin;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        distanceFromOrigin = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag() {
        transform.position = distanceFromOrigin + (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
