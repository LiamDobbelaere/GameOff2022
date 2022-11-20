using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DisableTriggerOnVariable : MonoBehaviour {
    public string variableName;

    private float updateTime;
    private const float MAX_UPDATE_TIME = 0.3f;

    private Usable usable;

    // Start is called before the first frame update
    void Start() {
        usable = GetComponent<Usable>();
    }

    // Update is called once per frame
    void Update() {
        updateTime += Time.deltaTime;
        if (updateTime > MAX_UPDATE_TIME) {
            GetComponent<DialogueSystemTrigger>().enabled = !DialogueLua.GetVariable(variableName).asBool;
            updateTime = 0f;
        }
    }
}
