﻿// Solution: PersonalLibs
// Project: DynamicLogParser.Tests
// FileName: DynamicModelTests.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 5:54 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DynamicModelTests
    {
        private dynamic _model;
        
        [TestInitialize]
        public void Start()
        {
            this._model = new DynamicModel();
        }

        [TestMethod]
        public void ShouldSetMember()
        {
            this._model.testProperty = "testVal";
        }

        [TestMethod]
        public void ShouldUpdateProperty()
        {
            const string initVal = "test1";
            this._model.testProperty = initVal;
            this._model.testProperty = "update";
            Assert.AreNotEqual(this._model.testProperty, initVal);
        }

        [TestMethod]
        public void ShouldSetIndex()
        {
            this._model[0] = "123";
            this._model[0] = "123";
            this._model.testProperty = new DynamicModel();
            this._model.testProperty.testCollection = new DynamicModel();
            this._model.testProperty.testCollection[1] = "123";
            Assert.AreEqual(this._model[0], 
                this._model.testProperty.testCollection[1]);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void ShouldThrowForInvalidIndex()
        {
            this._model[-1] = "123";
        }
    }
}
