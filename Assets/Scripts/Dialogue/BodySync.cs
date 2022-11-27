using UnityEngine;
using UnityEngine.UI;

public class BodySync : MonoBehaviour {
    private LipSync lipSync;
    private Transform mouth;
    private Transform eyes;
    private Transform simpleEyes;
    private Image portrait;
    private PoseLibrary poseLibrary;
    private Pose currentPose;
    private Eyes currentEyes;
    private float mouthScaleFactor = 0.5f;
    private float eyesScaleFactor = 1f; //0.75f;

    // Start is called before the first frame update
    void Start() {
        lipSync = GetComponent<LipSync>();
    }

    // Update is called once per frame
    void Update() {
        SetupPoseLibraryIfNotSet();
        SetupMouthIfNotSet();
        SetupEyesIfNotSet();
        SetupSimpleEyesIfNotSet();
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

        if (portrait != null) {
            portrait.enabled = currentLabelEntry != null;
        }

        if (mouth != null) {
            mouth.GetComponent<Image>().enabled = currentLabelEntry != null;
        }

        if (eyes != null) {
            eyes.GetComponent<Image>().enabled = currentLabelEntry != null;
        }

        if (simpleEyes != null) {
            simpleEyes.GetComponent<Image>().enabled = currentLabelEntry != null;
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
            eyes.GetComponent<Image>().enabled = true;
            if (simpleEyes != null) simpleEyes.GetComponent<Image>().enabled = false;

            if (currentPose.useSimpleEyes == false) {
                UpdateEyes();
            }
        }

        if (simpleEyes != null) {
            if (eyes != null) eyes.GetComponent<Image>().enabled = false;
            simpleEyes.GetComponent<Image>().enabled = true && currentLabelEntry != null;

            if (currentPose.useSimpleEyes == true) {
                UpdateSimpleEyes();
            }
        }
    }

    private void UpdatePortrait() {
        portrait.gameObject.SetActive(true);
        portrait.overrideSprite = currentPose.pose;
    }

    private void UpdateMouth() {
        ApplyReferenceToTransform(currentPose.mouth, mouth, mouthScaleFactor);
    }

    private void UpdateEyes() {
        ApplyReferenceToTransform(currentPose.eyes, eyes, eyesScaleFactor);
        eyes.GetComponent<Image>().overrideSprite = currentEyes.sprite;
    }

    private void UpdateSimpleEyes() {
        simpleEyes.GetComponent<Image>().overrideSprite = currentEyes.sprite;
    }

    private void ApplyReferenceToTransform(Reference reference, Transform target, float scaleFactor = 1f) {
        target.position = new Vector2(Screen.width, Screen.height) * reference.position;
        target.localScale = reference.scale * scaleFactor;
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

    private void SetupSimpleEyesIfNotSet() {
        if (simpleEyes != null) {
            return;
        }

        GameObject dialogueSystemEyes = GameObject.FindGameObjectWithTag("DialogueSystemSimpleEyes");
        if (dialogueSystemEyes != null) {
            simpleEyes = dialogueSystemEyes.transform;
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
