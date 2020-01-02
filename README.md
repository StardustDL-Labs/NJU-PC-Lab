# Parallel Sorting

This project contains the serial and parallel implements of several sort algorithms like Quicksort, Enumsort and Mergesort, and it use benchmark tests for these implements to compare the running time of them.

- This is the source codes of my programming assignment of PC2019 (Parallel computing - structures, algorithms, programming) courses.
- Each push (or pull request) will be tested and benchmarked. The reports will be uploaded to CI artifacts.

## Guide

The interface `ISorter` defines the main API:

```csharp
public interface ISorter
{
    Task<Memory<int>> Sort(in ReadOnlyMemory<int> seq);
}
```

- Each element of the argument `seq` should be distinct.
- The return array of the method is in ascending order.

Namespaces:

- `Systems`: Sorter built by the framework library.
- `Serials`: Serial sorting algorithms.
- `Parallels`: Parallel sorting algorithms.

## Test

Use the following command to test the correctness of the algorithms.

```sh
dotnet test
```

## Benchmark

The project `Benchmark.Base` create dynamic inputs, and run many times to get average running time for each algorithm.

1. Use the following command to run it.

```sh
dotnet run --project ./test/Benchmark.Base -c Release
```

2. The results and logs will also be saved at directory `BenchmarkDotNet.Artifacts`.

### Custom Input

Use the project `Benchmark.FromFile`.

1. Fill input to the file `assets/random.txt`. The current `random.txt` is generated randomly.
   - Each element from the input data should be distinct.
2. Run the project. The time will print to terminal, and the sorting result will write to `assets/output*.txt`.
```sh
cd ./test/Benchmark.FromFile
dotnet run
```

## Depedencies

- [.NET Core 3.1](https://dotnet.microsoft.com/)
- [BenchmarkDotnet](https://github.com/dotnet/BenchmarkDotNet)
