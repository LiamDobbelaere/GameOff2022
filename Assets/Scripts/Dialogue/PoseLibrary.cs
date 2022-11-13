using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Reference {
    public Vector2 position;
    public Quaternion rotation;
    public Vector2 scale;
}

[System.Serializable]
public class Pose {
    public string name;
    public Sprite pose;
    public Reference mouth;
    public Reference eyes;
}

public class PoseLibrary : MonoBehaviour {
    public List<Pose> poseLibrary = new List<Pose>();
    private Dictionary<string, Pose> posesByName = new Dictionary<string, Pose>();

    // Start is called before the first frame update
    void Start() {
        Transform posesContainer = transform.Find("Canvas");

        for (int i = 0; i < posesContainer.childCount; i++) {
            Transform poseTransform = posesContainer.GetChild(i);
            Transform mouthReferenceTransform = poseTransform.Find("MouthReference");
            Transform eyesReferenceTransform = poseTransform.Find("EyesReference");

            string poseName = poseTransform.name;
            Reference mouthReference = GetReferenceForTransform(poseTransform, mouthReferenceTransform);
            Reference eyesReference = GetReferenceForTransform(poseTransform, eyesReferenceTransform);

            Pose newPose = new Pose {
                name = poseName,
                pose = poseTransform.GetComponent<Image>().overrideSprite,
                mouth = mouthReference,
                eyes = eyesReference
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

    private Reference GetReferenceForTransform(Transform pose, Transform reference) {
        RectTransform poseRect = pose.GetComponent<RectTransform>();
        RectTransform referenceRect = reference.GetComponent<RectTransform>();

        float xPivot = referenceRect.anchoredPosition.x / poseRect.rect.width;
        float yPivot = referenceRect.anchoredPosition.y / poseRect.rect.height;

        return new Reference {
            position = new Vector2(xPivot, yPivot),
            rotation = referenceRect.localRotation,
            scale = referenceRect.localScale
        };
    }
}
