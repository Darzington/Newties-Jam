using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNShoot : MonoBehaviour
{
    public Texture2D reticule;
    public GameObject laserPrefab;
    public Camera orthoCam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(reticule, new Vector2(reticule.width/2.0f, reticule.height/2.0f), CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = orthoCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayHit;
            if (Physics.Raycast(ray, out RayHit))
            {
                GameObject laser = Instantiate(laserPrefab);
                laser.transform.position = orthoCam.transform.position + new Vector3(0, 0, 1);

                Vector3 targetPos = RayHit.point;
                LaserProjectile lp = laser.GetComponent<LaserProjectile>();
                lp.SetTarget(targetPos);

                Vector3 direction = targetPos - orthoCam.transform.position;
                Vector3 startRotation = laser.transform.eulerAngles;
                laser.transform.rotation = Quaternion.LookRotation(direction);
                laser.transform.eulerAngles += startRotation;
            }
        }
    }
}
