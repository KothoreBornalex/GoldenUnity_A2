using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;

    public Button PlayButton { get => _playButton; set => _playButton = value; }
    public TMP_Text Title { get => _title; set => _title = value; }
    public Image Image { get => _image; set => _image = value; }
}