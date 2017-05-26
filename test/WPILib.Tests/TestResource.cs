using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestResource
    {
        private Resource m_testResource;
        private const int ResourceMaxSize = 5;

        [SetUp]
        public void Setup()
        {
            m_testResource = new Resource(ResourceMaxSize);
        }

        [TearDown]
        public void TearDown()
        {
            Resource.RestartProgram();
        }

        [Test]
        public void TestAllocateAboveRange()
        {
            Assert.Throws<AllocationException>(() => m_testResource.Allocate(ResourceMaxSize));
        }

        [Test]
        public void TestAllocateBelowRange()
        {
            Assert.Throws<AllocationException>(() => m_testResource.Allocate(-1));
        }

        [Test]
        public void TestAlocateAlreadyAllocatedResource()
        {
            m_testResource.Allocate(0);
            Assert.Throws<AllocationException>(() => m_testResource.Allocate(0));
        }

        [Test]
        public void TestAllocateValidResource()
        {
            for (int i = 0; i < ResourceMaxSize; i++)
            {
                m_testResource.Allocate(i);
            }
        }

        [Test]
        public void TestAllocateFreedResource()
        {
            for (int i = 0; i < ResourceMaxSize; i++)
            {
                m_testResource.Allocate(i);
            }
            for (int i = 0; i < ResourceMaxSize; i++)
            {
                m_testResource.Deallocate(i);
            }
            for (int i = 0; i < ResourceMaxSize; i++)
            {
                m_testResource.Allocate(i);
            }
        }

        [Test]
        public void TestAllocateNextValidResource()
        {
            m_testResource.Allocate(3);
            int allocatedValue = m_testResource.Allocate();
            Assert.AreEqual(0, allocatedValue);
            allocatedValue = m_testResource.Allocate();
            Assert.AreEqual(1, allocatedValue);
            allocatedValue = m_testResource.Allocate();
            Assert.AreEqual(2, allocatedValue);
            //Three should be skipped since it already allocated.
            allocatedValue = m_testResource.Allocate();
            Assert.AreEqual(4, allocatedValue);
        }

        [Test]
        public void TestAllocateWithNoRemainingResources()
        {
            for (int i = 0; i < ResourceMaxSize; i++)
            {
                m_testResource.Allocate(i);
            }
            Assert.Throws<AllocationException>(() => m_testResource.Allocate());
        }

        [Test]
        public void TestDeallocateEmptyResource()
        {
            Assert.Throws<AllocationException>(() =>
            {
                m_testResource.Deallocate(0);
            });
        }
    }
}
