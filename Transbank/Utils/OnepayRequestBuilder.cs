﻿using System;
using System.Collections.Generic;
using System.Text;
using Transbank.Model;
using Transbank.Net;
using Transbank.Utils;

namespace Transbank.Utils
{
    public class OnePayRequestBuilder
    {
        private static volatile OnePayRequestBuilder instance;
        private static readonly object padlock = new object();

        public SendTransactionRequest Build(ShoppingCart cart, Options options)
        {
            SendTransactionRequest request = new SendTransactionRequest(
                Guid.NewGuid().ToString(), cart.Total, cart.ItemQuantity,
                    (long)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                        cart.Items, OnePay.FAKE_CALLBACK_URL, "WEB");
            PrepareRequest(request, options);
            return OnePaySignUtil.Instance.Sign(request, options.SharedSecret);
        }

        public GetTransactionNumberRequest Build(String occ, String externalUniqueNumber, Options options)
        {
            GetTransactionNumberRequest request = new GetTransactionNumberRequest(
                occ, externalUniqueNumber, (long)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            PrepareRequest(request, options);
            return OnePaySignUtil.Instance.Sign(request, options.SharedSecret);
        }

        protected void PrepareRequest(BaseRequest request, Options options)
        {
            request.ApiKey = options.ApiKey;
            request.AppKey = OnePay.APP_KEY;
        }

        private OnePayRequestBuilder() : base()
        {
        }

        public static OnePayRequestBuilder Instance
        {
            get
            {
                if (instance == null)
                    lock (padlock)
                        if (instance == null)
                            instance = new OnePayRequestBuilder();
                return instance;
            }
        }
        
    }
}