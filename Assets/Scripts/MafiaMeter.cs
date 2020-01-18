using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MafiaMeter : MonoBehaviour
{
    [SerializeField] private Image marker, meterBase, redZone;

    private float balance = 0, maxInEitherDirection, failBalance = 100;
    private bool isOver = false;


    // Start is called before the first frame update
    void Start()
    {
        float playableWidth = meterBase.gameObject.GetComponent<RectTransform>().rect.width - 2.0f * redZone.gameObject.GetComponent<RectTransform>().rect.width;
        maxInEitherDirection = playableWidth / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
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
        balance += amountToAddOrSubtract;
    }
}
