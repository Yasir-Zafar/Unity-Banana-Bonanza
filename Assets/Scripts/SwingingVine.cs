using UnityEngine;

public class SwingingVine : MonoBehaviour
{
    public float maxSwingAngle = 15f;
    public float swingSpeed = 1f;

    private HingeJoint2D vineHingeJoint; 
    private float time;

    private void Start() {
        time = 0f;
        vineHingeJoint = GetComponent<HingeJoint2D>();

        if (vineHingeJoint == null) {
            Debug.LogError("HingeJoint2D component not found on this GameObject.");
            return;
        }

        vineHingeJoint.anchor = Vector2.zero;
        vineHingeJoint.connectedAnchor = vineHingeJoint.connectedAnchor;
    }

    private void Update() {
        if (vineHingeJoint == null) return;

        time += Time.deltaTime * swingSpeed;

        float angle = maxSwingAngle * Mathf.Sin(time);

        vineHingeJoint.useLimits = true;
        JointAngleLimits2D limits = vineHingeJoint.limits;
        limits.min = -maxSwingAngle;
        limits.max = maxSwingAngle;
        vineHingeJoint.limits = limits;

        vineHingeJoint.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
