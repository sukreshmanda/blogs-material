```

BenchmarkDotNet v0.13.12, macOS 15.1 (24B83) [Darwin 24.1.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.413
  [Host]     : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.19 (8.0.1925.36514), X64 RyuJIT AVX2


```
| Method                         | Mean        | Error     | StdDev    | Rank | Gen0   | Allocated |
|------------------------------- |------------:|----------:|----------:|-----:|-------:|----------:|
| Improved_CountByLevel_1_000    |    39.93 μs |  0.277 μs |  0.259 μs |    1 | 0.0610 |     464 B |
| Improved_CountByLevel_10_000   |   393.78 μs |  0.865 μs |  0.722 μs |    2 |      - |     464 B |
| Improved_CountByLevel_1_00_000 | 3,902.36 μs | 13.405 μs | 11.194 μs |    3 |      - |     470 B |
