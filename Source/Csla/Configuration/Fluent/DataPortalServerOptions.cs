﻿//-----------------------------------------------------------------------
// <copyright file="DataPortalServerOptions.cs" company="Marimer LLC">
//     Copyright (c) Marimer LLC. All rights reserved.
//     Website: https://cslanet.com
// </copyright>
// <summary>Server-side data portal options.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Csla.Server;

namespace Csla.Configuration
{
  /// <summary>
  /// Server-side data portal options.
  /// </summary>
  public class DataPortalServerOptions
  {
    /// <summary>
    /// Gets or sets a value containing the type of the
    /// IDataPortalAuthorizer to be used by the data portal.
    /// An instance of this type is created using dependency
    /// injection.
    /// </summary>
    internal Type AuthorizerProviderType { get; set; } = typeof(ActiveAuthorizer);

    /// <summary>
    /// Sets the type of the IDataPortalAuthorizer to be 
    /// used by the data portal. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterAuthorizerProvider<T>() where T : IAuthorizeDataPortal
    {
      AuthorizerProviderType = typeof(T);
    } 

    /// <summary>
    /// Gets a list of the IInterceptDataPortal instances
    /// that should be executed by the server-side data portal.
    /// injection.
    /// </summary>
    internal List<Type> InterceptorProviders { get; } = new List<Type>() { typeof(Csla.Server.Interceptors.ServerSide.RevalidatingInterceptor) };

    /// <summary>
    /// Adds the type of an IInterceptDataPortal that will
    /// be executed by the server-side data portal.
    /// </summary>
    public void AddInterceptorProvider<T>() where T: IInterceptDataPortal
    {
      InterceptorProviders.Add(typeof(T));
    }

    /// <summary>
    /// Adds the type of an IInterceptDataPortal that will
    /// be executed by the server-side data portal.
    /// </summary>
    /// <param name="index">Index at which new item should be added.</param>
    public void AddInterceptorProvider<T>(int index) where T : IInterceptDataPortal
    {
      InterceptorProviders.Insert(index, typeof(T));
    }

    /// <summary>
    /// Removes a type of an IInterceptDataPortal.
    /// </summary>
    /// <param name="index">Index from which item will be removed.</param>
    public void RemoveInterceptorProvider<T>(int index) where T : IInterceptDataPortal
    {
      InterceptorProviders.RemoveAt(index);
    }

    /// <summary>
    /// Gets or sets the type of the ExceptionInspector.
    /// </summary>
    internal Type ExceptionInspectorType { get; set; } = typeof(DefaultExceptionInspector);

    /// <summary>
    /// Sets the type of the ExceptionInspector.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterExceptionInspector<T>() where T: IDataPortalExceptionInspector
    {
      ExceptionInspectorType = typeof(T);
    }

    /// <summary>
    /// Gets or sets the type of the Activator.
    /// </summary>
    internal Type ActivatorType { get; set; } = typeof(DefaultDataPortalActivator);

    /// <summary>
    /// Sets the type of the Activator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterActivator<T>() where T: IDataPortalActivator
    {
      ActivatorType = typeof(T);
    }

    /// <summary>
    /// Gets or sets the type name of the factor loader used to create
    /// server-side instances of business object factories when using
    /// the FactoryDataPortal model. Type must implement
    /// <see cref="IObjectFactoryLoader"/>.
    /// </summary>
    internal Type ObjectFactoryLoaderType { get; set; } = typeof(ObjectFactoryLoader);

    /// <summary>
    /// Gets or sets the type name of the factor loader used to create
    /// server-side instances of business object factories when using
    /// the FactoryDataPortal model. Type must implement
    /// <see cref="IObjectFactoryLoader"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterObjectFactoryLoader<T>() where T: IObjectFactoryLoader
    {
      ObjectFactoryLoaderType = typeof(T);
    }
  }
}