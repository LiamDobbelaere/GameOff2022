using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LabelEntry {
    public float startSeconds;
    public float endSeconds;
    public string phoneme;
    public string poseName;
}

[System.Serializable]
public struct PhonemeSprite {
    public string phoneme;
    public Sprite sprite;
}

public class LipSync : MonoBehaviour {
    public TextAsset labelsTextFile;
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
    private TextAsset lastLabelsTextFile;

    // SimpleLipSync related stuff
    private float updateStep = 0.1f;
    private int sampleDataLength = 1024;
    private float currentUpdateTime = 0f;
    private float[] clipSampleData;
    private int lastAudioTime;

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

                string[] label = parts[2].Split(' ');
                string pose = null;
                if (label.Length > 1) {
                    pose = label[1];
                }

                labels.Add(new LabelEntry {
                    startSeconds = float.Parse(parts[0]),
                    endSeconds = float.Parse(parts[1]),
                    phoneme = label[0],
                    poseName = pose
                });
            }
        }
    }

    // Update is called once per frame
    void Update() {
        SetupMouthIfNotSet();

        if (labelsTextFile != lastLabelsTextFile) {
            ReloadLabelsTextFile();
        }

        if (asource.isPlaying) {
            if (labelsTextFile != null) {
                LipSyncUpdate();
            } else {
                SimpleLipSyncUpdate();
            }
        }

        if (asource.timeSamples < lastAudioTime) {
            lastUsedLabelEntry = 0;
        }

        lastAudioTime = asource.timeSamples;
    }

    public LabelEntry GetCurrentLabelEntry() {
        return currentLabelEntry;
    }

    private void LipSyncUpdate() {
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

        Sprite targetSprite = null;
        if (currentLabelEntry == null) {
            targetSprite = phonemeSpritesDict["-"];
        } else {
            targetSprite = phonemeSpritesDict[currentLabelEntry.phoneme];
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

            asource.clip.GetData(clipSampleData, asource.timeSamples);
            float clipLoudness = 0f;
            foreach (var sample in clipSampleData) {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

            rend.sprite = simpleLipSyncSprites[Mathf.RoundToInt(
                Mathf.Clamp01(clipLoudness * 8f) * (simpleLipSyncSprites.Count - 1))
            ];
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
}
