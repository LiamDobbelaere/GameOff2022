using UnityEngine;
using UnityEngine.UI;

public class AppIcon : MonoBehaviour {
    private Phone phone;
    public string appName;

    // Start is called before the first frame update
    void Start() {
        phone = GetComponentInParent<Phone>();

        GetComponent<Button>().onClick.AddListener(() => {
            phone.StartApp(appName);
            GetComponent<AudioSource>().Play();
        });
    }

    // Update is called once per frame
    void Update() {

    }
}
