using System;

namespace GHC_Practice2017_Pizza
{
    class Program
    {
        const string inputFilePathExample = "inputs/example.in";
        const string inputFilePathSmall = "inputs/small.in";
        const string inputFilePathMedium = "inputs/medium.in";
        const string inputFilePathBig = "inputs/big.in";

        const string outputFilePathExample = "outputs/example.out";
        const string outputFilePathSmall = "outputs/small.out";
        const string outputFilePathMedium = "outputs/medium.out";
        const string outputFilePathBig = "outputs/big.out";

        static void Main(string[] args)
        {
            Solve(inputFilePathExample, outputFilePathExample);
            Solve(inputFilePathSmall, outputFilePathSmall);
            Solve(inputFilePathMedium, outputFilePathMedium);
            Solve(inputFilePathBig, outputFilePathBig);

            Console.ReadKey();
        }

        private static void Solve(string inputFilePathExample, string outputFilePathExample)
        {
            Solver solver = new Solver(inputFilePathExample, outputFilePathExample);
            solver.Solve();
        }
    }
}