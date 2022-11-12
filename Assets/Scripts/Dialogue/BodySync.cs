using UnityEngine;
using UnityEngine.UI;

public class BodySync : MonoBehaviour {
    private LipSync lipSync;
    private Transform mouth;
    private Image portrait;
    private PoseLibrary poseLibrary;
    private Pose currentPose;
    private float mouthScaleFactor = 0.5f;

    // Start is called before the first frame update
    void Start() {
        lipSync = GetComponent<LipSync>();
    }

    // Update is called once per frame
    void Update() {
        SetupPoseLibraryIfNotSet();
        SetupMouthIfNotSet();
        SetupPortraitIfNotSet();

        if (poseLibrary == null) {
            return;
        }

        LabelEntry currentLabelEntry = lipSync.GetCurrentLabelEntry();
        if (currentLabelEntry != null && currentLabelEntry.poseName != null) {
            currentPose = poseLibrary.GetPose(currentLabelEntry.poseName);
        }

        if (currentPose == null) {
            return;
        }

        if (portrait != null) {
            UpdatePortrait();
        }

        if (mouth != null) {
            UpdateMouth();
        }
    }

    private void UpdatePortrait() {
        portrait.gameObject.SetActive(true);
        portrait.overrideSprite = currentPose.pose;
    }

    private void UpdateMouth() {
        MouthReference mouthRef = currentPose.mouth;

        mouth.position = new Vector2(Screen.width, Screen.height) * mouthRef.position;
        mouth.localScale = mouthRef.scale * mouthScaleFactor;
        mouth.localRotation = mouthRef.rotation;
    }

    private void SetupMouthIfNotSet() {
        if (mouth != null) {
            return;
        }

        GameObject dialogueSystemMouth = GameObject.FindGameObjectWithTag("DialogueSystemMouth");
        if (dialogueSystemMouth != null) {
            mouth = dialogueSystemMouth.transform;
        }
    }

    private void SetupPortraitIfNotSet() {
        if (portrait != null) {
            return;
        }

        GameObject dialogueSystemPortrait = GameObject.FindGameObjectWithTag("DialogueSystemPortrait");
        if (dialogueSystemPortrait != null) {
            portrait = dialogueSystemPortrait.GetComponent<Image>();
        }
    }

    private void SetupPoseLibraryIfNotSet() {
        if (poseLibrary != null) {
            return;
        }

        GameObject poseLibraryGameObject = GameObject.FindGameObjectWithTag("PoseLibrary");
        if (poseLibraryGameObject != null) {
            poseLibrary = poseLibraryGameObject.GetComponent<PoseLibrary>();
        }
    }
}
