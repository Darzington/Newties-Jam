using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNShoot : MonoBehaviour
{
    [SerializeField] private Texture2D reticule;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Camera cam;

    private Vector2 mousePosition = Vector2.zero;
    private float cursorIconWidth, cursorIconHeight;

    void Start()
    {
        Cursor.visible = false;
        cursorIconHeight = reticule.height;
        cursorIconWidth = reticule.width;
    }
       
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(mousePosition.x - (cursorIconWidth / 2), mousePosition.y - (cursorIconHeight / 2), cursorIconWidth, cursorIconHeight), reticule);
    }

    void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayHit;
            if (Physics.Raycast(ray, out RayHit))
            {
                GameObject laser = Instantiate(laserPrefab);
                laser.transform.position = cam.transform.position + new Vector3(0, 0, 1);

                Vector3 targetPos = RayHit.point;
                LaserProjectile lp = laser.GetComponent<LaserProjectile>();
                lp.SetTarget(targetPos);

                Vector3 direction = targetPos - cam.transform.position;
                Vector3 startRotation = laser.transform.eulerAngles;
                laser.transform.rotation = Quaternion.LookRotation(direction);
                laser.transform.eulerAngles += startRotation;
            }
        }
    }
}
