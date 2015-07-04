// dllmain.h : Declaration of module class.

class CWrapperModule : public ATL::CAtlDllModuleT< CWrapperModule >
{
public :
	DECLARE_LIBID(LIBID_WrapperLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_WRAPPER, "{51984721-41C1-4DF7-BE6C-FF681F78505F}")
};

extern class CWrapperModule _AtlModule;
