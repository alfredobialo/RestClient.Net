﻿using RestClientDotNet.Abstractions;
using System;
using System.Net.Http;

namespace RestClientDotNet
{
    public class RestResponse<TResponseBody> : RestResponseBase<TResponseBody>
    {
        #region Public Properties
        public Uri BaseUri { get; }
        public Uri Resource { get; }
        public HttpResponseMessage HttpResponseMessage { get; }
        public override bool IsSuccess => HttpResponseMessage.IsSuccessStatusCode;
        #endregion

        #region Constructor
        public RestResponse
        (
            IRestHeadersCollection restHeadersCollection,
            int statusCode,
            Uri baseUri,
            Uri resource,
            HttpVerb httpVerb,
            byte[] responseContentData,
            TResponseBody body,
            HttpResponseMessage httpResponseMessage
            ) : base(restHeadersCollection, statusCode, httpVerb, responseContentData, body)
        {
            BaseUri = baseUri;
            Resource = resource;
            HttpResponseMessage = httpResponseMessage;

        }
        #endregion
    }
}
