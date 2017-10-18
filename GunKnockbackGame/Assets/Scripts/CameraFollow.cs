using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 startOffset;
    Vector3 offset;
    public GameObject toFollowObject;
    public List<Ship> shipsAffectingCamera = new List<Ship>();
    public Vector3 targetOffset;
    [Range(-1,1)]public float cameraPullDistancePercent = .5f;
    public float cameraPullDistance;
    public float cameraPullEffect = 1f;
    public LayerMask cameraPullMask;
    public float cameraHeight,
    minCameraHeight = 10f,
    maxCameraHeight = 100f;
    public float lerpSpeed = 5f;

    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        startOffset = transform.position - toFollowObject.transform.position;
        offset = startOffset;
        cameraHeight = 45f;
    }

    private void Update()
    {
        cameraHeight -= Input.mouseScrollDelta.y;
        cameraHeight = Mathf.Clamp(cameraHeight, minCameraHeight, maxCameraHeight);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        cameraPullDistance = cameraPullDistancePercent * cameraHeight;
        Collider[] ships = Physics.OverlapSphere(toFollowObject.transform.position, cameraPullDistance, cameraPullMask);
        shipsAffectingCamera.Clear();//clear this each frame
        targetOffset = Vector3.zero;
        foreach (Collider ship in ships)
        {
            var shipPart = ship.GetComponent<Ship>();
            if (shipPart != null)
            {
                shipsAffectingCamera.Add(ship.GetComponent<Ship>());
                targetOffset -= (toFollowObject.transform.position - ship.transform.position) * cameraPullEffect *
                    (cameraPullDistance - Vector3.Distance(toFollowObject.transform.position, ship.transform.position)) / cameraPullDistance;

            }

        }
        targetOffset /= shipsAffectingCamera.Count; //should never be 0 due too the players ship being counted too
        targetOffset.y += cameraHeight;//maintain camera height, but if it is changed, we do want to lerp it along.
        offset = Vector3.Lerp(offset, targetOffset, lerpSpeed * Time.deltaTime);
        //offset.y = 0;

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = toFollowObject.transform.position + offset;
    }
}
