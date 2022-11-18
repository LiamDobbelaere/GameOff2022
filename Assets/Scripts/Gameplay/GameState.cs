using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameState {
    public static string lastScene;
    public static Vector2? lastPosition = null;
    public static Texture2D customFlagTexture = new Texture2D(256, 256, TextureFormat.RGB24, false);
    public static Texture2D customFlagTextureDrawn = new Texture2D(256, 256, TextureFormat.RGB24, false);
    public static bool customFlagIsJollyRoger = false;
    public static Dictionary<string, bool> hasTakenPictureOf = new Dictionary<string, bool>() {
        ["flag"] = false,
        ["hearthorn"] = false,
        ["seagulls"] = false
    };

    public static void StoreLastLocation() {
        lastScene = SceneManager.GetActiveScene().name;
        lastPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public static void LoadLastLocation() {
        SceneManager.LoadScene(lastScene);
    }
}
