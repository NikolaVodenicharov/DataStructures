namespace Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass()]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void Count_AfterInsert()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(8);
            tree.Insert(0);

            // Assert
            Assert.AreEqual(10, tree.Count());
        }

        [TestMethod]
        public void Count_AfterInsertAndDeleteMin()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(8);
            tree.Insert(0);

            // Act
            tree.DeleteMin();

            // Assert
            Assert.AreEqual(9, tree.Count());
        }

        [TestMethod]
        public void Count_AfterInsertAndDeleteMax()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(8);
            tree.Insert(0);

            // Act
            tree.DeleteMax();

            // Assert
            Assert.AreEqual(9, tree.Count());
        }

        [TestMethod]
        public void Insert_MultipleInsertTraverseInOrder()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);

            // Act
            var actualElements = new List<int>();
            tree.EachInOrder(actualElements.Add);

            // Assert
            var expectedElements = new int[] { 2, 3, 4, 5, 6, 7, 9 };
            CollectionAssert.AreEqual(expectedElements, actualElements);
        }

        [TestMethod]
        public void DeleteMax_DeleteMiddleNodeElement()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(8);
            tree.Insert(0);

            // Act
            tree.DeleteMax();

            // Assert
            var isContained = tree.Contains(9);
            Assert.IsFalse(isContained);
        }

        [TestMethod]
        public void DeleteMax_DeleteRootNodeElement()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(9);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(3);
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(8);
            tree.Insert(0);

            // Act
            tree.DeleteMax();

            // Assert
            var isContained = tree.Contains(9);
            Assert.IsFalse(isContained);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteMax_DeleteFromEmptyCollection()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();

            // Act
            tree.DeleteMax();
        }



    }
}