using UnityEngine;
using UnityEngine.UI;

public class RotateButton : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            DragAndDropObject item =
                GameObject.FindGameObjectWithTag("DragAndDropItems").GetComponent<DragAndDropItems>().lastTouched;

            if (item != null) {
                item.Rotate();
            }
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
