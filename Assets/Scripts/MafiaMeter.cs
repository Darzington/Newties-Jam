using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MafiaMeter : MonoBehaviour
{
    public AK.Wwise.RTPC meterMusic;

    public GameObject wwiseObj;

    [SerializeField] private Image marker, meterBase;
    [SerializeField] private Text endText;

    private float balance = 0, desiredBalance = 0, maxInEitherDirection, failBalance = 100, balanceChangeTime = 0, adjustmentTime = 1.5f;
    private bool isOver = false;

    void Start()
    {
        float playableWidth = meterBase.gameObject.GetComponent<RectTransform>().rect.width;
        maxInEitherDirection = playableWidth / 2.0f;
    }

    void Update()
    {
        if (!isOver)
        {
            balance = Mathf.Lerp(balance, desiredBalance, (Time.time - balanceChangeTime)/ adjustmentTime);
            meterMusic.SetValue(wwiseObj, -balance);
            Vector3 markerPosition = marker.transform.localPosition;
            markerPosition.x = (balance / failBalance) * maxInEitherDirection;
            marker.transform.localPosition = markerPosition;

            if (HasLostGame())
            {
                StartCoroutine(EndGame());
                isOver = true;
                Debug.Log("Ya lose.");
            }
        }
    }

    private bool HasLostGame()
    {
        return Mathf.Abs(balance) >= failBalance;
    }

    public void ChangeBalance(int amountToAddOrSubtract)
    {
        desiredBalance += amountToAddOrSubtract;
        balanceChangeTime = Time.timeSinceLevelLoad;
    }

    private IEnumerator EndGame()
    {
        string mafiaEnding = "You relied too much on the mafia to fix potholes, and they became too powerful!";
        string otherEnding = "Everybody died in space accidents, you're going to jail!";

        endText.text = balance > 50 ? otherEnding : mafiaEnding;
        endText.enabled = true;

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(4.0f);

        SceneManager.LoadScene("LeaderboardIntro");
    }
}
