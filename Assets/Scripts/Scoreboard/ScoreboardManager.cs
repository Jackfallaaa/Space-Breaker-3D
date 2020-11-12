using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Scoreboard {
    public class ScoreboardManager : MonoBehaviour {
        //Constants used for API request
        private const string ExternalUrl = "http://dreamlo.com/lb/";
        private const string PrivateCode = "gfhLX9xePU-ol3ABMZxxQAEzcmwItiZEqa3ZJARG0tZg";
        private const string PublicCode = "5e949e56403b1511d87b1062";

        public string response = "";

        public void AddScore(string playerName, int score, int seed) {
            playerName = Clean(playerName);
            StartCoroutine(Request(ExternalUrl + PrivateCode + "/add/" + UnityWebRequest.EscapeURL(playerName) + "/" + score + "/" + 0 + "/" + seed));
        }

        public void Init() {
            response = "";
            StartCoroutine(Request(ExternalUrl + PublicCode + "/pipe"));
        }

        private IEnumerator Request(string url) {
            //Send API request and store response
            using (var www = UnityWebRequest.Get(url)) {
                yield return www.SendWebRequest();
                response = www.downloadHandler.text;
            }
        }

        private string[] ToStringArray() {
            if (string.IsNullOrEmpty(response))
                return null;

            //Convert response string into string array
            var rows = response.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            return rows;
        }

        private Score[] ToScoreArray() {
            var rows = ToStringArray();
            if (rows == null)
                return null;

            var rowcount = rows.Length;
            if (rowcount <= 0)
                return null;

            //Convert string array into array of Score objects
            var scoreList = new Score[rowcount];
            for (var i = 0; i < rowcount; i++) {
                //Separate fields from the row
                var values = rows[i].Split(new[] {'|'}, StringSplitOptions.None);

                //Create new Score object with relevant attributes
                scoreList[i] = new Score(values[0], int.Parse(values[1]), int.Parse(values[3]));
            }

            return scoreList;
        }

        public List<Score> GetSortedScoreboard() {
            var scoreList = ToScoreArray();
            if (scoreList == null)
                return null;

            //Sort scoreboard in descending order
            var genericList = new List<Score>(scoreList);
            genericList.Sort((x, y) => y.score.CompareTo(x.score));

            return genericList;
        }


        private static string Clean(string s) {
            //Remove invalid characters
            s = s.Replace("/", "");
            s = s.Replace("|", "");
            return s;
        }
    }
}