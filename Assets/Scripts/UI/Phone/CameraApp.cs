using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraApp : MonoBehaviour {
    private RawImage cameraFeed;

    // Start is called before the first frame update
    void Start() {
        cameraFeed = transform.Find("Body/CameraFeed").GetComponent<RawImage>();
        GameObject flagCaptureCamera = GameObject.FindGameObjectWithTag("FlagCaptureCamera");
        if (flagCaptureCamera != null) {
            cameraFeed.texture = flagCaptureCamera.GetComponent<Camera>().targetTexture;
        }
    }

    private void OnEnable() {
        if (SceneManager.GetActiveScene().name == "FlagEditor") {
            return;
        }

        GameObject[] photographables = GameObject.FindGameObjectsWithTag("Photographable");
        bool canTakePictures = false;
        foreach (GameObject photographable in photographables) {
            string key = photographable.GetComponent<Photographable>().key;

            if (!GameState.hasTakenPictureOf[key]) {
                canTakePictures = true;
                break;
            }
        }

        if (canTakePictures) {
            GameState.inPhotographyMode = true;
            GetComponentInParent<Phone>().StartApp("HomeScreen");
            GetComponentInParent<PhoneCanvas>().HidePhone();

            GameState.ShowUIHint("Click on a glowing character or object to take a photo of it!");
        } else {
            GetComponentInParent<Phone>().StartApp("HomeScreen");
            GetComponentInParent<PhoneCanvas>().HidePhone();

            GameState.ShowUIHint("There is nothing interesting to take a photo of here..");
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
