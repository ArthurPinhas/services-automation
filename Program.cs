using NUnit.ConsoleRunner;

namespace SeleniumTests
{
    class Program
    {
        static int Main(string[] args)
        {
            // Specify the directory containing the test assembly
            string testDirectory =
                "/Users/arthurpinhas/CodeRepos/services-automation/SeleniumTests/Tests";

            // Specify the test assembly filename
            string testAssembly = "SeleniumTests.dll";

            // Specify the test to run
            string[] modifiedArgs =
            {
                "--test",
                "SeleniumTests.ServiceTests.VerifyPortainerContainers",
                "--where",
                testDirectory,
                "--test-assembly",
                testAssembly
            };

            // Run NUnit tests using NUnit.ConsoleRunner
            return new ConsoleRunner().Execute(modifiedArgs);
        }
    }
}
