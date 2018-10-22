﻿namespace Tests
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
        public void Coutn_AfterDelete()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(4);

            tree.Insert(9);
            tree.Insert(2);

            tree.Insert(7);
            tree.Insert(10);

            tree.Insert(5);
            tree.Insert(8);

            tree.Insert(6);

            tree.Insert(1);
            tree.Insert(3);

            // Act
            tree.Delete(4);

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

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(4);

            tree.Insert(9);
            tree.Insert(2);

            tree.Insert(7);
            tree.Insert(10);

            tree.Insert(5);
            tree.Insert(8);

            tree.Insert(6);

            tree.Insert(1);
            tree.Insert(3);

            // Act
            tree.Delete(4);

            var actualElements = new List<int>();
            tree.EachInOrder(actualElements.Add);

            // Assert
            var expectedElements = new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 10 };
            CollectionAssert.AreEqual(expectedElements, actualElements);
        }


        [TestMethod]
        public void Rank_SomeSmallerNumbers()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(17);
            tree.Insert(9);
            tree.Insert(5);
            tree.Insert(11);
            tree.Insert(10);
            tree.Insert(14);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(8);
            tree.Insert(1);
            tree.Insert(3);

            // Act
            var rank = tree.Rank(10);

            // Assert
            Assert.AreEqual(8, rank);
        }

        [TestMethod]
        public void Rank_NoSmallerNumbers()
        {
            // Arrange
            var tree = new BinarySearchTree<int>();
            tree.Insert(17);
            tree.Insert(9);
            tree.Insert(5);
            tree.Insert(11);
            tree.Insert(10);
            tree.Insert(14);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(8);
            tree.Insert(1);
            tree.Insert(3);

            // Act
            var rank = tree.Rank(1);

            // Assert
            Assert.AreEqual(0, rank);
        }

        [TestMethod]
        public void Floor_ExistingSmallerElement()
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
            var firstSmallerElement = tree.Floor(3);

            // Assert
            Assert.AreEqual(2, firstSmallerElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Floor_NotExistingSmallerElement()
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
            var firstSmallerElement = tree.Floor(1);
        }

        [TestMethod]
        public void Ceiling_ExistingBiggerElement()
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
            var firstSmallerElement = tree.Ceiling(6);

            // Assert
            Assert.AreEqual(7, firstSmallerElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Ceiling_NotExistingBiggerElement()
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
            var firstSmallerElement = tree.Ceiling(20);
        }
    }
}