﻿//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace D20Tek.Spectre.Console.Extensions.Injection
{
    /// <summary>
    /// Type registry for Spectre.Console that uses the Microsoft.Extensions.DependencyInjection
    /// framework to register types with the DI engine.
    /// </summary>
    public sealed class DependencyInjectionTypeRegistrar : ITypeRegistrar
    {
        private readonly IServiceCollection _builder;

        /// <summary>
        /// Constructor that takes a service collection instance.
        /// </summary>
        /// <param name="builder">Service collection builder to use for registering types.</param>
        public DependencyInjectionTypeRegistrar(IServiceCollection builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            _builder = builder;
        }

        /// <summary>
        /// Builds the type resolver representing the registrations
        /// specified in the current instance.
        /// </summary>
        /// <returns>A type resolver.</returns>
        public ITypeResolver Build()
        {
            var provider = _builder.BuildServiceProvider();
            return new DependencyInjectionTypeResolver(provider);
        }

        /// <summary>
        /// Registers the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation type.</param>
        public void Register(Type service, Type implementation)
        {
            ArgumentNullException.ThrowIfNull(service, nameof(service));
            ArgumentNullException.ThrowIfNull(implementation, nameof(implementation));

            _builder.AddSingleton(service, implementation);
        }

        /// <summary>
        /// Registers the specified instance.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        public void RegisterInstance(Type service, object implementation)
        {
            ArgumentNullException.ThrowIfNull(service, nameof(service));
            ArgumentNullException.ThrowIfNull(implementation, nameof(implementation));

            _builder.AddSingleton(service, implementation);
        }

        /// <summary>
        /// Registers the specified instance lazily.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="factoryMethod">The factory that creates the implementation.</param>
        public void RegisterLazy(Type service, Func<object> factoryMethod)
        {
            ArgumentNullException.ThrowIfNull(service, nameof(service));
            ArgumentNullException.ThrowIfNull(factoryMethod, nameof(factoryMethod));

            _builder.AddSingleton(service, (provider) => factoryMethod());
        }
    }
}
