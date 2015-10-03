namespace WPILib.IntegrationTests.Fixtures
{
    interface ITestFixture
    {
        bool Setup();

        bool Reset();

        bool Teardown();
    }
}
