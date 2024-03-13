using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField, Range(0, 5.0f)] private float _radius;
    [SerializeField] private UnityEvent OnSoundEmitterTriggered;
    [SerializeField] private LayerMask _enemyLayerMask;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
        {
            if (playerManager.PlayerType == PlayerType.GrandMa)
            {
                PlaySoundEmitter();
            }
        }
    }

    public void PlaySoundEmitter()
    {
        AcheivementsManager.instance.UnlockSuccess(GameAssets.instance.AchievementsBank.AmogUs_ID);
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._soundEmitter);
        GetEnemiesAttention();
        OnSoundEmitterTriggered?.Invoke();
    }

    private void GetEnemiesAttention()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayerMask);

        foreach(Collider2D collider in colliders)
        {
            collider.GetComponent<Guard>().SetTarget(transform);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}



#if UNITY_EDITOR
[CustomEditor(typeof(SoundEmitter))]
public class SoundEmitterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Display the default inspector GUI
        DrawDefaultInspector();

        // Cast the target to MyClass
        SoundEmitter gameManager = (SoundEmitter)target;

        if (GUILayout.Button("Play Sound Emitter"))
        {
            gameManager.PlaySoundEmitter();
        }
    }
}
#endif