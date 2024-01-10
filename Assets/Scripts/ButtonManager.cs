using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

namespace NovelGame
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] Button _buttonImagePrefab;
        [SerializeField] RectTransform ButtonParent;
        [SerializeField] TMP_FontAsset _font;
            public void SetSelectButton(string btnName,string lang)
        {
            var btn = Instantiate<Button>(this._buttonImagePrefab, this.ButtonParent);
            var rect = btn.GetComponent<RectTransform>();
            rect.position = new Vector2(0,0);
            var text = btn.GetComponentInChildren<TextMeshProUGUI>();
            text.font = _font; 
            text.text = lang;
        }
    }
}