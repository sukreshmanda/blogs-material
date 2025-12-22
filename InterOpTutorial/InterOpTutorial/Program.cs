// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using InterOpTutorial.Basic;
using InterOpTutorial.Improved;
using InterOpTutorial.Native;

BenchmarkRunner.Run<BasicBenchmarks>();
BenchmarkRunner.Run<ImprovedBenchmark>();
BenchmarkRunner.Run<NativeBenchmark>();