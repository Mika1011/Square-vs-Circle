using UnityEngine;
using TMPro;
using Game.ScoreBoards;

public class ScoreBoardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText;
    [SerializeField] private TextMeshProUGUI entryTimeText;

    public void Initialise(ScoreBoardEntryData scoreBoardEntryData)
    {
        entryNameText.text = scoreBoardEntryData.entryName;
        entryTimeText.text = scoreBoardEntryData.entryTime.ToString();
    }
}
