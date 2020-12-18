using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AOC2020.HelperFunctions;

namespace AOC2020
{
    public static class Day17
    {
        public static long Part1(IReadOnlyList<string> data)
        {
            var grid = new Grid3(data);
            
            6.Repeat(grid.Cycle);

            return grid.ActiveCells.Count;
        }

        public static long Part2(IReadOnlyList<string> data)
        {
            var grid = new Grid4(data);

            Log("Before any cycles:\n");
            Log(grid.ToString());

            6.Repeat(grid.Cycle);
            // grid.Cycle();
            // Log("After 1 cycle:\n");
            // Log(grid.ToString());
            // grid.Cycle();
            // Log("After 2 cycle:\n");
            // Log(grid.ToString());

            return grid.ActiveCells.Count;
        }

    }

    public class Grid3
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MaxZ { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MinZ { get; set; }

        public HashSet<Vector3> ActiveCells = new HashSet<Vector3>();
        
        public Grid3(IReadOnlyList<string> data)
        {
            ActiveCells.Clear();
            for (var y = 0; y < data.Count; y++)
            for (var x = 0; x < data[0].Length; x++)
                if (data[y][x] == '#')
                    ActiveCells.Add(new Vector3(x, y, 0));

            UpdateRanges();
        }

        public void Cycle()
        {
            var newCells = new HashSet<Vector3>();

            for (var x = MinX - 1; x <= MaxX + 1; x++)
            for (var y = MinY - 1; y <= MaxY + 1; y++)
            for (var z = MinZ - 1; z <= MaxZ + 1; z++)
            {
                var count = activeNeighbours(x, y, z);

                if (count == 3 || count == 2 && isActive(x, y, z))
                    newCells.Add(new Vector3(x, y, z));
            }

            ActiveCells = newCells;
            UpdateRanges();
        }

        private bool isActive(int x, int y, int z) => 
            ActiveCells.Contains(new Vector3(x, y, z));

        private int activeNeighbours(int x, int y, int z)
        {
            var count = 0;

            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            for (var dz = -1; dz <= 1; dz++)
            {
                if (dx == 0 && dy == 0 && dz == 0) continue;
                if (isActive(x + dx, y + dy, z + dz)) count++;
            }

            return count;
        }

        private void UpdateRanges()
        {
            MaxX = ActiveCells.Max(v => v.X);
            MaxY = ActiveCells.Max(v => v.Y);
            MaxZ = ActiveCells.Max(v => v.Z);
            MinX = ActiveCells.Min(v => v.X);
            MinY = ActiveCells.Min(v => v.Y);
            MinZ = ActiveCells.Min(v => v.Z);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var z = MinZ; z <= MaxZ; z++)
            {
                sb.AppendLine($"z={z}");
                
                for (var y = MinY; y <= MaxY; y++)
                {
                    for (var x = MinX; x <= MaxX; x++)
                        sb.Append(isActive(x, y, z) ? "#" : ".");

                    sb.AppendLine("");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
    public class Grid4
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MaxZ { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MinZ { get; set; }
        public int MinW { get; set; }
        public int MaxW { get; set; }

        public HashSet<Vector4> ActiveCells = new HashSet<Vector4>();
        
        public Grid4(IReadOnlyList<string> data)
        {
            ActiveCells.Clear();
            for (var y = 0; y < data.Count; y++)
            for (var x = 0; x < data[0].Length; x++)
                if (data[y][x] == '#')
                    ActiveCells.Add(new Vector4(x, y, 0, 0));

            UpdateRanges();
        }

        public void Cycle()
        {
            var newCells = new HashSet<Vector4>();

            for (var x = MinX - 1; x <= MaxX + 1; x++)
            for (var y = MinY - 1; y <= MaxY + 1; y++)
            for (var z = MinZ - 1; z <= MaxZ + 1; z++)
            for (var w = MinW - 1; w <= MaxW + 1; w++)
            {
                var count = activeNeighbours(x, y, z, w);

                if (count == 3 || count == 2 && isActive(x, y, z, w))
                    newCells.Add(new Vector4(x, y, z, w));
            }

            ActiveCells = newCells;
            UpdateRanges();
        }

        private bool isActive(int x, int y, int z, int w) => 
            ActiveCells.Contains(new Vector4(x, y, z, w));

        private int activeNeighbours(int x, int y, int z, int w)
        {
            var count = 0;

            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            for (var dz = -1; dz <= 1; dz++)
            for (var dw = -1; dw <= 1; dw++)
            {
                if (dx == 0 && dy == 0 && dz == 0 && dw == 0) continue;
                if (isActive(x + dx, y + dy, z + dz, w + dw)) count++;
            }

            return count;
        }

        private void UpdateRanges()
        {
            MaxX = ActiveCells.Max(v => v.X);
            MaxY = ActiveCells.Max(v => v.Y);
            MaxZ = ActiveCells.Max(v => v.Z);
            MaxW = ActiveCells.Max(v => v.W);
            MinX = ActiveCells.Min(v => v.X);
            MinY = ActiveCells.Min(v => v.Y);
            MinZ = ActiveCells.Min(v => v.Z);
            MinW = ActiveCells.Min(v => v.W);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var w = MinW; w <= MaxW; w++)
            for (var z = MinZ; z <= MaxZ; z++)
            {
                sb.AppendLine($"z={z}, w={w}");

                for (var y = MinY; y <= MaxY; y++)
                {
                    for (var x = MinX; x <= MaxX; x++)
                        sb.Append(isActive(x, y, z, w) ? "#" : ".");

                    sb.AppendLine("");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
