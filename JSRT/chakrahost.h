#pragma once
#define USE_EDGEMODE_JSRT 1
#include "jsrt.h"


JsValueRef JScriptEval(JsRuntimeHandle runtime, std::wstring script);
JsErrorCode DefineHostCallback(JsValueRef globalObject, const wchar_t *callbackName, JsNativeFunction callback, void *callbackState);
JsErrorCode DefineHostInspectable(JsValueRef globalObject, const wchar_t *name, IInspectable* value);
Platform::String^ GetScriptException();
void ThrowScriptException();

JsValueRef CALLBACK RunScript(JsValueRef callee, bool isConstructCall, JsValueRef *arguments, unsigned short argumentCount, void *callbackState);

void ThrowException(std::wstring errorString);
void ThrowWinRTException(Platform::String^ errorString);

#define IfFailError(v, e) \
    { \
    JsErrorCode error = (v); \
if (error != JsNoError) \
        { \
        fwprintf(stderr, L"chakrahost: fatal error: %s.\n", (e)); \
        goto error; \
        } \
    }

#define IfFailRet(v) \
    { \
    JsErrorCode error = (v); \
if (error != JsNoError) \
        { \
        return error; \
        } \
    }

#define IfFailThrow(v, e) \
    { \
    JsErrorCode error = (v); \
if (error != JsNoError) \
        { \
        ThrowException((e)); \
        return JS_INVALID_REFERENCE; \
        } \
    }

#define IfFailThrowNoRet(v, e) \
    { \
    JsErrorCode error = (v); \
if (error != JsNoError) \
        { \
        ThrowException((e)); \
        } \
    }

#define IfFailThrowWinRTNoRet(v, e) \
    { \
    JsErrorCode error = (v); \
if (error != JsNoError) \
        { \
        ThrowWinRTException((e)); \
        } \
    }

#define IfComFailError(v) \
    { \
    hr = (v); \
if (FAILED(hr)) \
        { \
        goto error; \
        } \
    }

#define IfComFailRet(v) \
    { \
    HRESULT hr = (v); \
if (FAILED(hr)) \
        { \
        return hr; \
        } \
    }

