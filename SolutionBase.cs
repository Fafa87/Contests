using Deadline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

public class SolutionBase
{
    /// <summary>
    /// In miliseconds.
    /// </summary>
    protected double timeRemaining;
    public static Random random = new Random();
    Stopwatch timer = new Stopwatch();
    
    IClient client;
    protected IOClient ioClient { get { return client as IOClient; } }
    protected GameState state;
    protected Result best;

    public const long INF = long.MaxValue / 10;

    public SolutionBase(IClient client, double time = 0)
    {
        this.client = client;
        this.timeRemaining = time;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
    }

    public virtual void GetData()
    {
        this.state = new GameState();
        client.LearnState(state);
    }

    protected virtual bool TakeBestAction()
    {
        return client.TakeAction(best);
    }

    public virtual bool Act()
    {
        // solve problem described in this.state
        // solution present as Result object
        // then send results (PrintBest) 
        //
        // or talk in a more complicated way to tcp server
        throw new NotImplementedException();
    }

    public void RunIterations(Action<RunInfo> go, double timeAvailable, int iterations)
    {
        Stopwatch timer = new Stopwatch();
        double perIteration = 0;
        double timeLeft = timeAvailable;
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            timer.Start();
            go(new RunInfo(iteration, timeLeft / (iterations - iteration)));
            timer.Stop();
            timeLeft = timeAvailable - timer.ElapsedMilliseconds;
            perIteration = timer.ElapsedMilliseconds / (iteration + 1);

            if (1.1 * perIteration >= timeLeft)
                break;
        }
    }

    public void RunForTime(Func<RunInfo,bool> go, double timeAvailable)
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

    public void MeasureTime(string name, Action<int> go)
    {
        timer.Reset();
        timer.Start();
        go(0);
        timer.Stop();
        Console.Error.WriteLine(name + ": took {0} ms", timer.ElapsedMilliseconds);
    }
}

public struct RunInfo
{
    public readonly int seed;
    public readonly double timeLeft;
    public RunInfo(int s, double t)
    {
        seed = s;
        timeLeft = t;
    }
}