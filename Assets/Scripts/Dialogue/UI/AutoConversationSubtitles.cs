using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class AutoConversationSubtitles : MonoBehaviour {
    private Color defaultImageColor;
    private string lastConversation = null;
    private bool eliminateElements = false;

    // Start is called before the first frame update
    void Start() {
        defaultImageColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        string currentConversation = DialogueManager.LastConversationStarted;
        if (currentConversation != lastConversation) {
            OnStartConversation(currentConversation);
        }

        Transform portraitImage = transform.Find("Portrait Image");
        if (portraitImage != null) {
            portraitImage.gameObject.SetActive(!eliminateElements);
        }

        transform.Find("Portrait Name").gameObject.SetActive(!eliminateElements);
        transform.Find("Divider").gameObject.SetActive(!eliminateElements);
        transform.Find("Continue Button").gameObject.SetActive(!eliminateElements);

    }

    public void OnStartConversation(string conversationName) {
        lastConversation = conversationName;

        var conversation = DialogueManager.MasterDatabase.GetConversation(conversationName);
        bool automatic = conversation.LookupBool("automatic");

        if (automatic) {
            //GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

            eliminateElements = true;
            transform.Find("Subtitle Text").GetComponent<UnityUITypewriterEffect>().enabled = false;
            transform.Find("Subtitle Text").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            transform.Find("Subtitle Text").GetComponent<Text>().fontSize = 36;


        } else {
            //GetComponent<Image>().color = defaultImageColor;

            eliminateElements = false;
            transform.Find("Subtitle Text").GetComponent<UnityUITypewriterEffect>().enabled = true;
            transform.Find("Subtitle Text").GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            transform.Find("Subtitle Text").GetComponent<Text>().fontSize = 20;
        }
    }
}
