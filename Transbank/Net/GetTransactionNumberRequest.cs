﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using Transbank.Model;

namespace Transbank.Net
{
    public sealed class GetTransactionNumberRequest : BaseRequest
    {
        private string occ;
        private string externalUniqueNumber;
        [JsonProperty("issueadAt")]
        public long IssuedAt { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("occ")]
        public string Occ
        {
            get => occ;
            set => occ = value ?? throw
                new ArgumentNullException(nameof(occ));
        }

        [JsonProperty("externalUniqueNumber")]
        public string ExternalUniqueNumber
        {
            get => externalUniqueNumber;
            set => externalUniqueNumber = value ?? throw
                new ArgumentNullException(nameof(externalUniqueNumber));
        }

        public GetTransactionNumberRequest()
        {
        }

        public GetTransactionNumberRequest(string occ, 
                                           string externalUniqueNumber, long issueadAt)
        {
            Occ = occ;
            ExternalUniqueNumber = externalUniqueNumber;
        }

        public override string ToString()
        {
            return base.ToString() + $"Occ{Occ}, " +
                       $"ExternalUniqueNumber={ExternalUniqueNumber}, " +
                       $"IssueadAt={IssuedAt}, Signature{Signature}";
        }
    }
}
