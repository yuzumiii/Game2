using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace NovelGame
{
    public class ImageManager : MonoBehaviour
    {
        [SerializeField] Sprite _background1;
        [SerializeField] Sprite _charaImage;
        [SerializeField] GameObject _backgroundObject;
        [SerializeField] GameObject _charaObject;
        [SerializeField] GameObject _bgimagePrefab;
        [SerializeField] GameObject _fgimagePrefab;

        // テキストファイルから、文字列でSpriteやGameObjectを扱えるようにするための辞書
        Dictionary<string, Sprite> _textToSprite;
        Dictionary<string, GameObject> _textToParentObject;

        // 操作したいPrefabを指定できるようにするための辞書
        Dictionary<string, GameObject> _textToSpriteObject;

        void Awake()
        {
            _textToSprite = new Dictionary<string, Sprite>();
            _textToSprite.Add("background1", _background1);

            _textToParentObject = new Dictionary<string, GameObject>();
            _textToParentObject.Add("backgroundObject", _backgroundObject);
            _textToParentObject.Add("charaObject", _charaObject);

            _textToSpriteObject = new Dictionary<string, GameObject>();
        }
        //キャラクターを配置する
        public void CharactorImage(string parentObjectName, string[] imagesPathAndPosition)
        {
            RemoveImages();
            GameObject parentObject = _textToParentObject[parentObjectName];

            foreach (var imagePathAndPosition in imagesPathAndPosition)
            {
                var imageObject = imagePathAndPosition.Split(":");
                var path = "charaImage/" + imageObject[0];
                string imagePosition = imageObject.Length > 1 ? imageObject[1] : "center";

                Sprite image = Resources.Load<Sprite>(path);
                Quaternion rotation = Quaternion.identity;
                Transform parent = parentObject.transform;

                Vector2 position;
                switch (imagePosition)
                {
                    case "left":
                        position = new Vector2(-5, -1);
                        break;
                    case "right":
                        position = new Vector2(5, -1);
                        break;
                    default:
                        position = new Vector2(0, -1);
                        break;
                }

                //Instantiate-オブジェクトを生成する
                GameObject item = Instantiate(_fgimagePrefab, position, rotation, parent);
                item.GetComponent<Image>().sprite = image;
                // _textToSpriteObject.Add("image", item);
                _textToSpriteObject[imagePosition] = item;
            }

        }

        // 画像を配置する
        public void PutImage(string parentObjectName, string imageName)
        {
            Sprite image = Resources.Load<Sprite>("Backgrounds/" + imageName);

            GameObject parentObject = _textToParentObject[parentObjectName];
            if (_textToSpriteObject.ContainsKey("background1"))
            {
                Destroy(_textToSpriteObject["background1"]);
            }

            Vector2 position = new Vector2(0, 0);
            Quaternion rotation = Quaternion.identity;
            Transform parent = parentObject.transform;
            GameObject item = Instantiate(_bgimagePrefab, position, rotation, parent);
            //imageに適用させますよ（？）
            item.GetComponent<Image>().sprite = image;
            _textToSpriteObject["background1"] = item;
        }

        // 画像を削除する
        public void RemoveImage()
        {
            RemoveImages();
        }

        public void RemoveImages()
        {
            if (_textToSpriteObject.ContainsKey("center"))
            {
                Destroy(_textToSpriteObject["center"]);
            }
            if (_textToSpriteObject.ContainsKey("left"))
            {
                Destroy(_textToSpriteObject["left"]);
            }
            if (_textToSpriteObject.ContainsKey("right"))
            {
                Destroy(_textToSpriteObject["right"]);
            }
        }

        public void RemoveBgImages()
        {
            Destroy(_textToSpriteObject["background"]);
        }
    }
}