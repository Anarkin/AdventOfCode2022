using System.Collections.ObjectModel;

namespace AdventOfCode2022
{
    public class Day2 // aka how it would've been done in Java EE
    {
        enum Shape { Rock, Paper, Scissors };

        enum Outcome { Lost, Draw, Won };

        readonly Dictionary<Shape, int> shapeScoreLookup = new()
        {
            { Shape.Rock, 1 },
            { Shape.Paper, 2 },
            { Shape.Scissors, 3 }
        };

        readonly Dictionary<Outcome, int> outcomeScoreLookup = new()
        {
            { Outcome.Lost, 0 },
            { Outcome.Draw, 3 },
            { Outcome.Won, 6 }
        };

        readonly Dictionary<string, Shape> leftCommandLookup = new()
        {
            { "A", Shape.Rock },
            { "B", Shape.Paper },
            { "C", Shape.Scissors }
        };

        readonly Dictionary<Shape, Dictionary<Shape, Outcome>> outcomeLookup = new()
        {
            { Shape.Rock, new Dictionary<Shape, Outcome>()
                                {
                                    { Shape.Rock, Outcome.Draw },
                                    { Shape.Paper, Outcome.Lost },
                                    { Shape.Scissors, Outcome.Won }
                                }
            },
            { Shape.Paper, new Dictionary<Shape, Outcome>()
                                {
                                    { Shape.Rock, Outcome.Won },
                                    { Shape.Paper, Outcome.Draw },
                                    { Shape.Scissors, Outcome.Lost }
                                }
            },
            { Shape.Scissors, new Dictionary<Shape, Outcome>()
                                {
                                    { Shape.Rock, Outcome.Lost },
                                    { Shape.Paper, Outcome.Won },
                                    { Shape.Scissors, Outcome.Draw }
                                }
            }
        };

        readonly Dictionary<Shape, Dictionary<Outcome, Shape>> beatsLookup = new()
        {
            { Shape.Rock, new Dictionary<Outcome, Shape>()
                                {
                                    { Outcome.Draw, Shape.Rock },
                                    { Outcome.Won, Shape.Paper },
                                    { Outcome.Lost, Shape.Scissors }
                                }
            },
            { Shape.Paper, new Dictionary<Outcome, Shape>()
                                {
                                    { Outcome.Draw, Shape.Paper },
                                    { Outcome.Won, Shape.Scissors },
                                    { Outcome.Lost, Shape.Rock }
                                }
            },
            { Shape.Scissors, new Dictionary<Outcome, Shape>()
                                {
                                    { Outcome.Draw, Shape.Scissors },
                                    { Outcome.Won, Shape.Rock },
                                    { Outcome.Lost, Shape.Paper }
                                }
            },
        };

        public void Part1()
        {
            var rightCommandLookup = new Dictionary<string, Shape>()
            {
                { "X", Shape.Rock },
                { "Y", Shape.Paper },
                { "Z", Shape.Scissors }
            };

            var score =
                input
                    .Split("\r\n")
                    .Select(pair =>
                    {
                        if (!(pair.Split(" ") is [var leftCommand, var rightCommand])) throw new Exception();
                        var theirShape = leftCommandLookup[leftCommand];
                        var myShape = rightCommandLookup[rightCommand];
                        var outcome = outcomeLookup/*of*/[myShape]/*versus*/[theirShape]; // :D
                        var score = shapeScoreLookup[myShape] + outcomeScoreLookup[outcome];
                        return score;
                    })
                    .Sum();

            return;
        }

        public void Part2()
        {
            var rightCommandLookup = new Dictionary<string, Outcome>()
            {
                { "X", Outcome.Lost },
                { "Y", Outcome.Draw },
                { "Z", Outcome.Won }
            };

            var score =
                input
                    .Split("\r\n")
                    .Select(pair =>
                    {
                        if (!(pair.Split(" ") is [var leftCommand, var rightCommand])) throw new Exception();
                        var theirShape = leftCommandLookup[leftCommand];
                        var desiredOutcome = rightCommandLookup[rightCommand];
                        var shapeThatIPick = beatsLookup[theirShape][desiredOutcome];
                        var score = shapeScoreLookup[shapeThatIPick] + outcomeScoreLookup[desiredOutcome];
                        return score;
                    })
                    .Sum();

            return;
        }

        const string input = "B Z\r\nB X\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nA X\r\nB Z\r\nA Z\r\nB Z\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nA Z\r\nB Y\r\nA X\r\nB X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nB X\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nB Z\r\nC Y\r\nB Y\r\nB Z\r\nB X\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nA Z\r\nB X\r\nC X\r\nB Y\r\nB X\r\nC X\r\nA X\r\nB Y\r\nC Y\r\nB X\r\nC X\r\nC Y\r\nB X\r\nB Z\r\nB Y\r\nB X\r\nC Y\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nA Z\r\nB Z\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nC Z\r\nC Z\r\nC X\r\nB X\r\nC Z\r\nB Y\r\nB Y\r\nC X\r\nC Z\r\nC X\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nC X\r\nC X\r\nC Z\r\nA X\r\nB Y\r\nC X\r\nB Z\r\nB Y\r\nC X\r\nB X\r\nB Z\r\nA Z\r\nC Y\r\nB Z\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nB X\r\nB Y\r\nA Z\r\nA Z\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nB X\r\nA Z\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nB X\r\nB Y\r\nB Z\r\nA Z\r\nB X\r\nB Y\r\nC Z\r\nC Z\r\nB Z\r\nB Y\r\nB X\r\nA Y\r\nC Z\r\nC X\r\nA Z\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Y\r\nC Z\r\nC X\r\nA Z\r\nB X\r\nB Y\r\nA Y\r\nA Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nA Z\r\nC Y\r\nB X\r\nC X\r\nA X\r\nC X\r\nB Y\r\nB X\r\nA Z\r\nC X\r\nC Y\r\nA Z\r\nB Y\r\nA Z\r\nB X\r\nB X\r\nA Z\r\nB Y\r\nC X\r\nC X\r\nA Z\r\nA X\r\nC X\r\nC X\r\nC X\r\nB Y\r\nC Z\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nC Y\r\nC Z\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nA Z\r\nB X\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nA X\r\nA Z\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB X\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nB X\r\nB Z\r\nC Y\r\nB Z\r\nC X\r\nC X\r\nB Z\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nC X\r\nA X\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nA Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nA Z\r\nB Z\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nA X\r\nB Y\r\nB X\r\nC X\r\nC Z\r\nC Z\r\nC Y\r\nC Y\r\nA X\r\nC Y\r\nB Y\r\nA X\r\nC X\r\nA Z\r\nC X\r\nB Y\r\nC Z\r\nC Z\r\nA Z\r\nA X\r\nC Y\r\nC X\r\nB Y\r\nC Z\r\nB Y\r\nC Y\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nA Z\r\nB Y\r\nB X\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nA X\r\nA X\r\nA Z\r\nA Z\r\nB X\r\nC Y\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nC X\r\nC Y\r\nA Z\r\nC Y\r\nC Y\r\nC Y\r\nC Y\r\nC X\r\nA Z\r\nC Z\r\nA Z\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nB X\r\nB Z\r\nC X\r\nA Y\r\nC Y\r\nB X\r\nB X\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nB X\r\nB Z\r\nB X\r\nC Y\r\nA X\r\nA X\r\nC Z\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nB Z\r\nC Z\r\nC X\r\nB Z\r\nC Z\r\nB Z\r\nA X\r\nC Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nB X\r\nC X\r\nC Y\r\nC Y\r\nB Y\r\nC X\r\nC X\r\nA Y\r\nC Y\r\nC Y\r\nA Z\r\nC Z\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Y\r\nB Z\r\nB Y\r\nC X\r\nB Y\r\nC X\r\nA Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nC Y\r\nB Z\r\nB Z\r\nA Z\r\nB Y\r\nB Y\r\nC X\r\nA X\r\nB Y\r\nA Z\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nB Z\r\nB Y\r\nA Z\r\nC X\r\nC X\r\nC Y\r\nA Z\r\nC Y\r\nB Y\r\nB X\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nA Z\r\nA X\r\nA Z\r\nC Z\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nC X\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nA X\r\nC X\r\nC Y\r\nC Y\r\nB X\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nC X\r\nA Z\r\nC Z\r\nC Y\r\nA Z\r\nC X\r\nC Y\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nA Z\r\nB Y\r\nA X\r\nB Y\r\nA X\r\nA X\r\nC Y\r\nC X\r\nA Z\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB X\r\nA X\r\nC X\r\nB Y\r\nC Y\r\nA Z\r\nA Z\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nB Y\r\nC X\r\nC Y\r\nA Z\r\nB Y\r\nB Z\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nA Z\r\nA X\r\nC Z\r\nC Z\r\nB Z\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nA Z\r\nC X\r\nB X\r\nB Y\r\nA X\r\nA X\r\nB Y\r\nA Z\r\nA X\r\nC Y\r\nB Y\r\nC X\r\nA Z\r\nB Y\r\nC Z\r\nC Y\r\nB Y\r\nA X\r\nC Y\r\nB Y\r\nB Z\r\nA Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nC Z\r\nC X\r\nB Y\r\nA Y\r\nC Z\r\nB X\r\nA X\r\nB Y\r\nB Y\r\nB Z\r\nB Z\r\nC Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nB X\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Z\r\nB Z\r\nB Y\r\nC Y\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nC X\r\nC Z\r\nB Y\r\nC X\r\nB X\r\nA Z\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nA X\r\nB Z\r\nA Z\r\nC Z\r\nB X\r\nA Z\r\nC Y\r\nC Y\r\nA Z\r\nB Y\r\nC Z\r\nC Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nC X\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nB X\r\nC Y\r\nB Y\r\nB X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nC X\r\nA Z\r\nB Y\r\nC X\r\nA Z\r\nC Z\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nB X\r\nC Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nA Z\r\nB Y\r\nC X\r\nC X\r\nB X\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nC Z\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nC Y\r\nA Z\r\nA Z\r\nC Y\r\nC Y\r\nA X\r\nB Y\r\nC Y\r\nA X\r\nC X\r\nC Y\r\nA Z\r\nB Y\r\nB Z\r\nA X\r\nB Y\r\nB Z\r\nB Y\r\nB Y\r\nB Z\r\nC X\r\nC Y\r\nB Y\r\nB Z\r\nB X\r\nA Z\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nC Y\r\nA X\r\nA X\r\nA Z\r\nC Y\r\nB Y\r\nC Y\r\nC X\r\nB X\r\nC X\r\nC X\r\nA X\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nC Y\r\nB Z\r\nC Z\r\nB Y\r\nA Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nA Y\r\nC Z\r\nB Y\r\nC X\r\nC Y\r\nC Y\r\nC Y\r\nC X\r\nC Y\r\nB Z\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nB X\r\nC Z\r\nB Y\r\nC X\r\nB Z\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nC Y\r\nC Y\r\nA Z\r\nC Y\r\nC X\r\nB Y\r\nC X\r\nB X\r\nA Z\r\nB Y\r\nA Z\r\nA Z\r\nA Z\r\nC Y\r\nB X\r\nA Y\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nC X\r\nB X\r\nA X\r\nA Z\r\nC X\r\nA Y\r\nB X\r\nC Z\r\nB Y\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nC Y\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nC Z\r\nA Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nB Y\r\nC Z\r\nB Y\r\nC Z\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nC Z\r\nC Y\r\nA Z\r\nC X\r\nB X\r\nB Y\r\nC Y\r\nB Z\r\nC Z\r\nC X\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nB Z\r\nA X\r\nB Y\r\nA Z\r\nC Z\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nB X\r\nA Z\r\nB Y\r\nC Y\r\nA X\r\nC Z\r\nB Y\r\nB X\r\nB Y\r\nC Y\r\nC Z\r\nB Z\r\nA Z\r\nB Y\r\nA X\r\nC X\r\nB Y\r\nA Z\r\nC X\r\nB Z\r\nC Y\r\nC Y\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nB Z\r\nB X\r\nC X\r\nB Z\r\nC X\r\nB X\r\nC Z\r\nC Z\r\nC X\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nA Z\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nB Y\r\nA X\r\nC Z\r\nC X\r\nB Y\r\nC X\r\nC Z\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nA Z\r\nB Z\r\nC Z\r\nC X\r\nB Z\r\nB Z\r\nB Y\r\nA Y\r\nC Y\r\nC Y\r\nB X\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Z\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nA X\r\nC Y\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nA Z\r\nA X\r\nC Y\r\nC Z\r\nB Y\r\nC X\r\nB Y\r\nB X\r\nA Z\r\nB X\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nA Z\r\nA X\r\nB Y\r\nA Z\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nC Y\r\nA Y\r\nC X\r\nC Y\r\nB Y\r\nA Y\r\nA Z\r\nC Z\r\nA Y\r\nA Z\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nA Z\r\nB Y\r\nB X\r\nC Z\r\nC Y\r\nA Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nA Z\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Z\r\nA Y\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nC Y\r\nB Z\r\nC Y\r\nC Y\r\nC Z\r\nB Y\r\nC X\r\nB Z\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC X\r\nB Z\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nA Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB X\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nC Z\r\nA Z\r\nA Z\r\nC X\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Z\r\nB Z\r\nA Z\r\nB Z\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nA Z\r\nA X\r\nB Y\r\nC Z\r\nB X\r\nA X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nA X\r\nA X\r\nB Y\r\nB Y\r\nA Y\r\nC Z\r\nA Z\r\nA Z\r\nB Y\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nA X\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nA Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nB Z\r\nC X\r\nA X\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Z\r\nB Z\r\nB Y\r\nB Z\r\nB Y\r\nB Y\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nC Z\r\nB Y\r\nC X\r\nA Y\r\nB Y\r\nB Z\r\nA Z\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nA Z\r\nB Y\r\nB X\r\nB Y\r\nC Y\r\nB Y\r\nB Z\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nC X\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nB X\r\nC Z\r\nA X\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nA X\r\nC X\r\nB X\r\nA Z\r\nC X\r\nB Y\r\nC Y\r\nB X\r\nA Z\r\nC Y\r\nC Y\r\nB Z\r\nC X\r\nB Y\r\nC X\r\nC Z\r\nC Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nA Z\r\nB Z\r\nC Y\r\nC Y\r\nB Y\r\nA X\r\nC Y\r\nB Y\r\nC Z\r\nB Z\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nA X\r\nB Y\r\nC Y\r\nA X\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nA Y\r\nC X\r\nA Z\r\nA Z\r\nC Y\r\nB Y\r\nC Z\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nA X\r\nC Z\r\nB Y\r\nC X\r\nA Z\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nC Y\r\nC X\r\nC Y\r\nA Y\r\nC X\r\nA Z\r\nA Z\r\nB Z\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nA Z\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nA Y\r\nC X\r\nB Z\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nB Y\r\nC Z\r\nC Y\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nC X\r\nA Z\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB Z\r\nB Z\r\nC X\r\nC Y\r\nB Y\r\nB Z\r\nB Y\r\nB X\r\nA Z\r\nC X\r\nB Z\r\nA Z\r\nC X\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nA X\r\nB Y\r\nA X\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nB X\r\nB Y\r\nC X\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nC Z\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nA X\r\nC Y\r\nB Y\r\nA X\r\nB Y\r\nC X\r\nA Z\r\nB Y\r\nC X\r\nA Z\r\nB Y\r\nC X\r\nC Y\r\nC X\r\nB Z\r\nC Z\r\nB Y\r\nC Y\r\nC X\r\nC Y\r\nB Z\r\nB X\r\nA X\r\nC Z\r\nB Y\r\nB Y\r\nC Y\r\nB Z\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nA Z\r\nB Z\r\nB Z\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB X\r\nB Y\r\nB Y\r\nB X\r\nC Z\r\nB Y\r\nC Z\r\nC X\r\nB Y\r\nC Z\r\nB Y\r\nC Y\r\nB Z\r\nC Y\r\nC Y\r\nC Z\r\nC Z\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nB X\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB X\r\nB Z\r\nC Z\r\nB X\r\nB Z\r\nB Y\r\nC Y\r\nB Y\r\nB Z\r\nC X\r\nA Z\r\nB X\r\nB Z\r\nC X\r\nC Z\r\nB Y\r\nA Z\r\nC X\r\nC Y\r\nB Z\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nC X\r\nC Z\r\nB Y\r\nB Y\r\nB X\r\nC Z\r\nB X\r\nA Z\r\nC Y\r\nA Z\r\nC X\r\nC Y\r\nB Y\r\nC Z\r\nB Z\r\nC Z\r\nC Y\r\nC X\r\nB Y\r\nC Z\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nB Y\r\nC X\r\nA X\r\nB Z\r\nC X\r\nC Z\r\nB Y\r\nC Y\r\nB Z\r\nC Z\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nB Y\r\nC Z\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nA X\r\nA Z\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nA Z\r\nA Z\r\nC X\r\nC Z\r\nC X\r\nC X\r\nA X\r\nB Y\r\nB Y\r\nC Y\r\nC Z\r\nC Y\r\nB Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nB Z\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nC Y\r\nB Y\r\nA X\r\nC X\r\nA X\r\nC Z\r\nC Y\r\nA Y\r\nB Y\r\nB Y\r\nB Z\r\nC X\r\nC X\r\nB Y\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nA Z\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nB Z\r\nC Y\r\nC X\r\nC Z\r\nC Z\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nA X\r\nA Z\r\nB Y\r\nA Z\r\nC X\r\nB Y\r\nC Y\r\nB Z\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC Z\r\nB X\r\nB Y\r\nA Y\r\nC Y\r\nB Z\r\nB Y\r\nC Y\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nA Y\r\nC Y\r\nA Z\r\nC Y\r\nB X\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nC Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA X\r\nC Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nC Z\r\nC Y\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nC Y\r\nA Z\r\nA X\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nC Z\r\nA Z\r\nC X\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nC Z\r\nC X\r\nC Z\r\nB X\r\nB Y\r\nC Z\r\nB Z\r\nA Z\r\nC Y\r\nB Z\r\nC Y\r\nB Y\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nA X\r\nB Y\r\nB X\r\nB Z\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nA Z\r\nC X\r\nB Y\r\nC Y\r\nC Z\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nC X\r\nC Y\r\nC Z\r\nB X\r\nA Z\r\nA Z\r\nB Y\r\nB Z\r\nC Y\r\nA X\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nB Z\r\nC Y\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nC X\r\nC Z\r\nB X\r\nC X\r\nB Y\r\nC Z\r\nC X\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nA Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nC Z\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nB Z\r\nA X\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nB Y\r\nA Y\r\nB Y\r\nB Z\r\nB Y\r\nB Z\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nC X\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nA Z\r\nC X\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nB Z\r\nA Z\r\nA Z\r\nC X\r\nB X\r\nB X\r\nC Y\r\nC Z\r\nB Y\r\nC X\r\nA X\r\nB Y\r\nC X\r\nC Z\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Z\r\nC X\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nB X\r\nA X\r\nC Y\r\nC Z\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nA X\r\nA X\r\nC X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nB Z\r\nA Z\r\nB Y\r\nB X\r\nC Y\r\nB Z\r\nB Y\r\nA Z\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nA Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Y\r\nC Y\r\nB Y\r\nB X\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nC X\r\nB X\r\nA Y\r\nA Z\r\nC Y\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nC Y\r\nC X\r\nC Y\r\nC Z\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nC Z\r\nC Y\r\nA Z\r\nC Y\r\nB Y\r\nC Y\r\nB X\r\nB Y\r\nA X\r\nC Z\r\nC Z\r\nA X\r\nC X\r\nC Z\r\nC X\r\nC X\r\nB Y\r\nA X\r\nA Z\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nB X\r\nC Y\r\nC Z\r\nB Y\r\nB Z\r\nA Z\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nC X\r\nB Z\r\nC Y\r\nB X\r\nB Y\r\nC Y\r\nB Z\r\nA Z\r\nC X\r\nC Y\r\nC X\r\nC Z\r\nB Z\r\nC Z\r\nB Y\r\nB X\r\nB Y\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nC Y\r\nB Z\r\nC X\r\nB Y\r\nB Y\r\nB Z\r\nC Z\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nB Z\r\nB Y\r\nB X\r\nB Y\r\nC Y\r\nC X\r\nC Z\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nB X\r\nB Y\r\nC X\r\nB X\r\nA X\r\nB Y\r\nC Y\r\nB Y\r\nA X\r\nC Y\r\nB Z\r\nB Z\r\nC X\r\nC Z\r\nC X\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nC Z\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nA Z\r\nB Z\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nB Z\r\nA X\r\nC X\r\nC X\r\nB Y\r\nC X\r\nA Z\r\nC Y\r\nB Y\r\nA Z\r\nC Z\r\nB X\r\nC Y\r\nC X\r\nC X\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nA Y\r\nC Y\r\nB Y\r\nB X\r\nB Z\r\nC X\r\nB Y\r\nB X\r\nC Z\r\nB Y\r\nB X\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nB Z\r\nA Z\r\nC Y\r\nC Z\r\nB X\r\nC X\r\nA Z\r\nC X\r\nB Y\r\nB Y\r\nA X\r\nB X\r\nB Y\r\nB X\r\nB X\r\nA Y\r\nA Z\r\nC X\r\nB Z\r\nB Z\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nA Z\r\nB Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nC Z\r\nC X\r\nC Y\r\nB Y\r\nB X\r\nB Y\r\nA Y\r\nA X\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nB Y\r\nC Y\r\nC Z\r\nC Z\r\nC Y\r\nA Y\r\nB Y\r\nB Y\r\nC Y\r\nB X\r\nC Z\r\nC Z\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nA Z\r\nC X\r\nC Z\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nC X\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nA X\r\nC Z\r\nB Y\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nA Z\r\nB Y\r\nB Y\r\nB Y\r\nA Z\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nC Y\r\nC X\r\nC Y\r\nA Z\r\nB Y\r\nC Y\r\nB Y\r\nC Y\r\nC Y\r\nC Y\r\nA X\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nB Z\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB X\r\nC X\r\nC Y\r\nB Y\r\nA Z\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC Z\r\nC Z\r\nB Y\r\nA Z\r\nB Y\r\nC X\r\nC X\r\nC Y\r\nB X\r\nA X\r\nB X\r\nB Y\r\nC X\r\nC Z\r\nC Y\r\nC Y\r\nB Y\r\nC Z\r\nB Y\r\nB Y\r\nC Z\r\nA X\r\nB Y\r\nC Z\r\nB Z\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nB Y\r\nB X\r\nC Z\r\nA Z\r\nA X\r\nB Y\r\nC X\r\nB Y\r\nB Y\r\nC Z\r\nB Z\r\nC Y\r\nB Y\r\nB X\r\nC X\r\nC Z\r\nB Z\r\nB Z\r\nC Y\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nC X\r\nB X\r\nA Z\r\nB Y\r\nA Z\r\nB Y\r\nB Y\r\nA Z\r\nC X\r\nC X\r\nB Y\r\nB X\r\nB Y\r\nA Z\r\nC Y\r\nC Z\r\nB Y\r\nB Y\r\nA X\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nA Z\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nA X\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nB Y\r\nB Y\r\nB Z\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nC X\r\nC X\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nB Y\r\nB Z\r\nB Y\r\nA Z\r\nA Z\r\nC X\r\nA Z\r\nA Z\r\nB Y\r\nB X\r\nC Z\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nB Y\r\nC X\r\nC Y\r\nB Y\r\nA X\r\nC X\r\nC X\r\nC Y\r\nB Y\r\nC X\r\nC X\r\nC Z\r\nB Z\r\nC Y\r\nC X\r\nB Y\r\nB Y\r\nC X\r\nB X\r\nC Z\r\nB Y\r\nB Y\r\nC Y\r\nA Z\r\nC Y\r\nC X\r\nC Y\r\nB Y\r\nB Y\r\nC X\r\nC Y\r\nC Y\r\nC X\r\nB Z\r\nB Z\r\nB Y\r\nB Y\r\nC Y\r\nB Y\r\nC X\r\nA Z\r\nA X\r\nC Z\r\nB Y\r\nC Y\r\nC Y\r\nB Y\r\nB Y\r\nC Y\r\nB Y";
    }
}
