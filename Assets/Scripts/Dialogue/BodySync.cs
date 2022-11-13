using UnityEngine;
using UnityEngine.UI;

public class BodySync : MonoBehaviour {
    private LipSync lipSync;
    private Transform mouth;
    private Transform eyes;
    private Image portrait;
    private PoseLibrary poseLibrary;
    private Pose currentPose;
    private Eyes currentEyes;
    private float mouthScaleFactor = 0.5f;

    // Start is called before the first frame update
    void Start() {
        lipSync = GetComponent<LipSync>();
    }

    // Update is called once per frame
    void Update() {
        SetupPoseLibraryIfNotSet();
        SetupMouthIfNotSet();
        SetupEyesIfNotSet();
        SetupPortraitIfNotSet();

        if (poseLibrary == null) {
            return;
        }

        LabelEntry currentLabelEntry = lipSync.GetCurrentLabelEntry();
        if (currentLabelEntry != null && currentLabelEntry.poseName != null) {
            currentPose = poseLibrary.GetPose(currentLabelEntry.poseName);

            if (currentLabelEntry.eyesName != null) {
                currentEyes = poseLibrary.GetEyes(currentLabelEntry.eyesName);
            }
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

        if (eyes != null) {
            UpdateEyes();
        }
    }

    private void UpdatePortrait() {
        portrait.gameObject.SetActive(true);
        portrait.overrideSprite = currentPose.pose;
    }

    private void UpdateMouth() {
        ApplyReferenceToTransform(currentPose.mouth, mouth);
    }

    private void UpdateEyes() {
        ApplyReferenceToTransform(currentPose.eyes, eyes);
        eyes.GetComponent<Image>().overrideSprite = currentEyes.sprite;
    }

    private void ApplyReferenceToTransform(Reference reference, Transform target) {
        target.position = new Vector2(Screen.width, Screen.height) * reference.position;
        target.localScale = reference.scale * mouthScaleFactor;
        target.localRotation = reference.rotation;
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
    private void SetupEyesIfNotSet() {
        if (eyes != null) {
            return;
        }

        GameObject dialogueSystemEyes = GameObject.FindGameObjectWithTag("DialogueSystemEyes");
        if (dialogueSystemEyes != null) {
            eyes = dialogueSystemEyes.transform;
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
