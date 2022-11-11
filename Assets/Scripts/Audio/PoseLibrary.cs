using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MouthReference {
    public Vector2 position;
    public Quaternion rotation;
    public Vector2 scale;
}

[System.Serializable]
public class Pose {
    public string name;
    public Sprite pose;
    public MouthReference mouth;
}

public class PoseLibrary : MonoBehaviour {
    public List<Pose> poseLibrary = new List<Pose>();
    private Dictionary<string, Pose> posesByName = new Dictionary<string, Pose>();

    // Start is called before the first frame update
    void Start() {
        Transform posesContainer = transform.Find("Canvas");

        for (int i = 0; i < posesContainer.childCount; i++) {
            Transform pose = posesContainer.GetChild(i);
            Transform mouthReference = pose.Find("MouthReference");

            RectTransform poseRect = pose.GetComponent<RectTransform>();
            RectTransform mouthRect = mouthReference.GetComponent<RectTransform>();

            float xPivot = mouthRect.anchoredPosition.x / poseRect.rect.width;
            float yPivot = mouthRect.anchoredPosition.y / poseRect.rect.height;

            string poseName = pose.name;
            Vector2 mouthPosition = new Vector2(xPivot, yPivot);
            Quaternion mouthRotation = mouthRect.localRotation;
            Vector2 mouthScale = mouthRect.localScale;

            Pose newPose = new Pose {
                name = poseName,
                pose = pose.GetComponent<Image>().overrideSprite,
                mouth = new MouthReference {
                    position = mouthPosition,
                    rotation = mouthRotation,
                    scale = mouthScale
                }
            };

            posesByName.Add(poseName, newPose);
            poseLibrary.Add(newPose);
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public Pose GetPose(string name) {
        return posesByName[name];
    }
}
