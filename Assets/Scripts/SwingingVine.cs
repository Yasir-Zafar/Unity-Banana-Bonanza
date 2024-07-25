using UnityEngine;

public class SwingingVine : MonoBehaviour
{
    public float maxSwingAngle = 15f;
    public float swingSpeed = 1f;

    private HingeJoint2D hingeJoint; // HingeJoint2D component
    private float time; // Time to control the swing speed

    private void Start() {
        time = 0f;
        hingeJoint = GetComponent<HingeJoint2D>();

        if (hingeJoint == null) {
            Debug.LogError("HingeJoint2D component not found on this GameObject.");
            return;
        }

        hingeJoint.anchor = Vector2.zero;
        hingeJoint.connectedAnchor = hingeJoint.connectedAnchor;
    }

    private void Update() {
        if (hingeJoint == null) return;

        time += Time.deltaTime * swingSpeed;

        float angle = maxSwingAngle * Mathf.Sin(time);

        hingeJoint.useLimits = true;
        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min = -maxSwingAngle;
        limits.max = maxSwingAngle;
        hingeJoint.limits = limits;

        // Update the joint's angle to create the swing effect
        hingeJoint.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
