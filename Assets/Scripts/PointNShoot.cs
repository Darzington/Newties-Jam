using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointNShoot : MonoBehaviour
{
    public AK.Wwise.Event sndLaser;

    public GameObject wwiseObj;

    [SerializeField] private Texture2D reticule;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private Image cooldownOverlay, cursor;

    private Vector2 mousePosition = Vector2.zero;
    private float cursorIconWidth, cursorIconHeight, cooldownTime = 0.2f;
    private bool isOnCooldown = false, doCooldown = false;

    void Start()
    {
        Cursor.visible = false;
        cursorIconHeight = reticule.height;
        cursorIconWidth = reticule.width;
        cooldownOverlay.gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().name == "Level")
        {
            doCooldown = true;
        }
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        cursor.transform.position = new Vector2(mousePosition.x, Input.mousePosition.y);
        cooldownOverlay.transform.position = new Vector2(mousePosition.x, Input.mousePosition.y);

        if (Input.GetButtonDown("Fire1") && !isOnCooldown)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit RayHit;
            if (Physics.Raycast(ray, out RayHit))
            {
                sndLaser.Post(wwiseObj);
                GameObject laser = Instantiate(laserPrefab);
                laser.transform.position = cam.transform.position + new Vector3(0, 0, 1);

                Vector3 targetPos = RayHit.point;
                LaserProjectile lp = laser.GetComponent<LaserProjectile>();
                lp.SetTarget(targetPos);

                Vector3 direction = targetPos - cam.transform.position;
                Vector3 startRotation = laser.transform.eulerAngles;
                laser.transform.rotation = Quaternion.LookRotation(direction);
                laser.transform.eulerAngles += startRotation;

                if (doCooldown)
                {
                    StartCoroutine(DoCooldown());
                }
            }
        }
    }

    private IEnumerator DoCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float startTime = Time.time;
        float progress = 0;
        while (progress < 1.0f)
        {
            progress = Mathf.Lerp(0, 1, (Time.time - startTime) / cooldownTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        cooldownTime += 0.1f;
        isOnCooldown = false;
        cooldownOverlay.gameObject.SetActive(false);
    }

}
