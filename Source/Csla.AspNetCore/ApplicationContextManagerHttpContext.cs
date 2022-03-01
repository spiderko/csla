﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationContextManager.cs" company="Marimer LLC">
//     Copyright (c) Marimer LLC. All rights reserved.
//     Website: https://cslanet.com
// </copyright>
// <summary>Application context manager that uses HttpContextAccessor</summary>
//-----------------------------------------------------------------------
using Csla.Core;
using Csla.Runtime;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Csla.AspNetCore
{
  /// <summary>
  /// Application context manager that uses HttpContextAccessor when 
  /// resolving HttpContext to store context values.
  /// </summary>
  public class ApplicationContextManagerHttpContext : IContextManager
  {

    private const string _localContextName = "Csla.LocalContext";
    private const string _clientContextName = "Csla.ClientContext";

    /// <summary>
    /// Creates an instance of the object, initializing it
    /// with the required IServiceProvider.
    /// </summary>
    /// <param name="httpContextAccessor">HttpContext accessor</param>
    /// <param name="runtimeInfo"></param>
    public ApplicationContextManagerHttpContext(IHttpContextAccessor httpContextAccessor, IRuntimeInfo runtimeInfo)
    {
      if (runtimeInfo.IsHttpContextValid)
      {
        HttpContext = httpContextAccessor.HttpContext;
        if (HttpContext == null)
          throw new NullReferenceException(nameof(HttpContext));
      }
    }

    /// <summary>
    /// Gets the current HttpContext instance.
    /// </summary>
    protected virtual HttpContext HttpContext { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this
    /// context manager is valid for use in
    /// the current environment.
    /// </summary>
    public bool IsValid
    {
      get { return HttpContext != null; }
    }

    /// <summary>
    /// Gets a value indicating whether the current runtime
    /// is stateful (e.g. WPF, Blazor, etc.)
    /// </summary>
    public bool IsStatefulRuntime => false;

    /// <summary>
    /// Gets the current principal.
    /// </summary>
    public System.Security.Principal.IPrincipal GetUser()
    {
      var result = HttpContext?.User;
      if (result == null)
      {
        result = new Csla.Security.CslaClaimsPrincipal();
        SetUser(result);
      }
      return result;
    }

    /// <summary>
    /// Sets the current principal.
    /// </summary>
    /// <param name="principal">Principal object.</param>
    public void SetUser(System.Security.Principal.IPrincipal principal)
    {
      HttpContext.User = (ClaimsPrincipal)principal;
    }

    /// <summary>
    /// Gets the local context.
    /// </summary>
    public ContextDictionary GetLocalContext()
    {
      return (ContextDictionary)HttpContext?.Items[_localContextName];
    }

    /// <summary>
    /// Sets the local context.
    /// </summary>
    /// <param name="localContext">Local context.</param>
    public void SetLocalContext(ContextDictionary localContext)
    {
      HttpContext.Items[_localContextName] = localContext;
    }

    /// <summary>
    /// Gets the client context.
    /// </summary>
    /// <param name="executionLocation"></param>
    public ContextDictionary GetClientContext(ApplicationContext.ExecutionLocations executionLocation)
    {
      return (ContextDictionary)HttpContext?.Items[_clientContextName];
    }

    /// <summary>
    /// Sets the client context.
    /// </summary>
    /// <param name="clientContext">Client context.</param>
    /// <param name="executionLocation"></param>
    public void SetClientContext(ContextDictionary clientContext, ApplicationContext.ExecutionLocations executionLocation)
    {
      HttpContext.Items[_clientContextName] = clientContext;
    }

    private const string _applicationContextName = "Csla.ApplicationContext";

    /// <summary>
    /// Gets or sets a reference to the current ApplicationContext.
    /// </summary>
    public virtual ApplicationContext ApplicationContext
    {
      get
      {
        return (ApplicationContext)HttpContext?.Items[_applicationContextName];
      }
      set
      {
        HttpContext.Items[_applicationContextName] = value;
      }
    }
  }
}