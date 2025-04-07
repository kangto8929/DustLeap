using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;
using TMPro;


[System.Serializable]

public class SFX_Data
{
    public float SFX_Volume;//볼륨 저장
}

public class SFX_Manager : MonoBehaviour
{
    public Slider SFX_Volume_Silder;
    public AudioSource[] SFX_Audio;//효과음 넣을 곳
    public float Default_Volume = 0.5f;//기존 볼륨
    public float Current_Volume;//현재 볼륨

    public static SFX_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load_SFX();//현재 볼륨이 얼마인지 불러오기
    }

    private void Update()
    {
        foreach (var audioSource in SFX_Audio)
        {
            audioSource.volume = SFX_Volume_Silder.value;//볼륨 값을 슬라이더 값에 저장
            Current_Volume = audioSource.volume;//현재 볼륨은 효과음 볼륨값과 같다

            Save_SFX();//저장하기
            Debug.Log(Application.persistentDataPath);
        }
    }

    private void Save_SFX()
    {
        //데이터 저장
        SFX_Data data = new SFX_Data();
        data.SFX_Volume = SFX_Volume_Silder.value;//현재 효과음 볼륨을 슬라이더에서 가져와서
        //SFX_Volume에 할당

        string jsonData = JsonUtility.ToJson(data);

        //JSON문자열로 변환
        File.WriteAllText(Application.persistentDataPath + "/SFX.json", jsonData);
        Debug.Log("배경 음악 볼륨 저장");
        Debug.Log("현재 볼륨은:" + SFX_Volume_Silder.value);
    }

    private void Load_SFX()
    {
        string path = Application.persistentDataPath + "/SFX.json";
        //SFX.json이라는 파일이 존재하는지 확인

        if (File.Exists(path))
        {
            //파일이 존재하는 경우 파일을 읽어온다
            string json = File.ReadAllText(path);

            SFX_Data data = JsonUtility.FromJson<SFX_Data>(json);
            SFX_Volume_Silder.value = data.SFX_Volume;

            Debug.Log("현재 볼륨은:" + SFX_Volume_Silder.value);
        }

        else
        {
            // 저장된 값이 없을 경우 초기화
            SFX_Volume_Silder.value = Default_Volume;

            // 각 AudioSource에 기본 값으로 초기화
            foreach (var audioSource in SFX_Audio)
            {
                audioSource.volume = Default_Volume;
            }
        }
    }


    public void Reset_SFX_Settings()
    {
        string path = Application.persistentDataPath + "/SFX.json";

        if (File.Exists(path))
        {
            //파일이 존재할 경우, 지우기
            File.Delete(path);

            //초기화 될 내용들(볼륨 처음 설정대로)
            // 기본 값으로 초기화
            SFX_Volume_Silder.value = Default_Volume;

            // 각 AudioSource에 기본 값으로 초기화
            foreach (var audioSource in SFX_Audio)
            {
                audioSource.volume = Default_Volume;

                Current_Volume = audioSource.volume;
            }
        }

        else
        {
            Debug.Log("삭제할 볼륨 데이터 없음");
        }
    }

    public void Jump()
    {
        SFX_Audio[0].Play();
    }

    public void StopJump()
    {
        SFX_Audio[0].Stop();
    }

    public void Button()
    {
        SFX_Audio[1].Play();
    }
}

