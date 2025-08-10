using System;
using System.Collections;
using System.Threading.Tasks;
using AStar.Algolgorithms;
using UnityEngine;


public class EnemyPathfinding : MonoBehaviour
{
    [Header("Pathfinding Settings")]
    public Transform target; // Hedef nesne
    public float moveSpeed = 5f; // Hareket hızı
    public float pathUpdateInterval = 0.5f; // Path güncelleme sıklığı (saniye)
    public float reachDistance = 0.1f; // Hedefe ulaşma mesafesi
    
    [Header("Map Settings")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    public Vector3 mapOffset = Vector3.zero; // Harita offset'i (negatif koordinatlar için)
    
    private (int, int)[] currentPath; // Mevcut path
    private int currentPathIndex = 0; // Path'te hangi noktadayız
    private bool isMoving = false;
    private Vector3 targetPosition;
    private bool[,] walkableMap;

    void Start()
    {
        // Initialize the walkable map
        InitializeWalkableMap();
        
        // Target kontrolü
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned! Please assign a target in the inspector.");
            return;
        }
        
        // İlk path hesaplaması
        UpdatePath();
        
        // Periyodik path güncelleme başlat
        InvokeRepeating(nameof(UpdatePath), pathUpdateInterval, pathUpdateInterval);
    }
    
    void Update()
    {
        if (target != null && currentPath != null && currentPath.Length > 0 && !isMoving)
        {
            MoveAlongPath();
        }
    }
    
    private void InitializeWalkableMap()
    {
        walkableMap = new bool[mapHeight, mapWidth];
        
        // Tüm hücreleri yürünebilir yap
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                walkableMap[y, x] = true;
            }
        }
        
        // İsteğe bağlı engeller ekleyebilirsiniz
        // walkableMap[25, 25] = false; // Örnek engel
    }
    
    private void UpdatePath()
    {
        if (target == null) return;
        
        // Mevcut pozisyon ve hedef pozisyonu grid koordinatlarına çevir
        Vector3 currentPos = transform.position - mapOffset;
        Vector3 targetPos = target.position - mapOffset;
        
        int startX = Mathf.RoundToInt(currentPos.x);
        int startY = Mathf.RoundToInt(currentPos.z); // Z ekseni Y grid koordinatı olarak kullanılıyor
        int goalX = Mathf.RoundToInt(targetPos.x);
        int goalY = Mathf.RoundToInt(targetPos.z);
        
        // Grid sınırları kontrolü
        startX = Mathf.Clamp(startX, 0, mapWidth - 1);
        startY = Mathf.Clamp(startY, 0, mapHeight - 1);
        goalX = Mathf.Clamp(goalX, 0, mapWidth - 1);
        goalY = Mathf.Clamp(goalY, 0, mapHeight - 1);
        
        Debug.Log($"UpdatePath - Start: ({startX}, {startY}) Goal: ({goalX}, {goalY})");
        Debug.Log($"World positions - Start: {transform.position} Goal: {target.position}");
        Debug.Log($"Adjusted positions - Start: {currentPos} Goal: {targetPos}");
        
        // Pathfinding'i ana thread'de yap (basit haritalar için yeterli)
        var path = GenerateEnemyPath(startX, startY, goalX, goalY, walkableMap, false);
        
        if (path.Length > 0)
        {
            currentPath = path;
            currentPathIndex = 0;
            Debug.Log($"New path found with {path.Length} points");
            
            // Path noktalarını logla
            for (int i = 0; i < Mathf.Min(path.Length, 5); i++)
            {
                Vector3 worldPoint = new Vector3(path[i].Item1, transform.position.y, path[i].Item2) + mapOffset;
                Debug.Log($"Path point {i}: Grid({path[i].Item1}, {path[i].Item2}) -> World{worldPoint}");
            }
        }
        else
        {
            Debug.Log("No path found to target!");
            currentPath = null;
        }
    }
    
    private void MoveAlongPath()
    {
        if (currentPath == null || currentPathIndex >= currentPath.Length) return;
        
        // Mevcut hedef noktayı al
        var pathPoint = currentPath[currentPathIndex];
        Vector3 worldTarget = new Vector3(pathPoint.Item1, transform.position.y, pathPoint.Item2) + mapOffset;
        
        Debug.Log($"Moving to path point {currentPathIndex}: Grid({pathPoint.Item1}, {pathPoint.Item2}) -> World{worldTarget}");
        Debug.Log($"Current position: {transform.position}, Distance to target: {Vector3.Distance(transform.position, worldTarget)}");
        
        // Hedefe ulaştıysak sonraki noktaya geç
        if (Vector3.Distance(transform.position, worldTarget) < reachDistance)
        {
            currentPathIndex++;
            Debug.Log($"Reached point {currentPathIndex - 1}, moving to next point");
            
            if (currentPathIndex >= currentPath.Length)
            {
                Debug.Log("Reached end of path!");
                return;
            }
            pathPoint = currentPath[currentPathIndex];
            worldTarget = new Vector3(pathPoint.Item1, transform.position.y, pathPoint.Item2) + mapOffset;
        }
        
        // Hedefe doğru hareket et
        StartCoroutine(MoveToPosition(worldTarget));
    }
    
    private IEnumerator MoveToPosition(Vector3 target)
    {
        if (isMoving) yield break;
        
        isMoving = true;
        
        while (Vector3.Distance(transform.position, target) > reachDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        isMoving = false;
    }
    
    // Path'i görselleştir (Scene görünümünde)
    private void OnDrawGizmos()
    {
        if (currentPath == null || currentPath.Length <= 1) return;
        
        // Path çizgilerini çiz
        Gizmos.color = Color.green;
        for (int i = 0; i < currentPath.Length - 1; i++)
        {
            Vector3 start = new Vector3(currentPath[i].Item1, transform.position.y, currentPath[i].Item2) + mapOffset;
            Vector3 end = new Vector3(currentPath[i + 1].Item1, transform.position.y, currentPath[i + 1].Item2) + mapOffset;
            Gizmos.DrawLine(start, end);
        }
        
        // Path noktalarını çiz
        Gizmos.color = Color.yellow;
        for (int i = 0; i < currentPath.Length; i++)
        {
            Vector3 point = new Vector3(currentPath[i].Item1, transform.position.y, currentPath[i].Item2) + mapOffset;
            Gizmos.DrawSphere(point, 0.2f);
        }
        
        // Mevcut hedef noktayı vurgula
        if (currentPathIndex < currentPath.Length)
        {
            Gizmos.color = Color.red;
            Vector3 currentTarget = new Vector3(currentPath[currentPathIndex].Item1, transform.position.y, currentPath[currentPathIndex].Item2) + mapOffset;
            Gizmos.DrawSphere(currentTarget, 0.3f);
        }
        
        // Grid sınırlarını çiz
        Gizmos.color = Color.blue;
        Vector3 gridCenter = new Vector3(mapWidth * 0.5f - 0.5f, transform.position.y, mapHeight * 0.5f - 0.5f) + mapOffset;
        Vector3 gridSize = new Vector3(mapWidth, 0.1f, mapHeight);
        Gizmos.DrawWireCube(gridCenter, gridSize);
    }
    
    // Target değiştiğinde manuel path güncelleme
    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
        UpdatePath();
    }
    
    // Hareket durumunu durdur/başlat
    public void StopMovement()
    {
        CancelInvoke(nameof(UpdatePath));
        isMoving = false;
        StopAllCoroutines();
    }
    
    public void ResumeMovement()
    {
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateInterval);
    }


    public static (int, int)[] GenerateEnemyPath(int startX, int startY, int targetX, int targetY, bool[,] walkableMap, bool manhattanHeuristic = true)
    {
        Debug.Log($"GenerateEnemyPath called: Start({startX}, {startY}) -> Target({targetX}, {targetY})");
        Debug.Log($"Map dimensions: {walkableMap.GetLength(1)}x{walkableMap.GetLength(0)}");
        
        // walkableDiagonals'ı true yapalım, çünkü mevcut algoritmanın diagonal kontrolünde sorun var
        var result = AStarBoolMap.GeneratePath(
            startX, startY,
            targetX, targetY,
            walkableMap,
            manhattanHeuristic,
            true // diagonal hareket izin ver
        );
        
        Debug.Log($"Generated path length: {result.Length}");
        return result;
    }


}
