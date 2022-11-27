using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DocumentarySequencer : MonoBehaviour {
    private static DocumentarySequencer instance = null;
    private Stack<string> scenes = new Stack<string>(
        new string[] {
            "Plunder03", 
            "Plunder02",
            "Plunder01",
            "Intro02",
        }
    );
    private string lastTopOfStack = null;
    private bool allowAdvancing = false;
    private AudioSource audioSourceA;
    private AudioSource audioSourceB;
    private float audioSourceATargetVolume = 0f;
    private float audioSourceBTargetVolume = 0f;
    private const float AUDIO_FADE_SPEED = 0.5f;

    // Start is called before the first frame update
    void Start() {
        EnforceSingleton();
        SubscribeToSceneChangeEvent();
    }

    // Update is called once per frame
    void Update() {
        UpdateScenePreloading();
        UpdateAudioSources();
    }

    private void EnforceSingleton() {
        if (instance == null) {
            instance = this;
            SpawnAudioSources();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void SpawnAudioSources() {
        audioSourceA = gameObject.AddComponent<AudioSource>();
        audioSourceB = gameObject.AddComponent<AudioSource>();

        audioSourceA.spatialBlend = 0.0f;
        audioSourceB.spatialBlend = audioSourceA.spatialBlend;
    }

    public void Advance() {
        allowAdvancing = true;
    }
    private void UpdateAudioSources() {
        FadeAudioSourceToTargetVolume(audioSourceA, audioSourceATargetVolume);
        FadeAudioSourceToTargetVolume(audioSourceB, audioSourceBTargetVolume);
    }

    private void FadeAudioSourceToTargetVolume(AudioSource audioSource, float targetVolume) {
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, AUDIO_FADE_SPEED * Time.deltaTime);
    }

    private void UpdateScenePreloading() {
        if (MustPreloadNextScene()) {
            PreloadNextScene();
        }
    }

    private bool MustPreloadNextScene() {
        return scenes.Count > 0 && lastTopOfStack != scenes.Peek();
    }

    private void PreloadNextScene() {
        lastTopOfStack = scenes.Peek();
        StartCoroutine(PreloadScene());

        Debug.Log("[DS] Preloading " + lastTopOfStack);
    }

    IEnumerator PreloadScene() {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(lastTopOfStack);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone) {
            if (asyncOperation.progress >= 0.9f) {
                if (allowAdvancing) {
                    Debug.Log("[DS] Triggering advance to " + lastTopOfStack);
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    private void SubscribeToSceneChangeEvent() {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene oldScene, Scene newScene) {
        Debug.Log("[DS] Received active scene changed event");
        RemovePreviousSceneFromStack();
        ResetAdvancingFlag();
        ParseSceneOptions();
    }

    private void ParseSceneOptions() {
        DocumentarySceneOptions sceneOptions = GetDocumentarySceneOptions();

        SetMusicTrack(sceneOptions.music, sceneOptions.musicVolume);
    }

    private void SetMusicTrack(AudioClip clip, float volume) {
        if (!audioSourceA.isPlaying || audioSourceA.volume <= 0.05f) {
            if (clip == audioSourceB.clip) {
                return;
            }
            audioSourceA.clip = clip;
            audioSourceA.volume = 0f;
            audioSourceA.Play();
            audioSourceATargetVolume = volume;
            audioSourceBTargetVolume = 0f;
        } else {
            if (clip == audioSourceA.clip) {
                return;
            }
            audioSourceB.clip = clip;
            audioSourceB.volume = 0f;
            audioSourceB.Play();
            audioSourceBTargetVolume = volume;
            audioSourceATargetVolume = 0f;
        }
    }

    private DocumentarySceneOptions GetDocumentarySceneOptions() {
        return GameObject
            .FindGameObjectWithTag("DocumentarySceneOptions")
            .GetComponent<DocumentarySceneOptions>();
    }

    private void RemovePreviousSceneFromStack() {
        scenes.Pop();
    }

    private void ResetAdvancingFlag() {
        allowAdvancing = false;
    }
}
