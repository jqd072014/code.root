// Keypad.h : Declaration of the CKeypad

#pragma once
#include "resource.h"       // main symbols



#include "Wrapper_i.h"
#include "_IKeypadEvents_CP.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;

// CKeypad

class ATL_NO_VTABLE CKeypad :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CKeypad, &CLSID_Keypad>,
	public IConnectionPointContainerImpl<CKeypad>,
	public CProxy_IKeypadEvents<CKeypad>,
	public IDispatchImpl<IKeypad, &IID_IKeypad, &LIBID_WrapperLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CKeypad()
	{

	}

DECLARE_REGISTRY_RESOURCEID(IDR_KEYPAD)


BEGIN_COM_MAP(CKeypad)
	COM_INTERFACE_ENTRY(IKeypad)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(IConnectionPointContainer)
END_COM_MAP()

BEGIN_CONNECTION_POINT_MAP(CKeypad)
	CONNECTION_POINT_ENTRY(__uuidof(_IKeypadEvents))
END_CONNECTION_POINT_MAP()


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:
	STDMETHOD(Open_USB)();

};

OBJECT_ENTRY_AUTO(__uuidof(Keypad), CKeypad)
