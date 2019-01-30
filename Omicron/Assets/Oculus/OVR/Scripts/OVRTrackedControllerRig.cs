using UnityEngine;

public class OVRTrackedControllerRig : MonoBehaviour {
    public OVRInput.Controller simulateController;
    public bool updateTrackedController = true;

	public Transform trackingSpace { get; private set; }
	public Transform leftHandAnchor { get; private set; }
    public Transform rightHandAnchor { get; private set; }

	private readonly string trackingSpaceName = "TrackingSpace";
    private readonly string handAnchorName = "HandAnchor";

    private void Awake() {
#if UNITY_EDITOR || !UNITY_ANDROID
        EnsureGameObjectIntegrity();
#endif
    }

    private void Update() {
#if UNITY_EDITOR || !UNITY_ANDROID
        WarnIfControllerIsBad();
#endif
        if (updateTrackedController) {
            OVRTrackedController.simulateController = simulateController;
            OVRTrackedController.Update();
        }
    }

    private void LateUpdate() {
#if UNITY_EDITOR || !UNITY_ANDROID
        WarnIfControllerIsBad();
        UpdateAnchors();
#endif
    }

    private void WarnIfControllerIsBad() {
        if (simulateController != OVRInput.Controller.LTrackedRemote && simulateController != OVRInput.Controller.RTrackedRemote && simulateController != OVRInput.Controller.LTouch && simulateController != OVRInput.Controller.RTouch) {
            Debug.LogWarning("Simulated controller should be set to LTrackedRemote or RTrackedRemote");
        }
    }


    private void UpdateAnchors() {
        EnsureGameObjectIntegrity();

        if (!Application.isPlaying)
            return;

        if (simulateController == OVRInput.Controller.LTouch || simulateController == OVRInput.Controller.LTrackedRemote) {
            leftHandAnchor.localRotation = OVRTrackedController.LocalRotation;
            leftHandAnchor.localPosition = OVRTrackedController.LocalPosition;
        }
        else {
            leftHandAnchor.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            leftHandAnchor.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        }

        if (simulateController == OVRInput.Controller.RTouch || simulateController == OVRInput.Controller.RTrackedRemote) {
            rightHandAnchor.localRotation = OVRTrackedController.LocalRotation;
            rightHandAnchor.localPosition = OVRTrackedController.LocalPosition;
        }
        else {
            rightHandAnchor.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            rightHandAnchor.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
    }

    private Transform ConfigureHandAnchor(Transform root, OVRPlugin.Node hand) {
        string handName = (hand == OVRPlugin.Node.HandLeft) ? "Left" : "Right";
        string name = handName + handAnchorName;
        Transform anchor = transform.Find(root.name + "/" + name);

        if (anchor == null) {
            anchor = transform.Find(name);
        }

        if (anchor == null) {
            anchor = new GameObject(name).transform;
        }

        anchor.name = name;
        anchor.parent = root;
        anchor.localScale = Vector3.one;
        anchor.localPosition = Vector3.zero;
        anchor.localRotation = Quaternion.identity;

        return anchor;
    }

    private Transform ConfigureRootAnchor(string name) {
        Transform root = transform.Find(name);

        if (root == null) {
            root = new GameObject(name).transform;
        }

        root.parent = transform;
        root.localScale = Vector3.one;
        root.localPosition = Vector3.zero;
        root.localRotation = Quaternion.identity;

        return root;
    }

    public void EnsureGameObjectIntegrity() {
        if (trackingSpace == null)
            trackingSpace = ConfigureRootAnchor(trackingSpaceName);

        if (leftHandAnchor == null)
            leftHandAnchor = ConfigureHandAnchor(trackingSpace, OVRPlugin.Node.HandLeft);

        if (rightHandAnchor == null)
            rightHandAnchor = ConfigureHandAnchor(trackingSpace, OVRPlugin.Node.HandRight);
    }
}
