using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MafiaMeter : MonoBehaviour
{
    public AK.Wwise.RTPC meterMusic;

    public GameObject wwiseObj;

    [SerializeField] private Image marker, meterBase;

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
                //Lose game
                isOver = true;
                Debug.Log("Ya lose.");
            }
        }
    }

    private bool HasLostGame()
    {
        return Mathf.Abs(balance) > failBalance;
    }

    public void ChangeBalance(int amountToAddOrSubtract)
    {
        desiredBalance += amountToAddOrSubtract;
        balanceChangeTime = Time.timeSinceLevelLoad;
    }
}
