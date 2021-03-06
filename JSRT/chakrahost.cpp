#include "pch.h"

using namespace std;

//
// Source context counter.
//

unsigned currentSourceContext = 0;

//
// This "throws" an exception in the Chakra space. Useful routine for callbacks
// that need to throw a JS error to indicate failure.
//

void ThrowException(wstring errorString)
{
    // We ignore error since we're already in an error state.
    JsValueRef errorValue;
    JsValueRef errorObject;
    JsPointerToString(errorString.c_str(), errorString.length(), &errorValue);
    JsCreateError(errorValue, &errorObject);
    JsSetException(errorObject);
}

void ThrowWinRTException(Platform::String^ errorString)
{
    throw ref new Platform::Exception(E_FAIL, errorString);
}
//
// Helper to load a script from disk.
//

wstring LoadScript(wstring fileName)
{
    FILE *file;
    if (_wfopen_s(&file, fileName.c_str(), L"rb"))
    {
        fwprintf(stderr, L"chakrahost: unable to open file: %s.\n", fileName.c_str());
        return wstring();
    }

    unsigned int current = ftell(file);
    fseek(file, 0, SEEK_END);
    unsigned int end = ftell(file);
    fseek(file, current, SEEK_SET);
    unsigned int lengthBytes = end - current;
    char *rawBytes = (char *)calloc(lengthBytes + 1, sizeof(char));

    if (rawBytes == nullptr)
    {
        fwprintf(stderr, L"chakrahost: fatal error.\n");
        return wstring();
    }

    fread((void *)rawBytes, sizeof(char), lengthBytes, file);

    wchar_t *contents = (wchar_t *)calloc(lengthBytes + 1, sizeof(wchar_t));
    if (contents == nullptr)
    {
        free(rawBytes);
        fwprintf(stderr, L"chakrahost: fatal error.\n");
        return wstring();
    }

    if (MultiByteToWideChar(CP_UTF8, 0, rawBytes, lengthBytes + 1, contents, lengthBytes + 1) == 0)
    {
        free(rawBytes);
        free(contents);
        fwprintf(stderr, L"chakrahost: fatal error.\n");
        return wstring();
    }

    wstring result = contents;
    free(rawBytes);
    free(contents);
    return result;
}

//
// Callback to echo something to the command-line.
//


//
// Callback to load a script and run it.
//

JsValueRef CALLBACK RunScript(JsValueRef callee, bool isConstructCall, JsValueRef *arguments, unsigned short argumentCount, void *callbackState)
{
    JsValueRef result = JS_INVALID_REFERENCE;

    if (argumentCount < 2)
    {
        ThrowException(L"not enough arguments");
        return result;
    }

    //
    // Convert filename.
    //
    const wchar_t *filename;
    size_t length;

    IfFailThrow(JsStringToPointer(arguments[1], &filename, &length), L"invalid filename argument");

    //
    // Load the script from the disk.
    //

    wstring script = LoadScript(filename);
    if (script.empty())
    {
        ThrowException(L"invalid script");
        return result;
    }

    //
    // Run the script.
    //

    IfFailThrow(JsRunScript(script.c_str(), currentSourceContext++, filename, &result), L"failed to run script.");

    return result;
}
//
// Helper to define a host callback method on the global host object.
//

JsErrorCode DefineHostCallback(JsValueRef globalObject, const wchar_t *callbackName, JsNativeFunction callback, void *callbackState)
{
    //
    // Get property ID.
    //

    JsPropertyIdRef propertyId;
    IfFailRet(JsGetPropertyIdFromName(callbackName, &propertyId));

    //
    // Create a function
    //

    JsValueRef function;
    IfFailRet(JsCreateFunction(callback, callbackState, &function));

    //
    // Set the property
    //

    IfFailRet(JsSetProperty(globalObject, propertyId, function, true));

    return JsNoError;
}

JsErrorCode DefineHostInspectable(JsValueRef globalObject, const wchar_t *name, IInspectable* value)
{
    JsErrorCode c;
    //
    // Get property ID.
    //

    JsPropertyIdRef propertyId;
    IfFailRet(c = JsGetPropertyIdFromName(name, &propertyId));

    //
    // Create a function
    //

    JsValueRef obj;
    IfFailRet(c = JsInspectableToObject(value, &obj));

    //
    // Set the property
    //
    IfFailRet(c = JsSetProperty(globalObject, propertyId, obj, true));

    return c;
}

//
// Creates a host execution context and sets up the host object in it.
//


//
// Print out a script exception.
//

Platform::String^ GetScriptException()
{
    //
    // Get script exception.
    //

    JsValueRef exception;
    IfFailThrowNoRet(JsGetAndClearException(&exception), L"can't get exception");

    //
    // Get message.
    //

    JsPropertyIdRef messageName;
    IfFailThrowNoRet(JsGetPropertyIdFromName(L"message", &messageName), L"can't get exception");

    JsValueRef messageValue;
    IfFailThrowNoRet(JsGetProperty(exception, messageName, &messageValue), L"can't get exception");

    const wchar_t *message;
    size_t length;
    IfFailThrowNoRet(JsStringToPointer(messageValue, &message, &length), L"can't get exception");
    return ref new Platform::String(message);
}

void ThrowScriptException()
{
    throw ref new Platform::Exception(E_FAIL, GetScriptException());
}

JsValueRef JScriptEval(
    JsRuntimeHandle runtime,
    wstring script)
{
    JsValueRef result = JS_INVALID_REFERENCE;

    if (script.empty())
    {
        goto error;
    }

    //
    // Run the script.
    //
    JsErrorCode errorCode, parseError;
    JsValueRef parsed;

    parseError = JsParseScript(script.c_str(), currentSourceContext++, L"." /*source URL*/, &parsed);
    IfFailThrowNoRet(parseError, L"failed to parse code")
    errorCode = JsRunScript(script.c_str(), currentSourceContext++, L"." /*source URL*/, &result);

    if (errorCode == JsErrorScriptException)
    {
        ThrowScriptException();
    }
    else
    {
        IfFailThrowNoRet(errorCode, L"failed to run script.");
    }

error:
    return result;
}
