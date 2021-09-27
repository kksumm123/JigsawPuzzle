using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int currentScore;
    public int completeScore;

    internal void AddScore(int addValue)
    {
        currentScore += addValue;
        if (completeScore == currentScore)
            ToastMessage.Instance.ShowToast("스테이지 클리어");
    }
}
