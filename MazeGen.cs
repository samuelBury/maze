using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace theMaze
{
   
    public class MazeGen : MonoBehaviour
    {
        private  List<List<Cellule>> Cellules;//Cellules[0]
        private List<Spawn> Spawns;
        [SerializeField] private GameObject prefabsCellule;
        [SerializeField] private GameObject prefabsSpawn;

        private Vector3 groudSize; // valeur possible : 5n -1  
        private int sizeCellule;
        private Cellule current;

        // Start is called before the first frame update
        void Start()
        {

            Cellules = new List<List<Cellule>>();
            Spawns = new List<Spawn>();
            sizeCellule = 5;
            groudSize = transform.localScale;

            float nbCellule = (groudSize.x + 1) / 5;
            //Vector3 celDim = prefabCellule.transform.localScale;
            
            
            int indice = 0;
            int iIndex =0;
            for (float i = -groudSize.x / 2 +sizeCellule/5+1  ; i < groudSize.x / 2; i += sizeCellule)
            {
                Cellules.Add(new List<Cellule>());
                int jIndex = 0;

                for (float j = -groudSize.y / 2 + sizeCellule / 5 +1 ; j < groudSize.y / 2; j += sizeCellule)
                {
                    Cellule cel;
                    
                       
                     
                    cel = new Cellule(prefabsCellule, indice, new Vector3(i, 0, j));
                    cel.I = iIndex;
                    cel.J = jIndex;
                    indice++;
                    
                    Cellules[iIndex].Add(cel);
                    jIndex++;
                }

                iIndex++;
            }
            List<int> adjacentChoices = new List<int>();
            //GenerateWall();
            current = Cellules[Random.Range(0, Cellules.Count)][Random.Range(0, Cellules.Count)];
            current.Visited = true;
            int[] moveI = new int[] {0, 1, 0, -1 };
            int[] moveJ = new int[] {1, 0, -1, 0 };
            int[] murs = new int[] { 3, 2, 0, 1 };
            int[] mursVoisins = new int[] { 3, 2, 1, 0 };
            
            Stack<Cellule> pile = new Stack<Cellule>();
            pile.Push(current);

            while (pile.Count!=0)
            {
                
                
                current.Visited = true;
                adjacentChoices = getAdjacent();
                
                if (adjacentChoices.Count==0)
                {
                    current = pile.Pop();
                    continue;
                }
                
                int choix= adjacentChoices[Random.Range(0, adjacentChoices.Count)];
                Cellule nextPos= Cellules[current.I + moveI[choix]][current.J + moveJ[choix]];
                
                

                current.Cel.transform.GetChild(murs[choix]).gameObject.SetActive(false);




                current = nextPos;

                current.Cel.transform.GetChild(mursVoisins[murs[choix]]).gameObject.SetActive(false);

                pile.Push(current);
                
                

                
            }
            SetSpawn();
       
        }

        private void SetSpawn()
        {
            List<Cellule> CelSpawns = new List<Cellule>();

            
            int longueur = Cellules.Count ;
            //print(longueur/2);
            //print(Cellules[longueur / 2][1].Cel);
            
            CelSpawns.Add(Cellules[longueur / 2-1][0]);
            CelSpawns.Add(Cellules[0][longueur/2-1]);
            CelSpawns.Add(Cellules[longueur / 2-1][longueur-2]);
            CelSpawns.Add(Cellules[longueur-2][longueur / 2-1]);
            int[] listeX = new int[] { 0, -1, 0, 1 };
            int[] listeY = new int[] { -1, 0, 1, 0 };

            int i = 0;
            foreach (Cellule cel in CelSpawns)
            {
                Vector3 centre = new Vector3(cel.Cel.transform.position.x + sizeCellule / 2 + 0.5f, cel.Cel.transform.position.y, cel.Cel.transform.position.z + sizeCellule / 2+0.5f) ;
                Cellules[cel.I][cel.J + 1].Cel.SetActive(false);
                Cellules[cel.I + 1][cel.J + 1].Cel.SetActive(false);
                Cellules[cel.I + 1][cel.J].Cel.SetActive(false);
                cel.Cel.SetActive(false);
                
                Spawn spwn = new Spawn(prefabsSpawn, centre);
                spwn.I = listeX[i];
                spwn.J = listeY[i];
                Spawns.Add(spwn);
                i++;
            }
            


              
        }
        private List<GameObject> getAdjacentToSpawn(Spawn spwn)
        {
            List<GameObject> Walls = new List<GameObject>();




            return Walls;

        }
        private List<int> getAdjacent()
        {

           
            List<int> Cases = new List<int>();
            if(current.I<Cellules.Count-1 && !Cellules[current.I+1][current.J].Visited)
            {
                Cases.Add(1);
            }
            if (current.I>0 && !Cellules[current.I-1][current.J].Visited)
            {
                Cases.Add(3);
            }
            if (current.J< Cellules.Count-1 && !Cellules[current.I][current.J+1].Visited)
            {
                Cases.Add(0);
            }
            if (current.J>0 && !Cellules[current.I][current.J-1].Visited)
            {
                Cases.Add(2);
            }

            return Cases;
        }
        
        private bool CasesRestante()
        {
            foreach(List<Cellule> Cels in Cellules)
            {
                foreach(Cellule cel in Cels)
                {
                    if (!cel.Visited)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
       /** void GenerateWall()
        {
            GameObject Wall1 =GameObject.CreatePrimitive(PrimitiveType.Cube);
           
            
            Wall1.transform.localScale = new Vector3(1, 1, groudSize.x);
            Wall1.transform.position = new Vector3(groudSize.x / 2 + Wall1.transform.localScale.x/2 , 0, 0);


            GameObject Wall2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            
            Wall2.transform.localScale = new Vector3(1, 1, groudSize.x);
            Wall2.transform.position = new Vector3(-groudSize.x / 2 - Wall2.transform.localScale.x/2 , 0, 0);


            GameObject Wall3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
          
            
            Wall3.transform.localScale = new Vector3(groudSize.x, 1, 1);
            Wall3.transform.position = new Vector3(0, 0, groudSize.x / 2+ Wall3.transform.localScale.z/2);


            GameObject Wall4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
           
            
            Wall4.transform.localScale = new Vector3(groudSize.x, 1, 1);
            Wall4.transform.position = new Vector3(0, 0, -groudSize.x/2 - Wall4.transform.localScale.z/2 );




        }
        **/
        // Update is called once per frame
        void Update()
        {
            
        }
    }

    public class Spawn : MonoBehaviour
    {
        public GameObject prefabSpawn;
        public GameObject SpawnObject { get; set; }
        public int I {get;set;}
        public int J { get; set; }

        public Spawn(GameObject prefabs, Vector3 Pos)
        {
            prefabSpawn = prefabs;
            SpawnObject = Instantiate(prefabSpawn, Pos, Quaternion.identity);
        }

    }
    public class Cellule : MonoBehaviour
    {
        public GameObject Cel { get; set; }
        public GameObject prefabCellule;
        public int I { get; set; }
        public int J { get; set; }
        private int id;
        public bool Visited { get; set; }
        public override string ToString()
        {
            
            return "I :"+I+" J :"+J;
        }
        public Cellule(GameObject prefabs, int Id, Vector3 Pos ){
            id = Id;
            prefabCellule = prefabs;
            Cel=Instantiate(prefabCellule, Pos, Quaternion.identity);
            Visited = false;
            
     
            // Wall2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Wall2.transform.position = new Vector3(Pos.x, Pos.y, 3);
        }
    }
}
