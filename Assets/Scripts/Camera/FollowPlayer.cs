using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform player;
    private const float CAMERA_MOVE_SPEED = 8f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        transform.position =
            Vector3.Lerp(
                transform.position,
                new Vector3(player.position.x, player.position.y, transform.position.z),
                CAMERA_MOVE_SPEED * Time.fixedDeltaTime
            );
    }
}
