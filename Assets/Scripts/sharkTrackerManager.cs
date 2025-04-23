using UnityEngine;
using System.Collections.Generic;

public class SharkTrackerUI : MonoBehaviour
{
    public RectTransform canvasRect;           // Reference to Canvas RectTransform
    public Camera mainCam;
    public GameObject rayPrefab;
    public Transform squid;                    // Reference to the squid/player

    private Dictionary<sharkScript, GameObject> rays = new();

    void Start()
    {

    }
    void OnEnable()
    {
        print("ray enabled");
        sharkScript.OnSharkChasingPlayer += CreateRay;
        sharkScript.OnSharkStoppedChasing += RemoveRay;
    }

    void OnDisable()
    {
        print("ray disabled");
        sharkScript.OnSharkChasingPlayer -= CreateRay;
        sharkScript.OnSharkStoppedChasing -= RemoveRay;
    }

    void CreateRay(sharkScript shark)
    {
        if (rays.ContainsKey(shark)) return;

        GameObject ray = Instantiate(rayPrefab, canvasRect);
        print("instantiated");
        rays[shark] = ray;
    }

    void RemoveRay(sharkScript shark)
    {
        if (rays.TryGetValue(shark, out var ray))
        {
            Destroy(ray);
            rays.Remove(shark);
        }
    }

    void Update()
    {
        if (squid == null || mainCam == null) return;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);

        foreach (var pair in rays)
        {
            var shark = pair.Key;
            var ray = pair.Value;

            if (shark == null) continue;

            Vector3 dirToShark = shark.transform.position - squid.position;
            Vector3 screenPos = mainCam.WorldToScreenPoint(squid.position + dirToShark.normalized * 100f);

            // flip if behind the camera
            if (screenPos.z < 0)
                screenPos *= -1;

            // clamp to screen edges with padding
            screenPos.x = Mathf.Clamp(screenPos.x, 50, Screen.width - 50);
            screenPos.y = Mathf.Clamp(screenPos.y, 50, Screen.height - 50);

            ray.transform.position = screenPos;

            // rotate to point from center of screen toward shark
            Vector2 lookDir = (screenPos - screenCenter).normalized;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            ray.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
