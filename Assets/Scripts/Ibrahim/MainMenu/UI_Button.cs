using Menu;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Menu
{
    public class UI_Button : MonoBehaviour
    {
        private UI_MenuController _menuController;
        [SerializeField] private string _targetedButton;
        [SerializeField] private UnityEvent _playButtonEvent;
        private Button _button;

        void Awake()
        {
            _menuController = GetComponent<UI_MenuController>();
        }


        // Start is called before the first frame update
        void Start()
        {
            _button = _menuController.Document.rootVisualElement.Q<Button>(_targetedButton);
            _button.clicked += Button_clicked;
        }


        private void Button_clicked()
        {
            //Debug.Log("Button: " + _targetedButton + " is Working !");
            SoundManager.PlaySound(GameAssets.Instance.SoundBank._buttonFocus);
        }

    }
}