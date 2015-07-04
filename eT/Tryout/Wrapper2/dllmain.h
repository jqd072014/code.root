// dllmain.h : Declaration of module class.

class CWrapper2Module : public ATL::CAtlDllModuleT< CWrapper2Module >
{
public :
	DECLARE_LIBID(LIBID_Wrapper2Lib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_WRAPPER2, "{D2203B70-06D2-4885-9FD9-19765B02689F}")
};

extern class CWrapper2Module _AtlModule;
