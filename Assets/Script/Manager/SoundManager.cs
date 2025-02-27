using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class Sound
{
    //*************************************************************
    //각 사운드마다 AudioSource를 MaxSound만큼 들고 있던 방식에서
    //공용AudioSource를 사용하는 방식으로 변경되었다.
    //*************************************************************
    //[Range(1, 100)] //0을 못넣게 할려고!!
    //public int MaxSound = 1;
    public AudioClip Clip = null;
    [HideInInspector]
    public List<AudioSource> List_Source = new List<AudioSource>();
    [HideInInspector]
    public AudioSource PlayAudio = null; //BGM일때만
    [HideInInspector]
    public float Nowtime = 0f;

    public void Play(bool loop, float volume)
    {
        //중복 방지 체크
        if (Nowtime == Time.time)
            return;
        Nowtime = Time.time;

        for (int i = 0; i < List_Source.Count; i++)
        {
            if (List_Source[i].isPlaying == false)
            {
                //List_Source[i].enabled = true;
                List_Source[i].gameObject.SetActive(true);
                List_Source[i].clip = Clip;
                List_Source[i].loop = loop;
                List_Source[i].volume = volume;
                List_Source[i].Play();
                PlayAudio = List_Source[i]; //BGM일때만 사용
                break;
            }
        }


        if (PlayAudio == null)
        {
            Debug.LogError("MaxSound 적습니다. 일부 Effect사운드가 출력되지 않았습니다.");

            if (loop)
            {
                //Max개를 다쓰는중일때도 BGM은 틀어야 한다!!
                //BGM는 Dotween를 사용하는곳이 있어서 PlayAudio값이 없는 상황이 생기면 안된다.       
                GameObject obj = new GameObject(string.Format("MaxSoundOver"));
                obj.transform.parent = SoundManager.Instance.transform;
                AudioSource audiosource = obj.AddComponent<AudioSource>();

                audiosource.gameObject.SetActive(true);
                audiosource.clip = Clip;
                audiosource.loop = loop;
                audiosource.volume = volume;
                audiosource.Play();
                PlayAudio = audiosource; //BGM일때만 사용

                List_Source.Add(audiosource);
            }
        }

    }

    //제한갯수의 AudioSource를 돌려쓰기 때문에 이제는 SetActive(false)를 하지 않는다.
    //(많은 AudioSource가 Active 되어 있는것 만으로도 엄청만 부하를 줬기 때문에 SetActive(false)를 줬지만 몇개 안되므로 그냥 켜두자)
    //BGM만 스톱기능을 제공한다.
    public void BGMStop()
    {
        PlayAudio.Stop();
        //BGMAudio.gameObject.SetActive(false);
        PlayAudio.clip = null;
        PlayAudio = null;
    }

    public void SetSpeed(float speed)
    {
        for (int i = 0; i < List_Source.Count; ++i)
        {
            List_Source[i].pitch = speed;
        }
    }
}
public class SoundManager : Singleton<SoundManager>
{
    public int MaxSound = 30;
    [HideInInspector]
    public bool isBGM;
    [HideInInspector]
    public bool isEffect;
    [HideInInspector]
    public bool isBGMPlaying;
    [HideInInspector]
    public Sound curBGM;

    private List<Sound> m_List_Sound = new List<Sound>();
    public List<AudioClip> List_Sound = new List<AudioClip>();
    Dictionary<string, Sound> Dic_Sound = new Dictionary<string, Sound>();

    private List<AudioSource> AudioObjs = new List<AudioSource>();

    public bool isFading = false; //BGM은 fade하는데 시간이 걸린다. 연속클릭 방지.

    void Awake()
    {
        isBGM = SecurityPlayerPrefs.GetBool("isBGM", true);
        isEffect = SecurityPlayerPrefs.GetBool("isEffect", true);
        isBGMPlaying = false;
        curBGM = null;

        for (int i = 0; i < MaxSound; i++)
        {
            GameObject obj = new GameObject(string.Format("AuidoSource{0}", i));
            obj.transform.parent = this.transform;
            AudioSource audiosource = obj.AddComponent<AudioSource>();
            //audiosource.enabled = false;
            obj.SetActive(false);
            AudioObjs.Add(audiosource);
        }

        //MaxSound만큼만 만들어서 모든 sound가 AudioSource를 공유함.
        //즉, 동일한 객체들을 sound마다 참조하고 있다.
        for (int i = 0; i < List_Sound.Count; i++)
        {
            Sound sound = new Sound();
            sound.Clip = List_Sound[i];
            if (sound.Clip != null)
            {
                sound.List_Source.AddRange(AudioObjs);
                Dic_Sound.Add(sound.Clip.name, sound);
            }

            m_List_Sound.Add(sound);
        }
    }
    //void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //    isBGM = SecurityPlayerPrefs.GetBool("isBGM", true);
    //    isEffect = SecurityPlayerPrefs.GetBool("isEffect", true);

    //    if (!isBGM)
    //    {
    //        BGMStopchk = true;
    //    }

    //    foreach (Sound sound in List_Sound)
    //    {
    //        if (sound.Clip != null)
    //        {
    //            for (int i = 0; i < sound.MaxSound; i++)
    //            {
    //                GameObject obj = new GameObject(sound.Clip.name);
    //                obj.transform.parent = this.transform;
    //                AudioSource audiosource = obj.AddComponent<AudioSource>();
    //                //audiosource.enabled = false;
    //                obj.SetActive(false);
    //                sound.List_Source.Add(audiosource);
    //            }
    //            Dic_Sound.Add(sound.Clip.name, sound);
    //        }

    //    }

    //    StartCoroutine(SoundUpdate());
    //}

    public void SoundMgrUpdate()
    {
        for (int i = 0; i < AudioObjs.Count; ++i)
        {
            if (AudioObjs[i].isPlaying == false)
            {
                //List_Source[i].enabled = false;
                AudioObjs[i].gameObject.SetActive(false);
            }
        }
    }

    public void PreLoadAudioData()
    {
        foreach (Sound sound in m_List_Sound)
        {
            if (sound.Clip != null)
            {
                sound.Clip.LoadAudioData();
            }
        }
    }

    public void PlayEffect(string clip, bool loop = false, float volume = 1f)
    {
        if (isEffect == false)
            return;

        Dic_Sound[clip].Play(loop, volume);
    }

    //BGM 관련
    public void PlayBGM(string clip, bool loop = true, bool useFade = true, float volume = 1f)
    {
        if (isBGM == false)
            return;

        //배경음이 실행중인데 같은놈이면 리턴..
        if (isBGMPlaying && curBGM != null && clip.Equals(curBGM.Clip.name))
            return;

        Sound nextSnd = null;
        if (Dic_Sound.ContainsKey(clip))
            nextSnd = Dic_Sound[clip];
        else
            Debug.LogError("clip이 없습니다. clip 이름을 확인하세요!");

        float pFadeTime = 0;
        if (useFade)
        {
            isFading = true;

            Sequence seq = DOTween.Sequence();

            if (curBGM != null && isBGMPlaying)
            {
                pFadeTime = 0.5f;
                //StopBGM()를 사용할수 없음..seq가 다르면 순서 보장못함. 
                seq.Append(DOTween.To(() => curBGM.PlayAudio.volume, x => curBGM.PlayAudio.volume = x, 0f, pFadeTime));
                seq.InsertCallback(pFadeTime, () =>
                {
                    curBGM.BGMStop();
                });
            }

            if (nextSnd != null)
            {
                //AppendCallback또는 동일한시간에 InsertCallback은 add한 역순으로 실행되는 거지같은.. 그래서 BGMStop보다 Play가 늦게 실행되도록 시간을 조금 더준다.
                //추가수정 : 이번에 수정하면서 BGM에서만 사용하는 PlayAudio변수가 추가되었는데 이 변수는 Play함수에서 세팅한다.
                //          (왜냐면 공용리스트인 AudioSource중에 어떤걸 사용할껀지는 그때 그때 달라지기 때문에)
                //          그래서 볼륨을 조절하기전에 Play가 먼저 세팅되어야 하므로 Insert보다 InsertCallback이 시간을 더 낮게 수정한다.
                seq.InsertCallback(pFadeTime + 0.01f, () =>
                {
                    nextSnd.Play(loop, 0);
                    curBGM = nextSnd;
                    isBGMPlaying = true;
                });
                seq.Insert(pFadeTime + 0.02f, DOTween.To(() => nextSnd.PlayAudio.volume, x => nextSnd.PlayAudio.volume = x, volume, 0.5f));
                seq.OnComplete(() =>
                {
                    isFading = false;
                });
            }
        }
        else
        {
            StopBGM(false);

            if (nextSnd != null)
            {
                nextSnd.Play(loop, volume);
                curBGM = nextSnd;
                isBGMPlaying = true;
            }
        }


    }


    public void StopBGM(bool useFade = false, float fadetime = 0.5f)
    {
        //주의 사항
        //Play함수도 fade일때 시간을 두고 켜진다.
        //즉, 끄고 켜고를 빠르게 하면 문제가 생길수 있다.
        //현재 옵션에서는 fade를 사용하지 않기때문에 바로 꺼질테고 켜질때는 기존BGM이 없으니 Play쪽에 있는 0.01f만큼의 딜레만 존재하는데 그사이에 끄면 문제가 생기겠지.
        //하지만 불가능하기 때문에 무시한다.
        //중요한건 fade를 사용하는데 신중해야 한다.!! 플레이와 겹치만 안됨!!

        isBGMPlaying = false;

        if (curBGM == null)
            return;

        if (useFade)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => curBGM.PlayAudio.volume, x => curBGM.PlayAudio.volume = x, 0f, fadetime));

            seq.InsertCallback(fadetime, () =>
            {
                curBGM.BGMStop();
                curBGM = null;
            });
        }
        else
        {
            curBGM.BGMStop();
            curBGM = null;
        }


    }

    public void SoundUpdate()
    {
        SoundMgrUpdate();
    }
}
