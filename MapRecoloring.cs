using Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class MapRecoloring
{
    Map<int> regionsMap;
    Dictionary<int, List<int>> regionsOriginalNumbers = new Dictionary<int, List<int>>();
    Tuple<int, HashSet<int>>[] sas = new Tuple<int, HashSet<int>>[10000];
    HashSet<int> regionsNr = new HashSet<int>();

    Stopwatch timer = new Stopwatch();

    private void CreateGraph()
    {
        for (int i = 0; i < regionsMap.Rows; i++)
            for (int i2 = 0; i2 < regionsMap.Cols; i2++)
            {
                var point = new GridPoint(i2, i);
                var region = regionsMap[point];

                foreach (var move in Moves.All4)
                {
                    var newPoint = move.Move(point);
                    if (regionsMap.IsInside(newPoint))
                    {
                        var newRegion = regionsMap[newPoint];
                        if (newRegion != region)
                            sas[region].Item2.Add(newRegion);
                    }
                }
            }
    }

    private int GetColourOverlap(int region, int colour)
    {
        var original = regionsOriginalNumbers[region];
        if (colour >= original.Count)
            return 0;
        return original[colour];
    }

    public int ChooseNewColour(int region, Dictionary<int,int> alreadyColours, int coloursCount, bool randomize = false)
    {
        List<int> availableColours = new List<int>();
        var previousValue = alreadyColours.ContainsKey(region) ? alreadyColours[region] : -1;
        for (int c = 0; c < coloursCount; c++)
        {
            bool ok = true;
            foreach (var s in sas[region].Item2)
            {
                if (alreadyColours.ContainsKey(s) && alreadyColours[s] == c)
                {
                    ok = false;
                    break;
                }
            }
            if (ok && c != previousValue)
                availableColours.Add(c);
        }

        if (availableColours.Any() == false)
        {
            return -1;
        }
        int bestColour = 0;
        if (randomize)
            bestColour = availableColours.Randomize().FirstOrDefault();
        else
            bestColour = availableColours.OrderByDescending(p => GetColourOverlap(region, p)).FirstOrDefault();
        return bestColour;
    }

    public Dictionary<int, int> GreedyColours(List<int> sortedRegions, int coloursCount)
    {
        Dictionary<int, int> colours = new Dictionary<int, int>();
        foreach (var region in sortedRegions)
        {
            List<int> availableColours = new List<int>();
            for (int c = 0; c < coloursCount || availableColours.Count == 0; c++)
            {
                bool ok = true;
                foreach (var s in sas[region].Item2)
                {
                    if (colours.ContainsKey(s) && colours[s] == c)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    coloursCount = Math.Max(coloursCount, c + 1);
                    availableColours.Add(c);
                }
            }

            var bestColour = availableColours.OrderByDescending(p => GetColourOverlap(region, p)).FirstOrDefault();
            if (bestColour == null)
            {
                bestColour = coloursCount++;
            }
            colours[region] = bestColour;
        }
        return colours;
    }

    public IEnumerable<Tuple<int, T>> WithIndex<T>(IEnumerable<T> collection)
    {
        return collection.Select((obj, i) => Tuple.Create(i, obj));
    }

    public Dictionary<int, int> MinimalRecolouring(Dictionary<int, int> groups)
    {
        Dictionary<int,int> res = new Dictionary<int,int>();
        HashSet<int> used = new HashSet<int>();

        Dictionary<int, List<int>> sameCount = new Dictionary<int, List<int>>();
        foreach(var g in groups)
        {
            var coloursCounts = regionsOriginalNumbers[g.Key];

            if (sameCount.ContainsKey(g.Value) == false)
            {
                sameCount[g.Value] = new List<int>(Enumerable.Repeat(0, 20));
            }

            for(int i=0;i<10;i++)
            {
                sameCount[g.Value][i] += coloursCounts[i];
            }
        }
        /*
        var bySimilarity = sameCount.OrderByDescending(p => p.Value.Max());
        foreach(var c in bySimilarity)
        {
            var withIndexes = WithIndex(c.Value);
            var orderedColours = withIndexes.OrderByDescending(p => p.Item2).ToList();
            bool found = false;
            foreach(var oneColour in orderedColours)
            {
                if (used.Contains(oneColour.Item1) == false)
                {
                    found = true;
                    used.Add(oneColour.Item1);
                    res[c.Key] = oneColour.Item1;
                    break;
                }
            }

            if (!found)
                return null;
        }
        */
        return res;
    }


    public void RunForTime(Func<RunInfo, bool> go, double timeAvailable)
    {
        Stopwatch timer = new Stopwatch();
        int iterations = 0;
        double perIteration = 0;
        double timeLeft = timeAvailable;
        while (1.5 * perIteration < timeLeft)
        {
            timer.Start();
            var stop = go(new RunInfo(iterations, timeLeft)); // maximal time to take (should take very small part of it)
            timer.Stop();

            iterations++;
            timeLeft = timeAvailable - timer.ElapsedMilliseconds;
            perIteration = timer.ElapsedMilliseconds / iterations;

            if (stop)
                break;
        }
    }

    public static IEnumerable<T> Randomize<T>(IEnumerable<T> source)
    {
        // TODO is it not very slow??
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public int[] recolor(int H, int[] regions, int[] oldColors)
    {
        timer.Reset();
        timer.Start();

        regionsMap = new Map<int>(H, regions.Length / H);
        HashSet<int> regionsNr = new HashSet<int>();
        for (int i = 0; i < H; i++)
            for (int i2 = 0; i2 < regionsMap.Cols; i2++)
            {
                var nr = i * regionsMap.Cols + i2;
                var rId = regions[nr];
                regionsNr.Add(rId);
                regionsMap[i][i2] = rId;
                sas[rId] = new Tuple<int, HashSet<int>>(rId, new HashSet<int>());

                if (!regionsOriginalNumbers.ContainsKey(rId))
                {
                    regionsOriginalNumbers[rId] = new List<int>(Enumerable.Repeat(0, 20));
                }
                regionsOriginalNumbers[rId][oldColors[nr]]++;
            }

        CreateGraph();

        int regionsCount = regionsNr.Max() + 1;
        var orderedRegions = sas.Take(regionsCount).OrderByDescending(p => p.Item2.Count).Select(p => p.Item1).ToList();

        //var orderedRegions = sas.Take(regionsCount).OrderByDescending(p => (double)regionsOriginalNumbers[p.Item1].Max() / regionsOriginalNumbers[p.Item1].Sum()).Select(p => p.Item1).ToList();


        var greedMin = GreedyColours(orderedRegions, 0);
        var countMin = greedMin.Values.Distinct().Count();
        var recolourMax = regions.Length - greedMin.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();

        var greedMax = GreedyColours(orderedRegions, 40);
        var countMax = greedMax.Values.Distinct().Count();
        var recolourMin = regions.Length - greedMax.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();

        Console.Error.WriteLine(countMin);
        Console.Error.WriteLine(countMax);

        double bestRes = -1;
        int countColours = 0;
        Dictionary<int, int> res = new Dictionary<int,int>();
        
        for (int cnt = 0; cnt < 20; cnt++)
        {
            var regionsList = orderedRegions;
            var newGreed = GreedyColours(regionsList, cnt);
            var newCountGreed = newGreed.Values.Distinct().Count();
            var newRecolour = regions.Length - newGreed.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();

            var newQuality = (double)countMin / newCountGreed * recolourMin / newRecolour;
            if (newQuality > bestRes)
            {
                bestRes = newQuality;
                res = newGreed;
                countColours = newCountGreed;
                Console.Error.WriteLine(newQuality);
            }
        }

        var initialRecolour = regions.Length - res.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();
        Random random = new Random(123);
        //for (int cnt = 0; cnt < 10000; cnt++)
        //{
        //    var chosenRegion = random.Next(0, regionsCount);
        //    var oldColour = res[chosenRegion];
        //    var newColour = ChooseNewColour(chosenRegion, res, countColours);
        //    if (oldColour != newColour)
        //    {
        //        res[chosenRegion] = newColour;
        //        var newRecolour = regions.Length - res.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();
        //        Console.Error.WriteLine("poprawka: " + newRecolour.ToString());
        //    }
        //}

        timer.Stop();
        var leftTime = 18 * 1000 - timer.ElapsedMilliseconds;
        countColours += 1;
        RunForTime((r) =>
            {
                var chosenRegion = random.Next(0, regionsCount);
                var tempRes = new Dictionary<int, int>(res);

                var newRecolour = regions.Length - res.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();

                var area = sas[chosenRegion].Item2.ToList();
                area.Add(chosenRegion);

                var takeRandom = area.Count;// random.Next(0, area.Count + 1);
                area = area.Take(takeRandom).ToList();
                foreach (var a in area)
                    tempRes.Remove(a);

                bool ok = true;
                var tmpCountColours = countColours;
                if (r.Seed % 10 == 0)
                {
                    //tmpCountColours++;
                }
                else if (r.Seed % 10 == 5)
                {
                    //tmpCountColours--;

                }
                foreach (var newGuy in Randomize(area))
                {
                    var newColour = ChooseNewColour(newGuy, tempRes, tmpCountColours, true);
                    if (newColour < 0)
                    {
                        ok = false;
                        //countColours++;
                        break;
                    }
                    tempRes[newGuy] = newColour;
                }

                var betterRecolour = regions.Length - tempRes.Select(p => GetColourOverlap(p.Key, p.Value)).Sum();
                if (ok && betterRecolour < newRecolour)
                {
                    newRecolour = betterRecolour;
                    res = tempRes;
                    countColours = tmpCountColours;
                    Console.Error.WriteLine("poprawka: " + newRecolour.ToString());
                }

                return false;

            }, leftTime);

        Console.Error.WriteLine("start: " + initialRecolour.ToString());

        //for (int i = 0; i < 10; i++)
        //{
        //    greedRev = GreedyColours(RandomExtensions.Randomize(sas.Take(regionsCount),i).Select(p => p.Item1).ToList());
        //    countRev = greedRev.Values.Distinct().Count();

        //    Console.Error.WriteLine(countRev);
        //    if (count > countRev)
        //    {
        //        count = countRev;
        //        greed = greedRev;
        //    }
        //}

        //var mapping = MinimalRecolouring(greed);


        int[] ret = new int[regionsCount];
        for (int i = 0; i < regionsCount; i++)
        {
            ret[i] = res[i];
        }
        return ret;
    }
}

