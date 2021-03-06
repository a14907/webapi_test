﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using webapi.Models;

namespace webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("Person");
            builder.EntitySet<Photo>("Photo");

            builder.Namespace = "webapi";

            builder.EntityType<Photo>()
                .Action("PhotoPrice")
                .Parameter<int>("Price");

            builder.EntityType<Photo>()
                .Function("ChangeName")
                .Returns<bool>();

            builder.Function("GetAPrice").Returns<int>().Parameter<int>("Id");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
