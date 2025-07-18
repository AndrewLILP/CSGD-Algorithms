using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MengerSpongeFractal : MonoBehaviour
{
    [Header("Fractal Parameters")]
    [Range(0, 4)]
    public int iterations = 3; // Number of fractal iterations (be careful with high values!)
    public float baseSize = 3f; // Size of the initial cube

    [Header("Visual Settings")]
    public Material cubeMaterial; // Material for the cubes
    public bool animateGeneration = false; // Animate the generation process
    public float animationDelay = 0.1f; // Delay between cube spawns during animation

    [Header("Optimization")]
    public bool useCombinedMesh = false; // Combine all cubes into single mesh for performance
    public bool enableColliders = false; // Add colliders to cubes

    private List<GameObject> cubes = new List<GameObject>();
    private int totalCubes = 0;

    void Start()
    {
        if (animateGeneration)
        {
            StartCoroutine(AnimatedGeneration());
        }
        else
        {
            GenerateComplete();
        }
    }

    void GenerateComplete()
    {
        ClearExisting();
        GenerateMengerSponge(transform.position, baseSize, iterations);

        if (useCombinedMesh)
        {
            CombineMeshes();
        }

        Debug.Log($"Generated Menger Sponge with {totalCubes} cubes at {iterations} iterations");
    }

    IEnumerator AnimatedGeneration()
    {
        ClearExisting();
        yield return StartCoroutine(GenerateMengerSpongeAnimated(transform.position, baseSize, iterations));

        if (useCombinedMesh)
        {
            CombineMeshes();
        }

        Debug.Log($"Generated Menger Sponge with {totalCubes} cubes at {iterations} iterations");
    }

    void GenerateMengerSponge(Vector3 center, float size, int depth)
    {
        if (depth == 0)
        {
            CreateCube(center, size);
            return;
        }

        float newSize = size / 3f;

        // Generate 20 smaller cubes (27 total positions minus 7 removed positions)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    // Skip the center cube and face-centered cubes
                    if (ShouldSkipPosition(x, y, z))
                        continue;

                    Vector3 offset = new Vector3(x, y, z) * newSize;
                    Vector3 newCenter = center + offset;

                    GenerateMengerSponge(newCenter, newSize, depth - 1);
                }
            }
        }
    }

    IEnumerator GenerateMengerSpongeAnimated(Vector3 center, float size, int depth)
    {
        if (depth == 0)
        {
            CreateCube(center, size);
            yield return new WaitForSeconds(animationDelay);
            yield break;
        }

        float newSize = size / 3f;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (ShouldSkipPosition(x, y, z))
                        continue;

                    Vector3 offset = new Vector3(x, y, z) * newSize;
                    Vector3 newCenter = center + offset;

                    yield return StartCoroutine(GenerateMengerSpongeAnimated(newCenter, newSize, depth - 1));
                }
            }
        }
    }

    bool ShouldSkipPosition(int x, int y, int z)
    {
        // Remove center cube
        if (x == 0 && y == 0 && z == 0)
            return true;

        // Remove face-centered cubes (cubes that are in the center of each face)
        if ((x == 0 && y == 0) || (x == 0 && z == 0) || (y == 0 && z == 0))
            return true;

        return false;
    }

    void CreateCube(Vector3 position, float size)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = Vector3.one * size;
        cube.transform.parent = transform;

        if (cubeMaterial != null)
        {
            cube.GetComponent<Renderer>().material = cubeMaterial;
        }

        if (!enableColliders)
        {
            DestroyImmediate(cube.GetComponent<Collider>());
        }

        cubes.Add(cube);
        totalCubes++;
    }

    void CombineMeshes()
    {
        if (cubes.Count == 0) return;

        MeshFilter[] meshFilters = new MeshFilter[cubes.Count];
        for (int i = 0; i < cubes.Count; i++)
        {
            meshFilters[i] = cubes[i].GetComponent<MeshFilter>();
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        // Create combined mesh object
        GameObject combinedObject = new GameObject("CombinedMengerSponge");
        combinedObject.transform.parent = transform;
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        meshRenderer.material = cubeMaterial;

        // Remove individual cubes
        foreach (GameObject cube in cubes)
        {
            DestroyImmediate(cube);
        }
        cubes.Clear();
    }

    void ClearExisting()
    {
        cubes.Clear();
        totalCubes = 0;

        // Clear all child objects
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [ContextMenu("Regenerate Fractal")]
    public void RegenerateFractal()
    {
        if (Application.isPlaying)
        {
            if (animateGeneration)
            {
                StartCoroutine(AnimatedGeneration());
            }
            else
            {
                GenerateComplete();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw wireframe of the base cube
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * baseSize);
    }
}

// Bonus: Alternative fractal - 3D Sierpinski Pyramid
/*
public class SierpinskiPyramid : MonoBehaviour
{
    [Header("Fractal Parameters")]
    [Range(0, 6)]
    public int iterations = 4;
    public float baseSize = 4f;
    public Material pyramidMaterial;
    
    void Start()
    {
        GenerateTetrahedron(transform.position, baseSize, iterations);
    }
    
    void GenerateTetrahedron(Vector3 center, float size, int depth)
    {
        if (depth == 0)
        {
            CreateTetrahedron(center, size);
            return;
        }
        
        float newSize = size * 0.5f;
        float height = size * 0.866f; // Height of tetrahedron
        
        // Four vertices of tetrahedron
        Vector3[] vertices = {
            center + new Vector3(0, height/2, 0),  // Top
            center + new Vector3(-size/2, -height/2, -size/2),  // Bottom front left
            center + new Vector3(size/2, -height/2, -size/2),   // Bottom front right
            center + new Vector3(0, -height/2, size/2)          // Bottom back
        };
        
        // Generate smaller tetrahedra at each vertex
        foreach (Vector3 vertex in vertices)
        {
            GenerateTetrahedron(vertex, newSize, depth - 1);
        }
    }
    
    void CreateTetrahedron(Vector3 position, float size)
    {
        GameObject tetra = new GameObject("Tetrahedron");
        tetra.transform.position = position;
        tetra.transform.parent = transform;
        
        // Create tetrahedron mesh (simplified as a pyramid)
        MeshFilter meshFilter = tetra.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = tetra.AddComponent<MeshRenderer>();
        
        meshFilter.mesh = CreateTetrahedronMesh(size);
        meshRenderer.material = pyramidMaterial;
    }
    
    Mesh CreateTetrahedronMesh(float size)
    {
        Mesh mesh = new Mesh();
        
        float height = size * 0.866f;
        Vector3[] vertices = {
            new Vector3(0, height/2, 0),
            new Vector3(-size/2, -height/2, -size/2),
            new Vector3(size/2, -height/2, -size/2),
            new Vector3(0, -height/2, size/2)
        };
        
        int[] triangles = {
            0, 1, 2,  // Front face
            0, 2, 3,  // Right face
            0, 3, 1,  // Left face
            1, 3, 2   // Bottom face
        };
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }
}
*/