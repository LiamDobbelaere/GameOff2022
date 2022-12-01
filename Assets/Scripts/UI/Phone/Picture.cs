using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour {
    public string pictureKeyName;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SaneAudio.PlaySFX("phone.click-button");

            string finalKeyName = pictureKeyName;
            if (pictureKeyName == "flag") {
                finalKeyName += GameStateMono.instance.customFlagIsJollyRoger ? "-jolly" : "-other";
            }

            DialogueLua.SetVariable("Chosen Photo For Flag", finalKeyName);

            GetComponentInParent<PhoneCanvas>().HidePhone();
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
