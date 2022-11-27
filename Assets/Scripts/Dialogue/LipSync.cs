using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LabelEntry {
    public float startSeconds;
    public float endSeconds;
    public string phoneme;
    public string poseName;
    public string eyesName;
}

[System.Serializable]
public struct PhonemeSprite {
    public string phoneme;
    public Sprite sprite;
}

public class LipSync : MonoBehaviour {
    public List<PhonemeSprite> phonemeSprites;
    public List<Sprite> simpleLipSyncSprites;

    // Own components
    private AudioSource asource;
    private SpriteRenderer rend;
    private Image mouthImage;

    // LipSync related stuff
    private List<LabelEntry> labels;
    private LabelEntry currentLabelEntry;
    private int lastUsedLabelEntry = 0;
    private Dictionary<string, Sprite> phonemeSpritesDict;
    private TextAsset labelsTextFile;
    private TextAsset lastLabelsTextFile;
    private AudioClip lastAudioClip;
    private PoseLibrary poseLibrary;

    // SimpleLipSync related stuff
    private float updateStep = 0.1f;
    private int sampleDataLength = 1024;
    private float currentUpdateTime = 0f;
    private float[] clipSampleData;
    private int lastAudioTime;

    public const bool ALWAYS_USE_SIMPLE_LIPSYNC = true;

    // Start is called before the first frame update
    void Start() {
        asource = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();

        phonemeSpritesDict = new Dictionary<string, Sprite>();
        foreach (PhonemeSprite phonemeSprite in phonemeSprites) {
            phonemeSpritesDict.Add(phonemeSprite.phoneme, phonemeSprite.sprite);
        }

        clipSampleData = new float[sampleDataLength];
    }

    private void ReloadLabelsTextFile() {
        lastLabelsTextFile = labelsTextFile;
        lastUsedLabelEntry = 0;

        labels = new List<LabelEntry>();
        currentLabelEntry = null;

        string labelText = labelsTextFile.text;
        string[] labelLines = Regex.Split(labelText, "\n|\r|\r\n");

        foreach (string labelLine in labelLines) {
            if (labelLine.Trim().Length > 0) {
                string[] parts = labelLine.Split('\t');

                string[] label = new string[] { };
                if (parts.Length > 2) {
                    label = parts[2].Split(' ');
                }

                string pose = null;
                string eyes = null;
                if (label.Length > 1) {
                    for (int i = 1; i < label.Length; i++) {
                        Eyes eyesDef = poseLibrary.eyes.Find((eye) => eye.name == label[i]);

                        if (eyesDef != null) {
                            eyes = eyesDef.name;
                        } else {
                            pose = label[1];
                        }
                    }
                }

                labels.Add(new LabelEntry {
                    startSeconds = float.Parse(parts[0]),
                    endSeconds = float.Parse(parts[1]),
                    phoneme = label[0],
                    poseName = pose,
                    eyesName = eyes
                });
            }
        }
    }

    // Update is called once per frame
    void Update() {
        SetupPoseLibraryIfNotSet();
        SetupMouthIfNotSet();

        if (lastAudioClip != asource.clip) {
            lastUsedLabelEntry = 0;
            currentLabelEntry = null;
            string labelsPath = "Labels/" + asource.clip.name + ".mp3.labels";
            Debug.Log("Loading label file " + labelsPath);
            labelsTextFile = Resources.Load<TextAsset>(labelsPath);
            Debug.Log(labelsTextFile);
            lastAudioClip = asource.clip;
        }

        if (labelsTextFile != lastLabelsTextFile) {
            ReloadLabelsTextFile();
        }

        if (asource.isPlaying) {
            if (labelsTextFile != null) {
                UpdateCurrentLabelEntry();
            }

            if (labelsTextFile != null) {
                if (ALWAYS_USE_SIMPLE_LIPSYNC) {
                    if (labels.Count > 0 && labels[0].phoneme != "$") SimpleLipSyncUpdate();
                } else {
                    LipSyncUpdate();
                }
            } else {
                SimpleLipSyncUpdate();
            }
        }
    }

    public LabelEntry GetCurrentLabelEntry() {
        return currentLabelEntry;
    }

    public void OnConversationStart() {
        currentLabelEntry = null;
    }

    private void UpdateCurrentLabelEntry() {
        for (int i = lastUsedLabelEntry; i < labels.Count; i++) {
            LabelEntry labelEntry = labels[i];

            if (
                asource.timeSamples >= (labelEntry.startSeconds * asource.clip.frequency)
                && asource.timeSamples <= (labelEntry.endSeconds * asource.clip.frequency)
            ) {
                currentLabelEntry = labelEntry;
                lastUsedLabelEntry = i + 1;

                break;
            }
        }
    }

    private void LipSyncUpdate() {
        Sprite targetSprite = null;
        string phonemeCorrected = currentLabelEntry.phoneme;
        if (phonemeCorrected == "$") {
            phonemeCorrected = "-";
        }

        if (currentLabelEntry == null) {
            targetSprite = phonemeSpritesDict["-"];
        } else {
            targetSprite = phonemeSpritesDict[phonemeCorrected];
        }

        if (mouthImage != null && mouthImage.overrideSprite != targetSprite) {
            mouthImage.overrideSprite = targetSprite;
            mouthImage.SetNativeSize();
        }

        if (rend != null && rend.sprite != targetSprite) {
            rend.sprite = targetSprite;
        }
    }

    private void SimpleLipSyncUpdate() {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep) {
            currentUpdateTime = 0f;

            if (asource.clip == null) {
                return;
            }

            asource.clip.GetData(clipSampleData, asource.timeSamples);
            float clipLoudness = 0f;
            foreach (var sample in clipSampleData) {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

            Sprite targetSprite = simpleLipSyncSprites[Mathf.RoundToInt(
                Mathf.Clamp01(clipLoudness * 8f) * (simpleLipSyncSprites.Count - 1))
            ];

            if (mouthImage != null && mouthImage.overrideSprite != targetSprite) {
                mouthImage.overrideSprite = targetSprite;
                mouthImage.SetNativeSize();
            }

            if (rend != null && rend.sprite != targetSprite) {
                rend.sprite = targetSprite;
            }
        }
    }

    private void SetupMouthIfNotSet() {
        if (mouthImage != null) {
            return;
        }

        GameObject dialogueSystemMouth = GameObject.FindGameObjectWithTag("DialogueSystemMouth");
        if (dialogueSystemMouth != null) {
            mouthImage = dialogueSystemMouth.GetComponent<Image>();

            if (mouthImage != null) {
                mouthImage.overrideSprite = phonemeSpritesDict["-"];
            }
        }
    }

    private void SetupPoseLibraryIfNotSet() {
        if (poseLibrary != null) {
            return;
        }

        GameObject poseLibraryGameObject = GameObject.FindGameObjectWithTag("PoseLibrary");
        if (poseLibraryGameObject != null) {
            poseLibrary = poseLibraryGameObject.GetComponent<PoseLibrary>();
            Debug.Log(poseLibrary);
        }
    }
}
