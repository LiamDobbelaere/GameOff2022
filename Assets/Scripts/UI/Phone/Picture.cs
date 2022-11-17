using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour {
    public string pictureKeyName;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            DialogueLua.SetVariable("Chosen Photo For Flag", pictureKeyName);

            GetComponentInParent<PhoneCanvas>().HidePhone();
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
