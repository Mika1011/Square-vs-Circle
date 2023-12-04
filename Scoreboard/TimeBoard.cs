using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.ScoreBoards
{
    public class TimeBoard : MonoBehaviour
    {
        [SerializeField] private int maxTimeBoardEntries = 5;
        [SerializeField] private Transform timeScoreHolderTransform;
        [SerializeField] private GameObject timeScoreEntryObject;
        [SerializeField] private TMP_InputField entryNameInputField;
        [SerializeField] private Button entryButton;
        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        private void Start()
        {
            ScoreBoardSaveData savedTimes = getSavedTime();

            saveScores(savedTimes);

            updateUI(savedTimes);
        }

        public void addEntryToBoard()
        {
            ScoreBoardEntryData entryData = new ScoreBoardEntryData
            {
                entryName = entryNameInputField.text,
                entryTime = PlayerPrefs.GetFloat("CurrentTime")
            };

            addEntry(entryData);
            entryButton.interactable = false;
        }

        public void addEntry(ScoreBoardEntryData scoreBoardEntryData)
        {
            ScoreBoardSaveData savedTimes = getSavedTime();

            bool scoreAdded = false;

            for (int i = 0; i < savedTimes.bestTime.Count; i++)
            {
                if(scoreBoardEntryData.entryTime < savedTimes.bestTime[i].entryTime)
                {
                    savedTimes.bestTime.Insert(i, scoreBoardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if(!scoreAdded && savedTimes.bestTime.Count < maxTimeBoardEntries)
            {
                savedTimes.bestTime.Add(scoreBoardEntryData);
            }

            if(savedTimes.bestTime.Count > maxTimeBoardEntries)
            {
                savedTimes.bestTime.RemoveRange(maxTimeBoardEntries, savedTimes.bestTime.Count - maxTimeBoardEntries);
            }

            updateUI(savedTimes);

            saveScores(savedTimes);
        }

        private void updateUI(ScoreBoardSaveData savedScores)
        {
            foreach (Transform child in timeScoreHolderTransform)
            {
                Destroy(child.gameObject);
            }

            foreach(ScoreBoardEntryData highscore in savedScores.bestTime)
            {
                Instantiate(timeScoreEntryObject, timeScoreHolderTransform).GetComponent<ScoreBoardEntryUI>().Initialise(highscore);
            }
        }

        private ScoreBoardSaveData getSavedTime()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreBoardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreBoardSaveData>(json);
            }
        }

        private void saveScores(ScoreBoardSaveData scoreBoardSaveData)
        {
            using(StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreBoardSaveData, true);
                stream.Write(json);
            }
        }

        public void backToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}



