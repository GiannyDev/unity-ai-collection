using UnityEngine;

public class Target : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 targetPos = new Vector3(hitInfo.point.x, 1f, hitInfo.point.z);
                transform.position = targetPos;
            }
        }
    }
}
