using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

namespace NovelGame
{
    public class MainTextController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _mainTextObject;
        [SerializeField] TextMeshProUGUI _nameTextObject;
        int _displayedSentenceLength;
        int _sentenceLength;
        float _time;
        float _feedTime;

        // Start is called before the first frame update
        void Start()
        {
            _time = 0f;
            _feedTime = 0.05f;

            // �ŏ��̍s�̃e�L�X�g��\���A�܂��͖��߂����s
            string statement = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(statement))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(statement);
                GoToTheNextLine();
            }
            DisplayText();
        }

        // Update is called once per frame
        void Update()
        {
            // ���͂��P�������\������
            _time += Time.deltaTime;
            if (_time >= _feedTime)
            {
                _time -= _feedTime;
                if (!CanGoToTheNextLine())
                {
                    _displayedSentenceLength++;
                    _mainTextObject.maxVisibleCharacters = _displayedSentenceLength;
                }
            }

            // �N���b�N���ꂽ�Ƃ��A���̍s�ֈړ�
            if (Input.GetMouseButtonUp(0))
            {
                if (CanGoToTheNextLine())
                {
                    GoToTheNextLine();
                    DisplayText();
                }
                else
                {
                    _displayedSentenceLength = _sentenceLength;
                }
            }
        }

        public void setName(string name)
        {
            _nameTextObject.text = name;
        }

        // ���̍s�́A���ׂĂ̕������\������Ă��Ȃ���΁A�܂����̍s�֐i�ނ��Ƃ͂ł��Ȃ�
        public bool CanGoToTheNextLine()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _sentenceLength = sentence.Length;
            return (_displayedSentenceLength > sentence.Length);
        }

        // ���̍s�ֈړ�
        public void GoToTheNextLine()
        {
            _displayedSentenceLength = 0;
            _time = 0f;
            _mainTextObject.maxVisibleCharacters = 0;
            GameManager.Instance.lineNumber++;
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            if (GameManager.Instance.userScriptManager.IsStatement(sentence))
            {
                GameManager.Instance.userScriptManager.ExecuteStatement(sentence);
                GoToTheNextLine();
            }
        }

        // �e�L�X�g��\��
        public void DisplayText()
        {
            string sentence = GameManager.Instance.userScriptManager.GetCurrentSentence();
            _mainTextObject.text = sentence;
        }
    }
}