using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#endif

// Author: Ruiqi Pi
// Purpose: Evenly scatter trees of random sizes, avoiding rivers and mountains and keeping tree bases above ground.
public class ForestScatter : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject treeTemplate;
    public MeshFilter floor;
    public MeshFilter[] riverAndMountains;

    [Header("Random Placement (1 unit = 1 metre)")]
    public int seed = 20261024;
    [Min(1)] public float cellSize = 30f;
    [Min(0)] public float minimumDistance = 18f;
    [Range(0.1f, 100f)] public float minimumHeight = 2f;
    [Range(0.1f, 100f)] public float maximumHeight = 12f;

#if UNITY_EDITOR
    [SerializeField, HideInInspector] GameObject generatedForest;

    const int AttemptsPerCell = 32;
    const int MaximumCells = 5000;
    const float BankGap = 5f;
    const float CanopyGap = 2f;

    struct TreeSpot
    {
        public Vector2 position;
        public float height, scale, radius, angle;
    }

    struct Triangle
    {
        public Vector3 a, b, c;
        public Vector2 min, max;
        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            this.a = a; this.b = b; this.c = c;
            min = Vector2.Min(XZ(a), Vector2.Min(XZ(b), XZ(c)));
            max = Vector2.Max(XZ(a), Vector2.Max(XZ(b), XZ(c)));
        }
    }

    [ContextMenu("Generate Forest")]
    public void GenerateTrees()
    {
        GameObject newForest = null;
        try
        {
            CheckSettings();
            List<Triangle> ground = ReadTriangles(floor);
            var blocked = new List<Triangle>();
            foreach (MeshFilter mesh in riverAndMountains) blocked.AddRange(ReadTriangles(mesh));
            List<Vector2[]> edges = GetFloorEdges(ground, out Bounds floorBounds);
            Bounds treeBounds = GetTreeBounds(treeTemplate);
            List<TreeSpot> spots = ChoosePositions(ground, blocked, edges, floorBounds, treeBounds);
            if (spots.Count == 0) throw new Exception("No valid positions found. Check the area and spacing.");

            newForest = new GameObject("Generated Forest");
            SceneManager.MoveGameObjectToScene(newForest, gameObject.scene);
            for (int i = 0; i < spots.Count; i++)
            {
                CheckCancel("Creating trees", i, spots.Count);
                TreeSpot spot = spots[i];
                GameObject tree = Instantiate(treeTemplate, newForest.transform);
                tree.name = "Tree " + (i + 1);
                tree.transform.position = new Vector3(spot.position.x, floorBounds.max.y, spot.position.y);
                tree.transform.rotation = Quaternion.AngleAxis(spot.angle, Vector3.up) * treeTemplate.transform.rotation;
                tree.transform.localScale = treeTemplate.transform.lossyScale * spot.scale;
                Bounds actual = GetTreeBounds(tree);
                if (actual.size.y > 100f || Mathf.Abs(actual.size.y - spot.height) > 0.01f)
                    throw new Exception("Tree height does not match the settings. This generation was cancelled.");
                tree.transform.position += Vector3.up * (floorBounds.max.y + 0.01f - actual.min.y);
            }

            Undo.IncrementCurrentGroup();
            int undoGroup = Undo.GetCurrentGroup();
            Undo.SetCurrentGroupName("Generate Forest");
            Undo.RegisterCreatedObjectUndo(newForest, "Generate Forest");
            Undo.RecordObject(this, "Remember Forest");
            generatedForest = newForest;
            newForest = null;
            Undo.FlushUndoRecordObjects();
            Undo.CollapseUndoOperations(undoGroup);
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
            Debug.Log("Generated " + spots.Count + " trees. Press Ctrl+Z to undo. The scene has not been saved.", this);
        }
        catch (Exception error) { Debug.LogWarning(error.Message, this); }
        finally
        {
            if (newForest != null) DestroyImmediate(newForest);
            EditorUtility.ClearProgressBar();
        }
    }

    [ContextMenu("Clear Generated Forest")]
    public void ClearTrees()
    {
        if (Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode || !generatedForest) return;
        Undo.IncrementCurrentGroup();
        int undoGroup = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName("Clear Forest");
        GameObject oldForest = generatedForest;
        Undo.RecordObject(this, "Remember Forest");
        generatedForest = null;
        Undo.FlushUndoRecordObjects();
        Undo.DestroyObjectImmediate(oldForest);
        Undo.CollapseUndoOperations(undoGroup);
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }

    List<TreeSpot> ChoosePositions(List<Triangle> ground, List<Triangle> blocked, List<Vector2[]> edges, Bounds area, Bounds treeBounds)
    {
        int columns = Mathf.CeilToInt(area.size.x / cellSize);
        int rows = Mathf.CeilToInt(area.size.z / cellSize);
        if ((long)columns * rows > MaximumCells) throw new Exception("More than 5000 cells. Increase cellSize.");
        var cells = new List<int>();
        for (int i = 0; i < columns * rows; i++) cells.Add(i);
        var random = new System.Random(seed);
        for (int i = cells.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temporary = cells[i]; cells[i] = cells[j]; cells[j] = temporary;
        }

        var spots = new List<TreeSpot>();
        Vector2 sourcePosition = XZ(treeTemplate.transform.position);
        Vector2 halfSize = XZ(treeBounds.extents);
        Vector2 offset = XZ(treeBounds.center) - sourcePosition;
        float sourceRadius = new Vector2(Mathf.Abs(offset.x) + halfSize.x, Mathf.Abs(offset.y) + halfSize.y).magnitude;
        for (int i = 0; i < cells.Count; i++)
        {
            CheckCancel("Choosing tree positions", i, cells.Count);
            int cell = cells[i];
            float left = area.min.x + cell % columns * cellSize;
            float bottom = area.min.z + cell / columns * cellSize;
            float height = RandomValue(random, minimumHeight, maximumHeight);
            float scale = height / treeBounds.size.y;
            float radius = sourceRadius * scale;
            for (int attempt = 0; attempt < AttemptsPerCell; attempt++)
            {
                Vector2 point = new Vector2(RandomValue(random, left, Mathf.Min(left + cellSize, area.max.x)),
                    RandomValue(random, bottom, Mathf.Min(bottom + cellSize, area.max.z)));
                if (!ground.Exists(t => InsideTriangle(point, t))) continue;
                if (edges.Exists(e => DistanceToEdge(point, e[0], e[1]) <= radius)) continue;
                if (blocked.Exists(t => TouchesTriangle(point, t, radius + BankGap))) continue;
                if (TooClose(point, radius, sourcePosition, sourceRadius)) continue;
                if (spots.Exists(other => TooClose(point, radius, other.position, other.radius))) continue;
                spots.Add(new TreeSpot { position = point, height = height, scale = scale,
                    radius = radius, angle = RandomValue(random, 0f, 360f) });
                break;
            }
        }
        Debug.Log("Found " + spots.Count + " positions; " + (cells.Count - spots.Count) + " cells have no valid position after sampling (river, mountain, edge, or spacing constraints).", this);
        return spots;
    }

    bool TooClose(Vector2 point, float radius, Vector2 other, float otherRadius)
    {
        float distance = Mathf.Max(minimumDistance, radius + otherRadius + CanopyGap);
        return Vector2.Distance(point, other) < distance;
    }

    void CheckSettings()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode) throw new Exception("Generate trees in Edit Mode.");
        if (generatedForest) throw new Exception("Clear the previous forest before generating another one.");
        if (!gameObject.scene.IsValid() || EditorSceneManager.IsPreviewScene(gameObject.scene) || PrefabStageUtility.GetCurrentPrefabStage() != null)
            throw new Exception("Use this script in a regular scene, outside Prefab Mode.");
        if (!treeTemplate || !floor || riverAndMountains == null || riverAndMountains.Length == 0)
            throw new Exception("Assign the tree template, floor mesh, river meshes, and mountain meshes.");
        if (treeTemplate.scene != gameObject.scene || floor.gameObject.scene != gameObject.scene || treeTemplate.transform.parent || !treeTemplate.activeInHierarchy)
            throw new Exception("The template and floor must be in this scene. The template must be active and at the scene root, outside the terrain group.");
        if (treeTemplate.GetComponentsInChildren<MonoBehaviour>(true).Length > 0 || treeTemplate.GetComponentInChildren<Animator>(true) || treeTemplate.GetComponentInChildren<Rigidbody>(true))
            throw new Exception("Use a static tree template without MonoBehaviour scripts, an Animator, or a Rigidbody.");
        Vector3 scale = treeTemplate.transform.lossyScale;
        if (scale.x <= 0 || scale.y <= 0 || scale.z <= 0) throw new Exception("The template scale must be positive on every axis.");
        if (!Finite(cellSize) || !Finite(minimumDistance) || cellSize < 1 || minimumDistance < 0 ||
            !Finite(minimumHeight) || !Finite(maximumHeight) || minimumHeight <= 0 || maximumHeight < minimumHeight || maximumHeight > 100)
            throw new Exception("Use finite spacing and height values: cellSize >= 1, minimumDistance >= 0, and 0 < minimumHeight <= maximumHeight <= 100 metres.");
        foreach (MeshFilter mesh in riverAndMountains)
            if (!mesh || mesh == floor || mesh.gameObject.scene != gameObject.scene) throw new Exception("Exclusion meshes must be in this scene. Do not include empty entries or the floor mesh.");
    }

    static List<Triangle> ReadTriangles(MeshFilter mesh)
    {
        if (!mesh.sharedMesh || !mesh.sharedMesh.isReadable) throw new Exception(mesh.name + " has an unreadable mesh.");
        Vector3[] vertices = mesh.sharedMesh.vertices;
        for (int i = 0; i < vertices.Length; i++) vertices[i] = mesh.transform.TransformPoint(vertices[i]);
        int[] indices = mesh.sharedMesh.triangles;
        if (indices.Length == 0) throw new Exception(mesh.name + " has an empty mesh.");
        var triangles = new List<Triangle>();
        for (int i = 0; i < indices.Length; i += 3)
            triangles.Add(new Triangle(vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]]));
        return triangles;
    }

    static List<Vector2[]> GetFloorEdges(List<Triangle> ground, out Bounds area)
    {
        area = new Bounds(ground[0].a, Vector3.zero);
        foreach (Triangle t in ground) { area.Encapsulate(t.a); area.Encapsulate(t.b); area.Encapsulate(t.c); }
        ground.RemoveAll(t => Vector3.Cross(t.b - t.a, t.c - t.a).y <= 0.00001f);
        if (ground.Count == 0) throw new Exception("The floor mesh has no upward-facing surface.");
        var edges = new List<Vector2[]>();
        foreach (Triangle t in ground)
        {
            if (Mathf.Abs(t.a.y - area.max.y) > 0.005f || Mathf.Abs(t.b.y - area.max.y) > 0.005f || Mathf.Abs(t.c.y - area.max.y) > 0.005f)
                throw new Exception("This version supports a flat, horizontal floor mesh only.");
            AddOuterEdge(edges, XZ(t.a), XZ(t.b));
            AddOuterEdge(edges, XZ(t.b), XZ(t.c));
            AddOuterEdge(edges, XZ(t.c), XZ(t.a));
        }
        if (edges.Count == 0) throw new Exception("Could not identify the floor boundary.");
        return edges;
    }

    static void AddOuterEdge(List<Vector2[]> edges, Vector2 a, Vector2 b)
    {
        int match = edges.FindIndex(e => (e[0].Equals(a) && e[1].Equals(b)) || (e[0].Equals(b) && e[1].Equals(a)));
        if (match >= 0) edges.RemoveAt(match);
        else edges.Add(new[] { a, b });
    }

    static Bounds GetTreeBounds(GameObject tree)
    {
        bool found = false;
        Bounds bounds = default;
        foreach (Renderer renderer in tree.GetComponentsInChildren<Renderer>())
        {
            if (!renderer.enabled || !renderer.gameObject.activeInHierarchy) continue;
            if (!(renderer is MeshRenderer)) throw new Exception("The template must use static MeshRenderer components.");
            if (!found) bounds = renderer.bounds;
            else bounds.Encapsulate(renderer.bounds);
            found = true;
        }
        if (!found || bounds.size.y < 0.001f) throw new Exception("The template has no valid rendered height.");
        return bounds;
    }

    static bool InsideTriangle(Vector2 point, Triangle t)
    {
        Vector2 a = XZ(t.a), b = XZ(t.b), c = XZ(t.c);
        if (Mathf.Abs(Cross(b - a, c - a)) < 0.00001f) return false;
        float ab = Cross(b - a, point - a), bc = Cross(c - b, point - b), ca = Cross(a - c, point - c);
        return (ab >= 0 && bc >= 0 && ca >= 0) || (ab <= 0 && bc <= 0 && ca <= 0);
    }

    static bool TouchesTriangle(Vector2 point, Triangle t, float radius)
    {
        if (point.x < t.min.x - radius || point.x > t.max.x + radius || point.y < t.min.y - radius || point.y > t.max.y + radius) return false;
        return InsideTriangle(point, t) || DistanceToEdge(point, XZ(t.a), XZ(t.b)) <= radius ||
            DistanceToEdge(point, XZ(t.b), XZ(t.c)) <= radius || DistanceToEdge(point, XZ(t.c), XZ(t.a)) <= radius;
    }

    static float DistanceToEdge(Vector2 point, Vector2 a, Vector2 b)
    {
        Vector2 edge = b - a;
        float fraction = edge.sqrMagnitude < 0.000001f ? 0 : Mathf.Clamp01(Vector2.Dot(point - a, edge) / edge.sqrMagnitude);
        return Vector2.Distance(point, a + fraction * edge);
    }

    static void CheckCancel(string message, int current, int total)
    {
        if (current % 32 == 0 && EditorUtility.DisplayCancelableProgressBar("Forest Generation", message, (float)current / total))
            throw new OperationCanceledException("Cancelled. No new trees were kept.");
    }

    static Vector2 XZ(Vector3 point) => new Vector2(point.x, point.z);
    static float Cross(Vector2 a, Vector2 b) => a.x * b.y - a.y * b.x;
    static float RandomValue(System.Random random, float min, float max) => min + (float)random.NextDouble() * (max - min);
    static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);
#endif
}
