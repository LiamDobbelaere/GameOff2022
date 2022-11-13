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

[System.Serializable]
public class Eyes {
    public string name;
    public Sprite sprite;
}

public class PoseLibrary : MonoBehaviour {
    [Header("Pose library is populated at runtime")]
    public List<Pose> poseLibrary = new List<Pose>();

    [Header("Eyes must be manually populated")]
    public List<Eyes> eyes = new List<Eyes>();

    private Dictionary<string, Pose> posesByName;
    private Dictionary<string, Eyes> eyesByName;

    // Start is called before the first frame update
    void Start() {
        Transform posesContainer = transform.Find("Canvas");

        eyesByName = new Dictionary<string, Eyes>();
        foreach (Eyes currentEyes in eyes) {
            eyesByName.Add(currentEyes.name, currentEyes);
        }

        posesByName = new Dictionary<string, Pose>();
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

    public Eyes GetEyes(string name) {
        return eyesByName[name];
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
