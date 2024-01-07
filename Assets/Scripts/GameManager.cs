using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NovelGame
{
    public class GameManager : MonoBehaviour
    {
        // �ʂ̃N���X����GameManager�̕ϐ��Ȃǂ��g����悤�ɂ��邽�߂̂��́B�i�ύX�͂ł��Ȃ��j
        public static GameManager Instance { get; private set; }

        public UserScriptManager userScriptManager;
        public MainTextController mainTextController;
        public ImageManager imageManager;
        //public SelectManager selectManager;

        // ���[�U�X�N���v�g�́A���̍s�̐��l�B�N���b�N�i�^�b�v�j�̂��т�1��������B
        [System.NonSerialized] public int lineNumber;

        void Awake()
        {
            // ����ŁA�ʂ̃N���X����GameManager�̕ϐ��Ȃǂ��g����悤�ɂȂ�B
            Instance = this;

            lineNumber = 0;
        }
    }
}