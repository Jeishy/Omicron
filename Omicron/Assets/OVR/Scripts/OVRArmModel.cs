using UnityEngine;
using System.Collections.Generic;
using OvrJoint = System.Collections.Generic.KeyValuePair<OVRPose, int>;
// OvrJoint = Pair<Posef Pose, int ParentIndex>
using OvrJointMod = System.Collections.Generic.KeyValuePair<int, OVRPose>;
// OvrJointMod = Pair<int JointIndex, Posef Pose>

public class OVRArmModel {
    private List<OvrJoint> Skeleton;
    private List<OvrJoint> TransformedJoints;

    private float TorsoYaw; // current yaw of the torso
    private float LastUnclampedRoll = 0.0f;

    public int ClavicleJointIdx { get; private set; }
    public int ShoulderJointIdx { get; private set; }
    public int ElbowJointIdx { get; private set; }
    public int WristJointIdx { get; private set; }

    protected static OVRPose MakePose(Quaternion rot, Vector3 pos) {
        OVRPose result = OVRPose.identity;
        result.orientation = rot;
        result.position = pos;
        return result;
    }

    public OVRArmModel() {
        Skeleton = new List<OvrJoint>();
        TransformedJoints = new List<OvrJoint>();
        TorsoYaw = 0.0f;

        /*neck*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.0f, -0.2032f, 0.0f)), Skeleton.Count - 1));
        ClavicleJointIdx = Skeleton.Count;
        /*clavicle*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.2286f, 0.0f, 0.0f)), Skeleton.Count - 1));
        ShoulderJointIdx = Skeleton.Count;
        /*shoulder*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.0f, -0.2441f, 0.02134f)), Skeleton.Count - 1));
        ElbowJointIdx = Skeleton.Count;
        /*elbow*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.0f, 0.0f, -0.3048f)), Skeleton.Count - 1));
        WristJointIdx = Skeleton.Count;
        /*wrist*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.0f, 0.0f, -0.0762f)), Skeleton.Count - 1));
        /*hand*/
        Skeleton.Add(new OvrJoint(MakePose(Quaternion.identity, new Vector3(0.0f, 0.0381f, -0.0381f)), Skeleton.Count - 1));

        TransformedJoints = new List<OvrJoint>();
        TransformedJoints.Capacity = Skeleton.Count;
        for (int i = 0; i < Skeleton.Count; ++i) {
            OVRPose pose = MakePose(
                new Quaternion(Skeleton[i].Key.orientation.x, Skeleton[i].Key.orientation.y, Skeleton[i].Key.orientation.z, Skeleton[i].Key.orientation.w),
                new Vector3(Skeleton[i].Key.position.x, Skeleton[i].Key.position.y, Skeleton[i].Key.position.z));
            TransformedJoints.Add(new OvrJoint(
                pose, // Key = Pose
                Skeleton[i].Value // Value = ParentIndex
            ));
        }
    }

    #region MathHelpers 
    protected static void GetYawPitchRoll(Quaternion q, out float yaw, out float pitch, out float roll) {
        int A1 = 1; // Axis_Y = 1
        int A2 = 0; // Axis_X = 0
        int A3 = 2; //  Axis_Z = 2
        float D = 1.0f; // Rotate_CCW = 1, Rotate_CW  = -1 
        float S = 1.0f; //Handed_R = 1, Handed_L = -1

        float[] Q = new float[3] { q.x, q.y, q.z };  //Quaternion components x,y,z

        float ww = q.w * q.w;
        float Q11 = Q[A1] * Q[A1];
        float Q22 = Q[A2] * Q[A2];
        float Q33 = Q[A3] * Q[A3];

        float psign = -1.0f;
        // Determine whether even permutation
        if (((A1 + 1) % 3 == A2) && ((A2 + 1) % 3 == A3)) {
            psign = 1;
        }

        float s2 = psign * 2.0f * (psign * q.w * Q[A2] + Q[A1] * Q[A3]);

        float singularityRadius = 1e-7f;
        if (s2 < -1.0f + singularityRadius) { // South pole singularity
            yaw = 0.0f;
            pitch = -S * D * (Mathf.PI * 0.5f);
            roll = S * D * (Mathf.Atan2(2.0f * (psign * Q[A1] * Q[A2] + q.w * Q[A3]), ww + Q22 - Q11 - Q33));
        }
        else if (s2 > 1.0f - singularityRadius) {  // North pole singularity
            yaw = 0.0f;
            pitch = S * D * (Mathf.PI * 0.5f);
            roll = S * D * (Mathf.Atan2(2.0f * (psign * Q[A1] * Q[A2] + q.w * Q[A3]), ww + Q22 - Q11 - Q33));
        }
        else {
            yaw = -S * D * (Mathf.Atan2(-2.0f * (q.w * Q[A1] - psign * Q[A2] * Q[A3]), ww + Q33 - Q11 - Q22));
            pitch = S * D * (Mathf.Asin(s2));
            roll = S * D * (Mathf.Atan2(2.0f * (q.w * Q[A3] - psign * Q[A1] * Q[A2]), ww + Q11 - Q22 - Q33));
        }
    }
    #endregion


    #region ArmModel
    float ConstrainTorsoYaw(Quaternion headRot, float torsoYaw) {
        Vector3 worldUp = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 worldFwd = new Vector3(0.0f, 0.0f, -1.0f);

        Vector3 headFwd = headRot * worldFwd;
        float l2 = worldUp.sqrMagnitude;
        float d = Vector3.Dot(headFwd, worldUp) / l2;
        Vector3 o = new Vector3(worldUp.x * d, worldUp.y * d, worldUp.z * d);
        Vector3 projectedHeadFwd = new Vector3(headFwd.x - o.x, headFwd.y - o.y, headFwd.z - o.z);

        if (projectedHeadFwd.sqrMagnitude < 0.001f) {
            return torsoYaw;
        }

        // calculate the world rotation of the head on the horizon plane
        headFwd = projectedHeadFwd.normalized;

        // calculate the world rotation of the torso
        Quaternion torsoRot = Quaternion.AngleAxis(torsoYaw * Mathf.Rad2Deg, worldUp);
        Vector3 torsoFwd = torsoRot * worldFwd;

        // find the angle between the torso and head
        float torsoMaxYawOffset = Mathf.Deg2Rad * 30.0f;
        float torsoDot = Vector3.Dot(torsoFwd, headFwd);
        if (torsoDot >= Mathf.Cos(torsoMaxYawOffset)) {
            return torsoYaw;
        }

        // calculate the rotation of the torso when it's constrained in that direction
        Vector3 headRight = new Vector3(-headFwd.z, 0.0f, headFwd.x);
        Quaternion projectedHeadRot = Quaternion.LookRotation(-headFwd, worldUp);

        float offsetDir = Vector3.Dot(headRight, torsoFwd) < 0.0f ? 1.0f : -1.0f;
        float offsetYaw = torsoMaxYawOffset * offsetDir;
        Quaternion constrainedTorsoRot = projectedHeadRot * Quaternion.AngleAxis(offsetYaw * Mathf.Rad2Deg, worldUp);

        // slerp torso towards the constrained rotation
        float slerpFactor = 1.0f / 15.0f;
        Quaternion slerped = Quaternion.Slerp(torsoRot, constrainedTorsoRot, slerpFactor);

        float y = 0.0f;
        float p = 0.0f;
        float r = 0.0f;
        GetYawPitchRoll(slerped, out y, out p, out r);
        return y;
    }

    public void Update(OVRPose headPose, OVRPose remotePose, UnityEngine.XR.XRNode handNode, bool recenteredController, out OVRPose outPose) {
        float eyeYaw;
        float eyePitch;
        float eyeRoll;
        GetYawPitchRoll(headPose.orientation, out eyeYaw, out eyePitch, out eyeRoll);

        TorsoYaw = ConstrainTorsoYaw(headPose.orientation, TorsoYaw);

        if (recenteredController) {
            TorsoYaw = eyeYaw;
        }

        OVRPose FootPose = MakePose(Quaternion.AngleAxis(TorsoYaw * Mathf.Rad2Deg, new Vector3(0.0f, 1.0f, 0.0f)),
            new Vector3(headPose.position.x, headPose.position.y, headPose.position.z));

        float handSign = (handNode == UnityEngine.XR.XRNode.LeftHand) ? -1.0f : 1.0f;
        OVRPose pose = Skeleton[ClavicleJointIdx].Key;
        pose.position.x = Mathf.Abs(Skeleton[ClavicleJointIdx].Key.position.x) * handSign;
        Skeleton[ClavicleJointIdx] = new OvrJoint(pose, Skeleton[ClavicleJointIdx].Value);

        List<OvrJointMod> jointMods = new List<OvrJointMod>();

        Quaternion remoteRot = new Quaternion(remotePose.orientation.x, remotePose.orientation.y, remotePose.orientation.z, remotePose.orientation.w);

        float MAX_ROLL = Mathf.PI * 0.5f;
        float remoteYaw = 0.0f;
        float remotePitch = 0.0f;
        float remoteRoll = 0.0f;
        GetYawPitchRoll(remoteRot, out remoteYaw, out remotePitch, out remoteRoll);

        if (remoteRoll >= -MAX_ROLL && remoteRoll <= MAX_ROLL) {
            LastUnclampedRoll = remoteRoll;
        }
        else {
            remoteRoll = LastUnclampedRoll;
        }

        remoteRot = Quaternion.AngleAxis(remoteYaw * Mathf.Rad2Deg, new Vector3(0, 1, 0)) *
                    Quaternion.AngleAxis(remotePitch * Mathf.Rad2Deg, new Vector3(1, 0, 0)) *
                    Quaternion.AngleAxis(remoteRoll * Mathf.Rad2Deg, new Vector3(0, 0, 1));

        Quaternion localRemoteRot = Quaternion.Inverse(FootPose.orientation) * (remoteRot);

        Quaternion shoulderRot = Quaternion.Slerp(Quaternion.identity, localRemoteRot, 0.0f);
        Quaternion elbowRot = Quaternion.Slerp(Quaternion.identity, localRemoteRot, 0.6f);
        Quaternion wristRot = Quaternion.Slerp(Quaternion.identity, localRemoteRot, 0.4f);

        jointMods.Add(new OvrJointMod(ShoulderJointIdx, MakePose(shoulderRot, new Vector3(0.0f, 0.0f, 0.0f))));
        jointMods.Add(new OvrJointMod(ElbowJointIdx, MakePose(elbowRot, new Vector3(0.0f, 0.0f, 0.0f))));
        jointMods.Add(new OvrJointMod(WristJointIdx, MakePose(wristRot, new Vector3(0.0f, 0.0f, 0.0f))));

        Transform(FootPose, Skeleton, jointMods, ref TransformedJoints);

        outPose = MakePose(new Quaternion(remotePose.orientation.x, remotePose.orientation.y, remotePose.orientation.z, remotePose.orientation.w),
            TransformedJoints[TransformedJoints.Count - 1].Key.position);
    }
    #endregion

    #region Skeleton
    public static void Transform(OVRPose transformPose, OVRPose inPose, out OVRPose outPose) {
        transformPose = MakePose(transformPose.orientation, transformPose.position);
        inPose = MakePose(inPose.orientation, inPose.position);

        outPose = OVRPose.identity;
        outPose.position = transformPose.position + (transformPose.orientation * inPose.position);
        outPose.orientation = transformPose.orientation * inPose.orientation;
    }

    public static void TransformByParent(OVRPose parentPose, int jointIndex, OVRPose inPose, List<OvrJointMod> jointMods, ref OVRPose outPose) {
        parentPose = MakePose(parentPose.orientation, parentPose.position);
        inPose = MakePose(inPose.orientation, inPose.position);

        bool appliedJointMod = false;
        for (int i = 0; i < jointMods.Count; ++i) {
            OvrJointMod mod = jointMods[i];
            if (mod.Key == jointIndex) { // Key = JointIndex
                Transform(mod.Value, inPose, out outPose); // Value = Pose
                appliedJointMod = true;
            }
        }

        if (appliedJointMod) {
            Transform(parentPose, outPose, out outPose);
        }
        else {
            Transform(parentPose, inPose, out outPose);
        }
    }

    public static void Transform(OVRPose worldPose, List<OvrJoint> inJoints, List<OvrJointMod> jointMods, ref List<OvrJoint> outJoints) {
        worldPose = MakePose(worldPose.orientation, worldPose.position);

        if (outJoints.Count != inJoints.Count) {
            return;
        }

        OVRPose outPose = MakePose(outJoints[0].Key.orientation, outJoints[0].Key.position);
        TransformByParent(worldPose, 0, inJoints[0].Key, jointMods, ref outPose);
        outJoints[0] = new OvrJoint(outPose, outJoints[0].Value);

        for (int i = 1; i < inJoints.Count; ++i) {
            OvrJoint inJoint = inJoints[i];
            OvrJoint parentJoint = outJoints[inJoint.Value];

            outPose = outJoints[i].Key;
            TransformByParent(parentJoint.Key, i, inJoint.Key, jointMods, ref outPose);
            outJoints[i] = new OvrJoint(outPose, outJoints[i].Value);

        }
    }
    #endregion
}
