using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                combo--;
                SongManager.ManagerInstance.CreateComboNum();
            }
            //comboTimer = 0;
        }
    }
}
