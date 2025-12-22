```

BenchmarkDotNet v0.13.12, macOS 15.1 (24B83) [Darwin 24.1.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.413
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2


```
| Method                      | Mean        | Error    | StdDev   | Rank | Gen0      | Allocated   |
|---------------------------- |------------:|---------:|---------:|-----:|----------:|------------:|
| Basic_CountByLevel_1_000    |    120.6 μs |  1.37 μs |  1.28 μs |    1 |   26.8555 |   164.84 KB |
| Basic_CountByLevel_10_000   |  1,178.7 μs |  3.54 μs |  2.95 μs |    2 |  267.5781 |  1640.48 KB |
| Basic_CountByLevel_1_00_000 | 12,011.5 μs | 72.32 μs | 64.11 μs |    3 | 2671.8750 | 16401.99 KB |
