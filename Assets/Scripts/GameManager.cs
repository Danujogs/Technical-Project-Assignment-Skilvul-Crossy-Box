using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject stepPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent = 7;
    [SerializeField] int frontDistance = 10;
    [SerializeField] int backDistance = -5;
    [SerializeField] int maxSameTerrainRepeat = 3;
    public AudioSource barkDog;


    public static bool GameIsPaused = false;


    //int maxZPos 
    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    TMP_Text gameOverText;
    private void Start()
    {
        barkDog.PlayOneShot(barkDog.clip);
        pauseMenu.SetActive(false);
        stepPanel.SetActive(true);
        // setup panel game over
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();

        // belakang
        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        // depan
        for (int z = 1; z <= frontDistance; z++)
        {
            var prefab = GetNextRandomTerrainPrefab(z);

            // instantiate terrain block
            CreateTerrain(prefab, z);
        }

        player.SetUp(backDistance, extent);
    }

    private int playerLastMaxTravel;
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (GameIsPaused)
        //     {
        //         Resume();
        //     }
        //     else
        //     {
        //         Pause();
        //     }
        // }

        // void Resume ()
        // {
        //     stepPanel.SetActive(true);
        //     Time.timeScale = 1f;
        //     GameIsPaused = false;
        // }

        // cek player apakah masih hidup
        if (player.IsDie && gameOverPanel.activeInHierarchy == false)
            StartCoroutine(ShowGameOverPanel());

        // infinite terrain system
        if (player.MaxTravel == playerLastMaxTravel)
            return;

        playerLastMaxTravel = player.MaxTravel;

        // buat ke depam
        var randTbPrefab = GetNextRandomTerrainPrefab(player.MaxTravel + frontDistance);
        CreateTerrain(randTbPrefab, player.MaxTravel + frontDistance);

        // hapus terrain di belakang
        var lastTB = map[player.MaxTravel - 1 + backDistance];

        // hapus dari daftar
        map.Remove(player.MaxTravel - 1 + backDistance);

        // hapus gameobject
        Destroy(lastTB.gameObject);

        // setup lagi supaya player gak bisa gerak ke belakang
        player.SetUp(player.MaxTravel + backDistance, extent);
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3);
        pauseMenu.SetActive(false);
        stepPanel.SetActive(false);
        gameOverText.text = "YOUR SCORE: " + player.MaxTravel;
        gameOverPanel.SetActive(true);
    }

    private void CreateTerrain(GameObject prefab, int zPos)
    {
        var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);

        map.Add(zPos, tb);
    }

    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass)
                return road;
            else
                return grass;
        }

        // menentukan terrain block dengan probabilitas 50% 
        return Random.value > 0.5f ? road : grass;

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}

