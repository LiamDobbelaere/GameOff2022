using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour {
    Transform notificationTemplate;

    private void OnEnable() {
        notificationTemplate = transform.Find("NotificationTemplate");
        notificationTemplate.gameObject.SetActive(false);

        for (int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (Notification notification in GameStateMono.instance.notifications) {
            Transform newNotification = Instantiate(notificationTemplate, transform);

            newNotification.Find("Body/Message").GetComponent<TextMeshProUGUI>().text = notification.message;

            newNotification.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
