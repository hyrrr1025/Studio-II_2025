using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator2 : MonoBehaviour
{
    // ����ĵ�ͼ��
    public string[] map;

    // ���е�Ԥ���塣
    public GameObject wall;
    public GameObject player;
    public GameObject box1; // ����1����
    public GameObject box2; // ����2����
    public GameObject box3; // ����3����
    public GameObject box4; // ����4����
    public GameObject target;
    public GameObject ground;

    // ���ӣ�ǽ��Ŀ����λ�á�
    private Dictionary<int, GameObject> pos_box_map;
    private HashSet<int> wall_pos_set;
    private List<int> tar_pos_list;

    // ����2άתһά��
    // use to convert 2D position to 1D position.
    public const int SIZE = 1000;

    // ���Ͻǵ�ͼ��ʼ���λ�á�
    // Left top position
    public int left_top_x = -3;
    public int left_top_y = -4;

    private void Awake()
    {
        pos_box_map = new Dictionary<int, GameObject>();
        wall_pos_set = new HashSet<int>();
        tar_pos_list = new List<int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �����ң����ϵ��½�ͼ��
        int row_pos = left_top_x;
        foreach (var row in map)
        {
            int col_pos = left_top_y;
            for (int i = 0; i < row.Length; ++i)
            {
                Vector3 cell_pos = new Vector3(row_pos, col_pos);

                if (row[i] == '#')
                {
                    Instantiate(wall, cell_pos, Quaternion.identity);
                    wall_pos_set.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == 'A')
                {
                    Instantiate(player, cell_pos, Quaternion.identity);
                }
                else if (row[i] == '1') // ��������1����
                {
                    GameObject newbox = Instantiate(box1, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '2') // ��������2����
                {
                    GameObject newbox = Instantiate(box2, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '3') // ��������3����
                {
                    GameObject newbox = Instantiate(box3, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '4') // ��������4����
                {
                    GameObject newbox = Instantiate(box4, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == 'T')
                {
                    Instantiate(target, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }

                // Ground.
                Instantiate(ground, cell_pos, Quaternion.identity);
                col_pos++;
            }
            row_pos++;
        }
    }

    public Dictionary<int, GameObject> getPosBoxMap()
    {
        return pos_box_map;
    }

    public HashSet<int> getWallPosSet()
    {
        return wall_pos_set;
    }

    public List<int> getTargetPosList()
    {
        return tar_pos_list;
    }

    public int TwoDToOneD(int x, int y)
    {
        return SIZE * x + y;
    }
}
