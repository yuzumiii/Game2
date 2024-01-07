using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace NovelGame
{
    public class UserScriptManager : MonoBehaviour
    {
        [SerializeField] TextAsset _textFile;

        // 文章中の文（ここでは１行ごと）を入れておくためのリスト
        List<string> _sentences = new List<string>();

        void Awake()
        {
            // テキストファイルの中身を、１行ずつリストに入れておく
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        // 現在の行の文を取得する
        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        // 文が命令かどうか
        public bool IsStatement(string sentence)
        {
            if (sentence[0] == '&')
            {
                return true;
            }
            return false;
        }

        // 命令を実行する
        public void ExecuteStatement(string sentence)
        {
            string[] words = sentence.Split(' ');
            string[] images = words.Skip(2).ToArray();
            switch (words[0])
            {
                case "&chara":
                    GameManager.Instance.imageManager.CharactorImage(words[1], images);
                    break;
                case "&img":
                    GameManager.Instance.imageManager.PutImage(words[1], words[2]);
                    break;
                case "&name":
                    GameManager.Instance.mainTextController.setName(words[1]);
                    break;
                case "&rmimg":
                    GameManager.Instance.imageManager.RemoveImage();
                    break;
                //case "&select":
                //    GameManager.Instance.imageManager.RemoveImage();
                //    break;
                //case "&jump":
                //    GameManager.Instance.selectManager.JumpTo();
                //    break;
            }
        }
    }
}