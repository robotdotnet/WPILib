using System;
using Moq;
using WPILib.SmartDashboardNS;
using Xunit;

namespace wpilibsharp.test.SmartDashboard
{
    public class SendableRegistryTest
    {


        [Fact]
        public void TestAddAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.Add(sendableMock.Object, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.NotNull(storedName);

            Assert.Equal(name, storedName);
        }

        [Fact]
        public void TestAddWithChannelAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;

            registry.Add(sendableMock.Object, name, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{channel}]", storedName);
        }

        [Fact]
        public void TestAddWithModuleNumAndChannelAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;
            const int moduleNumber = 8;

            registry.Add(sendableMock.Object, name, moduleNumber, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{moduleNumber},{channel}]", storedName);
        }

        [Fact]
        public void TestAddWithSubsystemAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const string subsystem = "subsystem";

            registry.Add(sendableMock.Object, subsystem, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal(name, storedName);

            var storedSubsystem = registry.GetSubsystem(sendableMock.Object);

            Assert.NotNull(storedSubsystem);

            Assert.Equal(subsystem, storedSubsystem);
        }


        [Fact]
        public void TestAddLWAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.AddLW(sendableMock.Object, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.NotNull(storedName);

            Assert.Equal(name, storedName);
        }

        [Fact]
        public void TestAddLWWithChannelAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;

            registry.AddLW(sendableMock.Object, name, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{channel}]", storedName);
        }

        [Fact]
        public void TestAddLWWithModuleNumAndChannelAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;
            const int moduleNumber = 8;

            registry.AddLW(sendableMock.Object, name, moduleNumber, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{moduleNumber},{channel}]", storedName);
        }

        [Fact]
        public void TestAddLWWithSubsystemAddsCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const string subsystem = "subsystem";

            registry.AddLW(sendableMock.Object, subsystem, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal(name, storedName);

            var storedSubsystem = registry.GetSubsystem(sendableMock.Object);

            Assert.NotNull(storedSubsystem);

            Assert.Equal(subsystem, storedSubsystem);
        }


        [Fact]
        public void TestRemovesCorrectly()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.Add(sendableMock.Object, name);

            Assert.True(registry.Contains(sendableMock.Object));

            Assert.True(registry.Remove(sendableMock.Object));

            Assert.False(registry.Contains(sendableMock.Object));

            Assert.Empty(registry.GetName(sendableMock.Object));
        }

        [Fact]
        public void TestSetNameNonExistantDoesNothing()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.SetName(sendableMock.Object, name);

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal(string.Empty, storedName);
        }


        [Fact]
        public void TestSetNameStockWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.Add(sendableMock.Object, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.NotNull(storedName);

            Assert.Equal(name, storedName);

            const string newName = "1234";

            registry.SetName(sendableMock.Object, newName);

            Assert.Equal(newName, registry.GetName(sendableMock.Object));
        }

        [Fact]
        public void TestNameChangeChannelWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;

            registry.Add(sendableMock.Object, name, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{channel}]", storedName);

            const string newName = "1234";
            const int newChannel = 56;

            registry.SetName(sendableMock.Object, newName, newChannel);

            Assert.Equal($"{newName} [{newChannel}]", registry.GetName(sendableMock.Object));
        }

        [Fact]
        public void TestSetNameModuleNumberWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const int channel = 42;
            const int moduleNumber = 8;

            registry.Add(sendableMock.Object, name, moduleNumber, channel);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal($"{name} [{moduleNumber},{channel}]", storedName);

            const string newName = "1234";
            const int newChannel = 18;
            const int newModuleNumer = 56;

            registry.SetName(sendableMock.Object, newName, newModuleNumer, newChannel);

            Assert.Equal($"{newName} [{newModuleNumer},{newChannel}]", registry.GetName(sendableMock.Object));
        }

        [Fact]
        public void TestSetNameSubsystemWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";
            const string subsystem = "subsystem";

            registry.Add(sendableMock.Object, subsystem, name);

            Assert.True(registry.Contains(sendableMock.Object));

            var storedName = registry.GetName(sendableMock.Object);

            Assert.Equal(name, storedName);

            var storedSubsystem = registry.GetSubsystem(sendableMock.Object);

            Assert.NotNull(storedSubsystem);

            Assert.Equal(subsystem, storedSubsystem);

            const string newName = "1234";
            const string newSubsystem = "abc";

            registry.SetName(sendableMock.Object, newSubsystem, newName);

            Assert.Equal($"{newName}", registry.GetName(sendableMock.Object));

            Assert.Equal(newSubsystem, registry.GetSubsystem(sendableMock.Object));
        }

        [Fact]
        public void TestSetSubsystem()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            const string name = "mysendable";

            registry.Add(sendableMock.Object, name);

            Assert.True(registry.Contains(sendableMock.Object));

            Assert.Equal("Ungrouped", registry.GetSubsystem(sendableMock.Object));

            const string subsystem = "subsystem";

            registry.SetSubsystem(sendableMock.Object, subsystem);

            Assert.Equal(subsystem, registry.GetSubsystem(sendableMock.Object));
        }

        [Fact]
        public void TestDataHandleIncrements()
        {
            var registry = new SendableRegistry();

            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(i, registry.DataHandle);
            }
        }

        [Fact]
        public void TestGetDataNonExistant()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            Assert.Null(registry.GetData(sendableMock.Object, 0));
        }

        [Fact]
        public void TestSetDataOnceWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            registry.Add(sendableMock.Object, "mysendable");

            string data = "42";

            Assert.Null(registry.SetData(sendableMock.Object, 0, data));
            Assert.Equal(data, registry.GetData(sendableMock.Object, 0));
        }

        [Fact]
        public void TestSetDataTwiceWorks()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            registry.Add(sendableMock.Object, "mysendable");

            string data = "42";

            Assert.Null(registry.SetData(sendableMock.Object, 0, data));
            Assert.Equal(data, registry.GetData(sendableMock.Object, 0));

            string data2 = "world";

            Assert.Equal(data, registry.SetData(sendableMock.Object, 0, data2));
            Assert.Equal(data2, registry.GetData(sendableMock.Object, 0));
        }

        [Fact]
        public void TestSetDataNegativeIndex()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            registry.Add(sendableMock.Object, "mysendable");

            string data = "42";

            Assert.Throws<ArgumentOutOfRangeException>(() => registry.SetData(sendableMock.Object, -1, data));
        }

        [Fact]
        public void TestGetDataNegativeIndex()
        {
            var registry = new SendableRegistry();

            var sendableMock = new Mock<ISendable>();

            Assert.Throws<ArgumentOutOfRangeException>(() => registry.GetData(sendableMock.Object, -1));
        }
    }
}
