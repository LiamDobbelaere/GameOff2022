using PixelCrushers.DialogueSystem;
using UnityEngine;

public class AutoConversation : MonoBehaviour {
    private DialogueSystemController controller;

    // Start is called before the first frame update
    void Awake() {
        controller = GetComponent<DialogueSystemController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnConversationStart() {
        var conversation = DialogueManager.MasterDatabase.GetConversation(DialogueManager.LastConversationStarted);

        bool automatic = conversation.LookupBool("automatic");

        controller.displaySettings.subtitleSettings.continueButton =
            automatic ?
            DisplaySettings.SubtitleSettings.ContinueButtonMode.Never :
            DisplaySettings.SubtitleSettings.ContinueButtonMode.Always;
    }
}
