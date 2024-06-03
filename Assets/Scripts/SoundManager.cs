using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefSO audioClipRefSO;
    private const string PLAYER_PREFS_SOUNDEFFECT_VOLUME = "SoundEffectVolume;";
    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUNDEFFECT_VOLUME ,1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }

    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e)
    {
        TrashCounter TrashCounter = sender as TrashCounter;
        PlayeSound(audioClipRefSO.trash, TrashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayeSound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayeSound(audioClipRefSO.objectPickup, Player.instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayeSound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayeSound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayeSound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlayeSound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlayeSound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    private void PlayeSound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume)
    { 
        PlayeSound(audioClipRefSO.footstep, position, volume);
    }

    public void PlayWarnignSound(Vector3 position)
    {
        PlayeSound(audioClipRefSO.warning, position);
    }

    public void ChangeVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0f, 1f);
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUNDEFFECT_VOLUME, volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}