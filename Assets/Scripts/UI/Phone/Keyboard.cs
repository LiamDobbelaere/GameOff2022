using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
    public TextMeshProUGUI typedText;

    // Start is called before the first frame update
    void Start() {
        string keyboardKeys = "qwertyuiopasdfghjkl   zxcvbnm ";
        Transform keyTemplate = transform.Find("Key");

        foreach (char keyboardKey in keyboardKeys) {
            Transform newKey = Instantiate(keyTemplate, transform);
            newKey.GetComponentInChildren<TextMeshProUGUI>().text = keyboardKey.ToString().ToUpper();
            newKey.gameObject.SetActive(true);
            newKey.GetComponent<Button>().onClick.AddListener(() => {
                typedText.text += keyboardKey.ToString();
            });
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
