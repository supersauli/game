// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Gate.proto

#include "Gate.pb.h"

#include <algorithm>

#include <google/protobuf/stubs/common.h>
#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/extension_set.h>
#include <google/protobuf/wire_format_lite_inl.h>
#include <google/protobuf/descriptor.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/reflection_ops.h>
#include <google/protobuf/wire_format.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
#include <google/protobuf/message_lite.h>

namespace GateServer {
class NewGateDefaultTypeInternal {
 public:
  ::google::protobuf::internal::ExplicitlyConstructed<NewGate> _instance;
} _NewGate_default_instance_;
class UserLoginDefaultTypeInternal {
 public:
  ::google::protobuf::internal::ExplicitlyConstructed<UserLogin> _instance;
} _UserLogin_default_instance_;
}  // namespace GateServer
static void InitDefaultsNewGate_Gate_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::GateServer::_NewGate_default_instance_;
    new (ptr) ::GateServer::NewGate();
    ::google::protobuf::internal::OnShutdownDestroyMessage(ptr);
  }
  ::GateServer::NewGate::InitAsDefaultInstance();
}

::google::protobuf::internal::SCCInfo<0> scc_info_NewGate_Gate_2eproto =
    {{ATOMIC_VAR_INIT(::google::protobuf::internal::SCCInfoBase::kUninitialized), 0, InitDefaultsNewGate_Gate_2eproto}, {}};

static void InitDefaultsUserLogin_Gate_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::GateServer::_UserLogin_default_instance_;
    new (ptr) ::GateServer::UserLogin();
    ::google::protobuf::internal::OnShutdownDestroyMessage(ptr);
  }
  ::GateServer::UserLogin::InitAsDefaultInstance();
}

::google::protobuf::internal::SCCInfo<0> scc_info_UserLogin_Gate_2eproto =
    {{ATOMIC_VAR_INIT(::google::protobuf::internal::SCCInfoBase::kUninitialized), 0, InitDefaultsUserLogin_Gate_2eproto}, {}};

void InitDefaults_Gate_2eproto() {
  ::google::protobuf::internal::InitSCC(&scc_info_NewGate_Gate_2eproto.base);
  ::google::protobuf::internal::InitSCC(&scc_info_UserLogin_Gate_2eproto.base);
}

::google::protobuf::Metadata file_level_metadata_Gate_2eproto[2];
constexpr ::google::protobuf::EnumDescriptor const** file_level_enum_descriptors_Gate_2eproto = nullptr;
constexpr ::google::protobuf::ServiceDescriptor const** file_level_service_descriptors_Gate_2eproto = nullptr;

const ::google::protobuf::uint32 TableStruct_Gate_2eproto::offsets[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::GateServer::NewGate, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::GateServer::NewGate, dwid_),
  PROTOBUF_FIELD_OFFSET(::GateServer::NewGate, strip_),
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::GateServer::UserLogin, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::GateServer::UserLogin, dwid_),
  PROTOBUF_FIELD_OFFSET(::GateServer::UserLogin, str_),
};
static const ::google::protobuf::internal::MigrationSchema schemas[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  { 0, -1, sizeof(::GateServer::NewGate)},
  { 7, -1, sizeof(::GateServer::UserLogin)},
};

static ::google::protobuf::Message const * const file_default_instances[] = {
  reinterpret_cast<const ::google::protobuf::Message*>(&::GateServer::_NewGate_default_instance_),
  reinterpret_cast<const ::google::protobuf::Message*>(&::GateServer::_UserLogin_default_instance_),
};

::google::protobuf::internal::AssignDescriptorsTable assign_descriptors_table_Gate_2eproto = {
  {}, AddDescriptors_Gate_2eproto, "Gate.proto", schemas,
  file_default_instances, TableStruct_Gate_2eproto::offsets,
  file_level_metadata_Gate_2eproto, 2, file_level_enum_descriptors_Gate_2eproto, file_level_service_descriptors_Gate_2eproto,
};

::google::protobuf::internal::DescriptorTable descriptor_table_Gate_2eproto = {
  false, InitDefaults_Gate_2eproto, 
  "\n\nGate.proto\022\nGateServer\"&\n\007NewGate\022\014\n\004d"
  "wID\030\001 \001(\r\022\r\n\005strIP\030\002 \001(\t\"&\n\tUserLogin\022\014\n"
  "\004dwID\030\001 \001(\r\022\013\n\003str\030\002 \001(\014b\006proto3"
,
  "Gate.proto", &assign_descriptors_table_Gate_2eproto, 112,
};

void AddDescriptors_Gate_2eproto() {
  static constexpr ::google::protobuf::internal::InitFunc deps[1] =
  {
  };
 ::google::protobuf::internal::AddDescriptors(&descriptor_table_Gate_2eproto, deps, 0);
}

// Force running AddDescriptors() at dynamic initialization time.
static bool dynamic_init_dummy_Gate_2eproto = []() { AddDescriptors_Gate_2eproto(); return true; }();
namespace GateServer {

// ===================================================================

void NewGate::InitAsDefaultInstance() {
}
class NewGate::HasBitSetters {
 public:
};

#if !defined(_MSC_VER) || _MSC_VER >= 1900
const int NewGate::kDwIDFieldNumber;
const int NewGate::kStrIPFieldNumber;
#endif  // !defined(_MSC_VER) || _MSC_VER >= 1900

NewGate::NewGate()
  : ::google::protobuf::Message(), _internal_metadata_(NULL) {
  SharedCtor();
  // @@protoc_insertion_point(constructor:GateServer.NewGate)
}
NewGate::NewGate(const NewGate& from)
  : ::google::protobuf::Message(),
      _internal_metadata_(NULL) {
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  strip_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  if (from.strip().size() > 0) {
    strip_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.strip_);
  }
  dwid_ = from.dwid_;
  // @@protoc_insertion_point(copy_constructor:GateServer.NewGate)
}

void NewGate::SharedCtor() {
  ::google::protobuf::internal::InitSCC(
      &scc_info_NewGate_Gate_2eproto.base);
  strip_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  dwid_ = 0u;
}

NewGate::~NewGate() {
  // @@protoc_insertion_point(destructor:GateServer.NewGate)
  SharedDtor();
}

void NewGate::SharedDtor() {
  strip_.DestroyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}

void NewGate::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const NewGate& NewGate::default_instance() {
  ::google::protobuf::internal::InitSCC(&::scc_info_NewGate_Gate_2eproto.base);
  return *internal_default_instance();
}


void NewGate::Clear() {
// @@protoc_insertion_point(message_clear_start:GateServer.NewGate)
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  strip_.ClearToEmptyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  dwid_ = 0u;
  _internal_metadata_.Clear();
}

#if GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
const char* NewGate::_InternalParse(const char* begin, const char* end, void* object,
                  ::google::protobuf::internal::ParseContext* ctx) {
  auto msg = static_cast<NewGate*>(object);
  ::google::protobuf::uint32 size; (void)size;
  int depth; (void)depth;
  ::google::protobuf::internal::ParseFunc parser_till_end; (void)parser_till_end;
  auto ptr = begin;
  while (ptr < end) {
    ::google::protobuf::uint32 tag;
    ptr = Varint::Parse32Inline(ptr, &tag);
    GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
    switch (tag >> 3) {
      // uint32 dwID = 1;
      case 1: {
        if (static_cast<::google::protobuf::uint8>(tag) != 8) goto handle_unusual;
        ::google::protobuf::uint64 val;
        ptr = Varint::Parse64(ptr, &val);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        ::google::protobuf::uint32 value = val;
        msg->set_dwid(value);
        break;
      }
      // string strIP = 2;
      case 2: {
        if (static_cast<::google::protobuf::uint8>(tag) != 18) goto handle_unusual;
        ptr = Varint::Parse32Inline(ptr, &size);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        ctx->extra_parse_data().SetFieldName("GateServer.NewGate.strIP");
        parser_till_end = ::google::protobuf::internal::StringParserUTF8;
        ::std::string* str = msg->mutable_strip();
        str->clear();
        object = str;
        if (size > end - ptr) goto len_delim_till_end;
        auto newend = ptr + size;
        if (size) ptr = parser_till_end(ptr, newend, object, ctx);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr == newend);
        break;
      }
      default: {
      handle_unusual: (void)&&handle_unusual;
        if ((tag & 7) == 4 || tag == 0) {
          bool ok = ctx->ValidEndGroup(tag);
          GOOGLE_PROTOBUF_PARSER_ASSERT(ok);
          return ptr;
        }
        auto res = UnknownFieldParse(tag, {_InternalParse, msg},
          ptr, end, msg->_internal_metadata_.mutable_unknown_fields(), ctx);
        ptr = res.first;
        if (res.second) return ptr;
      }
    }  // switch
  }  // while
  return ptr;
len_delim_till_end: (void)&&len_delim_till_end;
  return ctx->StoreAndTailCall(ptr, end, {_InternalParse, msg},
                                 {parser_till_end, object}, size);
group_continues: (void)&&group_continues;
  GOOGLE_DCHECK(ptr >= end);
  ctx->StoreGroup({_InternalParse, msg}, {parser_till_end, object}, depth);
  return ptr;
}
#else  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
bool NewGate::MergePartialFromCodedStream(
    ::google::protobuf::io::CodedInputStream* input) {
#define DO_(EXPRESSION) if (!PROTOBUF_PREDICT_TRUE(EXPRESSION)) goto failure
  ::google::protobuf::uint32 tag;
  // @@protoc_insertion_point(parse_start:GateServer.NewGate)
  for (;;) {
    ::std::pair<::google::protobuf::uint32, bool> p = input->ReadTagWithCutoffNoLastTag(127u);
    tag = p.first;
    if (!p.second) goto handle_unusual;
    switch (::google::protobuf::internal::WireFormatLite::GetTagFieldNumber(tag)) {
      // uint32 dwID = 1;
      case 1: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (8 & 0xFF)) {

          DO_((::google::protobuf::internal::WireFormatLite::ReadPrimitive<
                   ::google::protobuf::uint32, ::google::protobuf::internal::WireFormatLite::TYPE_UINT32>(
                 input, &dwid_)));
        } else {
          goto handle_unusual;
        }
        break;
      }

      // string strIP = 2;
      case 2: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (18 & 0xFF)) {
          DO_(::google::protobuf::internal::WireFormatLite::ReadString(
                input, this->mutable_strip()));
          DO_(::google::protobuf::internal::WireFormatLite::VerifyUtf8String(
            this->strip().data(), static_cast<int>(this->strip().length()),
            ::google::protobuf::internal::WireFormatLite::PARSE,
            "GateServer.NewGate.strIP"));
        } else {
          goto handle_unusual;
        }
        break;
      }

      default: {
      handle_unusual:
        if (tag == 0) {
          goto success;
        }
        DO_(::google::protobuf::internal::WireFormat::SkipField(
              input, tag, _internal_metadata_.mutable_unknown_fields()));
        break;
      }
    }
  }
success:
  // @@protoc_insertion_point(parse_success:GateServer.NewGate)
  return true;
failure:
  // @@protoc_insertion_point(parse_failure:GateServer.NewGate)
  return false;
#undef DO_
}
#endif  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER

void NewGate::SerializeWithCachedSizes(
    ::google::protobuf::io::CodedOutputStream* output) const {
  // @@protoc_insertion_point(serialize_start:GateServer.NewGate)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    ::google::protobuf::internal::WireFormatLite::WriteUInt32(1, this->dwid(), output);
  }

  // string strIP = 2;
  if (this->strip().size() > 0) {
    ::google::protobuf::internal::WireFormatLite::VerifyUtf8String(
      this->strip().data(), static_cast<int>(this->strip().length()),
      ::google::protobuf::internal::WireFormatLite::SERIALIZE,
      "GateServer.NewGate.strIP");
    ::google::protobuf::internal::WireFormatLite::WriteStringMaybeAliased(
      2, this->strip(), output);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    ::google::protobuf::internal::WireFormat::SerializeUnknownFields(
        _internal_metadata_.unknown_fields(), output);
  }
  // @@protoc_insertion_point(serialize_end:GateServer.NewGate)
}

::google::protobuf::uint8* NewGate::InternalSerializeWithCachedSizesToArray(
    bool deterministic, ::google::protobuf::uint8* target) const {
  (void)deterministic; // Unused
  // @@protoc_insertion_point(serialize_to_array_start:GateServer.NewGate)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    target = ::google::protobuf::internal::WireFormatLite::WriteUInt32ToArray(1, this->dwid(), target);
  }

  // string strIP = 2;
  if (this->strip().size() > 0) {
    ::google::protobuf::internal::WireFormatLite::VerifyUtf8String(
      this->strip().data(), static_cast<int>(this->strip().length()),
      ::google::protobuf::internal::WireFormatLite::SERIALIZE,
      "GateServer.NewGate.strIP");
    target =
      ::google::protobuf::internal::WireFormatLite::WriteStringToArray(
        2, this->strip(), target);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    target = ::google::protobuf::internal::WireFormat::SerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields(), target);
  }
  // @@protoc_insertion_point(serialize_to_array_end:GateServer.NewGate)
  return target;
}

size_t NewGate::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:GateServer.NewGate)
  size_t total_size = 0;

  if (_internal_metadata_.have_unknown_fields()) {
    total_size +=
      ::google::protobuf::internal::WireFormat::ComputeUnknownFieldsSize(
        _internal_metadata_.unknown_fields());
  }
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // string strIP = 2;
  if (this->strip().size() > 0) {
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::StringSize(
        this->strip());
  }

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::UInt32Size(
        this->dwid());
  }

  int cached_size = ::google::protobuf::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void NewGate::MergeFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:GateServer.NewGate)
  GOOGLE_DCHECK_NE(&from, this);
  const NewGate* source =
      ::google::protobuf::DynamicCastToGenerated<NewGate>(
          &from);
  if (source == NULL) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:GateServer.NewGate)
    ::google::protobuf::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:GateServer.NewGate)
    MergeFrom(*source);
  }
}

void NewGate::MergeFrom(const NewGate& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:GateServer.NewGate)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.strip().size() > 0) {

    strip_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.strip_);
  }
  if (from.dwid() != 0) {
    set_dwid(from.dwid());
  }
}

void NewGate::CopyFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:GateServer.NewGate)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void NewGate::CopyFrom(const NewGate& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:GateServer.NewGate)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool NewGate::IsInitialized() const {
  return true;
}

void NewGate::Swap(NewGate* other) {
  if (other == this) return;
  InternalSwap(other);
}
void NewGate::InternalSwap(NewGate* other) {
  using std::swap;
  _internal_metadata_.Swap(&other->_internal_metadata_);
  strip_.Swap(&other->strip_, &::google::protobuf::internal::GetEmptyStringAlreadyInited(),
    GetArenaNoVirtual());
  swap(dwid_, other->dwid_);
}

::google::protobuf::Metadata NewGate::GetMetadata() const {
  ::google::protobuf::internal::AssignDescriptors(&::assign_descriptors_table_Gate_2eproto);
  return ::file_level_metadata_Gate_2eproto[kIndexInFileMessages];
}


// ===================================================================

void UserLogin::InitAsDefaultInstance() {
}
class UserLogin::HasBitSetters {
 public:
};

#if !defined(_MSC_VER) || _MSC_VER >= 1900
const int UserLogin::kDwIDFieldNumber;
const int UserLogin::kStrFieldNumber;
#endif  // !defined(_MSC_VER) || _MSC_VER >= 1900

UserLogin::UserLogin()
  : ::google::protobuf::Message(), _internal_metadata_(NULL) {
  SharedCtor();
  // @@protoc_insertion_point(constructor:GateServer.UserLogin)
}
UserLogin::UserLogin(const UserLogin& from)
  : ::google::protobuf::Message(),
      _internal_metadata_(NULL) {
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  str_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  if (from.str().size() > 0) {
    str_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.str_);
  }
  dwid_ = from.dwid_;
  // @@protoc_insertion_point(copy_constructor:GateServer.UserLogin)
}

void UserLogin::SharedCtor() {
  ::google::protobuf::internal::InitSCC(
      &scc_info_UserLogin_Gate_2eproto.base);
  str_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  dwid_ = 0u;
}

UserLogin::~UserLogin() {
  // @@protoc_insertion_point(destructor:GateServer.UserLogin)
  SharedDtor();
}

void UserLogin::SharedDtor() {
  str_.DestroyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}

void UserLogin::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const UserLogin& UserLogin::default_instance() {
  ::google::protobuf::internal::InitSCC(&::scc_info_UserLogin_Gate_2eproto.base);
  return *internal_default_instance();
}


void UserLogin::Clear() {
// @@protoc_insertion_point(message_clear_start:GateServer.UserLogin)
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  str_.ClearToEmptyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  dwid_ = 0u;
  _internal_metadata_.Clear();
}

#if GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
const char* UserLogin::_InternalParse(const char* begin, const char* end, void* object,
                  ::google::protobuf::internal::ParseContext* ctx) {
  auto msg = static_cast<UserLogin*>(object);
  ::google::protobuf::uint32 size; (void)size;
  int depth; (void)depth;
  ::google::protobuf::internal::ParseFunc parser_till_end; (void)parser_till_end;
  auto ptr = begin;
  while (ptr < end) {
    ::google::protobuf::uint32 tag;
    ptr = Varint::Parse32Inline(ptr, &tag);
    GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
    switch (tag >> 3) {
      // uint32 dwID = 1;
      case 1: {
        if (static_cast<::google::protobuf::uint8>(tag) != 8) goto handle_unusual;
        ::google::protobuf::uint64 val;
        ptr = Varint::Parse64(ptr, &val);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        ::google::protobuf::uint32 value = val;
        msg->set_dwid(value);
        break;
      }
      // bytes str = 2;
      case 2: {
        if (static_cast<::google::protobuf::uint8>(tag) != 18) goto handle_unusual;
        ptr = Varint::Parse32Inline(ptr, &size);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        parser_till_end = ::google::protobuf::internal::StringParser;
        ::std::string* str = msg->mutable_str();
        str->clear();
        object = str;
        if (size > end - ptr) goto len_delim_till_end;
        str->append(ptr, size);
        ptr += size;
        break;
      }
      default: {
      handle_unusual: (void)&&handle_unusual;
        if ((tag & 7) == 4 || tag == 0) {
          bool ok = ctx->ValidEndGroup(tag);
          GOOGLE_PROTOBUF_PARSER_ASSERT(ok);
          return ptr;
        }
        auto res = UnknownFieldParse(tag, {_InternalParse, msg},
          ptr, end, msg->_internal_metadata_.mutable_unknown_fields(), ctx);
        ptr = res.first;
        if (res.second) return ptr;
      }
    }  // switch
  }  // while
  return ptr;
len_delim_till_end: (void)&&len_delim_till_end;
  return ctx->StoreAndTailCall(ptr, end, {_InternalParse, msg},
                                 {parser_till_end, object}, size);
group_continues: (void)&&group_continues;
  GOOGLE_DCHECK(ptr >= end);
  ctx->StoreGroup({_InternalParse, msg}, {parser_till_end, object}, depth);
  return ptr;
}
#else  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
bool UserLogin::MergePartialFromCodedStream(
    ::google::protobuf::io::CodedInputStream* input) {
#define DO_(EXPRESSION) if (!PROTOBUF_PREDICT_TRUE(EXPRESSION)) goto failure
  ::google::protobuf::uint32 tag;
  // @@protoc_insertion_point(parse_start:GateServer.UserLogin)
  for (;;) {
    ::std::pair<::google::protobuf::uint32, bool> p = input->ReadTagWithCutoffNoLastTag(127u);
    tag = p.first;
    if (!p.second) goto handle_unusual;
    switch (::google::protobuf::internal::WireFormatLite::GetTagFieldNumber(tag)) {
      // uint32 dwID = 1;
      case 1: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (8 & 0xFF)) {

          DO_((::google::protobuf::internal::WireFormatLite::ReadPrimitive<
                   ::google::protobuf::uint32, ::google::protobuf::internal::WireFormatLite::TYPE_UINT32>(
                 input, &dwid_)));
        } else {
          goto handle_unusual;
        }
        break;
      }

      // bytes str = 2;
      case 2: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (18 & 0xFF)) {
          DO_(::google::protobuf::internal::WireFormatLite::ReadBytes(
                input, this->mutable_str()));
        } else {
          goto handle_unusual;
        }
        break;
      }

      default: {
      handle_unusual:
        if (tag == 0) {
          goto success;
        }
        DO_(::google::protobuf::internal::WireFormat::SkipField(
              input, tag, _internal_metadata_.mutable_unknown_fields()));
        break;
      }
    }
  }
success:
  // @@protoc_insertion_point(parse_success:GateServer.UserLogin)
  return true;
failure:
  // @@protoc_insertion_point(parse_failure:GateServer.UserLogin)
  return false;
#undef DO_
}
#endif  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER

void UserLogin::SerializeWithCachedSizes(
    ::google::protobuf::io::CodedOutputStream* output) const {
  // @@protoc_insertion_point(serialize_start:GateServer.UserLogin)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    ::google::protobuf::internal::WireFormatLite::WriteUInt32(1, this->dwid(), output);
  }

  // bytes str = 2;
  if (this->str().size() > 0) {
    ::google::protobuf::internal::WireFormatLite::WriteBytesMaybeAliased(
      2, this->str(), output);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    ::google::protobuf::internal::WireFormat::SerializeUnknownFields(
        _internal_metadata_.unknown_fields(), output);
  }
  // @@protoc_insertion_point(serialize_end:GateServer.UserLogin)
}

::google::protobuf::uint8* UserLogin::InternalSerializeWithCachedSizesToArray(
    bool deterministic, ::google::protobuf::uint8* target) const {
  (void)deterministic; // Unused
  // @@protoc_insertion_point(serialize_to_array_start:GateServer.UserLogin)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    target = ::google::protobuf::internal::WireFormatLite::WriteUInt32ToArray(1, this->dwid(), target);
  }

  // bytes str = 2;
  if (this->str().size() > 0) {
    target =
      ::google::protobuf::internal::WireFormatLite::WriteBytesToArray(
        2, this->str(), target);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    target = ::google::protobuf::internal::WireFormat::SerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields(), target);
  }
  // @@protoc_insertion_point(serialize_to_array_end:GateServer.UserLogin)
  return target;
}

size_t UserLogin::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:GateServer.UserLogin)
  size_t total_size = 0;

  if (_internal_metadata_.have_unknown_fields()) {
    total_size +=
      ::google::protobuf::internal::WireFormat::ComputeUnknownFieldsSize(
        _internal_metadata_.unknown_fields());
  }
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // bytes str = 2;
  if (this->str().size() > 0) {
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::BytesSize(
        this->str());
  }

  // uint32 dwID = 1;
  if (this->dwid() != 0) {
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::UInt32Size(
        this->dwid());
  }

  int cached_size = ::google::protobuf::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void UserLogin::MergeFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:GateServer.UserLogin)
  GOOGLE_DCHECK_NE(&from, this);
  const UserLogin* source =
      ::google::protobuf::DynamicCastToGenerated<UserLogin>(
          &from);
  if (source == NULL) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:GateServer.UserLogin)
    ::google::protobuf::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:GateServer.UserLogin)
    MergeFrom(*source);
  }
}

void UserLogin::MergeFrom(const UserLogin& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:GateServer.UserLogin)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.str().size() > 0) {

    str_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.str_);
  }
  if (from.dwid() != 0) {
    set_dwid(from.dwid());
  }
}

void UserLogin::CopyFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:GateServer.UserLogin)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void UserLogin::CopyFrom(const UserLogin& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:GateServer.UserLogin)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool UserLogin::IsInitialized() const {
  return true;
}

void UserLogin::Swap(UserLogin* other) {
  if (other == this) return;
  InternalSwap(other);
}
void UserLogin::InternalSwap(UserLogin* other) {
  using std::swap;
  _internal_metadata_.Swap(&other->_internal_metadata_);
  str_.Swap(&other->str_, &::google::protobuf::internal::GetEmptyStringAlreadyInited(),
    GetArenaNoVirtual());
  swap(dwid_, other->dwid_);
}

::google::protobuf::Metadata UserLogin::GetMetadata() const {
  ::google::protobuf::internal::AssignDescriptors(&::assign_descriptors_table_Gate_2eproto);
  return ::file_level_metadata_Gate_2eproto[kIndexInFileMessages];
}


// @@protoc_insertion_point(namespace_scope)
}  // namespace GateServer
namespace google {
namespace protobuf {
template<> PROTOBUF_NOINLINE ::GateServer::NewGate* Arena::CreateMaybeMessage< ::GateServer::NewGate >(Arena* arena) {
  return Arena::CreateInternal< ::GateServer::NewGate >(arena);
}
template<> PROTOBUF_NOINLINE ::GateServer::UserLogin* Arena::CreateMaybeMessage< ::GateServer::UserLogin >(Arena* arena) {
  return Arena::CreateInternal< ::GateServer::UserLogin >(arena);
}
}  // namespace protobuf
}  // namespace google

// @@protoc_insertion_point(global_scope)
#include <google/protobuf/port_undef.inc>