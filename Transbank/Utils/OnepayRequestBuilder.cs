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
            options = BuildOptions(options);
            SendTransactionRequest request = new SendTransactionRequest(
                Guid.NewGuid().ToString(), cart.Total, cart.GetItemQuantity(),
                    (long)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                        cart.GetItems(), OnePay.FAKE_CALLBACK_URL, "WEB");
            PrepareRequest(request, options);
            return OnePaySignUtil.GetInstance().Sign(request, options.SharedSecret);
        }

        public GetTransactionNumberRequest Build(String occ, String externalUniqueNumber, Options options)
        {
            if (options == null) return Options.getDefaults();

            if (options.ApiKey == null) options.ApiKey = OnePay.ApiKey;
            if (options.SharedSecret == null) options.SharedSecret = OnePay.SharedSecret;

            return options;
        }

        protected void PrepareRequest(BaseRequest request, Options options)
        {
            request.ApiKey = options.ApiKey;
            request.AppKey = OnePay.APP_KEY;
        }

        private OnePayRequestBuilder() : base()
        {
        }

        public static OnePayRequestBuilder GetInstance()
        {
            if (instance == null)
                lock(padlock)
                    if (instance == null)
                        instance = new OnePayRequestBuilder();
            return instance;
        }
        
    }
}
