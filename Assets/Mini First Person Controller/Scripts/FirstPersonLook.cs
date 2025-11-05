using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 1f; // NOTE: is this mouse sensitivity? default is 2f
    public float smoothing = 1.5f;
    Quaternion initialTransformRotation;
    Quaternion initialCharacterRotation;

    Vector2 velocity;
    Vector2 frameVelocity;

    [HideInInspector] public bool limitLook = false; // wird vom mainmanager gesetzt
    [HideInInspector] public float maxLookAngle = 5f; // maximal erlaubter Winkel (z.B. ±5°)
    private Quaternion centerRotation;


    public void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        // transform.localRotation = initialTransformRotation;
        // character.localRotation = initialCharacterRotation;
        character = GetComponentInParent<FirstPersonMovement>().transform;
        // Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
        initialCharacterRotation= character.rotation;
        initialTransformRotation = transform.rotation;
    }

    public Vector2 updateFPSController()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        if (!limitLook)
        {
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        }


        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);

        if (limitLook)
        {
            float angleDiff = Quaternion.Angle(centerRotation, transform.rotation);
            if (angleDiff > maxLookAngle)
            {
                // Kamera zurück in erlaubten Bereich drehen
                transform.rotation = Quaternion.RotateTowards(centerRotation, transform.rotation, maxLookAngle);
            }
        }

        return mouseDelta;
    }

    // Fügt die Möglichkeit hinzu, die Blickbewegung temporär einzuschränken
    public void SetLookLimit(bool active, float limitAngle = 5f)
    {
        limitLook = active;
        if (active)
        {
            centerRotation = transform.rotation; // aktuelle Blickrichtung merken
            maxLookAngle = limitAngle;
            frameVelocity = Vector2.zero; 
        }
    }


}
