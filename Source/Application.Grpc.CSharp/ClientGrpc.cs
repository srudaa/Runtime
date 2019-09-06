// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dolittle/application/client.proto
// </auto-generated>
// Original file comments:
// ---------------------------------------------------------------------------------------------
//  Copyright (c) Dolittle. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// --------------------------------------------------------------------------------------------
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Dolittle.Runtime.Application.Grpc {
  /// <summary>
  /// Represents the Client service
  /// </summary>
  public static partial class Client
  {
    static readonly string __ServiceName = "dolittle.runtime.application.Client";

    static readonly grpc::Marshaller<global::Dolittle.Runtime.Application.Grpc.ClientInfo> __Marshaller_dolittle_runtime_application_ClientInfo = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Dolittle.Runtime.Application.Grpc.ClientInfo.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Dolittle.Runtime.Application.Grpc.ConnectionResult> __Marshaller_dolittle_runtime_application_ConnectionResult = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Dolittle.Runtime.Application.Grpc.ConnectionResult.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::System.Protobuf.guid> __Marshaller_dolittle_guid = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::System.Protobuf.guid.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Dolittle.Runtime.Application.Grpc.DisconnectedResult> __Marshaller_dolittle_runtime_application_DisconnectedResult = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Dolittle.Runtime.Application.Grpc.DisconnectedResult.Parser.ParseFrom);

    static readonly grpc::Method<global::Dolittle.Runtime.Application.Grpc.ClientInfo, global::Dolittle.Runtime.Application.Grpc.ConnectionResult> __Method_Connect = new grpc::Method<global::Dolittle.Runtime.Application.Grpc.ClientInfo, global::Dolittle.Runtime.Application.Grpc.ConnectionResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Connect",
        __Marshaller_dolittle_runtime_application_ClientInfo,
        __Marshaller_dolittle_runtime_application_ConnectionResult);

    static readonly grpc::Method<global::System.Protobuf.guid, global::Dolittle.Runtime.Application.Grpc.DisconnectedResult> __Method_Disconnect = new grpc::Method<global::System.Protobuf.guid, global::Dolittle.Runtime.Application.Grpc.DisconnectedResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Disconnect",
        __Marshaller_dolittle_guid,
        __Marshaller_dolittle_runtime_application_DisconnectedResult);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Dolittle.Runtime.Application.Grpc.ClientReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Client</summary>
    [grpc::BindServiceMethod(typeof(Client), "BindService")]
    public abstract partial class ClientBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Dolittle.Runtime.Application.Grpc.ConnectionResult> Connect(global::Dolittle.Runtime.Application.Grpc.ClientInfo request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Dolittle.Runtime.Application.Grpc.DisconnectedResult> Disconnect(global::System.Protobuf.guid request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Client</summary>
    public partial class ClientClient : grpc::ClientBase<ClientClient>
    {
      /// <summary>Creates a new client for Client</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ClientClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Client that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ClientClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ClientClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ClientClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Dolittle.Runtime.Application.Grpc.ConnectionResult Connect(global::Dolittle.Runtime.Application.Grpc.ClientInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Connect(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Dolittle.Runtime.Application.Grpc.ConnectionResult Connect(global::Dolittle.Runtime.Application.Grpc.ClientInfo request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Connect, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Dolittle.Runtime.Application.Grpc.ConnectionResult> ConnectAsync(global::Dolittle.Runtime.Application.Grpc.ClientInfo request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ConnectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Dolittle.Runtime.Application.Grpc.ConnectionResult> ConnectAsync(global::Dolittle.Runtime.Application.Grpc.ClientInfo request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Connect, null, options, request);
      }
      public virtual global::Dolittle.Runtime.Application.Grpc.DisconnectedResult Disconnect(global::System.Protobuf.guid request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Disconnect(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Dolittle.Runtime.Application.Grpc.DisconnectedResult Disconnect(global::System.Protobuf.guid request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Disconnect, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Dolittle.Runtime.Application.Grpc.DisconnectedResult> DisconnectAsync(global::System.Protobuf.guid request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DisconnectAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Dolittle.Runtime.Application.Grpc.DisconnectedResult> DisconnectAsync(global::System.Protobuf.guid request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Disconnect, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ClientClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ClientClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ClientBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Connect, serviceImpl.Connect)
          .AddMethod(__Method_Disconnect, serviceImpl.Disconnect).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ClientBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Connect, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Dolittle.Runtime.Application.Grpc.ClientInfo, global::Dolittle.Runtime.Application.Grpc.ConnectionResult>(serviceImpl.Connect));
      serviceBinder.AddMethod(__Method_Disconnect, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::System.Protobuf.guid, global::Dolittle.Runtime.Application.Grpc.DisconnectedResult>(serviceImpl.Disconnect));
    }

  }
}
#endregion