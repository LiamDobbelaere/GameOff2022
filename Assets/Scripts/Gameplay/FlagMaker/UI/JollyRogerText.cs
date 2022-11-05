using UnityEngine;

public class JollyRogerText : MonoBehaviour {
    private JollyRogerCheck jrCheck;
    private TMPro.TextMeshProUGUI label;

    // Start is called before the first frame update
    void Start() {
        jrCheck = GameObject.FindGameObjectWithTag("DragAndDropItems").GetComponent<JollyRogerCheck>();
        label = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        label.text = jrCheck.isJollyRoger ? "Jolly roger!" : "Not a jolly roger..";
    }
}
