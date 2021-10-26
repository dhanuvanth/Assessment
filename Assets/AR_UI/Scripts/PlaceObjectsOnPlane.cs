using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }
    private const float k_PrefabRotation = 180.0f;
    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action OnObjectPlaced;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    bool stop = true;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (!stop)
        {
            return;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {

                        m_PlacedPrefab.SetActive(true);
                        m_PlacedPrefab.transform.position = hitPose.position;
                        m_PlacedPrefab.transform.rotation = hitPose.rotation;
                        m_PlacedPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        ARDebugManager.Instance.LogInfo($"{m_PlacedPrefab.transform.position}");
                        ARDebugManager.Instance.LogInfo($"{m_PlacedPrefab.transform.rotation}");
                        ARDebugManager.Instance.LogInfo($"{m_PlacedPrefab.transform.localScale}");
                        //spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                        m_PlacedPrefab.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
                        m_NumberOfPlacedObjects++;
                        stop = false;
                    }
                    //else
                    //{ 
                    //    spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    //    spawnedObject.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
                    //}

                    OnObjectPlaced?.Invoke();
                }
            }
        }
    }
    public void UpdateModel(GameObject gameObject)
    {
        m_PlacedPrefab = gameObject;
    }
}
