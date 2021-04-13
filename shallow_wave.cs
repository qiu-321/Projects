using UnityEngine;
using System.Collections;

public class shallow_wave : MonoBehaviour {
	int size;
	float[,] old_h;
	float[,] h;
	float[,] new_h;


	// Use this for initialization
	void Start () {
		size = 64;

		old_h = new float[size, size];
		h = new float[size, size];
		new_h = new float[size, size];
		for(int k =0; k < size; k++){
			for(int m = 0; m < size; m++){
				old_h[k,m] = 0;
				h[k,m] = 0;
				new_h[k,m] = 0;
			}
		}
	
		//Resize the mesh into a size*size grid
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		Vector3[] vertices=new Vector3[size*size];
		for (int i=0; i<size; i++)
		for (int j=0; j<size; j++) 
		{
			vertices[i*size+j].x=i*0.2f-size*0.1f;
			vertices[i*size+j].y=0;
			vertices[i*size+j].z=j*0.2f-size*0.1f;
		}
		int[] triangles = new int[(size - 1) * (size - 1) * 6];
		int index = 0;
		for (int i=0; i<size-1; i++)
		for (int j=0; j<size-1; j++)
		{
			triangles[index*6+0]=(i+0)*size+(j+0);
			triangles[index*6+1]=(i+0)*size+(j+1);
			triangles[index*6+2]=(i+1)*size+(j+1);
			triangles[index*6+3]=(i+0)*size+(j+0);
			triangles[index*6+4]=(i+1)*size+(j+1);
			triangles[index*6+5]=(i+1)*size+(j+0);
			index++;
		}
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();
	


	}

	void Shallow_Wave()
	{	
		float rate = 0.005f;
		float damping = 0.999f;

		int length = h.Length;
        int side = 64;
        float[,] new_h = new float[side, side];

        for(int y = 0; y < side; y++) {
            int y_1 = y;
            if((y-1)>= 0){ y_1 = y-1;}
            int yP1 = y;
            if((y+1) < size){yP1 = y+1;}

            for (int x = 0; x < side; x++) {
                int x_1 = x;
                if((x-1)>= 0){ x_1 = x-1;}
                int xP1 = x;
                if((x+1) < size){ xP1 = x+1;}
                new_h[y,x]=h[y,x]+(h[y,x]-old_h[y,x])*damping+(h[y_1,x]+h[yP1,x]+h[y,x_1]+h[y,xP1]-4*h[y,x])*rate;
            }
        }
        for(int i = 0; i < 64; i++){
        	for(int j =0; j<64; j++){
        		old_h[i,j] = h[i,j];
        		h[i,j] = new_h[i,j];
        	}
        }
	}

	// Update is called once per frame
	void Update () 
	{
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;

		//Step 1: Copy vertices.y into h
		int k = 0;
		for(int i = 0; i < 64; i++){
			for(int j = 0; j < 64; j++){
				h[i,j] = vertices[k].y;
				k++;
			}
		}
		//Step 2: User interaction
			//add water drops 
		if(Input.GetKeyDown("r")){
			int i = Random.Range(0, size);
			int j = Random.Range(0, size);
			float m = Random.Range(0.05f, 0.1f);
			h[i,j] = h[i,j] + m;
		}
		//Step 3: Run Shallow Wav
		for(int w = 0; w < 8; w++){
			Shallow_Wave();
		}
	
		//Step 4: Copy h back into mesh
		int kk = 0;
		for(int i =0; i < 64; i++){
			for(int j =0; j < 64; j++){
				vertices[kk].y = h[i,j];
				kk++;
			}
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals ();

	}
}
