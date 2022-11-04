using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AudioPhonemeGenerator : EditorWindow {
    private static string sentence;
    private static Dictionary<string, string[]> wordToPhonemes = null;

    void OnGUI() {
        sentence = EditorGUILayout.TextField("Sentence", sentence);

        if (GUILayout.Button("Generate")) {
            List<string> phonemesForSentence = new List<string>();
            string[] sentenceWords = sentence.Split(' ');
            foreach (string unfilteredWord in sentenceWords) {
                string word = unfilteredWord.Trim().ToUpper();
                string[] phonemes = wordToPhonemes[word];

                if (phonemes == null) {
                    Debug.LogError("Missing phonemes for word " + word);
                    return;
                }

                foreach (string phoneme in phonemes) {
                    phonemesForSentence.Add(phoneme);
                }
            }

            List<string> fileLines = new List<string>();
            float segmentInterval = (((AudioClip)Selection.activeObject).length) / (float)phonemesForSentence.Count;
            for (int i = 0; i < phonemesForSentence.Count; i++) {
                float startTime = i * segmentInterval;
                float endTime = startTime + segmentInterval;
                string phoneme = phonemesForSentence[i];

                string outputLine =
                    startTime.ToString("0.000000")
                    + "\t"
                    + endTime.ToString("0.000000")
                    + "\t"
                    + phoneme;

                fileLines.Add(outputLine);
            }

            File.WriteAllText(
                AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID()) + ".labels.txt",
                string.Join('\n', fileLines)
            );

            //Debug.Log(segmentInterval);
            Debug.Log("Generated phonemes for " + sentence);
            //Debug.Log(AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID()));
            //Debug.Log(((AudioClip)Selection.activeObject).length);
            //Debug.Log(Application.dataPath);

            Close();
        }

        if (GUILayout.Button("Cancel")) {
            Close();
        }
    }

    [MenuItem("Assets/Generate LipSync label template")]
    public static void Init() {
        if (wordToPhonemes == null) {
            wordToPhonemes = new Dictionary<string, string[]>();

            string[] cmuDictLines =
                File.ReadAllText(Path.Join(Application.dataPath, "Text/cmudict-0.7b.txt")).Split('\n');

            foreach (string cmuDictLine in cmuDictLines) {
                if (cmuDictLine.StartsWith(';')) {
                    continue;
                }

                string trimmedLine = cmuDictLine.Trim();
                if (trimmedLine.Length == 0) {
                    continue;
                }

                string[] wordAndPhonemes = trimmedLine.Split("  ");
                if (wordAndPhonemes.Length < 2) {
                    Debug.LogError(wordAndPhonemes[0]);
                }
                string[] phonemes = wordAndPhonemes[1].Split(' ');
                for (int i = 0; i < phonemes.Length; i++) {
                    if (phonemes[i].Length > 2) {
                        phonemes[i] = phonemes[i].Substring(0, 2);
                    }
                }

                wordToPhonemes.Add(wordAndPhonemes[0], phonemes);
            }

            Debug.Log("CMUDict loaded successfully for the first time");
        }
        new AudioPhonemeGenerator().ShowModal();
    }
}
