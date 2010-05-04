﻿using Machine.Specifications;
using Relax.Impl;
using StructureMap;

namespace Relax.Tests
{
    public abstract class with_configuration
    {
        protected static ICouchConfiguration configuration;
        private Establish context = () =>
                                        {
                                            configuration = new CouchConfiguration();
                                        };
        protected static void WireUpCommandMock(ICouchCommand commandMock)
        {
            ObjectFactory.Configure(x => x.For<ICouchCommand>().Use(commandMock));
        }
    }
}