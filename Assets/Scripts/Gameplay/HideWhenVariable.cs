using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HideWhenVariable : MonoBehaviour {
    public string whenVariable;
    public string isValue;

    // Start is called before the first frame update
    void Start() {
        if (DialogueLua.GetVariable(whenVariable).asString == isValue) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
