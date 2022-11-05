using System.Collections.Generic;
using UnityEngine;

public class JollyRogerCheck : MonoBehaviour {
    public bool isJollyRoger;
    private Bounds flagBaseBounds;

    // Start is called before the first frame update
    void Start() {
        flagBaseBounds = GameObject.FindGameObjectWithTag("FlagBase").GetComponent<CopyBoundsAndDisable>().bounds;
    }

    // Update is called once per frame
    void Update() {

    }

    public void DoCheck() {
        // TODO: limit check to within flag bounds only

        GameObject skull = null;
        // this is a list, but we assume there's only two bones
        List<GameObject> bones = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.name.ToLower().Contains("bone")) {
                bones.Add(child);
            }

            if (child.name.ToLower().Contains("skull")) {
                skull = child;
            }
        }

        bool boneRotationsCorrect = IsBoneRotationsCorrect(bones);
        bool bonePositionsCorrect = IsBonePositionsCorrect(bones);
        bool skullPositionCorrect = IsSkullPositionCorrect(bones, skull);
        bool areWithinBounds = AreWithinBounds(bones, skull);
        bool areThreeObjectsInBounds = AreThreeObjectsInBounds();

        isJollyRoger =
            boneRotationsCorrect
            && bonePositionsCorrect
            && skullPositionCorrect
            && areWithinBounds
            && areThreeObjectsInBounds;
    }

    private bool IsBoneRotationsCorrect(List<GameObject> bones) {
        List<int> validAngleBetweenBones = new List<int> {
            30, 60, 90, 120, 150
        };

        int angleBetweenBones = Mathf.RoundToInt(Vector3.Angle(bones[0].transform.right, bones[1].transform.right));
        Debug.Log(bones[0].transform.eulerAngles.z);

        List<int> invalidBoneAngles = new List<int> {
            90, 270
        };

        return !invalidBoneAngles.Contains((int)Mathf.Abs(bones[0].transform.eulerAngles.z))
            && !invalidBoneAngles.Contains((int)Mathf.Abs(bones[1].transform.eulerAngles.z))
            && validAngleBetweenBones.Contains(angleBetweenBones);
    }

    private bool IsBonePositionsCorrect(List<GameObject> bones) {
        return Vector2.Distance(bones[0].transform.position, bones[1].transform.position) < 1f;
    }

    private bool IsSkullPositionCorrect(List<GameObject> bones, GameObject skull) {
        return
            skull.transform.position.y > bones[0].transform.position.y
            && skull.transform.position.y > bones[1].transform.position.y
            && Mathf.Abs(skull.transform.position.y - bones[0].transform.position.y) > 2f
            && Mathf.Abs(skull.transform.position.y - bones[1].transform.position.y) > 2f;
    }

    private bool AreWithinBounds(List<GameObject> bones, GameObject skull) {
        return
            flagBaseBounds.Contains(skull.transform.position)
            && flagBaseBounds.Contains(bones[0].transform.position)
            && flagBaseBounds.Contains(bones[1].transform.position);
    }

    private bool AreThreeObjectsInBounds() {
        int numberOfObjectsWithinBounds = 0;
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;

            if (flagBaseBounds.Contains(child.transform.position)) {
                numberOfObjectsWithinBounds++;
            }
        }

        return numberOfObjectsWithinBounds == 3;
    }

}
