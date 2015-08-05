using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestResource
    {
        private Resource testResource;
        private const int RESOURCE_MAX_SIZE = 5;

        [SetUp]
        public void Setup()
        {
            testResource = new Resource(RESOURCE_MAX_SIZE);
        }

        [TearDown]
        public void TearDown()
        {
            Resource.RestartProgram();
        }

        [Test]
        public void TestAllocateAboveRange()
        {
            Assert.Throws<AllocationException>(() => testResource.Allocate(RESOURCE_MAX_SIZE));
        }

        [Test]
        public void TestAllocateBelowRange()
        {
            Assert.Throws<AllocationException>(() => testResource.Allocate(-1));
        }

        [Test]
        public void TestAlocateAlreadyAllocatedResource()
        {
            testResource.Allocate(0);
            Assert.Throws<AllocationException>(() => testResource.Allocate(0));
        }

        [Test]
        public void TestAllocateValidResource()
        {
            for (int i = 0; i < RESOURCE_MAX_SIZE; i++)
            {
                testResource.Allocate(i);
            }
        }

        [Test]
        public void TestAllocateFreedResource()
        {
            for (int i = 0; i < RESOURCE_MAX_SIZE; i++)
            {
                testResource.Allocate(i);
            }
            for (int i = 0; i < RESOURCE_MAX_SIZE; i++)
            {
                testResource.Deallocate(i);
            }
            for (int i = 0; i < RESOURCE_MAX_SIZE; i++)
            {
                testResource.Allocate(i);
            }
        }

        [Test]
        public void TestAllocateNextValidResource()
        {
            testResource.Allocate(3);
            int allocatedValue = testResource.Allocate();
            Assert.AreEqual(0, allocatedValue);
            allocatedValue = testResource.Allocate();
            Assert.AreEqual(1, allocatedValue);
            allocatedValue = testResource.Allocate();
            Assert.AreEqual(2, allocatedValue);
            //Three should be skipped since it already allocated.
            allocatedValue = testResource.Allocate();
            Assert.AreEqual(4, allocatedValue);
        }

        [Test]
        public void TestAllocateWithNoRemainingResources()
        {
            for (int i = 0; i < RESOURCE_MAX_SIZE; i++)
            {
                testResource.Allocate(i);
            }
            Assert.Throws<AllocationException>(() => testResource.Allocate());
        }
    }
}
