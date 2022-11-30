using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Notification {
    public string message;
}

public static class GameState {
    public static bool isFirstTime = true;
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
    public static bool inPhotographyMode = false;
    public static string targetMarker;
    public static List<Notification> notifications = new List<Notification> { };
    public static bool hasUnreadNotifications = false;
    public static List<string> documentaryScenes = new List<string> { };
    private static float lastNotificationTime = float.MinValue;
    public static bool justSawTheFlag = false;
    public static string lastChosenFilterPhoto = "";

    public static void StoreLastLocation() {
        lastScene = SceneManager.GetActiveScene().name;
        lastPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public static void LoadLastLocation() {
        SceneManager.LoadScene(lastScene);
    }

    public static void ShowUIHint(string message) {
        GameObject.FindGameObjectWithTag("HintsCanvas").GetComponent<HintsCanvas>().ShowHint(message);
    }

    public static void AddNotification(string message) {
        if (Time.time - lastNotificationTime > 0.5f) {
            SaneAudio.PlaySFX("phone.notification");
            lastNotificationTime = Time.time;
        }
        /*notifications.Insert(0, new Notification {
            message = message
        });*/
        notifications.Add(new Notification {
            message = (notifications.Count + 1).ToString() + " - " + message
        });
        hasUnreadNotifications = true;
        ShowUIHint("You have a new notification, press TAB to open your phone!");
    }

    public static void AddDocumentaryScene(string sceneName) {
        documentaryScenes.Add(sceneName);
        ShowUIHint("You made progress on your documentary!");
    }
}
