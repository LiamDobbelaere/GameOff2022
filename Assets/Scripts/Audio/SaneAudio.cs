using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaneSFX {
    public string name;
    public AudioClip[] audioClips;
    public float minPitch = 1f;
    public float maxPitch = 1f;
    public float volume = 1f;
}

[System.Serializable]
public class SaneSFXCategory {
    public string name;
    public SaneSFX[] soundEffects;
}

public class SaneAudio : MonoBehaviour {
    public static SaneAudio instance;

    [Header("SaneAudio 2")]
    public SaneSFXCategory[] soundEffectCategories;
    private List<AudioSource> genericSfxSources;

    private Dictionary<string, SaneSFX> soundEffectsDict;
    private void Awake() {
        Singletonify();
        BuildSoundEffectsDict();

        this.genericSfxSources = new List<AudioSource>();
    }

    private void Singletonify() {
        if (instance == null) {
            instance = this;

            // We have to be root for DontDestroyOnLoad to work
            gameObject.transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnDisable() {
        Lua.UnregisterFunction("AddMaxHealth");
    }


    private void BuildSoundEffectsDict() {
        soundEffectsDict = new Dictionary<string, SaneSFX>();

        foreach (SaneSFXCategory sfxCategory in soundEffectCategories) {
            foreach (SaneSFX sfx in sfxCategory.soundEffects) {
                soundEffectsDict.Add(sfxCategory.name + "." + sfx.name, sfx);
            }
        }
    }

    public void PlaySFX(AudioSource audioSource, string sfxName, float? pitchOverride = null, bool exclusive = false) {
        SaneSFX sfx = this.soundEffectsDict[sfxName];

        if (pitchOverride != null) {
            audioSource.pitch = (float)pitchOverride;
        } else {
            audioSource.pitch = Random.Range(sfx.minPitch, sfx.maxPitch);
        }
        audioSource.volume = sfx.volume;

        if (exclusive) {
            audioSource.clip = sfx.audioClips[Random.Range(0, sfx.audioClips.Length)];
            audioSource.Play();
        } else {
            audioSource.PlayOneShot(sfx.audioClips[Random.Range(0, sfx.audioClips.Length)]);
        }
    }

    /// <summary>
    /// Use this if you don't have an appropriate audio source to play the sound effect with.
    /// </summary>
    /// <param name="sfxName"></param>
    public void PlaySFX(string sfxName, float? pitchOverride = null) {
        AudioSource freeAudioSource = null;
        foreach (AudioSource audioSource in genericSfxSources) {
            if (audioSource != null && !audioSource.isPlaying) {
                freeAudioSource = audioSource;
                break;
            }
        }

        if (freeAudioSource == null) {
            freeAudioSource = gameObject.AddComponent<AudioSource>();
            genericSfxSources.Add(freeAudioSource);
        }

        PlaySFX(freeAudioSource, sfxName, pitchOverride, true);

        CleanUpGenericSources();
    }

    private void CleanUpGenericSources() {
        List<AudioSource> sourcesToRemove = new List<AudioSource>();

        foreach (AudioSource audioSource in genericSfxSources) {
            if (!audioSource.isPlaying) {
                sourcesToRemove.Add(audioSource);
            }
        }

        foreach (AudioSource sourceToRemove in sourcesToRemove) {
            genericSfxSources.Remove(sourceToRemove);
            Destroy(sourceToRemove);
        }
    }
}

public static class SaneAudioExtensions {
    /// <summary>
    /// Play a sound effect using the current GameObject's first found AudioSource.
    /// </summary>
    /// <param name="behaviour"></param>
    /// <param name="sfxName"></param>
    public static void PlaySFX(this MonoBehaviour behaviour, string sfxName) {
        AudioSource audioSource = behaviour.gameObject.GetComponent<AudioSource>();

        SaneAudio.instance.PlaySFX(audioSource, sfxName);
    }

    /// <summary>
    /// Play a sound effect using a specific AudioSource from the current GameObject.
    /// </summary>
    /// <param name="_"></param>
    /// <param name="audioSource"></param>
    /// <param name="sfxName"></param>
    public static void PlaySFX(this MonoBehaviour _, AudioSource audioSource, string sfxName) {
        SaneAudio.instance.PlaySFX(audioSource, sfxName);
    }
}
