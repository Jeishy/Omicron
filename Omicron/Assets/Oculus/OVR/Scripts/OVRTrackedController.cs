using UnityEngine;
using VR = UnityEngine.VR;

public static class OVRTrackedController {
    public static event System.Action OnTriggerDown;
    public static event System.Action OnTouchpadDown;

    public static event System.Action OnTriggerUp;
    public static event System.Action OnTouchpadUp;

    public static event System.Action OnBackClicked;
    public static event System.Action<Vector2> OnTouch;

    public static OVRInput.Controller simulateController = OVRInput.Controller.RTrackedRemote;

#if UNITY_EDITOR || !UNITY_ANDROID
    private static OVRArmModel ovrArmModel = new OVRArmModel();
    private static Quaternion localRotation;
    private static Vector3 localPosition;
#endif

    private static bool lastTriggerState = false;
    private static bool lastTouchpadState = false;

    public static Quaternion LocalRotation {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.GetLocalControllerRotation(PhysicalController);
#else
            return localRotation;
#endif
        }
    }

    public static Vector3 LocalPosition {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.GetLocalControllerPosition(PhysicalController);
#else
            return localPosition;
#endif
        }
    }

    public static bool TriggerDown {
        get {
            return OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, PhysicalController);
        }
    }

    public static bool TouchpadDown {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.Get(OVRInput.Button.PrimaryTouchpad, PhysicalController);
#else
            return OVRInput.Get(OVRInput.Button.PrimaryThumbstick, PhysicalController);
#endif
        }
    }

    public static bool TouchpadTouched {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.Get(OVRInput.Touch.PrimaryTouchpad);
#else
            return OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, PhysicalController);
#endif
        }
    }

    public static Vector2 TouchpadPosition {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
#else
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, PhysicalController);
#endif
        }
    }

    public static bool BackClicked {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return OVRInput.GetUp(OVRInput.Button.Back);
#else
            return OVRInput.GetUp(OVRInput.Button.Two, PhysicalController);
#endif
        }
    }

    public static bool LeftHanded {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return (OVRInput.GetActiveController() & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote;
#else
            return simulateController == OVRInput.Controller.LTrackedRemote || simulateController == OVRInput.Controller.LTouch;
#endif
        }
    }

    public static bool RightHanded {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            return (OVRInput.GetActiveController() & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote;
#else
            return simulateController == OVRInput.Controller.RTrackedRemote || simulateController == OVRInput.Controller.RTouch;
#endif
        }
    }

    private static Quaternion ConvertHandedness(Quaternion q) {
        return new Quaternion(-q.x, -q.y, q.z, q.w);
    }

    private static Vector3 ConvertHandedness(Vector3 v) {
        return new Vector3(v.x, v.y, -v.z);
    }

    public static OVRInput.Controller PhysicalController {
        get {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote)) {
                return OVRInput.Controller.LTrackedRemote;
            }
            else if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)) {
                return OVRInput.Controller.RTrackedRemote;
            }
            else if (OVRInput.IsControllerConnected(OVRInput.Controller.Touchpad)) {
                return OVRInput.Controller.Touchpad;
            }
#else
            if (simulateController == OVRInput.Controller.LTrackedRemote || simulateController == OVRInput.Controller.LTouch) {
                if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch)) {
                    return OVRInput.Controller.LTouch;
                }
            }
            else if (simulateController == OVRInput.Controller.RTrackedRemote || simulateController == OVRInput.Controller.RTouch) {
                if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch)) {
                    return OVRInput.Controller.RTouch;
                }
            }
#endif
            return OVRInput.Controller.Active;
        }
    }

    public static void Update() {
#if UNITY_EDITOR || !UNITY_ANDROID
        if (simulateController != OVRInput.Controller.LTrackedRemote && simulateController != OVRInput.Controller.RTrackedRemote && simulateController != OVRInput.Controller.LTouch && simulateController != OVRInput.Controller.RTouch) {
            // Default to a right handed controller if not set!
            simulateController = OVRInput.Controller.RTrackedRemote;
        }
        UnityEngine.XR.XRNode handNode = (simulateController == OVRInput.Controller.LTouch || simulateController == OVRInput.Controller.LTrackedRemote) ? UnityEngine.XR.XRNode.LeftHand : UnityEngine.XR.XRNode.RightHand;

        OVRPose remotePoseWithoutPosition = new OVRPose();
        remotePoseWithoutPosition.orientation = ConvertHandedness(UnityEngine.XR.InputTracking.GetLocalRotation(handNode));
        remotePoseWithoutPosition.position = new Vector3(0.0f, 0.0f, 0.0f);

        OVRPose headPoseWithoutPosition = new OVRPose();
        headPoseWithoutPosition.orientation = ConvertHandedness(UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.Head));

#if UNITY_ANDROID && !UNITY_EDITOR
        // Running on Gear VR, the head position is not needed
        headPoseWithoutPosition.position = new Vector3(0.0f, 0.0f, 0.0f);
#else
        // Running on Rift, the head position is needed
        headPoseWithoutPosition.position = ConvertHandedness(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.Head));
#endif

        OVRPose remotePose = new OVRPose();
        ovrArmModel.Update(headPoseWithoutPosition, remotePoseWithoutPosition, handNode, OVRPlugin.shouldRecenter, out remotePose);

        localRotation = ConvertHandedness(remotePose.orientation);
        localPosition = ConvertHandedness(remotePose.position);
#endif

        if (BackClicked && OnBackClicked != null) {
            OnBackClicked();
        }

        if (lastTriggerState && !TriggerDown && OnTriggerUp != null) {
            OnTriggerUp();
        }
        else if (!lastTriggerState && TriggerDown && OnTriggerDown != null) {
            OnTriggerDown();
        }

        if (lastTouchpadState && !TouchpadDown && OnTouchpadUp != null) {
            OnTouchpadUp();
        }
        else if (!lastTriggerState && TouchpadDown && OnTouchpadDown != null) {
            OnTouchpadDown();
        }

        if (TouchpadTouched && OnTouch != null) {
            OnTouch(TouchpadPosition);
        }

        lastTriggerState = TriggerDown;
        lastTouchpadState = TouchpadDown;
    }
}