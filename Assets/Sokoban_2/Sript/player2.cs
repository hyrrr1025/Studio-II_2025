using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public static Player2 instance;

    // Map.
    private MapCreator2 myMap;

    // Audo.
    private AudioSource audioSource;

    // Animator.
    //private Animator animator;

    // ������¼�ϴ���������ҵķ���
    private int x_dir = 0;
    private int y_dir = 0;

    private void Awake()
    {
        instance = this;
        myMap = FindObjectOfType<MapCreator2>();
        audioSource = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnResh(int dx, int dy)
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dx--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dx++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dy++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dy--;
        }

        // ������ҵĶ�����
        // Update player last movement.
        if (dx != 0 || dy != 0)
        {
            // Player movement animation.
            x_dir = dx;
            y_dir = dy;
        }

        // ����¸�λ�á�
        // Next position.
        int nx = dx + (int)transform.position.x;
        int ny = dy + (int)transform.position.y;

        // �ж��¸�λ���ǲ���ǽ��
        if (isWall(nx, ny)) return;

        // �ж��¸�λ���ǲ��Ǻ��ӡ�
        if (isBox(nx, ny))
        {
            // �õ���ҵ����¸�λ�á�
            // Next next position.
            int nnx = nx + dx;
            int nny = ny + dy;

            // �ж����¸�λ���ǲ���ǽ���ߺ��ӡ�
            if (isBox(nnx, nny) || isWall(nnx, nny)) return;

            // �Ѻ����Ƶ��¸�λ�á�
            GameObject box = getBox(nx, ny);
            box.transform.position = new Vector3(nnx, nny);

            // ���º�����Map����Ľṹ��
            myMap.getPosBoxMap().Remove(myMap.TwoDToOneD(nx, ny));
            myMap.getPosBoxMap().Add(myMap.TwoDToOneD(nnx, nny), box);
        }

        // ������ƶ����¸�λ�á�
        // Move player to next position.
        transform.position = new Vector3(nx, ny);

        // �������ƶ����������֡�
        if (dx != 0 || dy != 0)
        {
            // Play foot step sound.
            audioSource.Play();
        }

        // �ж��ǲ���Ӯ�ˡ�
        checkIfWin();
    }
    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("moveX", x_dir);
        //animator.SetFloat("moveY", y_dir);

        // delta x and delta y.
        int dx = 0;
        int dy = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dx--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dx++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dy++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dy--;
        }

        // ������ҵĶ�����
        // Update player last movement.
        if (dx != 0 || dy != 0)
        {
            // Player movement animation.
            x_dir = dx;
            y_dir = dy;
        }

        // ����¸�λ�á�
        // Next position.
        int nx = dx + (int)transform.position.x;
        int ny = dy + (int)transform.position.y;

        // �ж��¸�λ���ǲ���ǽ��
        if (isWall(nx, ny)) return;

        // �ж��¸�λ���ǲ��Ǻ��ӡ�
        if (isBox(nx, ny))
        {
            // �õ���ҵ����¸�λ�á�
            // Next next position.
            int nnx = nx + dx;
            int nny = ny + dy;

            // �ж����¸�λ���ǲ���ǽ���ߺ��ӡ�
            if (isBox(nnx, nny) || isWall(nnx, nny)) return;

            // �Ѻ����Ƶ��¸�λ�á�
            GameObject box = getBox(nx, ny);
            box.transform.position = new Vector3(nnx, nny);

            // ���º�����Map����Ľṹ��
            myMap.getPosBoxMap().Remove(myMap.TwoDToOneD(nx, ny));
            myMap.getPosBoxMap().Add(myMap.TwoDToOneD(nnx, nny), box);
        }

        // ������ƶ����¸�λ�á�
        // Move player to next position.
        transform.position = new Vector3(nx, ny);

        // �������ƶ����������֡�
        if (dx != 0 || dy != 0)
        {
            // Play foot step sound.
            audioSource.Play();
        }

        // �ж��ǲ���Ӯ�ˡ�
        checkIfWin();
    }

    bool isWall(int x, int y)
    {
        return myMap.getWallPosSet().Contains(myMap.TwoDToOneD(x, y));
    }

    bool isBox(int x, int y)
    {
        return myMap.getPosBoxMap().ContainsKey(myMap.TwoDToOneD(x, y));
    }

    GameObject getBox(int x, int y)
    {
        return myMap.getPosBoxMap()[myMap.TwoDToOneD(x, y)];
    }

    void checkIfWin()
    {
        int num = 0;
        foreach (var tar_pos in myMap.getTargetPosList())
        {
            if (myMap.getPosBoxMap().ContainsKey(tar_pos)) ++num;
        }
        if (num == myMap.getTargetPosList().Count)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.LoadNextScene();
        }

    }
}
