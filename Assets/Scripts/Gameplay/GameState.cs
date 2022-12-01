using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Notification {
    public string message;
}

public class GameState {
    public bool isFirstTime = true;
    public string lastScene;
    public Vector2? lastPosition = null;
    public Texture2D customFlagTexture = new Texture2D(256, 256, TextureFormat.RGB24, false);
    public Texture2D customFlagTextureDrawn = new Texture2D(256, 256, TextureFormat.RGB24, false);
    public bool customFlagIsJollyRoger = false;
    public Dictionary<string, bool> hasTakenPictureOf = new Dictionary<string, bool>() {
        ["flag"] = false,
        ["hearthorn"] = false,
        ["seagulls"] = false
    };
    public bool inPhotographyMode = false;
    public string targetMarker;
    public List<Notification> notifications = new List<Notification> { };
    public bool hasUnreadNotifications = false;
    public List<string> documentaryScenes = new List<string> { };
    private float lastNotificationTime = float.MinValue;
    public bool justSawTheFlag = false;
    public string lastChosenFilterPhoto = "";

    public void StoreLastLocation() {
        if (GameObject.FindGameObjectWithTag("Player") != null) {
            lastScene = SceneManager.GetActiveScene().name;
            lastPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    public void LoadLastLocation() {
        SceneManager.LoadScene(lastScene);
    }

    public void ShowUIHint(string message) {
        if (GameObject.FindGameObjectWithTag("HintsCanvas") != null) {
            HintsCanvas canvas = GameObject.FindGameObjectWithTag("HintsCanvas").GetComponent<HintsCanvas>();
            if (canvas != null) {
                canvas.ShowHint(message);
            }
        }
    }

    public void AddNotification(string message) {
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

    public void AddDocumentaryScene(string sceneName) {
        documentaryScenes.Add(sceneName);
        ShowUIHint("You made progress on your documentary!");
    }
}
