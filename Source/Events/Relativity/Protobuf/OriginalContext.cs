// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: dolittle/interaction/events.relativity/original_context.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dolittle.Runtime.Events.Relativity.Protobuf {

  /// <summary>Holder for reflection information generated from dolittle/interaction/events.relativity/original_context.proto</summary>
  public static partial class OriginalContextReflection {

    #region Descriptor
    /// <summary>File descriptor for dolittle/interaction/events.relativity/original_context.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static OriginalContextReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cj1kb2xpdHRsZS9pbnRlcmFjdGlvbi9ldmVudHMucmVsYXRpdml0eS9vcmln",
            "aW5hbF9jb250ZXh0LnByb3RvEhpkb2xpdHRsZS5ldmVudHMucmVsYXRpdml0",
            "eRoRc3lzdGVtL2d1aWQucHJvdG8aMmRvbGl0dGxlL2ludGVyYWN0aW9uL2V2",
            "ZW50cy5yZWxhdGl2aXR5L2NsYWltLnByb3RvIt4BCg9PcmlnaW5hbENvbnRl",
            "eHQSIwoLYXBwbGljYXRpb24YASABKAsyDi5kb2xpdHRsZS5ndWlkEiYKDmJv",
            "dW5kZWRDb250ZXh0GAIgASgLMg4uZG9saXR0bGUuZ3VpZBIeCgZ0ZW5hbnQY",
            "AyABKAsyDi5kb2xpdHRsZS5ndWlkEhMKC2Vudmlyb25tZW50GAQgASgJEjEK",
            "BmNsYWltcxgFIAMoCzIhLmRvbGl0dGxlLmV2ZW50cy5yZWxhdGl2aXR5LkNs",
            "YWltEhYKDmNvbW1pdEluT3JpZ2luGAYgASgEQi6qAitEb2xpdHRsZS5SdW50",
            "aW1lLkV2ZW50cy5SZWxhdGl2aXR5LlByb3RvYnVmYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::System.Protobuf.GuidReflection.Descriptor, global::Dolittle.Runtime.Events.Relativity.Protobuf.ClaimReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dolittle.Runtime.Events.Relativity.Protobuf.OriginalContext), global::Dolittle.Runtime.Events.Relativity.Protobuf.OriginalContext.Parser, new[]{ "Application", "BoundedContext", "Tenant", "Environment", "Claims", "CommitInOrigin" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Represents the original context
  /// </summary>
  public sealed partial class OriginalContext : pb::IMessage<OriginalContext> {
    private static readonly pb::MessageParser<OriginalContext> _parser = new pb::MessageParser<OriginalContext>(() => new OriginalContext());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<OriginalContext> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dolittle.Runtime.Events.Relativity.Protobuf.OriginalContextReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OriginalContext() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OriginalContext(OriginalContext other) : this() {
      Application = other.application_ != null ? other.Application.Clone() : null;
      BoundedContext = other.boundedContext_ != null ? other.BoundedContext.Clone() : null;
      Tenant = other.tenant_ != null ? other.Tenant.Clone() : null;
      environment_ = other.environment_;
      claims_ = other.claims_.Clone();
      commitInOrigin_ = other.commitInOrigin_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OriginalContext Clone() {
      return new OriginalContext(this);
    }

    /// <summary>Field number for the "application" field.</summary>
    public const int ApplicationFieldNumber = 1;
    private global::System.Protobuf.guid application_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::System.Protobuf.guid Application {
      get { return application_; }
      set {
        application_ = value;
      }
    }

    /// <summary>Field number for the "boundedContext" field.</summary>
    public const int BoundedContextFieldNumber = 2;
    private global::System.Protobuf.guid boundedContext_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::System.Protobuf.guid BoundedContext {
      get { return boundedContext_; }
      set {
        boundedContext_ = value;
      }
    }

    /// <summary>Field number for the "tenant" field.</summary>
    public const int TenantFieldNumber = 3;
    private global::System.Protobuf.guid tenant_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::System.Protobuf.guid Tenant {
      get { return tenant_; }
      set {
        tenant_ = value;
      }
    }

    /// <summary>Field number for the "environment" field.</summary>
    public const int EnvironmentFieldNumber = 4;
    private string environment_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Environment {
      get { return environment_; }
      set {
        environment_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "claims" field.</summary>
    public const int ClaimsFieldNumber = 5;
    private static readonly pb::FieldCodec<global::Dolittle.Runtime.Events.Relativity.Protobuf.Claim> _repeated_claims_codec
        = pb::FieldCodec.ForMessage(42, global::Dolittle.Runtime.Events.Relativity.Protobuf.Claim.Parser);
    private readonly pbc::RepeatedField<global::Dolittle.Runtime.Events.Relativity.Protobuf.Claim> claims_ = new pbc::RepeatedField<global::Dolittle.Runtime.Events.Relativity.Protobuf.Claim>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Dolittle.Runtime.Events.Relativity.Protobuf.Claim> Claims {
      get { return claims_; }
    }

    /// <summary>Field number for the "commitInOrigin" field.</summary>
    public const int CommitInOriginFieldNumber = 6;
    private ulong commitInOrigin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong CommitInOrigin {
      get { return commitInOrigin_; }
      set {
        commitInOrigin_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as OriginalContext);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(OriginalContext other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Application, other.Application)) return false;
      if (!object.Equals(BoundedContext, other.BoundedContext)) return false;
      if (!object.Equals(Tenant, other.Tenant)) return false;
      if (Environment != other.Environment) return false;
      if(!claims_.Equals(other.claims_)) return false;
      if (CommitInOrigin != other.CommitInOrigin) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (application_ != null) hash ^= Application.GetHashCode();
      if (boundedContext_ != null) hash ^= BoundedContext.GetHashCode();
      if (tenant_ != null) hash ^= Tenant.GetHashCode();
      if (Environment.Length != 0) hash ^= Environment.GetHashCode();
      hash ^= claims_.GetHashCode();
      if (CommitInOrigin != 0UL) hash ^= CommitInOrigin.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (application_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Application);
      }
      if (boundedContext_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(BoundedContext);
      }
      if (tenant_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Tenant);
      }
      if (Environment.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Environment);
      }
      claims_.WriteTo(output, _repeated_claims_codec);
      if (CommitInOrigin != 0UL) {
        output.WriteRawTag(48);
        output.WriteUInt64(CommitInOrigin);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (application_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Application);
      }
      if (boundedContext_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(BoundedContext);
      }
      if (tenant_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Tenant);
      }
      if (Environment.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Environment);
      }
      size += claims_.CalculateSize(_repeated_claims_codec);
      if (CommitInOrigin != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(CommitInOrigin);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(OriginalContext other) {
      if (other == null) {
        return;
      }
      if (other.application_ != null) {
        if (application_ == null) {
          application_ = new global::System.Protobuf.guid();
        }
        Application.MergeFrom(other.Application);
      }
      if (other.boundedContext_ != null) {
        if (boundedContext_ == null) {
          boundedContext_ = new global::System.Protobuf.guid();
        }
        BoundedContext.MergeFrom(other.BoundedContext);
      }
      if (other.tenant_ != null) {
        if (tenant_ == null) {
          tenant_ = new global::System.Protobuf.guid();
        }
        Tenant.MergeFrom(other.Tenant);
      }
      if (other.Environment.Length != 0) {
        Environment = other.Environment;
      }
      claims_.Add(other.claims_);
      if (other.CommitInOrigin != 0UL) {
        CommitInOrigin = other.CommitInOrigin;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (application_ == null) {
              application_ = new global::System.Protobuf.guid();
            }
            input.ReadMessage(application_);
            break;
          }
          case 18: {
            if (boundedContext_ == null) {
              boundedContext_ = new global::System.Protobuf.guid();
            }
            input.ReadMessage(boundedContext_);
            break;
          }
          case 26: {
            if (tenant_ == null) {
              tenant_ = new global::System.Protobuf.guid();
            }
            input.ReadMessage(tenant_);
            break;
          }
          case 34: {
            Environment = input.ReadString();
            break;
          }
          case 42: {
            claims_.AddEntriesFrom(input, _repeated_claims_codec);
            break;
          }
          case 48: {
            CommitInOrigin = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
