using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class BGM_Data
{
    public float BGM_Volume;//볼륨 저장
}

public class BGM_Manager : MonoBehaviour
{
    public Slider BGM_Volume_Silder;
    public AudioSource[] BGM_Audio;//배경음 넣을 곳
    public float Default_Volume = 0.5f;//기존 볼륨
    public float Current_Volume;//현재 볼륨

    private void Start()
    {
        Load_BGM();//현재 볼륨이 얼마인지 불러오기
    }

    private void Update()
    {
        foreach (var audioSource in BGM_Audio)
        {
            audioSource.volume = BGM_Volume_Silder.value;//볼륨 값을 슬라이더 값에 저장
            Current_Volume = audioSource.volume;//현재 볼륨은 효과음 볼륨값과 같다

            Save_BGM();//저장하기

            Debug.Log(Application.persistentDataPath);
        }
    }

    private void Save_BGM()
    {
        //데이터 저장
        BGM_Data data = new BGM_Data();
        data.BGM_Volume = BGM_Volume_Silder.value;//현재 효과음 볼륨을 슬라이더에서 가져와서
        //SFX_Volume에 할당

        string jsonData = JsonUtility.ToJson(data);

        // JSON문자열로 변환
        File.WriteAllText(Application.persistentDataPath + "/BGM.json", jsonData);
        Debug.Log("배경 음악 볼륨 저장");
        Debug.Log("현재 볼륨은:" + BGM_Volume_Silder.value);
    }

    private void Load_BGM()
    {
        string path = Application.persistentDataPath + "/BGM.json";
        //BGM.json이라는 파일이 존재하는지 확인

        if (File.Exists(path))
        {
            //파일이 존재하는 경우 파일을 읽어온다
            string json = File.ReadAllText(path);

            BGM_Data data = JsonUtility.FromJson<BGM_Data>(json);
            BGM_Volume_Silder.value = data.BGM_Volume;

            Debug.Log("현재 볼륨은:" + BGM_Volume_Silder.value);
        }

        else
        {
            // 저장된 값이 없을 경우 초기화
            BGM_Volume_Silder.value = Default_Volume;

            // 각 AudioSource에 기본 값으로 초기화
            foreach (var audioSource in BGM_Audio)
            {
                audioSource.volume = Default_Volume;
            }
        }

    }


    public void Reset_BGM_Settings()
    {

        string path = Application.persistentDataPath + "/BGM.json";

        if (File.Exists(path))
        {
            //파일이 존재할 경우, 지우기
            File.Delete(path);

            //초기화 될 내용들(볼륨 처음 설정대로)
            // 기본 값으로 초기화
            BGM_Volume_Silder.value = Default_Volume;

            // 각 AudioSource에 기본 값으로 초기화
            foreach (var audioSource in BGM_Audio)
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

}
