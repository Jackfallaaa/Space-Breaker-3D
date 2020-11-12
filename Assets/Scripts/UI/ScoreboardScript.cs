using System.Collections.Generic;
using System.Linq;
using Game;
using Scoreboard;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ScoreboardScript : MonoBehaviour {
        private bool _loaded;
        private List<Score> _scoreboard;
        private ScoreboardManager _scoreboardManager;

        public TextMeshProUGUI loadingField;
        public Transform scoreTemplate;
        public Transform scoreboardContainer;
        public Transform scrollableContent;

        public void Start() {
            _scoreboardManager = FindObjectOfType<ScoreboardManager>();

            //Request the scoreboard from the ScoreboardManager
            _scoreboardManager.Init();
        }

        public void Update() {
            //Once the ScoreboardManager has a response, load the scoreboard
            if (!_loaded && _scoreboardManager.response != "") {
                _loaded = true;
                _scoreboard = _scoreboardManager.GetSortedScoreboard();

                //For each score, create a clone of ScoreTemplate and set the text for each entry
                var scrollableContentTransform = scrollableContent.GetComponent<RectTransform>();
                for (var i = 0; i < _scoreboard.Count; i++) {
                    var entryTransform = Instantiate(scoreTemplate, scoreboardContainer);
                    var entryRectTransform = entryTransform.GetComponent<RectTransform>();
                    entryRectTransform.anchoredPosition = new Vector2(0, -60f * i);
                    entryTransform.gameObject.SetActive(true);

                    var rank = i + 1;
                    entryTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "#" + rank;
                    entryTransform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _scoreboard.ElementAt(i).playerName;
                    entryTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _scoreboard.ElementAt(i).score.ToString();
                }

                //Enable the "Play Seed" button for each score entry
                var buttons = GetComponentsInChildren<Button>();
                for (var i = 0; i < _scoreboard.Count; i++) {
                    var seed = _scoreboard.ElementAt(i).seed;
                    buttons[i].onClick.AddListener(delegate { PlaySeed(seed); });
                }

                loadingField.text = "";

                //Increase the size of the scrollable content for the scrollbar to work correctly
                scrollableContentTransform.sizeDelta = new Vector2(scrollableContentTransform.sizeDelta.x, _scoreboard.Count * 60);

                //Set the scroll position to the start of the scrollable content
                scrollableContentTransform.offsetMax = new Vector2(scrollableContentTransform.offsetMax.x, 0);
            }
        }

        private void PlaySeed(int seed) {
            //Enable the "Play Seed" feature
            GameManager.Seed = seed;
            GameManager.PlaySeed = true;

            FindObjectOfType<SceneSwitcherScript>().LoadScene("Play");
        }

        public void Back() {
            FindObjectOfType<SceneSwitcherScript>().LoadScene("MainMenu");
        }
    }
}