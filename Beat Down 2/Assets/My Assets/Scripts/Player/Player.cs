using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    public float playerHealth;
    private float playerMaxHealth;
    public Slider healthSlider;

    public float regenTime;
    private float regenTimer;
    public float regenRate;

    public GunBase selectedWeapon;
    public List<AudioSource> weaponMusic;

    public List<GameObject> guns;
    private int gunIndex = 0;

    public List<int> ammoTypes;

    public int money;
    public int combo;
    public float comboTime;
    public float comboTimer;



    [Header("Death Effects")]
    public PostProcessVolume volume;
    public ChromaticAberration chromaticAberration;
    private float deathTimer;
    public float deathTime;
    public AudioSource deathSound;

    public bool goToNextLevel;
    private float goTimer;
    public float goTime;

    // Start is called before the first frame update
    void Start()
    {
        playerMaxHealth = playerHealth;
        selectedWeapon = guns[0].GetComponent<GunBase>();
        SongManager.ManagerInstance.audioList.Add(guns[0].GetComponent<GunBase>().myAudio);
        guns[0].GetComponent<GunBase>().myAudio.volume = 0;
        for (int i = 1; i < guns.Count; i++)
        {
            SongManager.ManagerInstance.audioList.Add(guns[i].GetComponent<GunBase>().myAudio);
            guns[i].GetComponent<GunBase>().myAudio.volume = 0;
            guns[i].SetActive(false);
        }

        int b = guns.Count;
        for (int i = 1; i < b; i++)
        {
            guns.RemoveAt(1);
        }

        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromaticAberration);
        chromaticAberration.intensity.value = 0;

        goToNextLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth / playerMaxHealth;
        Regen();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
        UpdateComboText();
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        if(playerHealth <= 0)
        {
            deathTimer += Time.deltaTime;
            if (!deathSound.isPlaying)
            {
                deathSound.Play();
                
            }
            chromaticAberration.intensity.value = Mathf.Lerp(0,2f,deathTimer/deathTime);
            if(deathTimer > deathTime)
            {
                SceneManager.LoadScene("DeathScene");
            }
        }

        if (goToNextLevel)
        {
            goTimer += Time.deltaTime;
            if (!deathSound.isPlaying)
            {
                deathSound.Play();

            }
            chromaticAberration.intensity.value = Mathf.Lerp(0, 2f, goTimer / goTime);
            if (goTimer > goTime)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }


    public void Damage()
    {
        playerHealth -= 5f;
        regenTimer = 0;


    }

    public void DamageD(float d)
    {
        playerHealth -= d;
        regenTimer = 0;
    }

    void Regen()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer > regenTime)
        {
            if (playerHealth < playerMaxHealth && playerHealth > playerMaxHealth * 0.75)
            {
                if (playerHealth + Time.deltaTime * regenRate < playerMaxHealth)
                {
                    playerHealth += Time.deltaTime * regenRate;
                }
                else
                {
                    playerHealth = playerMaxHealth;
                }
            }

            if (playerHealth < playerMaxHealth * 0.75 && playerHealth > playerMaxHealth * 0.5)
            {
                if (playerHealth + Time.deltaTime * regenRate < playerMaxHealth * 0.75)
                {
                    playerHealth += Time.deltaTime * regenRate;
                }
                else
                {
                    playerHealth = playerMaxHealth * 0.75f;
                }
            }

            if (playerHealth < playerMaxHealth * 0.5 && playerHealth > playerMaxHealth * 0.25)
            {
                if (playerHealth + Time.deltaTime * regenRate < playerMaxHealth * 0.5)
                {
                    playerHealth += Time.deltaTime * regenRate;
                }
                else
                {
                    playerHealth = playerMaxHealth * 0.5f;
                }
            }

            if (playerHealth < playerMaxHealth * 0.25 && playerHealth > 0)
            {
                if (playerHealth + Time.deltaTime * regenRate < playerMaxHealth * 0.25)
                {
                    playerHealth += Time.deltaTime * regenRate;
                }
                else
                {
                    playerHealth = playerMaxHealth * 0.25f;
                }
            }


        }
    }


    void ChangeWeapon()
    {
        guns[gunIndex].GetComponent<GunBase>().myAudio.volume = 0;
        guns[gunIndex].SetActive(false);

        gunIndex++;

        if(gunIndex >= guns.Count)
        {
            gunIndex = 0;
        }

        guns[gunIndex].SetActive(true);
        selectedWeapon = guns[gunIndex].GetComponent<GunBase>();
        
    }

    public void UpdateComboText()
    {
        comboTimer += Time.deltaTime;
        if(comboTimer > comboTime)
        {
            if(combo > 0)
            {
                combo = 0;
                SongManager.ManagerInstance.CreateComboNum();
            }
            //comboTimer = 0;
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("DeathScene");

        // Wait until the asynchronous scene fully loads
        while (deathTimer < deathTime)
        {
            yield return null;
        }
    }


}
