# Parallel Sorting

This project contains the serial and parallel implements of several sort algorithms like Quicksort, Enumsort and Mergesort, and it use benchmark tests for these implements to compare the running time of them.

- This is the source codes of my programming assignment of PC2019 (Parallel computing - structures, algorithms, programming) courses.

## Benchmark

The project `Benchmark.Base` create dynamic inputs, and run many times to get average running time for each algorithm.

- It add two sorters built by the framework library for baseline.

1. Use the following command to run it.

```sh
dotnet run --project ./test/Benchmark.Base -c Release
```

2. The results and logs will also be saved at directory `BenchmarkDotNet.Artifacts`.

### Custom Input

Use the project `Benchmark.FromFile`.

1. Fill input to the file `assets/random.txt`. The current `random.txt` is generated randomly.
2. Run the project. The time will print to terminal, and the sorting result will write to `assets/outputx.txt`.
```sh
cd ./test/Benchmark.FromFile
dotnet run
```

## Depedencies

- [.NET Core 3.0](https://dotnet.microsoft.com/)
- [BenchmarkDotnet](https://github.com/dotnet/BenchmarkDotNet)
