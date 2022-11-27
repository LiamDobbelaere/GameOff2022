using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour {
    Transform notificationTemplate;

    // Start is called before the first frame update
    void Start() {
        notificationTemplate = transform.Find("NotificationTemplate");
        notificationTemplate.gameObject.SetActive(false);
    }

    private void OnEnable() {
        for (int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (Notification notification in GameState.notifications) {
            Transform newNotification = Instantiate(notificationTemplate, transform);

            newNotification.Find("Body/Message").GetComponent<TextMeshProUGUI>().text = notification.message;

            newNotification.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
