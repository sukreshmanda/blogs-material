// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ZeroCopy;

//Create larger files
/*
cat testfile_100mb.bin testfile_100mb.bin > testfile_200mb.bin
cat testfile_200mb.bin testfile_200mb.bin testfile_100mb.bin > testfile_500mb.bin
 */
BenchmarkRunner.Run<BenchMarks>();