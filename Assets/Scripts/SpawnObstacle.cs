using UnityEngine;
using System.Collections;

public class SpawnObstacle : MonoBehaviour
{
    /*
    [SerializeField]
    new ParticleSystem particleSystem;

    [SerializeField]
    GameObject portal;

    private Animator portalAnimator;
    private SphereCollider sphereCollider;
    private SpriteRenderer spriteRenderer;
    private float spawningTime;
    private Vector3 initialTriggerPosition;

    private bool isRunning = false;

    private void Awake()
    {
        initialTriggerPosition = this.transform.localPosition;

        portalAnimator = GetComponentInParent<Animator>();
        sphereCollider = portal.GetComponent<SphereCollider>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();

        spriteRenderer.color = spriteRenderer.color = new Color(1, 1, 1, 0);//just making sure black holes are invisible  

        GameEventManager.Instance.OnResetLevel += ReplaceTriggerAtInitialPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            particleSystem.Play();
            GameEventManager.Instance.OnPlaySoundfx("obstacle");
            spawningTime = SettingsManager.instance.getObstacleTime;
            if (!isRunning)
            {
                StartCoroutine(SpawnBlackHole());
            }
        }
    }

    IEnumerator SpawnBlackHole()
    {
        isRunning = true;
        yield return new WaitForSeconds(spawningTime);
        sphereCollider.enabled = true;       
        portalAnimator.SetTrigger("Spin");
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3f)
        {
            Color alphaUp = new Color(1, 1, 1, Mathf.Lerp(0f, 1f, t));
            spriteRenderer.color = alphaUp;
            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 3f)
        {
            Color alphaDown = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, t));
            spriteRenderer.color = alphaDown;
            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.color = new Color(1, 1, 1, 0);
        portal.SetActive(false);
        isRunning = false;
    }

    private void ReplaceTriggerAtInitialPos()
    {
        this.transform.localPosition = initialTriggerPosition;
        isRunning = false;
    }

    private void OnDestroy()
    {
        GameEventManager.Instance.OnResetLevel -= ReplaceTriggerAtInitialPos;
    }
    */
}
