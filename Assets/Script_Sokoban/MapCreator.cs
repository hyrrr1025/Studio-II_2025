using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    // 输入的地图。
    public string[] map;

    // 所有的预置体。
    public GameObject wall;
    public GameObject player;
    public GameObject box1; // 类型1箱子
    public GameObject box2; // 类型2箱子
    public GameObject box3; // 类型3箱子
    public GameObject box4; // 类型4箱子
    public GameObject target1; // 目标点1
    public GameObject target2; // 目标点2
    public GameObject target3; // 目标点3
    public GameObject target4; // 目标点4
    public GameObject ground;

    // 盒子，墙和目标点的位置。
    private Dictionary<int, GameObject> pos_box_map;
    private HashSet<int> wall_pos_set ;
    private List<int> tar_pos_list;

    // 用于2维转一维。
    // use to convert 2D position to 1D position.
    public const int SIZE = 1000;

    // 左上角地图起始点的位置。
    // Left top position
    public int left_top_x = -5;
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
        // 从左到右，从上到下建图。
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
                else if (row[i] == '1') // 生成类型1箱子
                {
                    GameObject newbox = Instantiate(box1, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '2') // 生成类型2箱子
                {
                    GameObject newbox = Instantiate(box2, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '3') // 生成类型3箱子
                {
                    GameObject newbox = Instantiate(box3, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == '4') // 生成类型4箱子
                {
                    GameObject newbox = Instantiate(box4, cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newbox);
                }
                else if (row[i] == 'Q') // 生成目标点1
                {
                    Instantiate(target1, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == 'W') // 生成目标点2
                {
                    Instantiate(target2, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == 'E') // 生成目标点3
                {
                    Instantiate(target3, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == 'R') // 生成目标点4
                {
                    Instantiate(target4, cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }

                // Ground.
                Instantiate(ground, cell_pos, Quaternion.identity);
                col_pos++;
            }
            row_pos++;
        }
    }

    public Dictionary<int, GameObject> getPosBoxMap() {
        return pos_box_map;
    }

    public HashSet<int> getWallPosSet() {
        return wall_pos_set;
    }

    public List<int> getTargetPosList() {
        return tar_pos_list;
    }

    public int TwoDToOneD(int x, int y) {
        return SIZE * x + y;
    }

    void Update()
    {
        //按esc退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
