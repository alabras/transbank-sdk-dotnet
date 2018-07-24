﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transbank;
using Transbank.Model;
using Transbank.Net;
using Transbank.Enums;

namespace TransbankTest
{
    [TestClass]
    public class OnePaySendTransactionTest
    {
        [TestInitialize]
        public void Setup()
        {
            // Setting comerce data
            OnePay.SharedSecret = "P4DCPS55QB2QLT56SQH6#W#LV76IAPYX";
            OnePay.ApiKey = "mUc0GxYGor6X8u-_oB3e-HWJulRG01WoC96-_tUA3Bg";
            OnePay.IntegrationType = OnePayIntegrationType.MOCK;

        }

        public ShoppingCart CreateCart()
        {
            // Setting items to the shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(new Item("Zapatos", 1, 10000, null, -1));
            cart.Add(new Item("Pantalon", 1, 5000, null, -1));
            return cart;
        }

        [TestMethod]
        public void TestOnePaySendtransaction()
        {
            var cart = CreateCart();
            TransactionCreateResponse response = Transaction.Create(cart);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestOnePayCommitTransaction()
        {

            var cart = CreateCart();
            // Send transaction to Transbank
            TransactionCreateResponse createResponse = Transaction.Create(cart);
            Assert.IsNotNull(createResponse);

            TransactionCommitResponse commitResponse = Transaction.Commit(
                createResponse.Occ, createResponse.ExternalUniqueNumber);
        }
    }
}