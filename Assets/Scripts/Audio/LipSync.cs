using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LabelEntry {
    public float startSeconds;
    public float endSeconds;
    public string label;
}

[System.Serializable]
public struct PhonemeSprite {
    public string phoneme;
    public Sprite sprite;
}

public class LipSync : MonoBehaviour {
    public TextAsset labelsTextFile;
    public List<PhonemeSprite> phonemeSprites;

    private AudioSource asource;
    private SpriteRenderer rend;
    private List<LabelEntry> labels;
    private int currentLabelEntryIndex;
    private LabelEntry currentLabelEntry;
    private Dictionary<string, Sprite> phonemeSpritesDict;

    // Start is called before the first frame update
    void Start() {
        asource = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();

        phonemeSpritesDict = new Dictionary<string, Sprite>();
        foreach (PhonemeSprite phonemeSprite in phonemeSprites) {
            phonemeSpritesDict.Add(phonemeSprite.phoneme, phonemeSprite.sprite);
        }

        labels = new List<LabelEntry>();
        currentLabelEntryIndex = 0;
        currentLabelEntry = null;

        string labelText = labelsTextFile.text;
        string[] labelLines = Regex.Split(labelText, "\n|\r|\r\n");

        foreach (string labelLine in labelLines) {
            if (labelLine.Trim().Length > 0) {
                string[] parts = labelLine.Split('\t');

                labels.Add(new LabelEntry {
                    startSeconds = float.Parse(parts[0]),
                    endSeconds = float.Parse(parts[1]),
                    label = parts[2]
                });
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (asource.isPlaying) {
            foreach (LabelEntry labelEntry in labels) {
                if (
                    asource.timeSamples >= (labelEntry.startSeconds * asource.clip.frequency)
                    && asource.timeSamples <= (labelEntry.endSeconds * asource.clip.frequency)) {
                    currentLabelEntry = labelEntry;
                    currentLabelEntryIndex++;

                    if (currentLabelEntryIndex >= labels.Count) {
                        currentLabelEntryIndex = 0;
                    }

                    break;
                }
            }

            if (currentLabelEntry == null) {
                rend.sprite = phonemeSpritesDict["-"];
            } else {
                rend.sprite = phonemeSpritesDict[currentLabelEntry.label];
            }
        }
    }
}
