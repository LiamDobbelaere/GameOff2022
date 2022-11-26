using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DynamicFlagTexture : MonoBehaviour {
    public Texture hearthornFlag;
    public Texture seagullsFlag;

    // Start is called before the first frame update
    void Start() {
        string chosenPhoto = DialogueLua.GetVariable("Chosen Photo For Flag").asString;

        if (chosenPhoto.Contains("flag")) {
            GetComponent<Renderer>().material.mainTexture = GameState.customFlagTextureDrawn;
        } else if (chosenPhoto == "hearthorn") {
            GetComponent<Renderer>().material.mainTexture = hearthornFlag;
        } else if (chosenPhoto == "seagulls") {
            GetComponent<Renderer>().material.mainTexture = seagullsFlag;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
