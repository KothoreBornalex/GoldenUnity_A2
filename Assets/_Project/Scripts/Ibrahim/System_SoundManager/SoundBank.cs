using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum SoundType
{
    Effect,
    Music
}

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private SoundType _type;
    [SerializeField] private bool _spammable = true;
    public string Name { get => _name; set => _name = value; }
    public AudioClip Clip { get => _clip; set => _clip = value; }
    public SoundType Type { get => _type; set => _type = value; }
    public bool Spammable { get => _spammable; set => _spammable = value; }
}



[CreateAssetMenu(fileName = "SoundBank", menuName = "SoundBank", order = 1)]
public class SoundBank : ScriptableObject
{
    [Header("Musics")]
    [SerializeField] public Sound _mainMenuMusic;
    [SerializeField] public Sound _levelOneMusic;


    [Header("UI Sounds")]
    [SerializeField] public Sound _buttonFocus;

    [Header("Game Sounds")]
    [SerializeField] public Sound _soundEmitter;

}


/*

[System.Serializable]
public struct Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private SoundType _type;

    public string Name { get => _name; set => _name = value; }
    public AudioClip Clip { get => _clip; set => _clip = value; }
    public SoundType Type { get => _type; set => _type = value; }
}


[CreateAssetMenu(fileName = "SoundBank", menuName = "SoundBank", order = 1)]
public class SoundBank : ScriptableObject
{
    private Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();
    private List<string> _soundsList = new List<string>();




    public void AddSound(Sound sound)
    {
        if(_sounds.ContainsKey(sound.Name)) throw new System.Exception("ERROR: Bank already contain this sound name !");
        else
        {
            _sounds.Add(sound.Name, sound);

            if(_sounds.ContainsKey(sound.Name)) Debug.Log("Sound: " + sound.Name + " Added to the Sound Bank !");
        }
    }

    public void DeleteSound(string soundName)
    {
        if (!_sounds.ContainsKey(soundName)) throw new System.Exception("ERROR: This Sound isn't in the Sounds Bank !");
        else
        {
            _sounds.Remove(soundName);
            if (!_sounds.ContainsKey(soundName)) Debug.Log("Sound: " + soundName + " Removed from the Sound Bank !");
        }
    }

    public string GetSoundBankCount()
    {
        return _sounds.Count.ToString();
    }


}



[CustomEditor(typeof(SoundBank))]
public class SoundBankEditor : Editor
{
    private GUIStyle _titleLabelStyle;


    [SerializeField] private string _searchField;
    [SerializeField] private Sound _newSound;

    void Awake()
    {
        // Create a GUIStyle with a larger font size for the title label
        _titleLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        _titleLabelStyle.fontSize = 25; // Set the font size to 16 (adjust as needed)
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        SoundBank soundBank = (SoundBank)target;

        EditorGUILayout.LabelField("Editeur Sounds Bank", _titleLabelStyle);

        EditorGUILayout.HelpBox("C'est la Banque de Sons contenant tout les sons du jeu.", MessageType.Info);
        EditorGUILayout.Space(35);


        #region Add New Sound Button
        EditorGUI.BeginChangeCheck();
        _newSound.Name = EditorGUILayout.TextField("Sound Name:", _newSound.Name);
        _newSound.Clip = (AudioClip)EditorGUILayout.ObjectField("Sound Clip:", _newSound.Clip, typeof(AudioClip), true);
        _newSound.Type = (SoundType)EditorGUILayout.EnumPopup("Sound Type:", _newSound.Type);


        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Modified YourStruct");
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Add New Sound"))
        {
            soundBank.AddSound(_newSound);
            ClearNewSound();
        }

        EditorGUILayout.HelpBox("Rempli les champs du bloque 'New Sound' juste au dessus et quand tu cliquera sur 'Add New Sound' le son sera ajouté à la base de données.", MessageType.Info);

        #endregion

        EditorGUILayout.Space(25);

        #region Delete Sound Button
        _searchField = EditorGUILayout.TextField("Search Field:", _searchField);
        if (GUILayout.Button("Delete This Sound"))
        {
            soundBank.DeleteSound(_searchField);
        }
        EditorGUILayout.HelpBox("Met le nom du son que tu souhaite retirer de la banque dans le 'search field' puis clique sur delete sound pour le delete.", MessageType.Info);
        #endregion

        EditorGUILayout.Space(25);


        if (GUILayout.Button("Sound Bank Length"))
        {
            Debug.Log(soundBank.GetSoundBankCount());
        }
    }

    public void ClearNewSound()
    {
        _newSound = new Sound();
    }
}*/