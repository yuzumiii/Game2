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

        // ���͒��̕��i�����ł͂P�s���Ɓj�����Ă������߂̃��X�g
        List<string> _sentences = new List<string>();

        void Awake()
        {
            // �e�L�X�g�t�@�C���̒��g���A�P�s�����X�g�ɓ���Ă���
            StringReader reader = new StringReader(_textFile.text);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                _sentences.Add(line);
            }
        }

        // ���݂̍s�̕����擾����
        public string GetCurrentSentence()
        {
            return _sentences[GameManager.Instance.lineNumber];
        }

        // �������߂��ǂ���
        public bool IsStatement(string sentence)
        {
            if (sentence[0] == '&')
            {
                return true;
            }
            return false;
        }

        // ���߂����s����
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