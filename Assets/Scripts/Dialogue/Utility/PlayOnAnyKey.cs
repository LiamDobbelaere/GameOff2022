using PixelCrushers.DialogueSystem;
using UnityEngine;

public class PlayOnAnyKey : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKeyDown) {
            GetComponent<DialogueSystemTrigger>().enabled = true;
            enabled = false;
        }
    }
}
