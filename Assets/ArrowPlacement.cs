using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArrowPlacement : MonoBehaviour
{
    public GameObject arrowPrefab;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameObject spawnedArrow;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField] private float rotateLerpSpeed = 5f;
    [SerializeField] private float prefabYawOffset = 0f;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        planeManager = FindObjectOfType<ARPlaneManager>();

        if (planeManager != null)
        {
            planeManager.detectionMode = PlaneDetectionMode.Horizontal;
        }
    }

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycastManager != null &&
            raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            ARPlane plane = planeManager != null ?
                planeManager.GetPlane(hits[0].trackableId) : null;

            if (plane == null)
                return;

            float upDot = Vector3.Dot(plane.transform.up, Vector3.up);

            if (upDot < 0.85f)
                return;

            if (spawnedArrow == null)
            {
                spawnedArrow = Instantiate(arrowPrefab, pose.position, Quaternion.identity);
            }
            else
            {
                spawnedArrow.transform.position = pose.position;
            }

            // ================================================
            // ⭐ 바닥(plane) 기준으로 카메라 forward를 투영하여 회전 계산
            // ================================================
            Vector3 camForward = Camera.main.transform.forward;

            // 바닥 평면에 카메라 forward를 투영
            Vector3 projectedForward = Vector3.ProjectOnPlane(camForward, plane.transform.up);

            if (projectedForward.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(projectedForward);
                targetRot *= Quaternion.Euler(0, prefabYawOffset, 0);

                spawnedArrow.transform.rotation = Quaternion.Slerp(
                    spawnedArrow.transform.rotation,
                    targetRot,
                    Time.deltaTime * rotateLerpSpeed
                );
            }
        }

        // ============================================
        // ⭐ 안드로이드 뒤로가기 → UnityActivity 종료
        // ============================================
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("finish");
        }
    }
}
