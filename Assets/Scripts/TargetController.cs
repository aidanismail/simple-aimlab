using UnityEngine;
using UnityEngine.InputSystem; // Wajib untuk New Input System

public class TargetController : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update()
    {
        // Mengecek apakah mouse ada DAN apakah klik kiri ditekan pada frame ini
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Menarik garis lurus (Raycast) dari tengah layar (crosshair)
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if(target != null)
                {
                    target.Hit();
                }
            }
        }
    }
}